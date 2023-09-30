using QuanLyPhatTu_API.Handle.HandlePagination;
using QuanLyPhatTu_API.Payloads.DTOs;
using QuanLyPhatTu_API.Payloads.Requests.ChuaRequest;
using QuanLyPhatTu_API.Payloads.Responses;

namespace QuanLyPhatTu_API.Service.Interfaces
{
    public interface IChuaService
    {
        Task<ResponseObject<ChuaDTO>> ThemChua(Request_ThemChua request);
        Task<ResponseObject<ChuaDTO>> SuaThongTinChua(int chuaId, Request_SuaThongTinChua request);
        Task<string> XoaChua(int chuaId);
        Task<PageResult<ChuaDTO>> LayTatCaChua(int pageSize = 10, int pageNumber = 1);
    }
}
