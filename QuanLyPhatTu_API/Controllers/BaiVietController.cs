using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuanLyPhatTu_API.Payloads.DTOs;
using QuanLyPhatTu_API.Payloads.Requests.BaiVietRequest;
using QuanLyPhatTu_API.Payloads.Responses;
using QuanLyPhatTu_API.Service.Implements;
using QuanLyPhatTu_API.Service.Interfaces;

namespace QuanLyPhatTu_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaiVietController : ControllerBase
    {
        private readonly IBaiVietService _iBaiVietService;
        public BaiVietController()
        {
            _iBaiVietService = new BaiVietService();
        }
        [HttpPost("TaoBaiViet")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> TaoBaiViet(Request_TaoBaiViet request)
        {
            int id = int.Parse(HttpContext.User.FindFirst("Id").Value);
            return Ok(await _iBaiVietService.TaoBaiViet(id, request));
        }

        [HttpPut("DuyetBaiViet/{baiVietId}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Authorize(Roles = "Admin, Mod")]
        public async Task<IActionResult> DuyetBaiViet([FromRoute] int baiVietId)
        {
            int id = int.Parse(HttpContext.User.FindFirst("Id").Value);
            return Ok(await _iBaiVietService.DuyetBaiViet(id, baiVietId));
        }

        [HttpPut("SuaBaiViet")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> SuaBaiViet(Request_SuaBaiViet request)
        {
            return Ok(await _iBaiVietService.SuaBaiViet(request));
        }

        [HttpPut("XoaBaiViet")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Authorize(Roles = "Admin, Mod, Member")]
        public async Task<IActionResult> XoaBaiViet(int baiVietId)
        {
            return Ok(await _iBaiVietService.XoaBaiViet(baiVietId));
        }

        [HttpPut("LayTatCaBaiViet")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> LayTatCaBaiViet(int pageSize, int pageNumber)
        {
            return Ok(await _iBaiVietService.LayTatCaBaiViet(pageSize, pageNumber));
        }

        [HttpPut("LayBaiVietTheoNguoiDung")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> LayBaiVietTheoNguoiDung(int nguoiDungId, int pageSize, int pageNumber)
        {
            return Ok(await _iBaiVietService.LayBaiVietTheoNguoiDung(nguoiDungId, pageSize, pageNumber));
        }

        [HttpPut("LayBaiVietTheoLoaiBaiViet")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> LayBaiVietTheoLoaiBaiViet(string tenLoai, int pageSize, int pageNumber)
        {
            return Ok(await _iBaiVietService.LayBaiVietTheoLoaiBaiViet(tenLoai, pageSize, pageNumber));
        }
    }
}
