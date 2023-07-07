using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using maERP.Server.Models;
using maERP.Server.Repository;
using maERP.Shared.Dtos.Warehouse;
using maERP.Shared.Models;

namespace maERP.Server.Controllers.Api;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class WarehouseController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IWarehouseRepository _repository;

    public WarehouseController(IMapper mapper, IWarehouseRepository repository)
    {
        _mapper = mapper;
        _repository = repository;
    }

    // GET: api/Warehouse
    [HttpGet("GetAll")]
    // GET: api/Warehouse?$select=id,name&$filter=name eq 'Testprodukt'&$orderby=name
    [EnableQuery] 
    public async Task<ActionResult<IEnumerable<WarehouseListDto>>> GetWarehouse()
    {
        var warehouse = await _repository.GetAllAsync<WarehouseListDto>();
        return Ok(warehouse);
    }

    // GET: api/Warehouse/5
    [HttpGet("{id}")]
    public async Task<ActionResult<WarehouseDetailDto>> GetWarehouse(uint id)
    {
        var warehouse = await _repository.GetDetails(id);
        return Ok(warehouse);
    }

    // PUT: api/Warehouse/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutWarehouse(uint id, WarehouseDetailDto warehouseDto)
    {
        try
        {
            await _repository.UpdateAsync(id, warehouseDto);
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

    // POST: api/Warehouse
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<WarehouseDetailDto>> PostWarehouse(WarehouseDetailDto warehouseDto)
    {
        var warehouse = await _repository.AddAsync<WarehouseDetailDto, WarehouseDetailDto>(warehouseDto);
        return CreatedAtAction(nameof(GetWarehouse), new { id = warehouse.Id }, warehouse);
    }

    // DELETE: api/Warehouse/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteWarehouse(uint id)
    {
        await _repository.DeleteAsync(id);

        return NoContent();
    }

    private async Task<bool> WarehouseExists(uint id)
    {
        return await _repository.Exists(id);
    }
}