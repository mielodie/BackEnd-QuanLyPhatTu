using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyPhatTu_API.Entities
{
    [Table("NguoiDungThichBinhLuanBaiViet_tbl")]
    public class NguoiDungThichBinhLuanBaiViet : BaseEntity
    {
        public int PhatTuId { get; set; }
        public int  BinhLuanBaiVietId { get; set; }
        public DateTime ThoiGianThich { get; set; }
        public bool DaXoa { get; set; } = false;
        public PhatTu PhatTu { get; set; }
        public BinhLuanBaiViet BinhLuanBaiViet { get; set; }
    }
}
