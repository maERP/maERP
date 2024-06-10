using Asp.Versioning;
using maERP.Application.Features.Warehouse.Commands.WarehouseCreate;
using maERP.Application.Features.Warehouse.Commands.WarehouseDelete;
using maERP.Application.Features.Warehouse.Commands.WarehouseUpdate;
using maERP.Application.Features.Warehouse.Queries.WarehouseDetail;
using maERP.Application.Features.Warehouse.Queries.WarehouseList;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace maERP.Server.Controllers.Api.V1;

[ApiController]
[Authorize]
[ApiVersion(1.0)]
[Route("/api/v{version:apiVersion}/[controller]")]
public class WarehousesController(IMediator mediator) : ControllerBase
{
    // GET: api/<WarehousesController>
    [HttpGet]
    public async Task<ActionResult<List<WarehouseListResponse>>> Get()
    {
        var warehouses = await mediator.Send(new WarehouseListQuery());
        return Ok(warehouses);
    }

    // GET api/<WarehousesController>/5
    [HttpGet("{id}")]
    public async Task<ActionResult<WarehouseDetailResponse>> GetDetails(int id)
    {
        var warehouse = await mediator.Send(new WarehouseDetailQuery { Id = id });
        return Ok(warehouse);
    }

    // POST api/<WarehousesController>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Create(WarehouseCreateCommand warehouseCreateCommand)
    {
        var response = await mediator.Send(warehouseCreateCommand);
        return CreatedAtAction(nameof(Get), new { id = response });
    }

    // PUT api/<WarehousesController>/5
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<WarehouseDetailResponse>> Update(int id, WarehouseUpdateCommand warehouseUpdateCommand)
    {
        warehouseUpdateCommand.Id = id;
        await mediator.Send(warehouseUpdateCommand);
        return NoContent();
    }

    // DELETE api/<WarehousesController>/5
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> Delete(int id)
    {
        var command = new WarehouseDeleteCommand { Id = id };
        await mediator.Send(command);
        return NoContent();
    }
}
