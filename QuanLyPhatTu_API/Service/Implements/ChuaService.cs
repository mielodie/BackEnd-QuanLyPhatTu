using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using QuanLyPhatTu_API.Entities;
using QuanLyPhatTu_API.Handle.HandlePagination;
using QuanLyPhatTu_API.Payloads.Converters;
using QuanLyPhatTu_API.Payloads.DTOs;
using QuanLyPhatTu_API.Payloads.Requests.ChuaRequest;
using QuanLyPhatTu_API.Payloads.Responses;
using QuanLyPhatTu_API.Service.Interfaces;

namespace QuanLyPhatTu_API.Service.Implements
{
    public class ChuaService : BaseService, IChuaService
    {
        private readonly ResponseObject<ChuaDTO> _responseObject;
        private readonly ChuaConverter _chuaConverter;
        public ChuaService(ResponseObject<ChuaDTO> responseObject, ChuaConverter chuaConverter)
        {
            _responseObject = responseObject;
            _chuaConverter = chuaConverter;
        }
        public async Task<PageResult<ChuaDTO>> LayTatCaChua(int pageSize = 10, int pageNumber = 1)
        {
            var chuaQuery = _context.chuas
                                    .Include(x => x.PhatTus).AsNoTracking()
                                    .Select(x => _chuaConverter.EntityToDTO(x));
            var result = Pagination.GetPageData(chuaQuery, pageSize, pageNumber);
            return result;
        }

        public async Task<ResponseObject<ChuaDTO>> SuaThongTinChua(int chuaId,Request_SuaThongTinChua request)
        {
            var chua = await _context.chuas.FirstOrDefaultAsync(x => x.Id == chuaId);
            if (chua == null)
            {
                return _responseObject.ResponseError(StatusCodes.Status404NotFound, "Chùa không tồn tại", null);
            }
            else
            {
                chua.TenChua = request.TenChua;
                chua.DiaChi = request.DiaChi;
                chua.NguoiTruTri = request.NguoiTruTri;
                chua.NgayCapNhat = DateTime.Now;
                _context.chuas.Update(chua);
                await _context.SaveChangesAsync();
                return _responseObject.ResponseSuccess("Cập nhật thông tin chùa thành công", _chuaConverter.EntityToDTO(chua));
            }
        }

        public async Task<ResponseObject<ChuaDTO>> ThemChua(Request_ThemChua request)
        {
            if(string.IsNullOrWhiteSpace(request.TenChua) || string.IsNullOrWhiteSpace(request.DiaChi) || string.IsNullOrWhiteSpace(request.NguoiTruTri))
            {
                return _responseObject.ResponseError(StatusCodes.Status400BadRequest, "Vui lòng điền đầy đủ thông tin", null);
            }
            else
            {
                Chua chua = new Chua();
                chua.NguoiTruTri = request.NguoiTruTri;
                chua.TenChua = request.TenChua;
                chua.DiaChi = request.DiaChi;
                chua.NgayCapNhat = DateTime.Now;
                chua.NgayThanhLap = DateTime.Now;
                await _context.chuas.AddAsync(chua);
                await _context.SaveChangesAsync();
                return _responseObject.ResponseSuccess("Thêm chùa thành công", _chuaConverter.EntityToDTO(chua));
            }
        }

        public async Task<string> XoaChua(int chuaId)
        {
            var chua = await _context.chuas.FirstOrDefaultAsync(x => x.Id == chuaId);
            if(chua is null)
            {
                return "Chùa không tồn tại";
            }
            else
            {
                chua.IsActive = false;
                _context.chuas.Update(chua);
                await _context.SaveChangesAsync();
                return "Thay đổi trạng thái chùa thành công";
            }
        }
    }
}
