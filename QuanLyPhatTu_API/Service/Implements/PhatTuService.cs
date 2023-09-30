using Microsoft.EntityFrameworkCore;
using QuanLyPhatTu_API.Entities;
using QuanLyPhatTu_API.Handle.Email;
using QuanLyPhatTu_API.Handle.HandlePagination;
using QuanLyPhatTu_API.Payloads.Converters;
using QuanLyPhatTu_API.Payloads.DTOs;
using QuanLyPhatTu_API.Payloads.Requests.PhatTuRequest;
using QuanLyPhatTu_API.Payloads.Responses;
using QuanLyPhatTu_API.Service.Interfaces;

namespace QuanLyPhatTu_API.Service.Implements
{
    public class PhatTuService : BaseService, IPhatTuService
    {
        private readonly PhatTuConverter _phatTuConverter;
        private readonly ResponseObject<PhatTuDTO> _responseObject;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public PhatTuService(PhatTuConverter phatTuConverter, ResponseObject<PhatTuDTO> responseObject, IHttpContextAccessor httpContextAccessor)
        {
            _phatTuConverter = phatTuConverter;
            _responseObject = responseObject;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<PageResult<PhatTuDTO>> LayPhatTuTheoChua(int? chuaId, int pageSize = 10, int pageNumber = 1)
        {
            var phatTuQuery =  _context.phatTus
                                    .Include(x => x.Chua)
                                    .Include(x => x.DonDangKies).AsNoTracking()
                                    .Where(x => x.ChuaId == chuaId && x.IsActive == true)
                                    .Select(x => _phatTuConverter.EntityToDTO(x))
                                    .AsQueryable();
            var result = Pagination.GetPageData(phatTuQuery, pageSize, pageNumber);
            return result;
        }

        public async Task<ResponseObject<PhatTuDTO>> LayPhatTuTheoEmail(string email)
        {
            var phatTuQuery = await _context.phatTus.FirstOrDefaultAsync(x => x.Email.Equals(email));
            return _responseObject.ResponseSuccess($"Phật tử có email: {email} có thông tin là: ", _phatTuConverter.EntityToDTO(phatTuQuery));
        }
        private string ChuanHoaChuoi(string str)
        {
            str = str.ToLower().Trim();
            while(str.Contains("  "))
            {
                str = str.Replace("  ", " ");
            }
            return str;
        }
        public async Task<PageResult<PhatTuDTO>> LayPhatTuTheoGioiTinh(string? gioiTinh, int pageSize = 10, int pageNumber = 1)
        {
            var phatTuQuery = _context.phatTus
                                    .Include(x => x.Chua)
                                    .Include(x => x.DonDangKies).AsNoTracking()
                                    .Where(x => ChuanHoaChuoi(x.GioiTinh).Equals(ChuanHoaChuoi(gioiTinh)) && x.IsActive == true)
                                    .Select(x => _phatTuConverter.EntityToDTO(x))
                                    .AsQueryable();
            var result = Pagination.GetPageData(phatTuQuery, pageSize, pageNumber);
            return result;
        }

        public async Task<PageResult<PhatTuDTO>> LayPhatTuTheoPhapDanh(string? phapDanh, int pageSize = 10, int pageNumber = 1)
        {
            var phatTuQuery = _context.phatTus
                                    .Include(x => x.Chua)
                                    .Include(x => x.DonDangKies).AsNoTracking()
                                    .Where(x => ChuanHoaChuoi(x.PhapDanh).Contains(ChuanHoaChuoi(phapDanh)) && x.IsActive == true)
                                    .Select(x => _phatTuConverter.EntityToDTO(x))
                                    .AsQueryable();
            var result = Pagination.GetPageData(phatTuQuery, pageSize, pageNumber);
            return result;
        }

        public async Task<PageResult<PhatTuDTO>> LayPhatTuTheoTen(string? name, int pageSize = 10, int pageNumber = 1)
        {
            var phatTuQuery = _context.phatTus
                                    .Include(x => x.Chua)
                                    .Include(x => x.DonDangKies).AsNoTracking()
                                    .Where(x => ChuanHoaChuoi(x.HoVaTen).Contains(ChuanHoaChuoi(name)) && x.IsActive == true)
                                    .Select(x => _phatTuConverter.EntityToDTO(x))
                                    .AsQueryable();
            var result = Pagination.GetPageData(phatTuQuery, pageSize, pageNumber);
            return result;
        }

        public async Task<PageResult<PhatTuDTO>> LayPhatTuTheoTrangThai(bool? status, int pageSize = 10, int pageNumber = 1)
        {
            var phatTuQuery = _context.phatTus
                                    .Include(x => x.Chua)
                                    .Include(x => x.DonDangKies).AsNoTracking()
                                    .Where(x => x.DaHoanTuc.Equals(status) && x.IsActive == true)
                                    .Select(x => _phatTuConverter.EntityToDTO(x))
                                    .AsQueryable();
            var result = Pagination.GetPageData(phatTuQuery, pageSize, pageNumber);
            return result;
        }

        public async Task<PageResult<PhatTuDTO>> LayTatCaPhatTu(int pageSize, int pageNumber)
        {
            var phatTuQuery = _context.phatTus
                                    .Include(x => x.Chua)
                                    .Include(x => x.DonDangKies).AsNoTracking()
                                    .Select(x => _phatTuConverter.EntityToDTO(x))
                                    .AsQueryable();
            var result = Pagination.GetPageData(phatTuQuery, pageSize, pageNumber);
            return result;
        }

        public async Task<ResponseObject<PhatTuDTO>> SuaThongTinPhatTu(int phatTuId, Request_CapNhatThongTinPhatTu request)
        {
            var phatTu = await _context.phatTus.FirstOrDefaultAsync(x => x.Id == phatTuId);
            try
            {
                phatTu.PhapDanh = request.PhapDanh;
                phatTu.NgayCapNhat = DateTime.Now;
                phatTu.Email = request.Email;
                phatTu.GioiTinh = request.GioiTinh;
                phatTu.NgaySinh = request.NgaySinh;
                if (!Validate.IsValidPhoneNumber(request.SoDienThoai))
                {
                    throw new Exception("Định dạng số điện thoại không hợp lệ");
                }
                else
                {
                    phatTu.SoDienThoai = request.SoDienThoai;
                    _context.phatTus.Update(phatTu);
                    await _context.SaveChangesAsync();
                }
                return _responseObject.ResponseSuccess("Cập nhật thông tin phật tử thành công", _phatTuConverter.EntityToDTO(phatTu));
            }
            catch (Exception ex)
            {
                return _responseObject.ResponseError(StatusCodes.Status500InternalServerError, ex.Message, null);
            }
        }

        public async Task<string> XoaPhatTu(int phatTuId)
        {
            var phatTu = await _context.phatTus.FirstOrDefaultAsync(x => x.Id == phatTuId);
            if(phatTu is null)
            {
                return "Phật tử không tồn tại";
            }
            else
            {
                phatTu.DaHoanTuc = true;
                _context.phatTus.Update(phatTu);
                await _context.SaveChangesAsync();
                return "Đã cập nhật trạng thái hoàn tục của phật tử";
            }
        }

        public async Task<ResponseObject<PhatTuDTO>> ThemPhatTuVaoChua(Request_ThemPhatTuVaoChua request)
        {
            var phatTu = await _context.phatTus.SingleOrDefaultAsync(x => x.Id == request.userId);
            var currentUser = _httpContextAccessor.HttpContext.User;
            if (!currentUser.Identity.IsAuthenticated)
            {
                return _responseObject.ResponseError(StatusCodes.Status401Unauthorized, "Người dùng không được xác định", null);
            }
            if (!currentUser.IsInRole("ADMIN"))
            {
                return _responseObject.ResponseError(StatusCodes.Status403Forbidden, "Người dùng không có quyền sử dụng chức năng này", null);
            }
            if(phatTu == null)
            {
                return _responseObject.ResponseError(StatusCodes.Status404NotFound, "Không tìm thấy người dùng", null);
            }
            var chua = await _context.chuas.SingleOrDefaultAsync(x => x.Id == request.chuaId);
            if(chua == null)
            {
                return _responseObject.ResponseError(StatusCodes.Status404NotFound, "Không tìm thấy chùa", null);
            }
            phatTu.ChuaId = request.chuaId;
            phatTu.PhapDanh = request.PhapDanh;
            phatTu.NgayXuatGia = DateTime.Now;
            _context.phatTus.Update(phatTu);
            await _context.SaveChangesAsync();
            return _responseObject.ResponseSuccess("Thêm phật tử vào chùa thành công", _phatTuConverter.EntityToDTO(phatTu));
        }
    }
}
