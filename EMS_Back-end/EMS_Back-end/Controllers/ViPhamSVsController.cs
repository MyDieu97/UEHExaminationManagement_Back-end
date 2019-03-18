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
    public class ViPhamSVsController : ControllerBase
    {
        private readonly Context _context;

        public ViPhamSVsController(Context context)
        {
            _context = context;
        }

        // GET: api/ViPhamSVs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ViPhamSV>>> GetViPhamSVs()
        {
            return await _context.ViPhamSVs.ToListAsync();
        }

        // GET: api/ViPhamSVs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ViPhamSV>> GetViPhamSV(int id)
        {
            var viPhamSV = await _context.ViPhamSVs.FindAsync(id);

            if (viPhamSV == null)
            {
                return NotFound();
            }

            return viPhamSV;
        }

        // PUT: api/ViPhamSVs/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutViPhamSV(int id, ViPhamSV viPhamSV)
        {
            if (id != viPhamSV.Id)
            {
                return BadRequest();
            }

            _context.Entry(viPhamSV).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ViPhamSVExists(id))
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

        // POST: api/ViPhamSVs
        [HttpPost]
        public async Task<ActionResult<ViPhamSV>> PostViPhamSV(ViPhamSV viPhamSV)
        {
            _context.ViPhamSVs.Add(viPhamSV);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetViPhamSV", new { id = viPhamSV.Id }, viPhamSV);
        }

        // DELETE: api/ViPhamSVs/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ViPhamSV>> DeleteViPhamSV(int id)
        {
            var viPhamSV = await _context.ViPhamSVs.FindAsync(id);
            if (viPhamSV == null)
            {
                return NotFound();
            }

            _context.ViPhamSVs.Remove(viPhamSV);
            await _context.SaveChangesAsync();

            return viPhamSV;
        }

        private bool ViPhamSVExists(int id)
        {
            return _context.ViPhamSVs.Any(e => e.Id == id);
        }
    }
}
