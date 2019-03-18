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
    public class LichThisController : ControllerBase
    {
        private readonly Context _context;

        public LichThisController(Context context)
        {
            _context = context;
        }

        // GET: api/LichThis
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LichThi>>> GetLichThis()
        {
            return await _context.LichThis.ToListAsync();
        }

        // GET: api/LichThis/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LichThi>> GetLichThi(int id)
        {
            var lichThi = await _context.LichThis.FindAsync(id);

            if (lichThi == null)
            {
                return NotFound();
            }

            return lichThi;
        }

        // PUT: api/LichThis/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLichThi(int id, LichThi lichThi)
        {
            if (id != lichThi.Id)
            {
                return BadRequest();
            }

            _context.Entry(lichThi).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LichThiExists(id))
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

        // POST: api/LichThis
        [HttpPost]
        public async Task<ActionResult<LichThi>> PostLichThi(LichThi lichThi)
        {
            _context.LichThis.Add(lichThi);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLichThi", new { id = lichThi.Id }, lichThi);
        }

        // DELETE: api/LichThis/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<LichThi>> DeleteLichThi(int id)
        {
            var lichThi = await _context.LichThis.FindAsync(id);
            if (lichThi == null)
            {
                return NotFound();
            }

            _context.LichThis.Remove(lichThi);
            await _context.SaveChangesAsync();

            return lichThi;
        }

        private bool LichThiExists(int id)
        {
            return _context.LichThis.Any(e => e.Id == id);
        }
    }
}
