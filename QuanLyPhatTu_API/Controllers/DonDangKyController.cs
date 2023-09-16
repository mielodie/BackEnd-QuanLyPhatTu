using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuanLyPhatTu_API.Payloads.DTOs;
using QuanLyPhatTu_API.Payloads.Requests.DonDangKyRequest;
using QuanLyPhatTu_API.Payloads.Responses;
using QuanLyPhatTu_API.Service.Implements;
using QuanLyPhatTu_API.Service.Interfaces;

namespace QuanLyPhatTu_API.Controllers
{
    [ApiController]
    public class DonDangKyController : ControllerBase
    {
        private readonly IDonDangKyService _donDangKyService;
        public DonDangKyController(IDonDangKyService donDangKyService)
        {
            _donDangKyService = donDangKyService;
        }
        [HttpPost]
        [Route("/api/dondangky/TaoDonDangKy")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> TaoDonDangKy(Request_TaoDonDangKy request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (!int.TryParse(HttpContext.User.FindFirst("Id")?.Value, out int id))
                {
                    return BadRequest("Id người dùng không hợp lệ");
                }

                var result = await _donDangKyService.TaoDonDangKy(id, request);

                if (result.Message.ToLower().Contains("Gửi đơn đăng ký thành công, vui lòng chờ duyệt".ToLower()))
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest(result);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpPut]
        [Route("/api/dondangky/SuaDonDangKy")]
        public async Task<IActionResult> SuaDonDangKy(int donDangKyId, Request_SuaDonDangKy request)
        {
            return Ok(await _donDangKyService.SuaDonDangKy(donDangKyId, request));
        }
        [HttpPut]
        [Route("/api/dondangky/DuyetDonDangKy")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DuyetDonDangKy(Request_DuyetDonDangKy request)
        {
            int id = int.Parse(HttpContext.User.FindFirst("Id")?.Value);
            return Ok(await _donDangKyService.DuyetDonDangKy(id,request));
        }
    }
}
