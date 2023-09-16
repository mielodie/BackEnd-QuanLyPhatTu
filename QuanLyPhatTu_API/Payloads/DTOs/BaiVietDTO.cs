namespace QuanLyPhatTu_API.Payloads.DTOs
{
    public class BaiVietDTO
    {
        public int LoaiBaiVietId { get; set; }
        public string TieuDe { get; set; }
        public string MoTa { get; set; }
        public string NoiDung { get; set; }
        public int SoLuotThich { get; set; }
        public int SoLuotBinhLuan { get; set; }
        public DateTime ThoiGianDang { get; set; }
        public DateTime ThoiGianCapNhat { get; set; }
        public DateTime ThoiGianXoa { get; set; }
    }
}
