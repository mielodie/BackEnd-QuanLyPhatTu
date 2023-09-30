using Microsoft.EntityFrameworkCore;
using QuanLyPhatTu_API.DataContext;
using QuanLyPhatTu_API.Entities;
using QuanLyPhatTu_API.Handle.HandlePagination;
using QuanLyPhatTu_API.Payloads.Converters;
using QuanLyPhatTu_API.Payloads.DTOs;
using QuanLyPhatTu_API.Payloads.Requests.LoaiBaiVietRequest;
using QuanLyPhatTu_API.Payloads.Responses;
using QuanLyPhatTu_API.Service.Interfaces;

namespace QuanLyPhatTu_API.Service.Implements
{
    public class LoaiBaiVietService : BaseService, ILoaiBaiVietService
    {
        private readonly AppDbContext _context;
        private readonly ResponseObject<LoaiBaiVietDTO> _responseObjectLoaiBaiVietDTO;
        private readonly LoaiBaiVietConverter _converter;
        public LoaiBaiVietService()
        {
            _context = new AppDbContext();
            _responseObjectLoaiBaiVietDTO = new ResponseObject<LoaiBaiVietDTO>();
            _converter = new LoaiBaiVietConverter();
        }

        public async Task<ResponseObject<LoaiBaiVietDTO>> ThemLoaiBaiViet(Request_TaoLoaiBaiViet request)
        {
            LoaiBaiViet loaiBaiViet = new LoaiBaiViet();
            loaiBaiViet.TenLoai = request.TenLoaiBaiViet;
            await _context.loaiBaiViets.AddAsync(loaiBaiViet);
            await _context.SaveChangesAsync();
            return _responseObjectLoaiBaiVietDTO.ResponseSuccess("Them loai bai viet thanh cong", _converter.EntityToDTO(loaiBaiViet));
        }
        public async Task<ResponseObject<LoaiBaiVietDTO>> SuaLoaiBaiViet(int loaiBaiVietID, Request_SuaLoaiBaiViet request)
        {
            var loaiBaiViet = await _context.loaiBaiViets.SingleOrDefaultAsync(x => x.Id == loaiBaiVietID);
            if (loaiBaiViet == null)
            {
                return _responseObjectLoaiBaiVietDTO.ResponseError(StatusCodes.Status404NotFound, "Khong tim thay loai bai viet", null);
            }
            else
            {
                loaiBaiViet.TenLoai = request.TenLoaiBaiViet;
                _context.loaiBaiViets.Update(loaiBaiViet);
                await _context.SaveChangesAsync();
                return _responseObjectLoaiBaiVietDTO.ResponseSuccess("Sua loai bai viet thanh cong", _converter.EntityToDTO(loaiBaiViet));
            }
        }


        public async Task<ResponseObject<LoaiBaiVietDTO>> XoaLoaiBaiViet(int loaiBaiVietID)
        {
            var loaiBaiViet = await _context.loaiBaiViets.SingleOrDefaultAsync(x => x.Id == loaiBaiVietID);
            if (loaiBaiViet == null)
            {
                return _responseObjectLoaiBaiVietDTO.ResponseError(StatusCodes.Status404NotFound, "Khong tim thay loai bai viet", null);
            }
            else
            {
                _context.loaiBaiViets.Remove(loaiBaiViet);
                await _context.SaveChangesAsync();
                return _responseObjectLoaiBaiVietDTO.ResponseSuccess("Xoa loai bai viet thanh cong", null);
            }
        }
        public async Task<PageResult<LoaiBaiVietDTO>> LayLoaiBaiViet(int pageSize = 10, int pageNumber = 1)
        {
            var loaiBaiVietResult = _context.loaiBaiViets
                                            .Include(x => x.BaiViets).AsNoTracking()
                                            .Select(x => _converter.EntityToDTO(x));
            var result = Pagination.GetPageData(loaiBaiVietResult, pageSize, pageNumber);
            return result;
        }
        public async Task<PageResult<LoaiBaiVietDTO>> LayLoaiBaiVietTheoTen(string? tenLoai, int pageSize = 10, int pageNumber = 1)
        {
            var loaiBaiVietResult = _context.loaiBaiViets
                                            .Include(x => x.BaiViets).AsNoTracking()
                                            .Where(x => ChuanHoaChuoi(x.TenLoai).Contains(ChuanHoaChuoi(tenLoai)))
                                            .Select(x => _converter.EntityToDTO(x));
            var result = Pagination.GetPageData(loaiBaiVietResult, pageSize, pageNumber);
            return result;
        }
        private string ChuanHoaChuoi(string str)
        {
            str = str.ToLower().Trim();
            while (str.Contains("  "))
            {
                str = str.Replace("  ", " ");
            }
            return str;
        }
    }
}
