using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace QuanLyPhatTu_API.Entities
{
    [Table("BaiViet_tbl")]
    public class BaiViet : BaseEntity
    {
        public int LoaiBaiVietId { get; set; }
        public string TieuDe { get; set; }
        public string MoTa { get; set; }
        public string NoiDung { get; set; }
        public int PhatTuId { get; set; } // đây là thằng đăng bài
        public int NguoiDuyetBaiId { get; set; }
        public int SoLuotThich { get; set; } = 0;
        public int SoLuotBinhLuan { get; set; } = 0;
        public DateTime ThoiGianDang { get; set; }
        [MaybeNull]
        public DateTime? ThoiGianCapNhat { get; set; }
        [MaybeNull]
        public DateTime? ThoiGianXoa { get; set; }
        public bool DaXoa { get; set; } = false;
        public int TrangThaiBaiVietId { get; set; }
        public LoaiBaiViet LoaiBaiViet { get; set; }
        public PhatTu PhatTu { get; set; }
        public TrangThaiBaiViet TrangThaiBaiViet { get; set; }
        public IEnumerable<NguoiDungThichBaiViet> NguoiDungThichBaiViets { get; set; }
        public IEnumerable<BinhLuanBaiViet> BinhLuanBaiViets { get; set; }

    }
}
