using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using maERP.Shared.Models;
using maERP.Shared.Dtos.Warehouse;
using AutoMapper;
using maERP.Server.Contracts;
using Microsoft.AspNetCore.OData.Query;
using maERP.Server.Models;

namespace maERP.Server.Controllers;

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

    // GET: api/Warehouse/?StartIndex=0&PageSize=25&PageNumber=1
    [HttpGet]
    public async Task<ActionResult<IEnumerable<WarehouseListDto>>> GetPagedWarehouse([FromQuery] QueryParameters queryParameters)
    {
        var pagedWarehouseResult = await _repository.GetAllAsync<WarehouseListDto>(queryParameters);
        return Ok(pagedWarehouseResult);
    }

    // GET: api/Warehouse/5
    [HttpGet("{id}")]
    public async Task<ActionResult<WarehouseDetailDto>> GetWarehouse(int id)
    {
        var warehouse = await _repository.GetDetails(id);
        return Ok(warehouse);
    }

    // PUT: api/Warehouse/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutWarehouse(int id, WarehouseDetailDto warehouseDto)
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