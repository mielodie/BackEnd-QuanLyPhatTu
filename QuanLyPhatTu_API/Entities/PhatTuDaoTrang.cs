using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyPhatTu_API.Entities
{
    [Table("PhatTuDaoTrang_tbl")]
    public class PhatTuDaoTrang : BaseEntity
    {
        public bool DaThamGia { get; set; } = false;
        public string LyDoKhongThamGia { get; set; }
        public int DaoTrangId { get; set; }
        public int PhatTuId { get; set; }
        public DaoTrang DaoTrang { get; set; }
        public PhatTu PhatTu { get; set; }
    }
}
