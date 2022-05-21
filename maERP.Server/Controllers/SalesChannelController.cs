#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using maERP_server.Models;

namespace maERP_server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesChannelController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public SalesChannelController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/SalesChannel
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SalesChannelDTO>>> GetTodoItem()
        {
            return await _context.SalesChannel.Select(x => SalesChannelToDTO(x)).ToListAsync();
        }

        // GET: api/SalesChannel/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SalesChannelDTO>> GetTodoItem(long id)
        {
            var todoItem = await _context.SalesChannel.FindAsync(id);

            if (todoItem == null)
            {
                return NotFound();
            }

            return SalesChannelToDTO(todoItem);
        }

        // PUT: api/SalesChannel/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTodoItem(long id, SalesChannelDTO salesChannelDTO)
        {
            if (id != salesChannelDTO.Id)
            {
                return BadRequest();
            }

            _context.Entry(salesChannelDTO).State = EntityState.Modified;

            var salesChannel = await _context.SalesChannel.FindAsync(id);
            if (salesChannel == null)
            {
                return NotFound();
            }

            salesChannel.Name = salesChannelDTO.Name;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TodoItemExists(id))
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

        // POST: api/SalesChannel
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<SalesChannelDTO>> PostTodoItem(SalesChannelDTO salesChannelDTO)
        {
            var salesChannel = new SalesChannel
            {
                Name = salesChannelDTO.Name
            };

            _context.SalesChannel.Add(salesChannel);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetTodoItem),
                new { id = salesChannel.SalesChannelId },
                SalesChannelToDTO(salesChannel));
        }

        // DELETE: api/SalesChannel/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItem(long id)
        {
            var todoItem = await _context.SalesChannel.FindAsync(id);
            if (todoItem == null)
            {
                return NotFound();
            }

            _context.SalesChannel.Remove(todoItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TodoItemExists(long id)
        {
            return _context.SalesChannel.Any(e => e.SalesChannelId == id);
        }

        private static SalesChannelDTO SalesChannelToDTO(SalesChannel salesChannel) =>
            new SalesChannelDTO
            {
                Id = salesChannel.SalesChannelId,
                Name = salesChannel.Name
            };
    }
}
