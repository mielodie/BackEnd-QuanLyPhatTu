using QuanLyPhatTu_API.Entities;
using QuanLyPhatTu_API.Payloads.DTOs;

namespace QuanLyPhatTu_API.Payloads.Converters
{
    public class DaoTrangConverter
    {
        public DaoTrangDTO EntityToDTO(DaoTrang daoTrang)
        {
            return new DaoTrangDTO
            {
                NguoiTruTri = daoTrang.NguoiTruTri,
                DaKetThuc = daoTrang.DaKetThuc,
                NoiDung = daoTrang.NoiDung,
                NoiToChuc = daoTrang.NoiToChuc,
                SoThanhVienThamGia = daoTrang.SoThanhVienThamGia,
                ThoiGianBatDau = daoTrang.ThoiGianBatDau,
                ThoiGianKetThuc = daoTrang.ThoiGianKetThuc
            };
        }
    }
}
