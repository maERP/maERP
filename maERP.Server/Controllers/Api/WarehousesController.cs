using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using maERP.Server.Repository;
using maERP.Shared.Dtos.Warehouse;

namespace maERP.Server.Controllers.Api;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class WarehousesController : ControllerBase
{
    private readonly IWarehouseRepository _repository;

    public WarehousesController(IWarehouseRepository repository)
    {
        _repository = repository;
    }

    // GET: api/Warehouses
    [HttpGet("GetAll")]
    // GET: api/Warehouses?$select=id,name&$filter=name eq 'Testprodukt'&$orderby=name
    [EnableQuery] 
    public async Task<ActionResult<IEnumerable<WarehouseListDto>>> GetWarehouse()
    {
        var warehouse = await _repository.GetAllAsync<WarehouseListDto>();
        return Ok(warehouse);
    }

    // GET: api/Warehouses/5
    [HttpGet("{id}")]
    public async Task<ActionResult<WarehouseDetailDto>> GetWarehouse(int id)
    {
        var warehouse = await _repository.GetByIdAsync(id);
        return Ok(warehouse);
    }

    // PUT: api/Warehouses/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutWarehouse(int id, [FromBody] WarehouseUpdateDto taxClassUpdateDto)
    {
        if (await _repository.Exists(id) == true)
        {
            await _repository.UpdateAsync<WarehouseUpdateDto>(id, taxClassUpdateDto);
        }
        else {
            return NotFound();
        }

        return NoContent();
    }

    // POST: api/Warehouses
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<WarehouseDetailDto>> PostWarehouse(WarehouseCreateDto warehouseDto)
    {
        var warehouse = await _repository.AddAsync<WarehouseCreateDto, WarehouseDetailDto>(warehouseDto);
        return CreatedAtAction(nameof(GetWarehouse), new { id = warehouse.Id }, warehouse);
    }

    // DELETE: api/Warehouses/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteWarehouse(int id)
    {
        await _repository.DeleteAsync(id);
        return NoContent();
    }
}