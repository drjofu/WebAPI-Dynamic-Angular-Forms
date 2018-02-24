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
    [Route("api/Flugzeuge")]
    public class FlugzeugeController : Controller
    {
        private readonly WarenhausContext _context;

        public FlugzeugeController(WarenhausContext context)
        {
            _context = context;
        }

        // GET: api/Flugzeuge
        [HttpGet]
        public IEnumerable<Flugzeug> GetFlugzeuge()
        {
            return _context.Flugzeuge;
        }

        // GET: api/Flugzeuge/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetFlugzeug([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var flugzeug = await _context.Flugzeuge.SingleOrDefaultAsync(m => m.Id == id);

            if (flugzeug == null)
            {
                return NotFound();
            }

            return Ok(flugzeug);
        }

        // PUT: api/Flugzeuge/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFlugzeug([FromRoute] int id, [FromBody] Flugzeug flugzeug)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != flugzeug.Id)
            {
                return BadRequest();
            }

            _context.Entry(flugzeug).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FlugzeugExists(id))
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

        // POST: api/Flugzeuge
        [HttpPost]
        public async Task<IActionResult> PostFlugzeug([FromBody] Flugzeug flugzeug)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Flugzeuge.Add(flugzeug);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFlugzeug", new { id = flugzeug.Id }, flugzeug);
        }

        // DELETE: api/Flugzeuge/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFlugzeug([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var flugzeug = await _context.Flugzeuge.SingleOrDefaultAsync(m => m.Id == id);
            if (flugzeug == null)
            {
                return NotFound();
            }

            _context.Flugzeuge.Remove(flugzeug);
            await _context.SaveChangesAsync();

            return Ok(flugzeug);
        }

        private bool FlugzeugExists(int id)
        {
            return _context.Flugzeuge.Any(e => e.Id == id);
        }
    }
}