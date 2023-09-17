using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuanLyPhatTu_API.Payloads.Requests.BinhLuanBaiVietRequest;
using QuanLyPhatTu_API.Service.Interfaces;

namespace QuanLyPhatTu_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BinhLuanController : ControllerBase
    {
        private readonly IBinhLuanBaiVietService _binhLuanService;
        public BinhLuanController(IBinhLuanBaiVietService binhLuanService)
        {
            _binhLuanService = binhLuanService;
        }
        [HttpPost("TaoBinhLuan")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> TaoBinhLuan(Request_TaoBinhLuan request)
        {
            int id = int.Parse(HttpContext.User.FindFirst("Id").Value);
            return Ok(await _binhLuanService.TaoBinhLuan(id, request));
        }
    }
}
