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
        public async Task<ActionResult<IEnumerable<LopSinhVien>>> GetLopSinhViens()
        {
            return await _context.LopSinhViens.ToListAsync();
        }

        // GET: api/LopSinhViens/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LopSinhVien>> GetLopSinhVien(int id)
        {
            var lopSinhVien = await _context.LopSinhViens.FindAsync(id);

            if (lopSinhVien == null)
            {
                return NotFound();
            }

            return lopSinhVien;
        }

        // PUT: api/LopSinhViens/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLopSinhVien(int id, LopSinhVien lopSinhVien)
        {
            if (id != lopSinhVien.Id)
            {
                return BadRequest();
            }

            _context.Entry(lopSinhVien).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                
            }

            return NoContent();
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
        public async Task<ActionResult<LopSinhVien>> DeleteLopSinhVien(int id)
        {
            var lopSinhVien = await _context.LopSinhViens.FindAsync(id);
            if (lopSinhVien == null)
            {
                return NotFound();
            }

            _context.LopSinhViens.Remove(lopSinhVien);
            await _context.SaveChangesAsync();

            return lopSinhVien;
        }

        private bool LopSinhVienExists(string maLop)
        {
            return _context.LopSinhViens.Any(e => e.MaLop == maLop);
        }
    }
}
