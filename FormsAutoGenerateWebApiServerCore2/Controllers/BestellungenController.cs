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
    [Route("api/Bestellungen")]
    public class BestellungenController : Controller
    {
        private readonly WarenhausContext _context;

        public BestellungenController(WarenhausContext context)
        {
            _context = context;
        }

        // GET: api/Bestellungen
        [HttpGet]
        public IEnumerable<Bestellung> GetBestellungen()
        {
            return _context.Bestellungen;
        }

        // GET: api/Bestellungen/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBestellung([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var bestellung = await _context.Bestellungen.SingleOrDefaultAsync(m => m.Id == id);

            if (bestellung == null)
            {
                return NotFound();
            }

            return Ok(bestellung);
        }

        // PUT: api/Bestellungen/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBestellung([FromRoute] int id, [FromBody] Bestellung bestellung)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != bestellung.Id)
            {
                return BadRequest();
            }

            _context.Entry(bestellung).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BestellungExists(id))
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

        // POST: api/Bestellungen
        [HttpPost]
        public async Task<IActionResult> PostBestellung([FromBody] Bestellung bestellung)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Bestellungen.Add(bestellung);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBestellung", new { id = bestellung.Id }, bestellung);
        }

        // DELETE: api/Bestellungen/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBestellung([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var bestellung = await _context.Bestellungen.SingleOrDefaultAsync(m => m.Id == id);
            if (bestellung == null)
            {
                return NotFound();
            }

            _context.Bestellungen.Remove(bestellung);
            await _context.SaveChangesAsync();

            return Ok(bestellung);
        }

        private bool BestellungExists(int id)
        {
            return _context.Bestellungen.Any(e => e.Id == id);
        }
    }
}