using QuanLyPhatTu_API.Entities;
using QuanLyPhatTu_API.Payloads.DTOs;

namespace QuanLyPhatTu_API.Payloads.Converters
{
    public class PhatTuConverter
    {
        public PhatTuDTO EntityToDTO(PhatTu phatTu)
        {
            return new PhatTuDTO()
            {
                TaiKhoan = phatTu.TaiKhoan,
                HoVaTen = phatTu.HoVaTen,
                AnhChup = phatTu.AnhChup,
                DaHoanTuc = phatTu.DaHoanTuc,
                Email = phatTu.Email,
                GioiTinh = phatTu.GioiTinh,
                NgayCapNhat = DateTime.Now,
                NgaySinh = phatTu.NgaySinh,
                NgayXuatGia = phatTu.NgayXuatGia,

                PhapDanh = phatTu.PhapDanh,
                SoDienThoai = phatTu.SoDienThoai
            };
        }
    }
}
