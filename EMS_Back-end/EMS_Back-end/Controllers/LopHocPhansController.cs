using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EMS_Back_end.Models;

namespace EMS_Back_end.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LopHocPhansController : ControllerBase
    {
        private readonly Context _context;

        public LopHocPhansController(Context context)
        {
            _context = context;
        }

        // GET: api/LopHocPhans
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LopHocPhan>>> GetLopHocPhans()
        {
            return await _context.LopHocPhans.ToListAsync();
        }

        // GET: api/LopHocPhans/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LopHocPhan>> GetLopHocPhan(int id)
        {
            var lopHocPhan = await _context.LopHocPhans.FindAsync(id);

            if (lopHocPhan == null)
            {
                return NotFound();
            }

            return lopHocPhan;
        }

        // PUT: api/LopHocPhans/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLopHocPhan(int id, LopHocPhan lopHocPhan)
        {
            if (id != lopHocPhan.Id)
            {
                return BadRequest();
            }

            _context.Entry(lopHocPhan).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LopHocPhanExists(id))
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

        // POST: api/LopHocPhans
        [HttpPost]
        public async Task<ActionResult<LopHocPhan>> PostLopHocPhan(LopHocPhan lopHocPhan)
        {
            _context.LopHocPhans.Add(lopHocPhan);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLopHocPhan", new { id = lopHocPhan.Id }, lopHocPhan);
        }

        // DELETE: api/LopHocPhans/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<LopHocPhan>> DeleteLopHocPhan(int id)
        {
            var lopHocPhan = await _context.LopHocPhans.FindAsync(id);
            if (lopHocPhan == null)
            {
                return NotFound();
            }

            _context.LopHocPhans.Remove(lopHocPhan);
            await _context.SaveChangesAsync();

            return lopHocPhan;
        }

        private bool LopHocPhanExists(int id)
        {
            return _context.LopHocPhans.Any(e => e.Id == id);
        }
    }
}
