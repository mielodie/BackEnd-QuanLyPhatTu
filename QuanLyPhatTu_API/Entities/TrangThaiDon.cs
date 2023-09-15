using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyPhatTu_API.Entities
{
    [Table("TrangThaiDon_tbl")]
    public class TrangThaiDon : BaseEntity
    {
        public string TenTrangThai { get; set; }
        public IEnumerable<DonDangKy> DonDangKies { get; set; }
    }
}
