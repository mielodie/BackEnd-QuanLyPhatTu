using QuanLyPhatTu_API.Payloads.DTOs;
using QuanLyPhatTu_API.Payloads.Requests.DonDangKyRequest;
using QuanLyPhatTu_API.Payloads.Responses;

namespace QuanLyPhatTu_API.Service.Interfaces
{
    public interface IDonDangKyService
    {
        Task<ResponseObject<DonDangKyDTO>> TaoDonDangKy(int phatTuId, Request_TaoDonDangKy request);
        Task<ResponseObject<DonDangKyDTO>> SuaDonDangKy(int donDangKyId, Request_SuaDonDangKy request);
        Task<ResponseObject<DonDangKyDTO>> DuyetDonDangKy(Request_DuyetDonDangKy request);
    }
}
