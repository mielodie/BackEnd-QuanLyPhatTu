using QuanLyPhatTu_API.Entities;
using QuanLyPhatTu_API.Payloads.DTOs;
using QuanLyPhatTu_API.Payloads.Requests.BaiVietRequest;
using QuanLyPhatTu_API.Payloads.Responses;

namespace QuanLyPhatTu_API.Service.Interfaces
{
    public interface INguoiDungThichBaiVietService
    {
        Task<string> Like(Request_NguoiDungThichBaiViet nguoiDung,int nguoiDungId);
        Task<string> Dislike(int thichBaiVietId, int baiVietId, int nguoiDungId);
    }
}
