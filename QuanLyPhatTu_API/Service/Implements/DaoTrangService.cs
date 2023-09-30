using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using QuanLyPhatTu_API.Entities;
using QuanLyPhatTu_API.Handle.HandlePagination;
using QuanLyPhatTu_API.Payloads.Converters;
using QuanLyPhatTu_API.Payloads.DTOs;
using QuanLyPhatTu_API.Payloads.DTOs.ThongKeDaoTrang;
using QuanLyPhatTu_API.Payloads.Requests.DaoTrangRequest;
using QuanLyPhatTu_API.Payloads.Responses;
using QuanLyPhatTu_API.Service.Interfaces;

namespace QuanLyPhatTu_API.Service.Implements
{
    public class DaoTrangService : BaseService, IDaoTrangService
    {
        private readonly ResponseObject<DaoTrangDTO> _responseObject;
        private readonly DaoTrangConverter _daoTrangConverter;
        public DaoTrangService()
        {
            _responseObject = new ResponseObject<DaoTrangDTO>();
            _daoTrangConverter = new DaoTrangConverter();
        }
        public async Task<IQueryable<DuLieuVeSoPhatTuCuaChuaThamGiaDaoTrang>> ThongKeSoPhatTuCuaChuaThamGiaDaoTrang()
        {
            var phatTuDaoTrangQuery = await _context.phatTuDaoTrangs
                .Include(x => x.DaoTrang)
                .Include(x => x.PhatTu)
                .ToListAsync();

            var duLieu = phatTuDaoTrangQuery.Select(phatTuDaoTrang =>
            {
                var chua = phatTuDaoTrang.PhatTu.Chua;
                var daoTrang = phatTuDaoTrang.DaoTrang;
                var thongKe = new ThongKeSoPhatTuCuaChua
                {
                    ChuaId = chua.Id,
                    SoPhatTu = daoTrang.SoThanhVienThamGia
                };

                var item = new DuLieuVeSoPhatTuCuaChuaThamGiaDaoTrang
                {
                    DaoTrangId = daoTrang.Id,
                    ThongKeSoPhatTuCuaChua = thongKe
                };

                return item;
            });

            return duLieu.AsQueryable();
        }


        public async Task<string> XoaDaoTrang(int daoTrangId)
        {
            var daoTrang = await _context.daoTrangs.FirstOrDefaultAsync(x => x.Id == daoTrangId);
            if(daoTrang is null)
            {
                return "Đạo tràng không tồn tại";
            }
            else
            {
                _context.daoTrangs.Remove(daoTrang);
                await _context.SaveChangesAsync();
                return "Xóa đạo tràng thành công";
            }

        }

        public async Task<ResponseObject<DaoTrangDTO>> ThemDaoTrang(Request_ThemDaoTrang request)
        {
            var nguoiTruTri = await _context.phatTus.FirstOrDefaultAsync(x => x.Id == request.NguoiTruTri);
            if(nguoiTruTri is null)
            {
                return _responseObject.ResponseError(StatusCodes.Status404NotFound, "Không tìm thấy phật tử", null);
            }
            else
            {
                DaoTrang daoTrang = new DaoTrang();
                daoTrang.NguoiTruTri = request.NguoiTruTri;
                daoTrang.SoThanhVienThamGia = 0;
                daoTrang.ThoiGianBatDau = request.ThoiGianBatDau;
                daoTrang.ThoiGianKetThuc = request.ThoiGianKetThuc;
                if(daoTrang.ThoiGianKetThuc < DateTime.Now)
                {
                    daoTrang.DaKetThuc = true;
                }
                daoTrang.DaKetThuc = false;
                daoTrang.NoiDung = request.NoiDung;
                daoTrang.NoiToChuc = request.NoiToChuc;
                await _context.daoTrangs.AddAsync(daoTrang);
                await _context.SaveChangesAsync();
                return _responseObject.ResponseSuccess("Thêm đạo tràng thành công", _daoTrangConverter.EntityToDTO(daoTrang));
            }
        }

        public async Task<ResponseObject<DaoTrangDTO>> SuaThongTinDaoTrang(Request_SuaThongTinDaoTrang request)
        {
            var daoTrang = await _context.daoTrangs.FirstOrDefaultAsync(x => x.Id == request.DaoTrangId);
            if(daoTrang is null)
            {
                return _responseObject.ResponseError(StatusCodes.Status404NotFound, "Đạo tràng không tồn tại", null);
            }
            else
            {
                daoTrang.ThoiGianBatDau = request.ThoiGianBatDau;
                daoTrang.ThoiGianKetThuc = request.ThoiGianKetThuc;
                daoTrang.NguoiTruTri = request.NguoiTruTri;
                daoTrang.NoiDung = request.NoiDung;
                daoTrang.NoiToChuc = request.NoiToChuc;
                _context.daoTrangs.Update(daoTrang);
                await _context.SaveChangesAsync();
                return _responseObject.ResponseSuccess("Cập nhật thông tin đạo tràng thành công", _daoTrangConverter.EntityToDTO(daoTrang));
            }
        }

        public async Task<PageResult<DaoTrangDTO>> LayTatCaDaoTrang(int pageSize = 10, int pageNumber = 1)
        {
            var daoTrangQuery = _context.daoTrangs
                                                  .Include(x => x.PhatTuDaoTrangs)
                                                  .Include(x => x.DonDangKies).AsNoTracking()
                                                  .Select(x => _daoTrangConverter.EntityToDTO(x));
            var result = Pagination.GetPageData(daoTrangQuery, pageSize, pageNumber);
            return result;
        }
    }
}
