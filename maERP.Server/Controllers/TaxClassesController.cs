using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using maERP.Server.Data;

namespace maERP.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TaxClassesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TaxClassesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/TaxClasses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaxClass>>> GetTaxClass()
        {
          if (_context.TaxClass == null)
          {
              return NotFound();
          }
            return await _context.TaxClass.ToListAsync();
        }

        // GET: api/TaxClasses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TaxClass>> GetTaxClass(int id)
        {
          if (_context.TaxClass == null)
          {
              return NotFound();
          }
            var taxClass = await _context.TaxClass.FindAsync(id);

            if (taxClass == null)
            {
                return NotFound();
            }

            return taxClass;
        }

        // PUT: api/TaxClasses/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTaxClass(int id, TaxClass taxClass)
        {
            if (id != taxClass.Id)
            {
                return BadRequest();
            }

            _context.Entry(taxClass).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TaxClassExists(id))
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

        // POST: api/TaxClasses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TaxClass>> PostTaxClass(TaxClass taxClass)
        {
          if (_context.TaxClass == null)
          {
              return Problem("Entity set 'ApplicationDbContext.TaxClass'  is null.");
          }
            _context.TaxClass.Add(taxClass);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTaxClass", new { id = taxClass.Id }, taxClass);
        }

        // DELETE: api/TaxClasses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTaxClass(int id)
        {
            if (_context.TaxClass == null)
            {
                return NotFound();
            }
            var taxClass = await _context.TaxClass.FindAsync(id);
            if (taxClass == null)
            {
                return NotFound();
            }

            _context.TaxClass.Remove(taxClass);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TaxClassExists(int id)
        {
            return (_context.TaxClass?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
