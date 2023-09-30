using Microsoft.AspNetCore.Mvc.RazorPages;
using QuanLyPhatTu_API.Handle.HandlePagination;
using QuanLyPhatTu_API.Payloads.DTOs;
using QuanLyPhatTu_API.Payloads.Requests.PhatTuRequest;
using QuanLyPhatTu_API.Payloads.Responses;

namespace QuanLyPhatTu_API.Service.Interfaces
{
    public interface IPhatTuService
    {
        Task<PageResult<PhatTuDTO>> LayTatCaPhatTu(int pageSize = 10, int pageNumber = 1);
        Task<PageResult<PhatTuDTO>> LayPhatTuTheoTen(string? name, int pageSize = 10, int pageNumber = 1);
        Task<PageResult<PhatTuDTO>> LayPhatTuTheoGioiTinh(string? gioiTinh, int pageSize = 10, int pageNumber = 1);
        Task<PageResult<PhatTuDTO>> LayPhatTuTheoChua(int? chuaId, int pageSize = 10, int pageNumber = 1);
        Task<PageResult<PhatTuDTO>> LayPhatTuTheoPhapDanh(string? phapDanh, int pageSize = 10, int pageNumber = 1);
        Task<ResponseObject<PhatTuDTO>> LayPhatTuTheoEmail(string email);
        Task<ResponseObject<PhatTuDTO>> SuaThongTinPhatTu(int phatTuId, Request_CapNhatThongTinPhatTu request);
        Task<PageResult<PhatTuDTO>> LayPhatTuTheoTrangThai(bool? status, int pageSize = 10, int pageNumber = 1);
        Task<string> XoaPhatTu(int phatTuId);
        Task<ResponseObject<PhatTuDTO>> ThemPhatTuVaoChua(Request_ThemPhatTuVaoChua request);
    }
}
