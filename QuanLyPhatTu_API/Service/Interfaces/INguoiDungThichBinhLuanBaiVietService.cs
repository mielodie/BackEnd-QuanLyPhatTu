using QuanLyPhatTu_API.Payloads.Requests.BaiVietRequest;
using QuanLyPhatTu_API.Payloads.Requests.BinhLuanBaiVietRequest;

namespace QuanLyPhatTu_API.Service.Interfaces
{
    public interface INguoiDungThichBinhLuanBaiVietService
    {
        Task<string> Like(int binhLuanBaiVietId, int nguoiDungId);
        Task<string> Dislike(int thichBinhLuanBaiVietId, int binhLuanBaiVietId, int nguoiDungId);
    }
}
