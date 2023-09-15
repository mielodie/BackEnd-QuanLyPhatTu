using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyPhatTu_API.Entities
{
    [Table("DonDangKy_tbl")]
    public class DonDangKy : BaseEntity
    {
        public DateTime NgayGuiDon { get; set; }
        public DateTime? NgayXuLy { get; set; }
        public int? NguoiXuLy { get; set; }
        public int TrangThaiDonId { get; set; } = 1;
        public int DaoTrangId { get; set; }
        public int PhatTuId { get; set; }
        public PhatTu PhatTu { get; set; }
        public DaoTrang DaoTrang { get; set; }
        public TrangThaiDon TrangThaiDon { get; set; }
    }       
}
