using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EMS_Back_end.Models;
using EMS_Back_end.Models.Responses;

namespace EMS_Back_end.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LopSinhViensController : ControllerBase
    {
        private readonly Context _context;

        public LopSinhViensController(Context context)
        {
            _context = context;
        }

        // GET: api/LopSinhViens
        [HttpGet]
        public async Task<ActionResult<BaseResponse>> GetLopSinhViens()
        {
            return new BaseResponse
            {
                Message = "Lấy danh sách thành công!",
                Data = await _context.LopSinhViens.Where(x => x.Id > 0).ToListAsync()
            };
        }

        // GET: api/LopSinhViens/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BaseResponse>> GetLopSinhVien(int id)
        {
            var lopSinhVien = await _context.LopSinhViens.FindAsync(id);

            if (lopSinhVien == null)
            {
                return new BaseResponse
                {
                    ErrorCode = 1,
                    Message = "Không tìm thấy"
                };
            }

            return new BaseResponse
            {
                Message = "Tìm thành công",
                Data = lopSinhVien
            };
        }

        // PUT: api/LopSinhViens/5
        [HttpPut("{id}")]
        public async Task<ActionResult<BaseResponse>> PutLopSinhVien(int id, LopSinhVien lopSinhVien)
        {
            var lopSinhVienSua = await _context.LopSinhViens.FindAsync(id);
            if (lopSinhVienSua == null)
            {
                return new BaseResponse
                {
                    ErrorCode = 1,
                    Message = "Không tìm thấy"
                };
            }

            lopSinhVienSua.MaLop = lopSinhVien.MaLop;
            lopSinhVienSua.NganhHoc = lopSinhVien.NganhHoc;
            lopSinhVienSua.Khoa = lopSinhVien.Khoa;

            _context.LopSinhViens.Update(lopSinhVienSua);
            await _context.SaveChangesAsync();
            return new BaseResponse
            {
                Message = "Cập nhật thành công",
                Data = lopSinhVien
            };
        }

        // POST: api/LopSinhViens
        [HttpPost]
        public async Task<ActionResult<BaseResponse>> PostLopSinhVien(LopSinhVien lopSinhVien)
        {
            try
            {
                _context.LopSinhViens.Add(lopSinhVien);
                await _context.SaveChangesAsync();
                return new BaseResponse
                {
                    Message = "Thêm mới thành công",
                    Data = lopSinhVien
                };
            }
            catch
            {
                return new BaseResponse
                {
                    ErrorCode = 1,
                    Message = "Thêm mới thất bại"
                };
            }
        }

        // DELETE: api/LopSinhViens/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<BaseResponse>> DeleteLopSinhVien(int id)
        {
            var lopSinhVien = await _context.LopSinhViens.FindAsync(id);
            if (lopSinhVien == null)
            {
                return new BaseResponse
                {
                    ErrorCode = 1,
                    Message = "Xóa thất bại"
                };
            }

            _context.LopSinhViens.Remove(lopSinhVien);
            await _context.SaveChangesAsync();

            return new BaseResponse
            {
                Message = "Xóa thành công"
            };
        }

        private bool LopSinhVienExists(string maLop)
        {
            return _context.LopSinhViens.Any(e => e.MaLop == maLop);
        }
    }
}
