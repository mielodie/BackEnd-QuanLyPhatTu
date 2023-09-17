using Microsoft.EntityFrameworkCore;
using QuanLyPhatTu_API.Entities;
using QuanLyPhatTu_API.Payloads.Converters;
using QuanLyPhatTu_API.Payloads.DTOs;
using QuanLyPhatTu_API.Payloads.Requests.BaiVietRequest;
using QuanLyPhatTu_API.Payloads.Responses;
using QuanLyPhatTu_API.Service.Interfaces;

namespace QuanLyPhatTu_API.Service.Implements
{
    public class NguoiDungThichBaiVietService : BaseService, INguoiDungThichBaiVietService
    {
        private readonly NguoiDungThichBaiVietConverter _converter;
        public NguoiDungThichBaiVietService()
        {
            _converter = new NguoiDungThichBaiVietConverter();
        }
        public async Task<string> Dislike(int thichBaiVietId, int baiVietId, int nguoiDungId)
        {
            var baiViet = await _context.baiViets.SingleOrDefaultAsync(x => x.Id == baiVietId);
            if(baiViet == null)
            {
                return "Bài viết không được tìm thấy";
            }
            var nguoiDislike = await _context.phatTus.SingleOrDefaultAsync(x => x.Id == nguoiDungId);
            var thichBaiViet = await _context.nguoiDungThichBaiViets.SingleOrDefaultAsync(x => x.Id == thichBaiVietId);
            if (thichBaiViet.DaXoa == true)
            {
                return "Người dùng đã dislike bài viết này";
            }
            thichBaiViet.DaXoa = true;
            _context.nguoiDungThichBaiViets.Update(thichBaiViet);
            await _context.SaveChangesAsync();
            baiViet.SoLuotThich -= 1;
            _context.baiViets.Update(baiViet);
            await _context.SaveChangesAsync();
            return "Dislike bài viết thành công";
        }


        public async Task<string> Like(Request_NguoiDungThichBaiViet nguoiDung,int nguoiDungId)
        {
            var baiViet = await _context.baiViets.SingleOrDefaultAsync(x => x.Id == nguoiDung.BaiVietId);
            if (baiViet == null)
            {
                return "Bài viết không được tìm thấy";
            }
            var nguoiLike = await _context.phatTus.SingleOrDefaultAsync(x => x.Id == nguoiDungId);
            var thichBaiViet = new NguoiDungThichBaiViet
            {
                PhatTuId = nguoiDungId,
                BaiVietId = nguoiDung.BaiVietId,
                DaXoa = false,
                ThoiGianThich = DateTime.Now,
            };
            await _context.nguoiDungThichBaiViets.AddAsync(thichBaiViet);
            await _context.SaveChangesAsync();
            baiViet.SoLuotThich += 1;
            _context.baiViets.Update(baiViet);
            await _context.SaveChangesAsync();
            return "Like bài viết thành công";
        }
    }
}
