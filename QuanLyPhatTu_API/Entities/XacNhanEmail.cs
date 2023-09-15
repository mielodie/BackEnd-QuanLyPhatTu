using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyPhatTu_API.Entities
{
    [Table("XacNhanEmail_tbl")]
    public class XacNhanEmail : BaseEntity
    {
        public int PhatTuId { get; set; }
        public PhatTu PhatTu { get; set; }
        public DateTime ThoiGianHetHan { get; set; }
        public string MaXacNhan { get; set; }
        public bool DaXacNhan { get; set; } = false;
    }
}
