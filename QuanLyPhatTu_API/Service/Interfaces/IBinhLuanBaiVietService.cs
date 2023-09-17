using QuanLyPhatTu_API.Payloads.DTOs;
using QuanLyPhatTu_API.Payloads.Requests.BinhLuanBaiVietRequest;
using QuanLyPhatTu_API.Payloads.Responses;

namespace QuanLyPhatTu_API.Service.Interfaces
{
    public interface IBinhLuanBaiVietService
    {
        Task<ResponseObject<BinhLuanBaiVietDTO>> TaoBinhLuan(int nguoiDungId, Request_TaoBinhLuan request);
        Task<ResponseObject<BinhLuanBaiVietDTO>> SuaBinhLuan(int binhLuanId, int nguoiDungId, Request_SuaBinhLuan request);
        Task<ResponseObject<BinhLuanBaiVietDTO>> XoaBinhLuan(int binhLuanId, int nguoiDungId, int baiVietId);
        Task<IQueryable<BinhLuanBaiVietDTO>> LayBinhLuanTheoTenTaiKhoan(string tenTaiKhoan, int pageSize, int pageNumber);
    }
}
