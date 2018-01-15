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
    [Route("api/Artikelkategorien")]
    public class ArtikelkategorienController : Controller
    {
        private readonly WarenhausContext _context;

        public ArtikelkategorienController(WarenhausContext context)
        {
            _context = context;
        }

        // GET: api/Artikelkategorien
        [HttpGet]
        public IEnumerable<Artikelkategorie> GetArtikelkategorien()
        {
            return _context.Artikelkategorien;
        }

        // GET: api/Artikelkategorien/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetArtikelkategorie([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var artikelkategorie = await _context.Artikelkategorien.SingleOrDefaultAsync(m => m.Id == id);

            if (artikelkategorie == null)
            {
                return NotFound();
            }

            return Ok(artikelkategorie);
        }

        // PUT: api/Artikelkategorien/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutArtikelkategorie([FromRoute] int id, [FromBody] Artikelkategorie artikelkategorie)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != artikelkategorie.Id)
            {
                return BadRequest();
            }

            _context.Entry(artikelkategorie).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ArtikelkategorieExists(id))
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

        // POST: api/Artikelkategorien
        [HttpPost]
        public async Task<IActionResult> PostArtikelkategorie([FromBody] Artikelkategorie artikelkategorie)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Artikelkategorien.Add(artikelkategorie);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetArtikelkategorie", new { id = artikelkategorie.Id }, artikelkategorie);
        }

        // DELETE: api/Artikelkategorien/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteArtikelkategorie([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var artikelkategorie = await _context.Artikelkategorien.SingleOrDefaultAsync(m => m.Id == id);
            if (artikelkategorie == null)
            {
                return NotFound();
            }

            _context.Artikelkategorien.Remove(artikelkategorie);
            await _context.SaveChangesAsync();

            return Ok(artikelkategorie);
        }

        private bool ArtikelkategorieExists(int id)
        {
            return _context.Artikelkategorien.Any(e => e.Id == id);
        }
    }
}