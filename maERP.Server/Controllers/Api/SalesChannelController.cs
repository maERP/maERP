using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using maERP.Server.Repository;
using maERP.Shared.Dtos.SalesChannel;
using maERP.Shared.Models;
using maERP.Shared.Dtos;

namespace maERP.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class SalesChannelController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly ISalesChannelRepository _repository;

    public SalesChannelController(IMapper mapper, ISalesChannelRepository salesChannelRepository)
    {
        this._mapper = mapper;
        this._repository = salesChannelRepository;
    }

    // GET: api/SalesChannel
    [HttpGet("GetAll")]
    public async Task<ActionResult<IEnumerable<SalesChannelListDto>>> GetSalesChannel()
    {
        var salesChannel = await _repository.GetAllAsync();
        var records = _mapper.Map<List<SalesChannelListDto>>(salesChannel);
        return Ok(records);
    }

    // GET: api/SalesChannel/5
    [HttpGet("{id}")]
    public async Task<ActionResult<SalesChannelDetailDto>> GetSalesChannel(uint id)
    {
        var salesChannel = await _repository.GetByIdAsync(id);

        if (salesChannel == null)
        {
            return NotFound();
        }

        var salesChannelDto = _mapper.Map<SalesChannelDetailDto>(salesChannel);

        return Ok(salesChannelDto);
    }

    // PUT: api/SalesChannel/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutSalesChannel(uint id, SalesChannelUpdateDto salesChannelUpdateDto)
    {
        if (id != salesChannelUpdateDto.Id)
        {
            return BadRequest("Invalid Record Id");
        }

        var salesChannel = await _repository.GetByIdAsync(id);

        if (salesChannel == null)
        {
            return NotFound();
        }

        _mapper.Map(salesChannelUpdateDto, salesChannel);

        try
        {
            await _repository.UpdateAsync(salesChannel);
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await SalesChannelExists(id))
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
    public async Task<ActionResult<SalesChannel>> PostSalesChannel(SalesChannelCreateDto salesChannelCreateDto)
    {
        var salesChannel = _mapper.Map<SalesChannel>(salesChannelCreateDto);

        await _repository.AddAsync(salesChannel);

        return CreatedAtAction("GetSalesChannel", new { id = salesChannel.Id }, salesChannel);
    }

    // DELETE: api/SalesChannel/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteSalesChannel(uint id)
    {
        var salesChannel = await _repository.GetByIdAsync(id);

        if (salesChannel == null)
        {
            return NotFound();
        }

        await _repository.DeleteAsync(id);

        return NoContent();
    }

    private async Task<bool> SalesChannelExists(uint id)
    {
        return await _repository.Exists(id);
    }
}