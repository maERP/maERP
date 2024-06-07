using Asp.Versioning;
using maERP.Application.Features.Warehouse.Commands.CreateWarehouse;
using maERP.Application.Features.Warehouse.Commands.DeleteWarehouse;
using maERP.Application.Features.Warehouse.Commands.UpdateWarehouse;
using maERP.Application.Features.Warehouse.Queries.GetWarehouseDetail;
using maERP.Application.Features.Warehouse.Queries.GetWarehouses;
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
    public async Task<ActionResult<List<GetWarehousesResponse>>> Get()
    {
        var warehouses = await mediator.Send(new GetWarehousesQuery());
        return Ok(warehouses);
    }

    // GET api/<WarehousesController>/5
    [HttpGet("{id}")]
    public async Task<ActionResult<GetWarehouseDetailResponse>> GetDetails(int id)
    {
        var warehouse = await mediator.Send(new GetWarehouseDetailQuery { Id = id });
        return Ok(warehouse);
    }

    // POST api/<WarehousesController>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Create(CreateWarehouseCommand createWarehouseCommand)
    {
        var response = await mediator.Send(createWarehouseCommand);
        return CreatedAtAction(nameof(Get), new { id = response });
    }

    // PUT api/<WarehousesController>/5
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<GetWarehouseDetailResponse>> Update(int id, UpdateWarehouseCommand updateWarehouseCommand)
    {
        updateWarehouseCommand.Id = id;
        await mediator.Send(updateWarehouseCommand);
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
        var command = new DeleteWarehouseCommand { Id = id };
        await mediator.Send(command);
        return NoContent();
    }
}
