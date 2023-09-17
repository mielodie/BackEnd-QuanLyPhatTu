namespace QuanLyPhatTu_API.Payloads.Requests.BinhLuanBaiVietRequest
{
    public class Request_SuaBinhLuan
    {
        public int BaiVietId { get; set; }
        public string BinhLuan { get; set; }
        public DateTime ThoiGianCapNhat { get; set; }
    }
}
