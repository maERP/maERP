using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using maERP.Server.Data;
using maERP.Server.Models;
using AutoMapper;
using maERP.Server.Contracts;

namespace maERP.Server.Controllers
{
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
        public async Task<ActionResult<IEnumerable<SalesChannel>>> GetSalesChannel()
        {
            var salesChannels = await _repository.GetAllAsync();
            var records = _mapper.Map<List<GetSalesChannelDto>>(salesChannels);
            return Ok(records);
        }

        // GET: api/SalesChannels/?StartIndex=0&PageSize=25&PageNumber=1
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SalesChannel>>> GetPagedSalesChannel([FromQuery] QueryParameters queryParameters)
        {
            var pagedSalesChannelsResult = await _repository.GetAllAsync<GetSalesChannelDto>(queryParameters);
            return Ok(pagedSalesChannelsResult);
        }

        // GET: api/SalesChannel/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SalesChannel>> GetSalesChannel(int id)
        {
            var salesChannel = await _repository.getDetails(id);

            if (salesChannel == null)
            {
                return NotFound();
            }

            var salesChannelDto = _mapper.Map<GetSalesChannelDto>(salesChannel);

            return Ok(salesChannelDto);
        }

        // PUT: api/SalesChannels/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSalesChannel(int id, UpdateSalesChannelDto updateSalesChannelDto)
        {
            if (id != updateSalesChannelDto.Id)
            {
                return BadRequest("Invalid Record Id");
            }

            var salesChannel = await _repository.GetAsync(id);

            if (salesChannel == null)
            {
                return NotFound();
            }

            _mapper.Map(updateSalesChannelDto, salesChannel);

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

        // POST: api/SalesChannels
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<SalesChannel>> PostSalesChannel(CreateSalesChannelDto createSalesChannelDto)
        {
            var salesChannel = _mapper.Map<SalesChannel>(createSalesChannelDto);

            await _repository.AddAsync(salesChannel);

            return CreatedAtAction("GetSalesChannel", new { id = salesChannel.Id }, salesChannel);
        }

        // DELETE: api/SalesChannels/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSalesChannel(int id)
        {
            var salesChannel = await _repository.GetAsync(id);

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
}
