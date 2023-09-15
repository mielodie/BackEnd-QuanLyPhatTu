using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyPhatTu_API.Entities
{
    [Table("Chua_tbl")]
    public class Chua : BaseEntity
    {
        public string TenChua { get; set; }
        public string DiaChi { get; set; }
        public DateTime NgayThanhLap { get; set; }
        public string NguoiTruTri { get; set; }
        public DateTime NgayCapNhat { get; set; } = DateTime.Now;
        public IEnumerable<PhatTu> PhatTus { get; set; }
        public bool? IsActive { get; set; } = true;
    }
}
