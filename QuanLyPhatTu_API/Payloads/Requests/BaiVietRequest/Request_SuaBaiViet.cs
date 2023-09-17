namespace QuanLyPhatTu_API.Payloads.Requests.BaiVietRequest
{
    public class Request_SuaBaiViet
    {
        public int BaiVietId { get; set; }
        public int LoaiBaiBietId { get; set; }
        public string TieuDe { get; set; }
        public string MoTa { get; set; }
        public string NoiDung { get; set; }
    }
}
