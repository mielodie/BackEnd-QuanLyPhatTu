using Microsoft.EntityFrameworkCore;
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
    }
}
