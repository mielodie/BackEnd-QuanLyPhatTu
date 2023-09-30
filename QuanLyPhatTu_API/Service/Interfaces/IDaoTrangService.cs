using Microsoft.AspNetCore.Mvc.RazorPages;
using QuanLyPhatTu_API.Handle.HandlePagination;
using QuanLyPhatTu_API.Payloads.DTOs;
using QuanLyPhatTu_API.Payloads.DTOs.ThongKeDaoTrang;
using QuanLyPhatTu_API.Payloads.Requests.DaoTrangRequest;
using QuanLyPhatTu_API.Payloads.Responses;

namespace QuanLyPhatTu_API.Service.Interfaces
{
    public interface IDaoTrangService
    {
        Task<ResponseObject<DaoTrangDTO>> ThemDaoTrang(Request_ThemDaoTrang request);
        Task<ResponseObject<DaoTrangDTO>> SuaThongTinDaoTrang(Request_SuaThongTinDaoTrang request);
        Task<string> XoaDaoTrang(int daoTrangId);
        Task<PageResult<DaoTrangDTO>> LayTatCaDaoTrang(int pageSize = 10, int pageNumber = 1);
        Task<IQueryable<DuLieuVeSoPhatTuCuaChuaThamGiaDaoTrang>> ThongKeSoPhatTuCuaChuaThamGiaDaoTrang();
    }
}
