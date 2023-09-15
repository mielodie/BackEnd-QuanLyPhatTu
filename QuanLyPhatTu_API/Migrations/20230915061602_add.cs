using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuanLyPhatTu_API.Migrations
{
    /// <inheritdoc />
    public partial class add : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Chua_tbl",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenChua = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DiaChi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NgayThanhLap = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NguoiTruTri = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NgayCapNhat = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chua_tbl", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DaoTrang_tbl",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DaKetThuc = table.Column<bool>(type: "bit", nullable: true),
                    NoiDung = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NoiToChuc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SoThanhVienThamGia = table.Column<int>(type: "int", nullable: false),
                    ThoiGianBatDau = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NguoiTruTri = table.Column<int>(type: "int", nullable: false),
                    ThoiGianKetThuc = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DaoTrang_tbl", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LoaiBaiViet_tbl",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenLoai = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoaiBaiViet_tbl", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "QuyenHan_tbl",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenQuyenHan = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuyenHan_tbl", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TrangThaiBaiViet_tbl",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenTrangThai = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrangThaiBaiViet_tbl", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TrangThaiDon_tbl",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenTrangThai = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrangThaiDon_tbl", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PhatTu_tbl",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TaiKhoan = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HoVaTen = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AnhChup = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DaHoanTuc = table.Column<bool>(type: "bit", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GioiTinh = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NgayCapNhat = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NgayXuatGia = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NgaySinh = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LaThanhVienThamGiaDaoTrang = table.Column<bool>(type: "bit", nullable: true),
                    MatKhau = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhapDanh = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SoDienThoai = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ChuaId = table.Column<int>(type: "int", nullable: false),
                    QuyenHanId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhatTu_tbl", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PhatTu_tbl_Chua_tbl_ChuaId",
                        column: x => x.ChuaId,
                        principalTable: "Chua_tbl",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PhatTu_tbl_QuyenHan_tbl_QuyenHanId",
                        column: x => x.QuyenHanId,
                        principalTable: "QuyenHan_tbl",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BaiViet_tbl",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LoaiBaiVietId = table.Column<int>(type: "int", nullable: false),
                    TieuDe = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MoTa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NoiDung = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhatTuId = table.Column<int>(type: "int", nullable: false),
                    NguoiDuyetBaiId = table.Column<int>(type: "int", nullable: false),
                    SoLuotThich = table.Column<int>(type: "int", nullable: false),
                    SoLuotBinhLuan = table.Column<int>(type: "int", nullable: false),
                    ThoiGianDang = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ThoiGianCapNhat = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ThoiGianXoa = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DaXoa = table.Column<bool>(type: "bit", nullable: false),
                    TrangThaiBaiVietId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BaiViet_tbl", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BaiViet_tbl_LoaiBaiViet_tbl_LoaiBaiVietId",
                        column: x => x.LoaiBaiVietId,
                        principalTable: "LoaiBaiViet_tbl",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BaiViet_tbl_PhatTu_tbl_PhatTuId",
                        column: x => x.PhatTuId,
                        principalTable: "PhatTu_tbl",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BaiViet_tbl_TrangThaiBaiViet_tbl_TrangThaiBaiVietId",
                        column: x => x.TrangThaiBaiVietId,
                        principalTable: "TrangThaiBaiViet_tbl",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DonDangKy_tbl",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NgayGuiDon = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NgayXuLy = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NguoiXuLy = table.Column<int>(type: "int", nullable: true),
                    TrangThaiDonId = table.Column<int>(type: "int", nullable: false),
                    DaoTrangId = table.Column<int>(type: "int", nullable: false),
                    PhatTuId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DonDangKy_tbl", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DonDangKy_tbl_DaoTrang_tbl_DaoTrangId",
                        column: x => x.DaoTrangId,
                        principalTable: "DaoTrang_tbl",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DonDangKy_tbl_PhatTu_tbl_PhatTuId",
                        column: x => x.PhatTuId,
                        principalTable: "PhatTu_tbl",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DonDangKy_tbl_TrangThaiDon_tbl_TrangThaiDonId",
                        column: x => x.TrangThaiDonId,
                        principalTable: "TrangThaiDon_tbl",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PhatTuDaoTrang_tbl",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DaThamGia = table.Column<bool>(type: "bit", nullable: false),
                    LyDoKhongThamGia = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DaoTrangId = table.Column<int>(type: "int", nullable: false),
                    PhatTuId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhatTuDaoTrang_tbl", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PhatTuDaoTrang_tbl_DaoTrang_tbl_DaoTrangId",
                        column: x => x.DaoTrangId,
                        principalTable: "DaoTrang_tbl",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PhatTuDaoTrang_tbl_PhatTu_tbl_PhatTuId",
                        column: x => x.PhatTuId,
                        principalTable: "PhatTu_tbl",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RefreshToken_tbl",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ThoiGianHetHan = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PhatTuId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshToken_tbl", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RefreshToken_tbl_PhatTu_tbl_PhatTuId",
                        column: x => x.PhatTuId,
                        principalTable: "PhatTu_tbl",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "XacNhanEmail_tbl",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PhatTuId = table.Column<int>(type: "int", nullable: false),
                    ThoiGianHetHan = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MaXacNhan = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DaXacNhan = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_XacNhanEmail_tbl", x => x.Id);
                    table.ForeignKey(
                        name: "FK_XacNhanEmail_tbl_PhatTu_tbl_PhatTuId",
                        column: x => x.PhatTuId,
                        principalTable: "PhatTu_tbl",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BinhLuanBaiViet_tbl",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BaiVietId = table.Column<int>(type: "int", nullable: false),
                    PhatTuId = table.Column<int>(type: "int", nullable: false),
                    BinhLuan = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SoLuotThich = table.Column<int>(type: "int", nullable: false),
                    ThoiGianTao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ThoiGianCapNhat = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ThoiGianXoa = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DaXoa = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BinhLuanBaiViet_tbl", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BinhLuanBaiViet_tbl_BaiViet_tbl_BaiVietId",
                        column: x => x.BaiVietId,
                        principalTable: "BaiViet_tbl",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BinhLuanBaiViet_tbl_PhatTu_tbl_PhatTuId",
                        column: x => x.PhatTuId,
                        principalTable: "PhatTu_tbl",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "NguoiDungThichBaiViet_tbl",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PhatTuId = table.Column<int>(type: "int", nullable: false),
                    BaiVietId = table.Column<int>(type: "int", nullable: false),
                    ThoiGianThich = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DaXoa = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NguoiDungThichBaiViet_tbl", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NguoiDungThichBaiViet_tbl_BaiViet_tbl_BaiVietId",
                        column: x => x.BaiVietId,
                        principalTable: "BaiViet_tbl",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_NguoiDungThichBaiViet_tbl_PhatTu_tbl_PhatTuId",
                        column: x => x.PhatTuId,
                        principalTable: "PhatTu_tbl",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "NguoiDungThichBinhLuanBaiViet_tbl",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PhatTuId = table.Column<int>(type: "int", nullable: false),
                    BinhLuanBaiVietId = table.Column<int>(type: "int", nullable: false),
                    ThoiGianThich = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DaXoa = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NguoiDungThichBinhLuanBaiViet_tbl", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NguoiDungThichBinhLuanBaiViet_tbl_BinhLuanBaiViet_tbl_BinhLuanBaiVietId",
                        column: x => x.BinhLuanBaiVietId,
                        principalTable: "BinhLuanBaiViet_tbl",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_NguoiDungThichBinhLuanBaiViet_tbl_PhatTu_tbl_PhatTuId",
                        column: x => x.PhatTuId,
                        principalTable: "PhatTu_tbl",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BaiViet_tbl_LoaiBaiVietId",
                table: "BaiViet_tbl",
                column: "LoaiBaiVietId");

            migrationBuilder.CreateIndex(
                name: "IX_BaiViet_tbl_PhatTuId",
                table: "BaiViet_tbl",
                column: "PhatTuId");

            migrationBuilder.CreateIndex(
                name: "IX_BaiViet_tbl_TrangThaiBaiVietId",
                table: "BaiViet_tbl",
                column: "TrangThaiBaiVietId");

            migrationBuilder.CreateIndex(
                name: "IX_BinhLuanBaiViet_tbl_BaiVietId",
                table: "BinhLuanBaiViet_tbl",
                column: "BaiVietId");

            migrationBuilder.CreateIndex(
                name: "IX_BinhLuanBaiViet_tbl_PhatTuId",
                table: "BinhLuanBaiViet_tbl",
                column: "PhatTuId");

            migrationBuilder.CreateIndex(
                name: "IX_DonDangKy_tbl_DaoTrangId",
                table: "DonDangKy_tbl",
                column: "DaoTrangId");

            migrationBuilder.CreateIndex(
                name: "IX_DonDangKy_tbl_PhatTuId",
                table: "DonDangKy_tbl",
                column: "PhatTuId");

            migrationBuilder.CreateIndex(
                name: "IX_DonDangKy_tbl_TrangThaiDonId",
                table: "DonDangKy_tbl",
                column: "TrangThaiDonId");

            migrationBuilder.CreateIndex(
                name: "IX_NguoiDungThichBaiViet_tbl_BaiVietId",
                table: "NguoiDungThichBaiViet_tbl",
                column: "BaiVietId");

            migrationBuilder.CreateIndex(
                name: "IX_NguoiDungThichBaiViet_tbl_PhatTuId",
                table: "NguoiDungThichBaiViet_tbl",
                column: "PhatTuId");

            migrationBuilder.CreateIndex(
                name: "IX_NguoiDungThichBinhLuanBaiViet_tbl_BinhLuanBaiVietId",
                table: "NguoiDungThichBinhLuanBaiViet_tbl",
                column: "BinhLuanBaiVietId");

            migrationBuilder.CreateIndex(
                name: "IX_NguoiDungThichBinhLuanBaiViet_tbl_PhatTuId",
                table: "NguoiDungThichBinhLuanBaiViet_tbl",
                column: "PhatTuId");

            migrationBuilder.CreateIndex(
                name: "IX_PhatTu_tbl_ChuaId",
                table: "PhatTu_tbl",
                column: "ChuaId");

            migrationBuilder.CreateIndex(
                name: "IX_PhatTu_tbl_QuyenHanId",
                table: "PhatTu_tbl",
                column: "QuyenHanId");

            migrationBuilder.CreateIndex(
                name: "IX_PhatTuDaoTrang_tbl_DaoTrangId",
                table: "PhatTuDaoTrang_tbl",
                column: "DaoTrangId");

            migrationBuilder.CreateIndex(
                name: "IX_PhatTuDaoTrang_tbl_PhatTuId",
                table: "PhatTuDaoTrang_tbl",
                column: "PhatTuId");

            migrationBuilder.CreateIndex(
                name: "IX_RefreshToken_tbl_PhatTuId",
                table: "RefreshToken_tbl",
                column: "PhatTuId");

            migrationBuilder.CreateIndex(
                name: "IX_XacNhanEmail_tbl_PhatTuId",
                table: "XacNhanEmail_tbl",
                column: "PhatTuId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DonDangKy_tbl");

            migrationBuilder.DropTable(
                name: "NguoiDungThichBaiViet_tbl");

            migrationBuilder.DropTable(
                name: "NguoiDungThichBinhLuanBaiViet_tbl");

            migrationBuilder.DropTable(
                name: "PhatTuDaoTrang_tbl");

            migrationBuilder.DropTable(
                name: "RefreshToken_tbl");

            migrationBuilder.DropTable(
                name: "XacNhanEmail_tbl");

            migrationBuilder.DropTable(
                name: "TrangThaiDon_tbl");

            migrationBuilder.DropTable(
                name: "BinhLuanBaiViet_tbl");

            migrationBuilder.DropTable(
                name: "DaoTrang_tbl");

            migrationBuilder.DropTable(
                name: "BaiViet_tbl");

            migrationBuilder.DropTable(
                name: "LoaiBaiViet_tbl");

            migrationBuilder.DropTable(
                name: "PhatTu_tbl");

            migrationBuilder.DropTable(
                name: "TrangThaiBaiViet_tbl");

            migrationBuilder.DropTable(
                name: "Chua_tbl");

            migrationBuilder.DropTable(
                name: "QuyenHan_tbl");
        }
    }
}
