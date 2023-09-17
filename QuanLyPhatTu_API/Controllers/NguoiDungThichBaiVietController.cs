using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuanLyPhatTu_API.Entities;
using QuanLyPhatTu_API.Payloads.Requests.BaiVietRequest;
using QuanLyPhatTu_API.Service.Interfaces;

namespace QuanLyPhatTu_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NguoiDungThichBaiVietController : ControllerBase
    {
        private readonly INguoiDungThichBaiVietService _iNguoiDungThichBaiVietService;
        public NguoiDungThichBaiVietController(INguoiDungThichBaiVietService iNguoiDungThichBaiVietService)
        {
            _iNguoiDungThichBaiVietService = iNguoiDungThichBaiVietService;
        }
        [HttpPut("Dislike")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Dislike(int thichBaiVietId, int baiVietId)
        {
            int id = int.Parse(HttpContext.User.FindFirst("Id").Value);
            return Ok(await _iNguoiDungThichBaiVietService.Dislike(thichBaiVietId, baiVietId, id));
        }
        [HttpPost("Like")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Like(Request_NguoiDungThichBaiViet nguoiDung)
        {
            int id = int.Parse(HttpContext.User.FindFirst("Id").Value);
            return Ok(await _iNguoiDungThichBaiVietService.Like(nguoiDung, id));
        }
    }
}
