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
        public DonDangKyService(ResponseObject<DonDangKyDTO> responseObject,DonDangKyConverter donDangKyConverter)
        {
            _responseObject = responseObject;
            _donDangKyConverter = donDangKyConverter;
        }

        public async Task<ResponseObject<DonDangKyDTO>> DuyetDonDangKy(int nguoiXuLyId, Request_DuyetDonDangKy request)
        {
            var donDangKy = await _context.donDangKies.FirstOrDefaultAsync(x => x.Id == request.DonDangKyId);

            if (donDangKy == null)
            {
                return _responseObject.ResponseError(StatusCodes.Status404NotFound, "Không tìm thấy đơn đăng ký", null);
            }

            var nguoiDuyet = await _context.phatTus.FirstOrDefaultAsync(x => x.Id == nguoiXuLyId);

            if (nguoiDuyet == null)
            {
                return _responseObject.ResponseError(StatusCodes.Status404NotFound, "Không tìm thấy người duyệt", null);
            }

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
                //var phatTuDaoTrangs = await _context.phatTuDaoTrangs.Where(x => x.DaoTrangId == daoTrang.Id).ToListAsync();
                //phatTuDaoTrangs.ForEach(x =>
                //{
                //    x.LyDoKhongThamGia = "Không thích";
                //    x.DaThamGia = donDangKy.TrangThaiDonId == 2;
                //    x.DaoTrangId = daoTrang.Id;
                //    x.PhatTuId = donDangKy.PhatTuId;

                //});
                //await _context.phatTuDaoTrangs.AddRangeAsync(phatTuDaoTrangs);
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
