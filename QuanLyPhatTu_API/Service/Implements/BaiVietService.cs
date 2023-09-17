using Microsoft.EntityFrameworkCore;
using QuanLyPhatTu_API.Payloads.Converters;
using QuanLyPhatTu_API.Payloads.DTOs;
using QuanLyPhatTu_API.Payloads.Requests.BaiVietRequest;
using QuanLyPhatTu_API.Payloads.Responses;
using QuanLyPhatTu_API.Service.Interfaces;

namespace QuanLyPhatTu_API.Service.Implements
{
    public class BaiVietService : BaseService, IBaiVietService
    {
        private readonly BaiVietConverter _converter;
        private readonly ResponseObject<BaiVietDTO> _responseObjectBaiVietDTO;
        public BaiVietService()
        {
            _converter = new BaiVietConverter();
            _responseObjectBaiVietDTO = new ResponseObject<BaiVietDTO>();
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
        public async Task<IQueryable<BaiVietDTO>> LayBaiVietTheoLoaiBaiViet(string tenLoai, int pageSize, int pageNumber)
        {
            return _context.baiViets
                .Where(x => ChuanHoaChuoi(x.LoaiBaiViet.TenLoai).Contains(ChuanHoaChuoi(tenLoai)))
                .Select(x => _converter.EntityToDTO(x))
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize);
        }

        public async Task<IQueryable<BaiVietDTO>> LayBaiVietTheoNguoiDung(int nguoiDungId, int pageSize, int pageNumber)
        {
            return _context.baiViets
                .Where(x => x.PhatTuId == nguoiDungId)
                .Select(x => _converter.EntityToDTO(x))
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize);
        }

        public async Task<IQueryable<BaiVietDTO>> LayTatCaBaiViet(int pageSize, int pageNumber)
        {
            return _context.baiViets
                 .Select(x => _converter.EntityToDTO(x))
                 .Skip((pageNumber - 1) * pageSize)
                 .Take(pageSize);
        }

        public async Task<ResponseObject<BaiVietDTO>> SuaBaiViet(Request_SuaBaiViet request)
        {
            var baiViet = await _context.baiViets.SingleOrDefaultAsync(x => x.Id == request.BaiVietId);
            if (baiViet == null)
            {
                return _responseObjectBaiVietDTO.ResponseError(StatusCodes.Status404NotFound, "Không tìm thấy bài viết", null);
            }
            var baiVietSua = _converter.SuaBaiViet(request);
            baiVietSua.ThoiGianCapNhat = DateTime.Now;
            _context.baiViets.Update(baiVietSua);
            await _context.SaveChangesAsync();
            return _responseObjectBaiVietDTO.ResponseSuccess("Sửa bài viết thành công", _converter.EntityToDTO(baiVietSua));
        }

        public async Task<ResponseObject<BaiVietDTO>> TaoBaiViet(int nguoiDangBaiId, Request_TaoBaiViet request)
        {
            var loaiBaiViet = await _context.loaiBaiViets.SingleOrDefaultAsync(x => x.Id == request.LoaiBaiVietId);
            if (loaiBaiViet == null)
            {
                return _responseObjectBaiVietDTO.ResponseError(StatusCodes.Status404NotFound, "Không tìm thấy loại bài viết", null);
            }
            var nguoiDangBai = await _context.phatTus.SingleOrDefaultAsync(x => x.Id == nguoiDangBaiId);
            var baiVietTao = _converter.TaoBaiViet(request);
            baiVietTao.ThoiGianDang = DateTime.MaxValue;
            baiVietTao.PhatTuId = nguoiDangBaiId;
            baiVietTao.NguoiDuyetBaiId = 0;
            baiVietTao.DaXoa = false;
            baiVietTao.SoLuotBinhLuan = 0;
            baiVietTao.SoLuotThich = 0;
            baiVietTao.TrangThaiBaiVietId = 1;
            baiVietTao.ThoiGianCapNhat = DateTime.MaxValue;
            baiVietTao.ThoiGianXoa = DateTime.MaxValue;
            await _context.baiViets.AddAsync(baiVietTao);
            await _context.SaveChangesAsync();
            return _responseObjectBaiVietDTO.ResponseSuccess("Tạo bài viết thành công", _converter.EntityToDTO(baiVietTao));
        }

        public async Task<string> XoaBaiViet(int baiVietId)
        {
            var baiViet = await _context.baiViets.SingleOrDefaultAsync(x => x.Id == baiVietId);
            if (baiViet == null)
            {
                return "Không tìm thấy bài viết";
            }
            var nguoiTaoBai = await _context.phatTus.SingleOrDefaultAsync(x => x.Id == baiViet.PhatTuId);
            if(nguoiTaoBai == null)
            {
                return "Không muốn xóa bài viết không phải là người đăng bài viết đó";
            }
            baiViet.DaXoa = true;
            _context.baiViets.Update(baiViet);
            await _context.SaveChangesAsync();
            return "Xóa bài viết thành công";
        }

        public async Task<string> DuyetBaiViet(int nguoiDuyetId, int baiVietId)
        {
            var baiViet = await _context.baiViets.SingleOrDefaultAsync(x => x.Id == baiVietId);
            if (baiViet is null)
            {
                return "Bài viết không tồn tại";
            }
            var nguoiDuyet = await _context.phatTus.SingleOrDefaultAsync(x => x.Id == nguoiDuyetId);
            baiViet.ThoiGianDang = DateTime.Now;
            baiViet.TrangThaiBaiVietId = 3;
            baiViet.NguoiDuyetBaiId = nguoiDuyetId;
            _context.baiViets.Update(baiViet);
            await _context.SaveChangesAsync();
            return "Đã duyệt bài thành công";

        }
    }
}
