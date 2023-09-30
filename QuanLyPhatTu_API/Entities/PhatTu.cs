using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyPhatTu_API.Entities
{
    [Table("PhatTu_tbl")]
    public class PhatTu : BaseEntity
    {
        public string TaiKhoan { get; set; }
        public string HoVaTen { get; set; }
        public string AnhChup { get; set; }
        public bool? DaHoanTuc { get; set; } = false;
        public string Email { get; set; }
        public string? GioiTinh { get; set; }
        public DateTime? NgayCapNhat { get; set; } = DateTime.Now;
        public DateTime? NgayXuatGia { get; set; }
        public DateTime NgaySinh { get; set; }
        public bool? LaThanhVienThamGiaDaoTrang { get; set; } = false;
        public string MatKhau { get; set; }
        public string? PhapDanh { get; set; }
        public string SoDienThoai { get; set; }
        public int? ChuaId { get; set; }
        public int QuyenHanId { get; set; }
        public Chua? Chua { get; set; }
        public QuyenHan? QuyenHan { get; set; }
        public bool IsActive { get; set; } = false;
        public IEnumerable<PhatTuDaoTrang> PhatTuDaoTrangs { get; set; }
        public IEnumerable<RefreshToken> RefreshTokens { get; set; }
        public IEnumerable<DonDangKy> DonDangKies { get; set; } 
    }
}
