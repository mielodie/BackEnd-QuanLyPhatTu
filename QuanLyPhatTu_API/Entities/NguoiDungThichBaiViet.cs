using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyPhatTu_API.Entities
{
    [Table("NguoiDungThichBaiViet_tbl")]
    public class NguoiDungThichBaiViet : BaseEntity
    {
        public int PhatTuId { get; set; }
        public int BaiVietId { get; set; }
        public DateTime ThoiGianThich { get; set; }
        public bool DaXoa { get; set; } = false;
        public PhatTu PhatTu { get; set; }
        public BaiViet BaiViet { get; set; }
    }
}
