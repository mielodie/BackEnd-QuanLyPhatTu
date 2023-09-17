using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuanLyPhatTu_API.Payloads.Requests.BaiVietRequest;
using QuanLyPhatTu_API.Service.Implements;
using QuanLyPhatTu_API.Service.Interfaces;

namespace QuanLyPhatTu_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NguoiDungThichBinhLuanBaiVietController : ControllerBase
    {
        private readonly INguoiDungThichBinhLuanBaiVietService _iNguoiDungThichBinhLuanBaiVietService;
        public NguoiDungThichBinhLuanBaiVietController(INguoiDungThichBinhLuanBaiVietService iNguoiDungThichBinhLuanBaiVietService)
        {
            _iNguoiDungThichBinhLuanBaiVietService = iNguoiDungThichBinhLuanBaiVietService;
        }
        [HttpPost("LikeComment")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Like(int binhLuanBaiVietId)
        {
            int id = int.Parse(HttpContext.User.FindFirst("Id").Value);
            return Ok(await _iNguoiDungThichBinhLuanBaiVietService.Like(binhLuanBaiVietId, id));
        }
        [HttpPut("DislikeComment")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Dislike(int thichBinhLuanBaiVietId, int binhLuanBaiVietId)
        {
            int id = int.Parse(HttpContext.User.FindFirst("Id").Value);
            return Ok(await _iNguoiDungThichBinhLuanBaiVietService.Dislike(thichBinhLuanBaiVietId, binhLuanBaiVietId, id));
        }
    }
}
