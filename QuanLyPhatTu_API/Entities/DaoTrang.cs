using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyPhatTu_API.Entities
{
    [Table("DaoTrang_tbl")]
    public class DaoTrang : BaseEntity
    {
        public bool? DaKetThuc { get; set; } = false;
        public string NoiDung { get; set; }
        public string NoiToChuc { get; set; }
        public int SoThanhVienThamGia { get; set; } = 0;
        public DateTime ThoiGianBatDau { get ; set; }
        public int NguoiTruTri { get; set; }
        public DateTime ThoiGianKetThuc { get; set; }
        public IEnumerable<DonDangKy> DonDangKies { get; set; }
        public IEnumerable<PhatTuDaoTrang> PhatTuDaoTrangs { get; set; }
    }
}
