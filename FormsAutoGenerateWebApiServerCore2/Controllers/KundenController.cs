using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FormsAutoGenerateWebApiServerCore2.Models;

namespace FormsAutoGenerateWebApiServerCore2.Controllers
{
    [Produces("application/json")]
    [Route("api/Kunden")]
    public class KundenController : Controller
    {
        private readonly WarenhausContext _context;

        public KundenController(WarenhausContext context)
        {
            _context = context;
        }

        // GET: api/Kunden
        [HttpGet]
        public IEnumerable<Kunde> GetKunden()
        {
            return _context.Kunden;
        }

        // GET: api/Kunden/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetKunde([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var kunde = await _context.Kunden.SingleOrDefaultAsync(m => m.Id == id);

            if (kunde == null)
            {
                return NotFound();
            }

            return Ok(kunde);
        }

        // PUT: api/Kunden/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutKunde([FromRoute] int id, [FromBody] Kunde kunde)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != kunde.Id)
            {
                return BadRequest();
            }

            _context.Entry(kunde).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!KundeExists(id))
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

        // POST: api/Kunden
        [HttpPost]
        public async Task<IActionResult> PostKunde([FromBody] Kunde kunde)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Kunden.Add(kunde);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetKunde", new { id = kunde.Id }, kunde);
        }

        // DELETE: api/Kunden/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteKunde([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var kunde = await _context.Kunden.SingleOrDefaultAsync(m => m.Id == id);
            if (kunde == null)
            {
                return NotFound();
            }

            _context.Kunden.Remove(kunde);
            await _context.SaveChangesAsync();

            return Ok(kunde);
        }

        private bool KundeExists(int id)
        {
            return _context.Kunden.Any(e => e.Id == id);
        }
    }
}