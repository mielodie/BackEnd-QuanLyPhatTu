using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyPhatTu_API.Entities
{
    [Table("RefreshToken_tbl")]
    public class RefreshToken : BaseEntity
    {
        public string Token { get; set; }
        public DateTime ThoiGianHetHan { get; set; }
        public int PhatTuId { get; set; }
        public PhatTu PhatTu { get; set; }
    }
}
