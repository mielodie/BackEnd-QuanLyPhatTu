using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyPhatTu_API.Entities
{
    [Table("LoaiBaiViet_tbl")]
    public class LoaiBaiViet : BaseEntity
    {
        public string TenLoai { get; set; }
        public IEnumerable<BaiViet> BaiViets { get; set; }
    }
}
