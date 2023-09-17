using QuanLyPhatTu_API.Payloads.DTOs;
using QuanLyPhatTu_API.Payloads.Requests.BaiVietRequest;
using QuanLyPhatTu_API.Payloads.Responses;

namespace QuanLyPhatTu_API.Service.Interfaces
{
    public interface IBaiVietService
    {
        Task<ResponseObject<BaiVietDTO>> TaoBaiViet(int nguoiDangBaiId, Request_TaoBaiViet request);
        Task<ResponseObject<BaiVietDTO>> SuaBaiViet(Request_SuaBaiViet request);
        Task<string> XoaBaiViet(int baiVietId);
        Task<IQueryable<BaiVietDTO>> LayTatCaBaiViet(int pageSize, int pageNumber);
        Task<IQueryable<BaiVietDTO>> LayBaiVietTheoNguoiDung(int nguoiDungId, int pageSize, int pageNumber);
        Task<IQueryable<BaiVietDTO>> LayBaiVietTheoLoaiBaiViet(string tenLoai, int pageSize, int pageNumber);
        Task<string> DuyetBaiViet(int nguoiDuyetId,int baiVietId);

    }
}
