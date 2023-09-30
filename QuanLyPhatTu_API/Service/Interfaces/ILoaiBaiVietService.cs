using Microsoft.AspNetCore.Mvc.RazorPages;
using QuanLyPhatTu_API.Handle.HandlePagination;
using QuanLyPhatTu_API.Payloads.DTOs;
using QuanLyPhatTu_API.Payloads.Requests.LoaiBaiVietRequest;
using QuanLyPhatTu_API.Payloads.Responses;

namespace QuanLyPhatTu_API.Service.Interfaces
{
    public interface ILoaiBaiVietService
    {
        Task<ResponseObject<LoaiBaiVietDTO>> ThemLoaiBaiViet(Request_TaoLoaiBaiViet request);
        Task<ResponseObject<LoaiBaiVietDTO>> SuaLoaiBaiViet(int loaiBaiVietID, Request_SuaLoaiBaiViet request);
        Task<ResponseObject<LoaiBaiVietDTO>> XoaLoaiBaiViet(int loaiBaiVietID);
        Task<PageResult<LoaiBaiVietDTO>> LayLoaiBaiViet(int pageSize = 10, int pageNumber = 1);
        Task<PageResult<LoaiBaiVietDTO>> LayLoaiBaiVietTheoTen(string? tenLoai, int pageSize = 10, int pageNumber = 1);
    }
}
