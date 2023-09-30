namespace QuanLyPhatTu_API.Payloads.Requests.PhatTuRequest
{
    public class Request_DangKy
    {
        public string TaiKhoan { get; set; }
        public string HoVaTen { get; set; }
        public IFormFile AnhChup { get; set; }
        public string Email { get; set; }
        public DateTime NgaySinh { get; set; }
        public string MatKhau { get; set; }
        public string SoDienThoai { get; set; }
        public string GioiTinh { get; set; }
    }
}
