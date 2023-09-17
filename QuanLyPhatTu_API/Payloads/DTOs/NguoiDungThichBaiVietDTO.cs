namespace QuanLyPhatTu_API.Payloads.DTOs
{
    public class NguoiDungThichBaiVietDTO
    {
        public int PhatTuId { get; set; }
        public int BaiVietId { get; set; }
        public DateTime ThoiGianThich { get; set; }
        public bool DaXoa { get; set; }
    }
}
