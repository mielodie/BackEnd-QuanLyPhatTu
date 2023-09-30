using Microsoft.AspNetCore.Mvc.RazorPages;
using QuanLyPhatTu_API.Handle.HandlePagination;
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
        Task<PageResult<BaiVietDTO>> LayTatCaBaiViet(int pageSize = 10, int pageNumber = 1);
        Task<PageResult<BaiVietDTO>> LayBaiVietTheoNguoiDung(int? nguoiDungId, int pageSize = 10, int pageNumber = 1);
        Task<PageResult<BaiVietDTO>> LayBaiVietTheoLoaiBaiViet(string? tenLoai, int pageSize = 10, int pageNumber = 1);
        Task<string> DuyetBaiViet(int nguoiDuyetId,int baiVietId);

    }
}
