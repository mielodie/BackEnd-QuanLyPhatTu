using FluentEmail.Core;
using Microsoft.EntityFrameworkCore;
using QuanLyPhatTu_API.Entities;
using QuanLyPhatTu_API.Payloads.Converters;
using QuanLyPhatTu_API.Payloads.DTOs;
using QuanLyPhatTu_API.Payloads.Requests.DonDangKyRequest;
using QuanLyPhatTu_API.Payloads.Responses;
using QuanLyPhatTu_API.Service.Interfaces;

namespace QuanLyPhatTu_API.Service.Implements
{
    public class DonDangKyService : BaseService, IDonDangKyService
    {
        private readonly ResponseObject<DonDangKyDTO> _responseObject;
        private readonly DonDangKyConverter _donDangKyConverter;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public DonDangKyService(ResponseObject<DonDangKyDTO> responseObject,DonDangKyConverter donDangKyConverter, IHttpContextAccessor httpContextAccessor)
        {
            _responseObject = responseObject;
            _donDangKyConverter = donDangKyConverter;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ResponseObject<DonDangKyDTO>> DuyetDonDangKy(int nguoiXuLyId, Request_DuyetDonDangKy request)
        {
            var donDangKy = await _context.donDangKies.FirstOrDefaultAsync(x => x.Id == request.DonDangKyId);
            var currentUser = _httpContextAccessor.HttpContext.User;
            if (!currentUser.Identity.IsAuthenticated)
            {
                return _responseObject.ResponseError(StatusCodes.Status401Unauthorized, "Người dùng không được xác định", null);
            }
            if (!currentUser.IsInRole("ADMIN"))
            {
                return _responseObject.ResponseError(StatusCodes.Status403Forbidden, "Người dùng không có quyền sử dụng chức năng này", null);
            }
            if (donDangKy == null)
            {
                return _responseObject.ResponseError(StatusCodes.Status404NotFound, "Không tìm thấy đơn đăng ký", null);
            }

            var nguoiDuyet = await _context.phatTus.FirstOrDefaultAsync(x => x.Id == nguoiXuLyId);

            donDangKy.NgayXuLy = DateTime.Now;
            donDangKy.NguoiXuLy = nguoiDuyet.Id;
            donDangKy.TrangThaiDonId = 2;

            try
            {
                _context.donDangKies.Update(donDangKy);

                var daoTrang = await _context.daoTrangs.FirstOrDefaultAsync(x => x.Id == donDangKy.DaoTrangId);
                if (daoTrang != null)
                {
                    daoTrang.SoThanhVienThamGia += 1;
                    _context.daoTrangs.Update(daoTrang);
                }
                var phatTuDaoTrang = new PhatTuDaoTrang
                {
                    DaoTrangId = daoTrang.Id,
                    DaThamGia = donDangKy.TrangThaiDonId == 2,
                    LyDoKhongThamGia = donDangKy.TrangThaiDonId == 2 ? "Đã tham gia" : "Không tham gia",
                    PhatTuId = donDangKy.PhatTuId
                };
                await _context.phatTuDaoTrangs.AddAsync(phatTuDaoTrang);

                await _context.SaveChangesAsync();

                return _responseObject.ResponseSuccess("Duyệt đơn đăng ký thành công", _donDangKyConverter.EntityToDTO(donDangKy));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi: {ex.Message}");
                return _responseObject.ResponseError(StatusCodes.Status500InternalServerError, "Lỗi trong quá trình duyệt đơn đăng ký", null);
            }
        }


        public async Task<ResponseObject<DonDangKyDTO>> SuaDonDangKy(int donDangKyId, Request_SuaDonDangKy request)
        {
            var donDangKy = await _context.donDangKies.FirstOrDefaultAsync(x => x.Id == donDangKyId);
            if(donDangKy is null)
            {
                return _responseObject.ResponseError(StatusCodes.Status404NotFound, "Không tìm thấy đơn đăng ký", null);
            }
            donDangKy.DaoTrangId = request.DaoTrangId;
            _context.donDangKies.Update(donDangKy);
            await _context.SaveChangesAsync();
            return _responseObject.ResponseSuccess("Sửa đơn đăng ký thành công", _donDangKyConverter.EntityToDTO(donDangKy));
        }

        public async Task<ResponseObject<DonDangKyDTO>> TaoDonDangKy(int phatTuId, Request_TaoDonDangKy request)
        {
            var phatTu = await _context.phatTus.FirstOrDefaultAsync(x => x.Id == phatTuId);
            var daoTrang = await _context.daoTrangs.FirstOrDefaultAsync(x => x.Id == request.DaoTrangId);
            if(daoTrang is null)
            {
                return _responseObject.ResponseError(StatusCodes.Status404NotFound, "Không tìm thấy đạo tràng", null);
            }
            else
            {
                DonDangKy donDangKy = new DonDangKy();
                donDangKy.DaoTrangId = request.DaoTrangId;
                donDangKy.NgayGuiDon = DateTime.Now;
                donDangKy.PhatTuId = phatTuId;
                donDangKy.NgayXuLy = DateTime.Now;
                donDangKy.NguoiXuLy = 0;
                donDangKy.TrangThaiDonId = 1;

                await _context.donDangKies.AddAsync(donDangKy);
                await _context.SaveChangesAsync();
                return _responseObject.ResponseSuccess("Gửi đơn đăng ký thành công, vui lòng chờ duyệt", _donDangKyConverter.EntityToDTO(donDangKy));
            }
        }
    }
}
