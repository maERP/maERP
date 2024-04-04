using maERP.Application.Features.Warehouse.Commands.CreateWarehouseCommand;
using maERP.Application.Features.Warehouse.Queries.GetAllWarehouses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace maERP.Server.Controllers.Api;

[Route("api/[controller]")]
[ApiController]
public class WarehousesController : ControllerBase
{
    private readonly IMediator _mediator;

    public WarehousesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    // GET: api/<WarehousesController>
    [HttpGet]
    public async Task<List<WarehouseListDto>> Get()
    {
        var warehouses = await _mediator.Send(new GetAllWarehousesQuery());
        return warehouses;
    }

    // GET api/<WarehousesController>/5
    [HttpGet("{id}")]
    public string Get(int id)
    {
        return "value";
    }

    // POST api/<WarehousesController>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Post(CreateWarehouseCommand warehouseCommand)
    {
        var response = await _mediator.Send(warehouseCommand);
        return CreatedAtAction(nameof(Get), new { id = response });
    }

    // PUT api/<WarehousesController>/5
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> Put(int id, [FromBody] string value)
    {
        // await _mediator.Send(new UpdateWarehouseCommand { Id = id, Name = value });
        return NoContent();
    }

    // DELETE api/<WarehousesController>/5
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> Delete(int id)
    {
        //var command = new DeleteWarehouseCommand { Id = id };
        // await _mediator.Send(command);
        return NoContent();
    }
}
