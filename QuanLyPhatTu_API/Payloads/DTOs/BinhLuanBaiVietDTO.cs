namespace QuanLyPhatTu_API.Payloads.DTOs
{
    public class BinhLuanBaiVietDTO
    {
        public int BaiVietId { get; set; }
        public int PhatTuId { get; set; }
        public string BinhLuan { get; set; }
        public int SoLuotThich { get; set; } = 0;
        public DateTime ThoiGianTao { get; set; }
        public DateTime ThoiGianCapNhat { get; set; }
        public DateTime ThoiGianXoa { get; set; }
        public bool DaXoa { get; set; } = false;
    }
}
