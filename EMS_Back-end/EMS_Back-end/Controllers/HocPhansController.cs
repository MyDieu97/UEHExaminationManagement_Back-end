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
    public class HocPhansController : ControllerBase
    {
        private readonly Context _context;

        public HocPhansController(Context context)
        {
            _context = context;
        }

        // GET: api/HocPhans
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HocPhan>>> GetHocPhans()
        {
            return await _context.HocPhans.ToListAsync();
        }

        // GET: api/HocPhans/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HocPhan>> GetHocPhan(int id)
        {
            var hocPhan = await _context.HocPhans.FindAsync(id);

            if (hocPhan == null)
            {
                return NotFound();
            }

            return hocPhan;
        }

        // PUT: api/HocPhans/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHocPhan(int id, HocPhan hocPhan)
        {
            if (id != hocPhan.Id)
            {
                return BadRequest();
            }

            _context.Entry(hocPhan).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HocPhanExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/HocPhans
        [HttpPost]
        public async Task<ActionResult<BaseResponse>> PostHocPhan(HocPhan hocPhan)
        {
            try
            {
                _context.HocPhans.Add(hocPhan);
                await _context.SaveChangesAsync();
                return new BaseResponse
                {
                    Message = "Thêm mới thành công",
                    Data = hocPhan
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

        // DELETE: api/HocPhans/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<HocPhan>> DeleteHocPhan(int id)
        {
            var hocPhan = await _context.HocPhans.FindAsync(id);
            if (hocPhan == null)
            {
                return NotFound();
            }

            _context.HocPhans.Remove(hocPhan);
            await _context.SaveChangesAsync();

            return hocPhan;
        }

        private bool HocPhanExists(int id)
        {
            return _context.HocPhans.Any(e => e.Id == id);
        }
    }
}
