using Microsoft.EntityFrameworkCore;
using QuanLyPhatTu_API.Entities;
using QuanLyPhatTu_API.Payloads.Converters;
using QuanLyPhatTu_API.Payloads.Requests.BinhLuanBaiVietRequest;
using QuanLyPhatTu_API.Service.Interfaces;

namespace QuanLyPhatTu_API.Service.Implements
{
    public class NguoiDungThichBinhLuanBaiVietService : BaseService, INguoiDungThichBinhLuanBaiVietService
    {
        private readonly NguoiDungThichBinhLuanBaiVietConverter _converter;
        public NguoiDungThichBinhLuanBaiVietService()
        {
            _converter = new NguoiDungThichBinhLuanBaiVietConverter();
        }
        public async Task<string> Dislike(int thichBinhLuanBaiVietId, int binhLuanBaiVietId, int nguoiDungId)
        {
            var binhLuanBaiViet = await _context.binhLuanBaiViets.SingleOrDefaultAsync(x => x.Id == binhLuanBaiVietId);
            if (binhLuanBaiViet == null)
            {
                return "Bình luận bài viết không được tìm thấy";
            }
            var nguoiDislike = await _context.phatTus.SingleOrDefaultAsync(x => x.Id == nguoiDungId);
            var thichBinhLuanBaiViet = await _context.nguoiDungThichBinhLuanBaiViets.SingleOrDefaultAsync(x => x.Id == thichBinhLuanBaiVietId);
            if (thichBinhLuanBaiViet.DaXoa == true)
            {
                return "Người dùng đã dislike bình luận bài viết này";
            }
            thichBinhLuanBaiViet.DaXoa = true;
            _context.nguoiDungThichBinhLuanBaiViets.Update(thichBinhLuanBaiViet);
            await _context.SaveChangesAsync();
            binhLuanBaiViet.SoLuotThich -= 1;
            _context.binhLuanBaiViets.Update(binhLuanBaiViet);
            await _context.SaveChangesAsync();
            return "Dislike bình luận bài viết thành công";
        }

        public async Task<string> Like(int binhLuanBaiVietId, int nguoiDungId)
        {
            var binhLuanBaiViet = await _context.binhLuanBaiViets.SingleOrDefaultAsync(x => x.Id == binhLuanBaiVietId);
            if (binhLuanBaiViet == null)
            {
                return "Bình luận bài viết không được tìm thấy";
            }
            var nguoiDislike = await _context.phatTus.SingleOrDefaultAsync(x => x.Id == nguoiDungId);
            var nguoiDungThichBinhLuan = new NguoiDungThichBinhLuanBaiViet
            {
                DaXoa = false,
                BinhLuanBaiVietId = binhLuanBaiVietId,
                PhatTuId = nguoiDungId,
                ThoiGianThich = DateTime.Now
            };
            _context.nguoiDungThichBinhLuanBaiViets.Add(nguoiDungThichBinhLuan);
            await _context.SaveChangesAsync();
            binhLuanBaiViet.SoLuotThich += 1;
            _context.binhLuanBaiViets.Update(binhLuanBaiViet);
            await _context.SaveChangesAsync();
            return "Like bình luận bài viết thành công";
        }
    }
}
