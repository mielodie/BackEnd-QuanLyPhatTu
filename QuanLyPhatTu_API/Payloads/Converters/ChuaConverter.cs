using QuanLyPhatTu_API.Entities;
using QuanLyPhatTu_API.Payloads.DTOs;

namespace QuanLyPhatTu_API.Payloads.Converters
{
    public class ChuaConverter
    {
        public ChuaDTO EntityToDTO(Chua chua)
        {
            return new ChuaDTO
            {
                TenChua = chua.TenChua,
                DiaChi = chua.DiaChi,
                NgayThanhLap = chua.NgayThanhLap,
                NguoiTruTri = chua.NguoiTruTri,
                NgayCapNhat = chua.NgayCapNhat
            };
        }
    }
}
