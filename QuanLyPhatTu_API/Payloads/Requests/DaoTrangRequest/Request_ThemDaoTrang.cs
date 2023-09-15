namespace QuanLyPhatTu_API.Payloads.Requests.DaoTrangRequest
{
    public class Request_ThemDaoTrang
    {
        public string NoiDung { get; set; }
        public string NoiToChuc { get; set; }
        public DateTime ThoiGianBatDau { get; set; }
        public int NguoiTruTri { get; set; }
        public DateTime ThoiGianKetThuc { get; set; }
    }
}
