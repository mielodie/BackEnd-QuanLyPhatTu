namespace QuanLyPhatTu_API.Payloads.Requests.BinhLuanBaiVietRequest
{
    public class Request_TaoBinhLuan
    {
        public int BaiVietId { get; set; }
        
        public string BinhLuan { get; set; }
        public DateTime ThoiGianTao { get; set; }
    }
}
