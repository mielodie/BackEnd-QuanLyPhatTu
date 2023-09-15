using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuanLyPhatTu_API.Payloads.DTOs;
using QuanLyPhatTu_API.Payloads.Requests.ChuaRequest;
using QuanLyPhatTu_API.Payloads.Responses;
using QuanLyPhatTu_API.Service.Interfaces;

namespace QuanLyPhatTu_API.Controllers
{
    [ApiController]
    public class ChuaController : ControllerBase
    {
        private readonly IChuaService _chuaService;
        public ChuaController(IChuaService chuaService)
        {
            _chuaService = chuaService;
        }
        [HttpGet("/api/chua/LayTatCaChua")]
        [Authorize(Roles = "Admin, Mod")]
        public async Task<IActionResult> LayTatCaChua()
        {
            return Ok(await _chuaService.LayTatCaChua());
        }
        [HttpPut("/api/chua/SuaThongTinChua")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> SuaThongTinChua(int chuaId, Request_SuaThongTinChua request)
        {
            return Ok(await _chuaService.SuaThongTinChua(chuaId, request));
        }
        [HttpPost("/api/chua/ThemChua")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ThemChua(Request_ThemChua request)
        {
            return Ok(await _chuaService.ThemChua(request));
        }
        [HttpDelete("/api/chua/XoaChua")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> XoaChua(int chuaId)
        {
            return Ok(await _chuaService.XoaChua(chuaId));
        }
    }
}
