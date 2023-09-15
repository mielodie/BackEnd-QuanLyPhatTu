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
        Task<IQueryable<ChuaDTO>> LayTatCaChua();
    }
}
