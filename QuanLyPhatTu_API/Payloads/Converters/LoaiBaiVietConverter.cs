using QuanLyPhatTu_API.Entities;
using QuanLyPhatTu_API.Payloads.DTOs;

namespace QuanLyPhatTu_API.Payloads.Converters
{
    public class LoaiBaiVietConverter
    {
        public LoaiBaiVietDTO EntityToDTO(LoaiBaiViet loaiBaiViet)
        {
            return new LoaiBaiVietDTO
            {
                TenLoaiBaiViet = loaiBaiViet.TenLoai
            };
        }
    }
}
