namespace QuanLyPhatTu_API.Payloads.DTOs
{
    public class DaoTrangDTO
    {
        public bool? DaKetThuc { get; set; }
        public string NoiDung { get; set; }
        public string NoiToChuc { get; set; }
        public int SoThanhVienThamGia { get; set; }
        public DateTime ThoiGianBatDau { get; set; }
        public int NguoiTruTri { get; set; }
        public DateTime ThoiGianKetThuc { get; set; }
    }
}
