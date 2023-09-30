using QuanLyPhatTu_API.Entities;
using QuanLyPhatTu_API.Handle.Email;
using QuanLyPhatTu_API.Payloads.DTOs;
using QuanLyPhatTu_API.Payloads.Requests.PhatTuRequest;
using QuanLyPhatTu_API.Payloads.Responses;

namespace QuanLyPhatTu_API.Service.Interfaces
{
    public interface IAuthService
    {
        Task<ResponseObject<PhatTuDTO>> DangKy(Request_DangKy request);
        Task<ResponseObject<TokenDTO>> DangNhap(Request_DangNhap request);
        TokenDTO GenerateAccessToken(PhatTu phatTu);
        string GenerateRefreshToken();
        ResponseObject<TokenDTO> RenewAccessToken(TokenDTO request);
        string SendEmail(EmailTo emailTo);
        Task<string> XacNhanQuenMatKhau(Request_XacNhanQuenMatKhau request);
        Task<ResponseObject<PhatTuDTO>> TaoMatKhauMoi(Request_TaoMatKhauMoi request);
        Task<string> DoiMatKhau(int phatTuId, Request_DoiMatKhau request);
        Task<string> ThayDoiQuyenHan(int phatTuId);
        Task<string> XacNhanDangKyTaiKhoan(string maXacNhan);
    }
}
