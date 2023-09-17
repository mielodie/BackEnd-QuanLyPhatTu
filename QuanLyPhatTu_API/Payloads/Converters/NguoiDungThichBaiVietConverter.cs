using QuanLyPhatTu_API.Entities;
using QuanLyPhatTu_API.Payloads.DTOs;

namespace QuanLyPhatTu_API.Payloads.Converters
{
    public class NguoiDungThichBaiVietConverter
    {
        public NguoiDungThichBaiVietDTO EntityToDTO(NguoiDungThichBaiViet nguoiDungThichBaiViet)
        {
            return new NguoiDungThichBaiVietDTO
            {
                BaiVietId = nguoiDungThichBaiViet.BaiVietId,
                PhatTuId = nguoiDungThichBaiViet.PhatTuId,
                ThoiGianThich = nguoiDungThichBaiViet.ThoiGianThich,
                DaXoa = nguoiDungThichBaiViet.DaXoa
            };
        }
    }
}
