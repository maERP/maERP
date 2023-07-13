using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using maERP.Server.Repository;
using maERP.Shared.Dtos.SalesChannel;
using maERP.Shared.Models;
using maERP.Shared.Dtos;
using maERP.Shared.Dtos.Warehouse;

namespace maERP.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class SalesChannelsController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly ISalesChannelRepository _repository;

    public SalesChannelsController(IMapper mapper, ISalesChannelRepository salesChannelRepository)
    {
        this._mapper = mapper;
        this._repository = salesChannelRepository;
    }

    // GET: api/SalesChannels
    [HttpGet("GetAll")]
    public async Task<ActionResult<IEnumerable<SalesChannelListDto>>> GetSalesChannel()
    {
        var salesChannel = await _repository.GetAllAsync();
        var records = _mapper.Map<List<SalesChannelListDto>>(salesChannel);
        return Ok(records);
    }

    // GET: api/SalesChannels/5
    [HttpGet("{id}")]
    public async Task<ActionResult<SalesChannelDetailDto>> GetSalesChannel(int id)
    {
        var salesChannel = await _repository.GetByIdAsync(id);

        if (salesChannel == null)
        {
            return NotFound();
        }

        var salesChannelDto = _mapper.Map<SalesChannelDetailDto>(salesChannel);

        return Ok(salesChannelDto);
    }

    // PUT: api/SalesChannels/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutSalesChannel(int id, [FromBody] SalesChannelUpdateDto salesChannelUpdateDto)
    {
        if (await _repository.Exists(id) == true)
        {
            await _repository.UpdateAsync<SalesChannelUpdateDto>(id, salesChannelUpdateDto);
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
    public async Task<ActionResult<SalesChannel>> PostSalesChannel(SalesChannelUpdateDto salesChannelUpdateDto)
    {
        var salesChannel = _mapper.Map<SalesChannel>(salesChannelUpdateDto);

        await _repository.AddAsync(salesChannel);

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

    private async Task<bool> SalesChannelExists(int id)
    {
        return await _repository.Exists(id);
    }
}