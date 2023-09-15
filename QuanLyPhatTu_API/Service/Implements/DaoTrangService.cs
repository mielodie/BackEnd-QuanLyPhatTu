using Microsoft.EntityFrameworkCore;
using QuanLyPhatTu_API.Entities;
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
        w 

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
    }
}
