using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyPhatTu_API.Entities
{
    [Table("TrangThaiBaiViet_tbl")]
    public class TrangThaiBaiViet : BaseEntity
    {
        public string TenTrangThai { get; set; }
    }
}
