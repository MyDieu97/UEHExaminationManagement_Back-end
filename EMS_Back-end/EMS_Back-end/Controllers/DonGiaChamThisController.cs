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
    public class DonGiaChamThisController : ControllerBase
    {
        private readonly Context _context;

        public DonGiaChamThisController(Context context)
        {
            _context = context;
        }

        // GET: api/DonGiaChamThis
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DonGiaChamThi>>> GetDonGiaChamThis()
        {
            return await _context.DonGiaChamThis.ToListAsync();
        }

        // GET: api/DonGiaChamThis/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DonGiaChamThi>> GetDonGiaChamThi(int id)
        {
            var donGiaChamThi = await _context.DonGiaChamThis.FindAsync(id);

            if (donGiaChamThi == null)
            {
                return NotFound();
            }

            return donGiaChamThi;
        }

        // PUT: api/DonGiaChamThis/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDonGiaChamThi(int id, DonGiaChamThi donGiaChamThi)
        {
            if (id != donGiaChamThi.Id)
            {
                return BadRequest();
            }

            _context.Entry(donGiaChamThi).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DonGiaChamThiExists(id))
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

        // POST: api/DonGiaChamThis
        [HttpPost]
        public async Task<ActionResult<DonGiaChamThi>> PostDonGiaChamThi(DonGiaChamThi donGiaChamThi)
        {
            _context.DonGiaChamThis.Add(donGiaChamThi);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDonGiaChamThi", new { id = donGiaChamThi.Id }, donGiaChamThi);
        }

        // DELETE: api/DonGiaChamThis/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<DonGiaChamThi>> DeleteDonGiaChamThi(int id)
        {
            var donGiaChamThi = await _context.DonGiaChamThis.FindAsync(id);
            if (donGiaChamThi == null)
            {
                return NotFound();
            }

            _context.DonGiaChamThis.Remove(donGiaChamThi);
            await _context.SaveChangesAsync();

            return donGiaChamThi;
        }

        private bool DonGiaChamThiExists(int id)
        {
            return _context.DonGiaChamThis.Any(e => e.Id == id);
        }
    }
}
