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
    [Route("api/Artikel")]
    public class ArtikelController : Controller
    {
        private readonly WarenhausContext _context;

        public ArtikelController(WarenhausContext context)
        {
            _context = context;
        }

        // GET: api/Artikel
        [HttpGet]
        public IEnumerable<Artikel> GetArtikel()
        {
            return _context.Artikel;
        }

        // GET: api/Artikel/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetArtikel([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var artikel = await _context.Artikel.SingleOrDefaultAsync(m => m.Id == id);

            if (artikel == null)
            {
                return NotFound();
            }

            return Ok(artikel);
        }

        // PUT: api/Artikel/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutArtikel([FromRoute] int id, [FromBody] Artikel artikel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != artikel.Id)
            {
                return BadRequest();
            }

            _context.Entry(artikel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ArtikelExists(id))
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

        // POST: api/Artikel
        [HttpPost]
        public async Task<IActionResult> PostArtikel([FromBody] Artikel artikel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Artikel.Add(artikel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetArtikel", new { id = artikel.Id }, artikel);
        }

        // DELETE: api/Artikel/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteArtikel([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var artikel = await _context.Artikel.SingleOrDefaultAsync(m => m.Id == id);
            if (artikel == null)
            {
                return NotFound();
            }

            _context.Artikel.Remove(artikel);
            await _context.SaveChangesAsync();

            return Ok(artikel);
        }

        private bool ArtikelExists(int id)
        {
            return _context.Artikel.Any(e => e.Id == id);
        }
    }
}