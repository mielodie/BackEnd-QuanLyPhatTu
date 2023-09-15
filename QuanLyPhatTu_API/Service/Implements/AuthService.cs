using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using QuanLyPhatTu_API.DataContext;
using QuanLyPhatTu_API.Entities;
using QuanLyPhatTu_API.Handle.Email;
using QuanLyPhatTu_API.Handle.Image;
using QuanLyPhatTu_API.Payloads.Converters;
using QuanLyPhatTu_API.Payloads.DTOs;
using QuanLyPhatTu_API.Payloads.Requests.PhatTuRequest;
using QuanLyPhatTu_API.Payloads.Responses;
using QuanLyPhatTu_API.Service.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Mail;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;
using BCryptNet = BCrypt.Net.BCrypt;
using SmtpClient = System.Net.Mail.SmtpClient;
using System.Text;

namespace QuanLyPhatTu_API.Service.Implements
{
    public class AuthService : BaseService, IAuthService
    {
        private readonly IConfiguration _configuration;
        private readonly ResponseObject<PhatTuDTO> _responseObject;
        private readonly ResponseObject<TokenDTO> _responseObjectToken;
        private readonly PhatTuConverter _phatTuConverter;
        public AuthService(
            IConfiguration configuration,
            ResponseObject<PhatTuDTO> responseObject, 
            ResponseObject<TokenDTO> responseObjectToken,
            PhatTuConverter phatTuConverter)
        {
            _configuration = configuration;
            _responseObject = responseObject;
            _responseObjectToken = responseObjectToken;
            _phatTuConverter = phatTuConverter;
        }

        public async Task<ResponseObject<PhatTuDTO>> DangKyPhatTu(Request_DangKy request)
        {
            if(string.IsNullOrWhiteSpace(request.HoVaTen) 
                || string.IsNullOrWhiteSpace(request.TaiKhoan)
                || string.IsNullOrWhiteSpace(request.MatKhau)
                || string.IsNullOrWhiteSpace(request.Email))
            {
                return _responseObject.ResponseError(StatusCodes.Status404NotFound, "Bạn cần truyền vào đầy đủ thông tin", null);
            }
            if (Validate.IsValidEmail(request.Email) == false)
            {
                return _responseObject.ResponseError(StatusCodes.Status400BadRequest, "Định dạng Email không hợp lệ", null);
            }
            if(Validate.IsValidPhoneNumber(request.SoDienThoai) == false)
            {
                return _responseObject.ResponseError(StatusCodes.Status400BadRequest, "Định dạng số điện thoại không hợp lệ", null);
            }

            if (await _context.phatTus.FirstOrDefaultAsync(x => x.TaiKhoan.Equals(request.TaiKhoan)) != null)
            {
                return _responseObject.ResponseError(StatusCodes.Status400BadRequest, "Tên tài khoản đã tồn tại trên hệ thống", null);
            }
            if (await _context.phatTus.FirstOrDefaultAsync(x => x.Email.Equals(request.Email)) != null)
            {
                return _responseObject.ResponseError(StatusCodes.Status400BadRequest, "Email đã tồn tại trên hệ thống", null);
            }
            else
            {
                int imageSize = 2 * 1024 * 768;
                try
                {
                    PhatTu phatTu = new PhatTu();
                    phatTu.TaiKhoan = request.TaiKhoan;
                    phatTu.MatKhau = BCryptNet.HashPassword(request.MatKhau);
                    phatTu.NgayCapNhat = DateTime.Now;
                    phatTu.NgaySinh = request.NgaySinh;
                    phatTu.Email = request.Email;
                    phatTu.HoVaTen = request.HoVaTen;
                    phatTu.QuyenHanId = 3;
                    phatTu.SoDienThoai = request.SoDienThoai;
                    phatTu.GioiTinh = request.GioiTinh;
                    phatTu.PhapDanh = request.PhapDanh;
                    phatTu.ChuaId = request.ChuaId;
                    phatTu.NgayXuatGia = request.NgayXuatGia;
                    phatTu.DaHoanTuc = false;
                    phatTu.LaThanhVienThamGiaDaoTrang = false;
                    string imageUrl = "";
                    if (request.AnhChup != null)
                    {


                        if (!HandleImage.IsImage(request.AnhChup, imageSize))
                        {
                            return _responseObject.ResponseError(StatusCodes.Status400BadRequest, "Ảnh không hợp lệ", null);
                        }
                        else
                        {
                            var avatarFile = await HandleUploadImage.Upfile(request.AnhChup);
                            phatTu.AnhChup = avatarFile == "" ? "https://media.istockphoto.com/id/1300845620/vector/user-icon-flat-isolated-on-white-background-user-symbol-vector-illustration.jpg?s=612x612&w=0&k=20&c=yBeyba0hUkh14_jgv1OKqIH0CCSWU_4ckRkAoy2p73o=" : avatarFile;
                        }
                    }

                    await _context.phatTus.AddAsync(phatTu);
                    await _context.SaveChangesAsync();
                    return _responseObject.ResponseSuccess("Đăng ký tài khoản thành công", _phatTuConverter.EntityToDTO(phatTu));
                }
                catch (Exception ex)
                {
                    return _responseObject.ResponseError(StatusCodes.Status500InternalServerError, ex.Message, null);
                }
            }
        }

        public async Task<ResponseObject<TokenDTO>> DangNhap(Request_DangNhap request)
        {
            if(string.IsNullOrWhiteSpace(request.TaiKhoan) || string.IsNullOrWhiteSpace(request.MatKhau))
            {
                return _responseObjectToken.ResponseError(StatusCodes.Status400BadRequest, "Vui lòng điền đầy đủ thông tin", null);
            }
            var phatTu = await _context.phatTus.FirstOrDefaultAsync(x => x.TaiKhoan.Equals(request.TaiKhoan));
            if(phatTu is null)
            {
                return _responseObjectToken.ResponseError(StatusCodes.Status404NotFound, "Tên tài khoản không tồn tại trên hệ thống", null);
            }
            bool checkPass = BCryptNet.Verify(request.MatKhau, phatTu.MatKhau);
            if(!checkPass)
            {
                return _responseObjectToken.ResponseError(StatusCodes.Status400BadRequest, "Mật khẩu không chính xác", null);
            }
            else
            {
                return _responseObjectToken.ResponseSuccess("Đăng nhập thành công", GenerateAccessToken(phatTu));
            }
        }

        public TokenDTO GenerateAccessToken(PhatTu phatTu)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var secretKeyBytes = System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:SecretKey").Value!);

            var decentralization = _context.quyenHans.FirstOrDefault(x => x.Id == phatTu.QuyenHanId);

            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("Id", phatTu.Id.ToString()),
                    new Claim(ClaimTypes.Email, phatTu.Email),
                    new Claim("TaiKhoan", phatTu.TaiKhoan),
                    new Claim("AnhChup", phatTu.AnhChup),
                    new Claim("QuyenHanId", phatTu.QuyenHanId.ToString()),
                    new Claim(ClaimTypes.Role, decentralization?.TenQuyenHan ?? "")
                }),
                Expires = DateTime.Now.AddHours(4),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKeyBytes), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = jwtTokenHandler.CreateToken(tokenDescription);
            var accessToken = jwtTokenHandler.WriteToken(token);
            var refreshToken = GenerateRefreshToken();

            RefreshToken rf = new RefreshToken
            {
                Token = refreshToken,
                ThoiGianHetHan = DateTime.Now.AddHours(4),
                PhatTuId = phatTu.Id
            };

            _context.refreshTokens.Add(rf);
            _context.SaveChanges();

            TokenDTO tokenDTO = new TokenDTO
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };
            return tokenDTO;
        }

        public string GenerateRefreshToken()
        {
            var random = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(random);
                return Convert.ToBase64String(random);
            }
        }

        public ResponseObject<TokenDTO> RenewAccessToken(TokenDTO request)
        {
            try
            {
                var jwtTokenHandler = new JwtSecurityTokenHandler();
                var secretKey = _configuration.GetSection("AppSettings:SecretKey").Value;

                var tokenValidation = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
                };

                var tokenAuthentication = jwtTokenHandler.ValidateToken(request.AccessToken, tokenValidation, out var validatedToken);

                if (!(validatedToken is JwtSecurityToken jwtSecurityToken) || jwtSecurityToken.Header.Alg != SecurityAlgorithms.HmacSha256)
                {
                    return _responseObjectToken.ResponseError(StatusCodes.Status400BadRequest, "Token không hợp lệ", null);
                }

                var refreshToken = _context.refreshTokens.FirstOrDefault(x => x.Token == request.RefreshToken);

                if (refreshToken == null)
                {
                    return _responseObjectToken.ResponseError(StatusCodes.Status404NotFound, "RefreshToken không tồn tại trong database", null);
                }

                if (refreshToken.ThoiGianHetHan >= DateTime.Now)
                {
                    return _responseObjectToken.ResponseError(StatusCodes.Status401Unauthorized, "Token chưa hết hạn", null);
                }

                var user = _context.phatTus.FirstOrDefault(x => x.Id == refreshToken.PhatTuId);

                if (user == null)
                {
                    return _responseObjectToken.ResponseError(StatusCodes.Status404NotFound, "Người dùng không tồn tại", null);
                }

                var newToken = GenerateAccessToken(user);

                return _responseObjectToken.ResponseSuccess("Làm mới token thành công", newToken);
            }
            catch (SecurityTokenValidationException ex)
            {
                return _responseObjectToken.ResponseError(StatusCodes.Status400BadRequest, "Lỗi xác thực token: " + ex.Message, null);
            }
            catch (Exception ex)
            {
                return _responseObjectToken.ResponseError(StatusCodes.Status500InternalServerError, "Lỗi không xác định: " + ex.Message, null);
            }
        }

        public string SendEmail(EmailTo emailTo)
        {
            if (!Validate.IsValidEmail(emailTo.Mail))
            {
                return "Định dạng email không hợp lệ";
            }
            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("minhquantb00@gmail.com", "jvztzxbtyugsiaea"),
                EnableSsl = true
            };
            try
            {
                var message = new MailMessage();
                message.From = new MailAddress("minhquantb00@gmail.com");
                message.To.Add(emailTo.Mail);
                message.Subject = emailTo.Subject;
                message.Body = emailTo.Content;
                message.IsBodyHtml = true;
                smtpClient.Send(message);

                return "Gửi email thành công";
            }
            catch (Exception ex)
            {
                return "Lỗi khi gửi email: " + ex.Message;
            }
        }

        public async Task<ResponseObject<PhatTuDTO>> TaoMatKhauMoi(Request_TaoMatKhauMoi request)
        {
            XacNhanEmail confirmEmail = await _context.xacNhanEmails.Where(x => x.MaXacNhan.Equals(request.MaXacNhan)).FirstOrDefaultAsync();
            if (confirmEmail is null)
            {
                return _responseObject.ResponseError(StatusCodes.Status400BadRequest, "Mã xác nhận không chính xác", null);
            }
            if (confirmEmail.ThoiGianHetHan < DateTime.Now)
            {
                return _responseObject.ResponseError(StatusCodes.Status400BadRequest, "Mã xác nhận đã hết hạn", null);
            }
            PhatTu phatTu = _context.phatTus.FirstOrDefault(x => x.Id == confirmEmail.PhatTuId);
            phatTu.MatKhau = BCryptNet.HashPassword(request.MatKhauMoi);
            _context.xacNhanEmails.Remove(confirmEmail);
            _context.phatTus.Update(phatTu);
            await _context.SaveChangesAsync();
            return _responseObject.ResponseSuccess("Tạo mật khẩu mới thành công", _phatTuConverter.EntityToDTO(phatTu));
        }
        private int GenerateCodeActive()
        {
            Random random = new Random();
            return random.Next(100000, 999999);
        }

        public async Task<string> XacNhanQuenMatKhau(Request_XacNhanQuenMatKhau request)
        {
            PhatTu phatTu = await _context.phatTus.FirstOrDefaultAsync(x => x.Email.Equals(request.Email));
            if (phatTu is null)
            {
                return "Email không tồn tại trong hệ thống";
            }
            else
            {
                var confirms = _context.xacNhanEmails.Where(x => x.PhatTuId == phatTu.Id).ToList();
                _context.xacNhanEmails.RemoveRange(confirms);
                await _context.SaveChangesAsync();
                XacNhanEmail confirmEmail = new XacNhanEmail
                {
                    PhatTuId = phatTu.Id,
                    DaXacNhan = false,
                    ThoiGianHetHan = DateTime.Now.AddHours(4),
                    MaXacNhan = "MyBugs" + "_" + GenerateCodeActive().ToString()
                };
                await _context.xacNhanEmails.AddAsync(confirmEmail);
                await _context.SaveChangesAsync();
                string message = SendEmail(new EmailTo
                {
                    Mail = request.Email,
                    Subject = "Nhận mã xác nhận để tạo mật khẩu mới từ đây: ",
                    Content = $"Mã kích hoạt của bạn là: {confirmEmail.MaXacNhan}, mã này sẽ hết hạn sau 4 tiếng"
                });
                return "Gửi mã xác nhận về email thành công, vui lòng kiểm tra email";
            }
        }
        public async Task<string> DoiMatKhau(int phatTuId, Request_DoiMatKhau request)
        {
            var phatTu = await _context.phatTus.FirstOrDefaultAsync(x => x.Id == phatTuId);
            bool checkPass = BCryptNet.Verify(request.MatKhauCu, phatTu.MatKhau);
            if(!checkPass)
            {
                return "Mật khẩu không khớp với mật khẩu hiện tại";
            }
            phatTu.MatKhau = BCryptNet.HashPassword(request.MatKhauMoi);
            _context.phatTus.Update(phatTu);
            await _context.SaveChangesAsync();
            return "Đổi mật khẩu thành công";
        }

        public async Task<string> ThayDoiQuyenHan(int phatTuId)
        {
            var phatTu = await _context.phatTus.FirstOrDefaultAsync(x => x.Id == phatTuId);

            if (phatTu == null)
            {
                return "Tài khoản không tồn tại";
            }

            phatTu.QuyenHanId = (phatTu.QuyenHanId == 2) ? 3 : 2;

            try
            {
                _context.phatTus.Update(phatTu);
                await _context.SaveChangesAsync();
                return "Thay đổi quyền tài khoản thành công";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return "Lỗi trong quá trình thay đổi quyền tài khoản";
            }
        }

    }
}
