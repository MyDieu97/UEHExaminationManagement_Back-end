using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EMS_Back_end.Models;
using EMS_Back_end.Models.Response;

namespace EMS_Back_end.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GiangViensController : ControllerBase
    {
        private readonly Context _context;

        public GiangViensController(Context context)
        {
            _context = context;
        }

        // GET: api/GiangViens
        [HttpGet]
        public async Task<ActionResult<BaseResponse>> GetGiangViens()
        {
            return new BaseResponse
            {
                Message = "Lấy danh sách thành công!",
                Data = await _context.GiangViens.ToListAsync()
            };
        }

        // GET: api/GiangViens/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BaseResponse>> GetGiangVien(int id)
        {
            var giangVien = await _context.GiangViens.FindAsync(id);

            if (giangVien == null)
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
                Data = giangVien
            };
        }

        // PUT: api/GiangViens/5
        [HttpPut("{id}")]
        public async Task<ActionResult<BaseResponse>> PutGiangVien(int id, GiangVien giangVien)
        {
            var giangVienSua = await _context.GiangViens.FindAsync(id);
            if (giangVienSua == null)
            {
                return new BaseResponse
                {
                    ErrorCode = 1,
                    Message = "Not Found"
                };
            }

            giangVienSua.DonViId = giangVien.DonViId;
            giangVienSua.Email = giangVien.Email;
            giangVienSua.GhiChu = giangVien.GhiChu;
            giangVienSua.GioiTinh = giangVien.GioiTinh;
            giangVienSua.HuuTri = giangVien.HuuTri;
            giangVienSua.MaGV = giangVien.MaGV;
            giangVienSua.SoDT = giangVien.SoDT;
            giangVienSua.TenGV = giangVien.TenGV;
            giangVienSua.HoGV = giangVien.HoGV;

            _context.GiangViens.Update(giangVienSua);
            await _context.SaveChangesAsync();
            return new BaseResponse
            {
                Message = "Update success",
                Data = giangVien
            };
        }

        // POST: api/GiangViens
        [HttpPost]
        public async Task<ActionResult<BaseResponse>> PostGiangVien(GiangVien giangVien)
        {
            try
            {
                _context.GiangViens.Add(giangVien);
                await _context.SaveChangesAsync();
                return new BaseResponse
                {
                    Message = "Added success",
                    Data = giangVien
                };
            }
            catch
            {
                return new BaseResponse
                {
                    ErrorCode = 1,
                    Message = "Adding fail"
                };
            }
        }

        // DELETE: api/GiangViens/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<BaseResponse>> DeleteGiangVien(int id)
        {
            var giangVien = await _context.GiangViens.FindAsync(id);
            if (giangVien == null)
            {
                return new BaseResponse
                {
                    ErrorCode = 1,
                    Message = "Delete fail"
                };
            }

            _context.GiangViens.Remove(giangVien);
            await _context.SaveChangesAsync();

            return new BaseResponse
            {
                Message = "Delete success"
            };
        }

        private bool GiangVienExists(int id)
        {
            return _context.GiangViens.Any(e => e.Id == id);
        }
    }
}
