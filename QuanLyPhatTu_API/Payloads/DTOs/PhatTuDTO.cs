namespace QuanLyPhatTu_API.Payloads.DTOs
{
    public class PhatTuDTO
    {
        public string HoVaTen { get; set; }
        public string TaiKhoan { get; set; }
        public string AnhChup { get; set; }
        public bool? DaHoanTuc { get; set; }
        public string Email { get; set; }
        public string? GioiTinh { get; set; }
        public DateTime? NgayCapNhat { get; set; }
        public DateTime? NgayXuatGia { get; set; }
        public DateTime NgaySinh { get; set; }
        public string? PhapDanh { get; set; }
        public string? SoDienThoai { get; set; }
        
    }
}
