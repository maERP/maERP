using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using maERP.Shared.Dtos.SalesChannel;
using maERP.Shared.Models;
using maERP.Shared.Dtos;
using maERP.Shared.Dtos.Warehouse;
using maERP.Shared.Pages.SalesChannels;
using maERP.Server.Contracts;

namespace maERP.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class SalesChannelsController : ControllerBase
{
    private readonly ISalesChannelRepository _repository;

    public SalesChannelsController(ISalesChannelRepository salesChannelRepository)
    {
        this._repository = salesChannelRepository;
    }

    // GET: api/SalesChannels
    [HttpGet("GetAll")]
    public async Task<ActionResult<IEnumerable<SalesChannelListDto>>> GetSalesChannel()
    {
        var salesChannels = await _repository.GetAllAsync<SalesChannelListDto>();
        return Ok(salesChannels);
    }

    // GET: api/SalesChannels/5
    [HttpGet("{id}")]
    public async Task<ActionResult<SalesChannelDetailDto>> GetSalesChannel(int id)
    {
        var salesChannel = await _repository.GetDetails(id);

        if (salesChannel == null)
        {
            return NotFound();
        }

        return Ok(salesChannel);
    }

    // PUT: api/SalesChannels/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutSalesChannel(int id, [FromBody] SalesChannelUpdateDto salesChannelUpdateDto)
    {
        if (await _repository.Exists(id) == true)
        {
            await _repository.UpdateAsync(id, salesChannelUpdateDto);
        }
        else
        {
            return NotFound();
        }

        return NoContent();
    }

    // POST: api/SalesChannels
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<SalesChannelDetailDto>> PostSalesChannel(SalesChannelCreateDto salesChannelCreateDto)
    {
        var salesChannel = await _repository.AddWithDetailsAsync(salesChannelCreateDto);
        return CreatedAtAction("GetSalesChannel", new { id = salesChannel.Id }, salesChannel);
    }

    // DELETE: api/SalesChannels/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteSalesChannel(int id)
    {
        var salesChannel = await _repository.GetByIdAsync(id);

        if (salesChannel == null)
        {
            return NotFound();
        }

        await _repository.DeleteAsync(id);

        return NoContent();
    }
}