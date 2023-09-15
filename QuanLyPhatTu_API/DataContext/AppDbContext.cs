using Microsoft.EntityFrameworkCore;
using QuanLyPhatTu_API.Entities;

namespace QuanLyPhatTu_API.DataContext
{
    public class AppDbContext : DbContext
    {
        public DbSet<Chua> chuas { get; set; }
        public DbSet<DaoTrang> daoTrangs { get; set; }
        public DbSet<DonDangKy> donDangKies { get; set; }
        public DbSet<PhatTu> phatTus { get; set; }
        public DbSet<PhatTuDaoTrang> phatTuDaoTrangs { get; set; }
        public DbSet<QuyenHan> quyenHans { get; set; }
        public DbSet<RefreshToken> refreshTokens { get; set; }
        public DbSet<TrangThaiDon> trangThaiDons { get; set; }
        public DbSet<XacNhanEmail> xacNhanEmails { get; set; }
        public DbSet<BaiViet> baiViets { get; set; }
        public DbSet<LoaiBaiViet> loaiBaiViets { get; set; }
        public DbSet<BinhLuanBaiViet> binhLuanBaiViets { get; set; }
        public DbSet<NguoiDungThichBinhLuanBaiViet> nguoiDungThichBinhLuanBaiViets { get; set; }
        public DbSet<NguoiDungThichBaiViet> nguoiDungThichBaiViets { get; set; }
        public DbSet<TrangThaiBaiViet> trangThaiBaiViets { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(Settings.MyConnectString());
        }
    }
}
