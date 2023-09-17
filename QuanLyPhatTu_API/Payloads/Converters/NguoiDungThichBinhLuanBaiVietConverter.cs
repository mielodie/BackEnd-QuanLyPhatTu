using QuanLyPhatTu_API.Entities;
using QuanLyPhatTu_API.Payloads.DTOs;

namespace QuanLyPhatTu_API.Payloads.Converters
{
    public class NguoiDungThichBinhLuanBaiVietConverter
    {
        public NguoiDungThichBinhLuanBaiVietDTO EntityToDTO(NguoiDungThichBinhLuanBaiViet thichBinhLuanBaiViet)
        {
            return new NguoiDungThichBinhLuanBaiVietDTO
            {
                BinhLuanBaiVietId = thichBinhLuanBaiViet.BinhLuanBaiVietId,
                PhatTuId = thichBinhLuanBaiViet.PhatTuId,
                DaXoa = thichBinhLuanBaiViet.DaXoa,
                ThoiGianThich = thichBinhLuanBaiViet.ThoiGianThich
            };
        }
    }
}
