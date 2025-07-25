using Asp.Versioning;
using maERP.Application.Features.Warehouse.Commands.WarehouseCreate;
using maERP.Application.Features.Warehouse.Commands.WarehouseDelete;
using maERP.Application.Features.Warehouse.Commands.WarehouseUpdate;
using maERP.Application.Features.Warehouse.Queries.WarehouseDetail;
using maERP.Application.Features.Warehouse.Queries.WarehouseList;
using maERP.Domain.Dtos.Warehouse;
using maERP.Domain.Wrapper;
using maERP.Application.Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace maERP.Server.Controllers.Api.V1;

[ApiController]
[Authorize]
[ApiVersion(1.0)]
[Route("/api/v{version:apiVersion}/[controller]")]
public class WarehousesController(IMediator mediator) : ControllerBase
{
    // GET: api/v1/<WarehousesController>
    [HttpGet]
    public async Task<ActionResult<PaginatedResult<WarehouseListDto>>> GetAll(int pageNumber = 0, int pageSize = 10, string searchString = "", string orderBy = "")
    {
        if (string.IsNullOrEmpty(orderBy))
        {
            orderBy = "DateCreated Descending";
        }

        var response = await mediator.Send(new WarehouseListQuery(pageNumber, pageSize, searchString, orderBy));
        return StatusCode((int)response.StatusCode, response);
    }

    // GET: api/v1/<WarehousesController>/5
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<WarehouseDetailDto>> GetDetails(int id)
    { 
        var response = await mediator.Send(new WarehouseDetailQuery { Id = id });
        return StatusCode((int)response.StatusCode, response);
    }

    // POST: api/v1/<WarehousesController>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Create(WarehouseCreateCommand warehouseCreateCommand)
    {
        var response = await mediator.Send(warehouseCreateCommand);
        return StatusCode((int)response.StatusCode, response);
    }

    // PUT: api/v1/<WarehousesController>/5
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<WarehouseDetailDto>> Update(int id, WarehouseUpdateCommand warehouseUpdateCommand)
    {
        warehouseUpdateCommand.Id = id;
        var response = await mediator.Send(warehouseUpdateCommand);
        return StatusCode((int)response.StatusCode, response);
    }

    // DELETE: api/v1/<WarehousesController>/5?newWarehouseId=2
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> Delete(int id, [FromQuery] int? newWarehouseId = null)
    {
        var command = new WarehouseDeleteCommand { Id = id, NewWarehouseId = newWarehouseId };
        var response = await mediator.Send(command);
        
        if (response.Succeeded)
        {
            return NoContent();
        }
        
        return StatusCode((int)response.StatusCode, response);
    }
}
