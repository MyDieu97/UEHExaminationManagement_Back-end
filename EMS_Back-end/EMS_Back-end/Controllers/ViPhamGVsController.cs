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
    public class ViPhamGVsController : ControllerBase
    {
        private readonly Context _context;

        public ViPhamGVsController(Context context)
        {
            _context = context;
        }

        // GET: api/ViPhamGVs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ViPhamGV>>> GetViPhamGVs()
        {
            return await _context.ViPhamGVs.ToListAsync();
        }

        // GET: api/ViPhamGVs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ViPhamGV>> GetViPhamGV(int id)
        {
            var viPhamGV = await _context.ViPhamGVs.FindAsync(id);

            if (viPhamGV == null)
            {
                return NotFound();
            }

            return viPhamGV;
        }

        // PUT: api/ViPhamGVs/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutViPhamGV(int id, ViPhamGV viPhamGV)
        {
            if (id != viPhamGV.Id)
            {
                return BadRequest();
            }

            _context.Entry(viPhamGV).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ViPhamGVExists(id))
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

        // POST: api/ViPhamGVs
        [HttpPost]
        public async Task<ActionResult<ViPhamGV>> PostViPhamGV(ViPhamGV viPhamGV)
        {
            _context.ViPhamGVs.Add(viPhamGV);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetViPhamGV", new { id = viPhamGV.Id }, viPhamGV);
        }

        // DELETE: api/ViPhamGVs/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ViPhamGV>> DeleteViPhamGV(int id)
        {
            var viPhamGV = await _context.ViPhamGVs.FindAsync(id);
            if (viPhamGV == null)
            {
                return NotFound();
            }

            _context.ViPhamGVs.Remove(viPhamGV);
            await _context.SaveChangesAsync();

            return viPhamGV;
        }

        private bool ViPhamGVExists(int id)
        {
            return _context.ViPhamGVs.Any(e => e.Id == id);
        }
    }
}
