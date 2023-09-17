using QuanLyPhatTu_API.Payloads.DTOs;
using QuanLyPhatTu_API.Payloads.Requests.BinhLuanBaiVietRequest;
using QuanLyPhatTu_API.Payloads.Responses;

namespace QuanLyPhatTu_API.Service.Interfaces
{
    public interface IBinhLuanBaiVietService
    {
        Task<ResponseObject<BinhLuanBaiVietDTO>> TaoBinhLuan(int nguoiDungId, Request_TaoBinhLuan request);
    }
}
