using QuanLyPhatTu_API.Payloads.DTOs;
using QuanLyPhatTu_API.Payloads.Requests.PhatTuRequest;
using QuanLyPhatTu_API.Payloads.Responses;

namespace QuanLyPhatTu_API.Service.Interfaces
{
    public interface IPhatTuService
    {
        Task<IQueryable<PhatTuDTO>> LayTatCaPhatTu(int pageSize, int pageNumber);
        Task<IQueryable<PhatTuDTO>> LayPhatTuTheoTen(string name, int pageSize, int pageNumber);
        Task<IQueryable<PhatTuDTO>> LayPhatTuTheoGioiTinh(string gioiTinh, int pageSize, int pageNumber);
        Task<IQueryable<PhatTuDTO>> LayPhatTuTheoChua(int chuaId, int pageSize, int pageNumber);
        Task<IQueryable<PhatTuDTO>> LayPhatTuTheoPhapDanh(string phapDanh, int pageSize, int pageNumber);
        Task<ResponseObject<PhatTuDTO>> LayPhatTuTheoEmail(string email, int pageSize, int pageNumber);
        Task<ResponseObject<PhatTuDTO>> SuaThongTinPhatTu(int phatTuId, Request_CapNhatThongTinPhatTu request);
        Task<IQueryable<PhatTuDTO>> LayPhatTuTheoTrangThai(bool status, int pageSize, int pageNumber);
        Task<string> XoaPhatTu(int phatTuId);
    }
}
