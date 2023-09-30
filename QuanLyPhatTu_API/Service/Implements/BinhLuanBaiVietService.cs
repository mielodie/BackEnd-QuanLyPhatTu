using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using QuanLyPhatTu_API.Entities;
using QuanLyPhatTu_API.Handle.HandlePagination;
using QuanLyPhatTu_API.Payloads.Converters;
using QuanLyPhatTu_API.Payloads.DTOs;
using QuanLyPhatTu_API.Payloads.Requests.BinhLuanBaiVietRequest;
using QuanLyPhatTu_API.Payloads.Responses;
using QuanLyPhatTu_API.Service.Interfaces;

namespace QuanLyPhatTu_API.Service.Implements
{
    public class BinhLuanBaiVietService : BaseService, IBinhLuanBaiVietService
    {
        private readonly ResponseObject<BinhLuanBaiVietDTO> _responseObject;
        private readonly BinhLuanBaiVietConverter _converter;
        public BinhLuanBaiVietService()
        {
            _converter = new BinhLuanBaiVietConverter();
            _responseObject = new ResponseObject<BinhLuanBaiVietDTO>();
        }
        private string ChuanHoaChuoi(string name)
        {
            name = name.Trim().ToLower();
            while (name.Contains("  "))
            {
                name = name.Replace("  ", " ");
            }
            return name;
        }

        public async Task<PageResult<BinhLuanBaiVietDTO>> LayBinhLuanTheoNguoiDung(int? userId, int pageSize = 10, int pageNumber = 1)
        {
            var query = _context.binhLuanBaiViets
                                .Include(x => x.BaiViet)
                                .Where(x => x.Id == userId)
                                .Select(x => _converter.EntityToDTO(x));
            var result = Pagination.GetPageData(query, pageSize, pageNumber);
            return result;
        }

        public async Task<ResponseObject<BinhLuanBaiVietDTO>> SuaBinhLuan(int binhLuanId, int nguoiDungId, Request_SuaBinhLuan request)
        {
            var baiViet = await _context.baiViets.SingleOrDefaultAsync(x => x.Id.Equals(request.BaiVietId));
            if(baiViet == null)
            {
                return _responseObject.ResponseError(StatusCodes.Status404NotFound, "Không tìm thấy bài viết này", null);
            }
            var binhLuan = await _context.binhLuanBaiViets.SingleOrDefaultAsync(x => x.Id.Equals(binhLuanId));
            if(binhLuan == null)
            {
                return _responseObject.ResponseError(StatusCodes.Status404NotFound, "Không tìm thấy bình luận này", null);
            }
            var nguoiDung = await _context.phatTus.SingleOrDefaultAsync(x => x.Id.Equals(nguoiDungId));
            var updateComment = _converter.SuaBinhLuan(binhLuan, request);
            updateComment.PhatTuId = nguoiDungId;
            updateComment.ThoiGianCapNhat = DateTime.Now;
            _context.binhLuanBaiViets.Update(updateComment);
            await _context.SaveChangesAsync();
            return _responseObject.ResponseSuccess("Sửa bình luận bài viết thành công", _converter.EntityToDTO(updateComment));
        }

        public async Task<ResponseObject<BinhLuanBaiVietDTO>> TaoBinhLuan(int nguoiDungId,Request_TaoBinhLuan request)
        {
            var baiViet = await _context.baiViets.SingleOrDefaultAsync(x => x.Id.Equals(request.BaiVietId));
            if (baiViet == null)
            {
                return _responseObject.ResponseError(StatusCodes.Status404NotFound, "Không tìm thấy bài viết", null);
            }
            var nguoiBinhLuan = await _context.phatTus.SingleOrDefaultAsync(x => x.Id == (nguoiDungId));
            var addComment = _converter.TaoBinhLuan(request);
            addComment.PhatTuId = nguoiDungId;
            addComment.ThoiGianTao = DateTime.Now;
            await _context.binhLuanBaiViets.AddAsync(addComment);
            await _context.SaveChangesAsync();
            baiViet.SoLuotBinhLuan += 1;
            _context.baiViets.Update(baiViet);
            await _context.SaveChangesAsync();
            return _responseObject.ResponseSuccess("Tạo bình luận bài viết thành công", _converter.EntityToDTO(addComment));
        }

        public async Task<ResponseObject<BinhLuanBaiVietDTO>> XoaBinhLuan(int binhLuanId, int nguoiDungId, int baiVietId)
        {
            var baiViet = await _context.baiViets.SingleOrDefaultAsync(x => x.Id.Equals(baiVietId));
            if (baiViet == null)
            {
                return _responseObject.ResponseError(StatusCodes.Status404NotFound, "Không tìm thấy bài viết này", null);
            }
            var binhLuan = await _context.binhLuanBaiViets.SingleOrDefaultAsync(x => x.Id.Equals(binhLuanId));
            if (binhLuan == null)
            {
                return _responseObject.ResponseError(StatusCodes.Status404NotFound, "Không tìm thấy bình luận này", null);
            }
            var nguoiDung = await _context.phatTus.SingleOrDefaultAsync(x => x.Id.Equals(nguoiDungId));
            binhLuan.DaXoa = true;
            binhLuan.ThoiGianXoa = DateTime.Now;
            _context.binhLuanBaiViets.Update(binhLuan);
            await _context.SaveChangesAsync();
            baiViet.SoLuotBinhLuan -= 1;
            _context.baiViets.Update(baiViet);
            await _context.SaveChangesAsync();
            return _responseObject.ResponseSuccess("Xóa bình luận bài viết thành công", _converter.EntityToDTO(binhLuan));
        }
    }
}
