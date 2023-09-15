using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyPhatTu_API.Entities
{
    [Table("BinhLuanBaiViet_tbl")]
    public class BinhLuanBaiViet : BaseEntity
    {
        public int BaiVietId { get; set; }
        public int PhatTuId { get; set; }
        public string BinhLuan { get; set; }
        public int SoLuotThich { get; set; } = 0;
        public DateTime ThoiGianTao { get; set; }
        public DateTime ThoiGianCapNhat { get; set; }
        public DateTime ThoiGianXoa { get; set; }
        public bool DaXoa { get; set; } = false;
        public BaiViet BaiViet { get; set; }
        public PhatTu PhatTu { get; set; }
        public IEnumerable<NguoiDungThichBinhLuanBaiViet> NguoiDungThichBinhLuanBaiViets { get; set; }
    }
}
