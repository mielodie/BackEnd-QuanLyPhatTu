using QuanLyPhatTu_API.Entities;
using QuanLyPhatTu_API.Payloads.DTOs;
using QuanLyPhatTu_API.Payloads.Requests.BinhLuanBaiVietRequest;

namespace QuanLyPhatTu_API.Payloads.Converters
{
    public class BinhLuanBaiVietConverter
    {
        public BinhLuanBaiVietDTO EntityToDTO(BinhLuanBaiViet binhLuan)
        {
            return new BinhLuanBaiVietDTO
            {
                BaiVietId = binhLuan.BaiVietId,
                BinhLuan = binhLuan.BinhLuan,
                PhatTuId = binhLuan.PhatTuId,
                DaXoa = binhLuan.DaXoa,
                SoLuotThich = binhLuan.SoLuotThich,
                ThoiGianCapNhat = binhLuan.ThoiGianCapNhat,
                ThoiGianTao = binhLuan.ThoiGianTao,
                ThoiGianXoa = binhLuan.ThoiGianXoa
            };
        }
        public BinhLuanBaiViet TaoBinhLuan(Request_TaoBinhLuan request)
        {
            return new BinhLuanBaiViet
            {
                BinhLuan = request.BinhLuan,
                BaiVietId = request.BaiVietId
            };
        }
        public BinhLuanBaiViet SuaBinhLuan(BinhLuanBaiViet binhLuan, Request_SuaBinhLuan request)
        {
            binhLuan.BaiVietId = request.BaiVietId;
            binhLuan.BinhLuan = request.BinhLuan;
            return binhLuan;
        }
    }
}
