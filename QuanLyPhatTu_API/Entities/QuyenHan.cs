using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyPhatTu_API.Entities
{
    [Table("QuyenHan_tbl")]
    public class QuyenHan : BaseEntity
    {
        public string TenQuyenHan { get; set; }
        public IEnumerable<PhatTu> PhatTus { get; set; }
    }
}
