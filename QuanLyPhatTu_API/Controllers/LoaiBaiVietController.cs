using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuanLyPhatTu_API.Payloads.Requests.LoaiBaiVietRequest;
using QuanLyPhatTu_API.Service.Interfaces;

namespace QuanLyPhatTu_API.Controllers
{
    [Route("/api/loaibaiviet")]   
    [ApiController]
    public class LoaiBaiVietController : ControllerBase
    {
        private readonly ILoaiBaiVietService _iLoaiBaiVietService;
        public LoaiBaiVietController(ILoaiBaiVietService iLoaiBaiVietService)
        {
            _iLoaiBaiVietService = iLoaiBaiVietService;
        }
        [HttpPost("ThemLoaiBaiViet")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ThemLoaiBaiViet(Request_TaoLoaiBaiViet request)
        {
            return Ok(await _iLoaiBaiVietService.ThemLoaiBaiViet(request));
        }
        [HttpPut("SuaLoaiBaiViet")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> SuaLoaiBaiViet(int loaiBaiVietID, Request_SuaLoaiBaiViet request)
        {
            return Ok(await _iLoaiBaiVietService.SuaLoaiBaiViet(loaiBaiVietID, request));
        }
        [HttpDelete("XoaLoaiBaiViet")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> XoaLoaiBaiViet(int loaiBaiVietID)
        {
            return Ok(await _iLoaiBaiVietService.XoaLoaiBaiViet(loaiBaiVietID));
        }
        [HttpGet("LayLoaiBaiViet")]
        [Authorize(Roles = "Admin, Mod")]
        public async Task<IActionResult> LayLoaiBaiViet(int pageSize, int pageNumber)
        {
            return Ok(await _iLoaiBaiVietService.LayLoaiBaiViet(pageSize, pageNumber));
        }
        [HttpGet("LayLoaiBaiVietTheoTen")]
        [Authorize(Roles = "Admin, Mod")]
        public async Task<IActionResult> LayLoaiBaiVietTheoTen(string tenLoai, int pageSize, int pageNumber)
        {
            return Ok(await _iLoaiBaiVietService.LayLoaiBaiVietTheoTen(tenLoai, pageSize, pageNumber));
        }
    }
}
