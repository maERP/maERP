using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using maERP.Server.Data;
using maERP.Server.Models.Warehouse;
using AutoMapper;
using maERP.Server.Contracts;
using Microsoft.AspNetCore.OData.Query;
using maERP.Server.Models;

namespace maERP.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class WarehousesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IWarehousesRepository _repository;

        public WarehousesController(IMapper mapper, IWarehousesRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        // GET: api/Warehouses
        [HttpGet("GetAll")]
        // GET: api/Warehouses?$select=id,name&$filter=name eq 'Testprodukt'&$orderby=name
        [EnableQuery] 
        public async Task<ActionResult<IEnumerable<GetWarehouseDto>>> GetWarehouse()
        {
            var warehouses = await _repository.GetAllAsync<GetWarehouseDto>();
            return Ok(warehouses);
        }

        // GET: api/Warehouses/?StartIndex=0&PageSize=25&PageNumber=1
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Warehouse>>> GetPagedWarehouse([FromQuery] QueryParameters queryParameters)
        {
            var pagedWarehousesResult = await _repository.GetAllAsync<GetWarehouseDto>(queryParameters);
            return Ok(pagedWarehousesResult);
        }

        // GET: api/Warehouses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BaseWarehouseDto>> GetWarehouse(int id)
        {
            var warehouse = await _repository.GetDetails(id);
            return Ok(warehouse);
        }

        // PUT: api/Warehouses/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWarehouse(int id, UpdateWarehouseDto updateWarehouseDto)
        {
            if (id != updateWarehouseDto.Id)
            {
                return BadRequest("Invalid Record Id");
            }

            try
            {
                await _repository.UpdateAsync(id, updateWarehouseDto);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await WarehouseExists(id))
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

        // POST: api/Warehouses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<WarehouseDto>> PostWarehouse(CreateWarehouseDto createWarehouseDto)
        {
            var warehouse = await _repository.AddAsync<CreateWarehouseDto, GetWarehouseDto>(createWarehouseDto);
            return CreatedAtAction(nameof(GetWarehouse), new { id = warehouse.Id }, warehouse);
        }

        // DELETE: api/Warehouses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWarehouse(int id)
        {
            await _repository.DeleteAsync(id);

            return NoContent();
        }

        private async Task<bool> WarehouseExists(int id)
        {
            return await _repository.Exists(id);
        }
    }
}