using Microsoft.EntityFrameworkCore;
using QuanLyPhatTu_API.Entities;
using QuanLyPhatTu_API.Handle.Email;
using QuanLyPhatTu_API.Payloads.Converters;
using QuanLyPhatTu_API.Payloads.DTOs;
using QuanLyPhatTu_API.Payloads.Requests.PhatTuRequest;
using QuanLyPhatTu_API.Payloads.Responses;
using QuanLyPhatTu_API.Service.Interfaces;

namespace QuanLyPhatTu_API.Service.Implements
{
    public class PhatTuService : BaseService, IPhatTuService
    {
        private readonly PhatTuConverter _phatTuConverter;
        private readonly ResponseObject<PhatTuDTO> _responseObject;
        public PhatTuService(PhatTuConverter phatTuConverter, ResponseObject<PhatTuDTO> responseObject)
        {
            _phatTuConverter = phatTuConverter;
            _responseObject = responseObject;
        }

        public async Task<IQueryable<PhatTuDTO>> LayPhatTuTheoChua(int chuaId, int pageSize, int pageNumber)
        {
            var phatTuQuery = _context.phatTus.Where(x => x.ChuaId == chuaId).Skip((pageNumber - 1) * pageSize).Take(pageSize).Select(x => _phatTuConverter.EntityToDTO(x));
            return phatTuQuery;
        }

        public async Task<ResponseObject<PhatTuDTO>> LayPhatTuTheoEmail(string email, int pageSize, int pageNumber)
        {
            var phatTuQuery = await _context.phatTus.FirstOrDefaultAsync(x => x.Email.Equals(email));
            return _responseObject.ResponseSuccess($"Phật tử có email: {email} có thông tin là: ", _phatTuConverter.EntityToDTO(phatTuQuery));
        }

        public async Task<IQueryable<PhatTuDTO>> LayPhatTuTheoGioiTinh(string gioiTinh, int pageSize, int pageNumber)
        {
            var phatTuQuery = _context.phatTus.Where(x => x.GioiTinh.Equals(gioiTinh)).Skip((pageNumber - 1) * pageSize).Take(pageSize).Select(x => _phatTuConverter.EntityToDTO(x));
            return phatTuQuery;
        }

        public async Task<IQueryable<PhatTuDTO>> LayPhatTuTheoPhapDanh(string phapDanh, int pageSize, int pageNumber)
        {
            var phatTuQuery = _context.phatTus.Where(x => x.PhapDanh.Equals(phapDanh)).Skip((pageNumber - 1) * pageSize).Take(pageSize).Select(x => _phatTuConverter.EntityToDTO(x));
            return phatTuQuery;
        }

        public async Task<IQueryable<PhatTuDTO>> LayPhatTuTheoTen(string name, int pageSize, int pageNumber)
        {
            var phatTuQuery = _context.phatTus.Where(x => x.HoVaTen.ToLower().Contains(name.ToLower())).Skip((pageNumber - 1) * pageSize).Take(pageSize).Select(x => _phatTuConverter.EntityToDTO(x));
            return phatTuQuery;
        }

        public async Task<IQueryable<PhatTuDTO>> LayPhatTuTheoTrangThai(bool status, int pageSize, int pageNumber)
        {
            var phatTuQuery = _context.phatTus.Where(x => x.DaHoanTuc == status).Skip((pageNumber - 1) * pageSize).Take(pageSize).Select(x => _phatTuConverter.EntityToDTO(x));
            return phatTuQuery;
        }

        public async Task<IQueryable<PhatTuDTO>> LayTatCaPhatTu(int pageSize, int pageNumber)
        {
            var skipCount = (pageNumber - 1) * pageSize;
            var phatTuQuery = _context.phatTus
                .Skip(skipCount)
                .Take(pageSize)
                .Select(x => _phatTuConverter.EntityToDTO(x));

            return phatTuQuery;
        }

        public async Task<ResponseObject<PhatTuDTO>> SuaThongTinPhatTu(int phatTuId, Request_CapNhatThongTinPhatTu request)
        {
            var phatTu = await _context.phatTus.FirstOrDefaultAsync(x => x.Id == phatTuId);
            try
            {
                phatTu.PhapDanh = request.PhapDanh;
                phatTu.NgayCapNhat = DateTime.Now;
                phatTu.Email = request.Email;
                phatTu.GioiTinh = request.GioiTinh;
                phatTu.NgaySinh = request.NgaySinh;
                if (!Validate.IsValidPhoneNumber(request.SoDienThoai))
                {
                    throw new Exception("Định dạng số điện thoại không hợp lệ");
                }
                else
                {
                    phatTu.SoDienThoai = request.SoDienThoai;
                    _context.phatTus.Update(phatTu);
                    await _context.SaveChangesAsync();
                }
                return _responseObject.ResponseSuccess("Cập nhật thông tin phật tử thành công", _phatTuConverter.EntityToDTO(phatTu));
            }
            catch (Exception ex)
            {
                return _responseObject.ResponseError(StatusCodes.Status500InternalServerError, ex.Message, null);
            }
        }

        public async Task<string> XoaPhatTu(int phatTuId)
        {
            var phatTu = await _context.phatTus.FirstOrDefaultAsync(x => x.Id == phatTuId);
            if(phatTu is null)
            {
                return "Phật tử không tồn tại";
            }
            else
            {
                phatTu.DaHoanTuc = true;
                _context.phatTus.Update(phatTu);
                await _context.SaveChangesAsync();
                return "Đã cập nhật trạng thái hoàn tục của phật tử";
            }
        }
    }
}
