using QuanLyPhatTu_API.Entities;
using QuanLyPhatTu_API.Payloads.DTOs;

namespace QuanLyPhatTu_API.Payloads.Converters
{
    public class DonDangKyConverter
    {
        public DonDangKyDTO EntityToDTO(DonDangKy donDangKy)
        {
            return new DonDangKyDTO
            {
                DaoTrangId = donDangKy.DaoTrangId,
                NgayGuiDon = donDangKy.NgayGuiDon,
                NgayXuLy = donDangKy.NgayXuLy,
                NguoiXuLy = donDangKy.NguoiXuLy,
                PhatTuId = donDangKy.PhatTuId,
                TrangThaiDonId = donDangKy.TrangThaiDonId
            };
        }
    }
}
