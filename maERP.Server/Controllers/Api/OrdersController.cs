using maERP.Application.Dtos.Order;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using maERP.Application.Features.Order.Commands.CreateOrderCommand;
using maERP.Application.Features.Order.Commands.DeleteOrderCommand;
using maERP.Application.Features.Order.Commands.UpdateOrderCommand;
using maERP.Application.Features.Order.Queries.GetOrderDetailQuery;
using maERP.Application.Features.Order.Queries.GetOrdersQuery;

namespace maERP.Server.Controllers.Api;

[Route("api/[controller]")]
[ApiController]
public class OrdersController(IMediator mediator) : ControllerBase
{
    // GET: api/<OrdersController>
    [HttpGet]
    public async Task<List<OrderListDto>> Get()
    {
        var orders = await mediator.Send(new GetOrdersQuery());
        return orders;
    }

    // GET api/<OrdersController>/5
    [HttpGet("{id}")]
    public async Task<OrderDetailDto> GetDetails(int id)
    {
        return await mediator.Send(new GetOrderDetailQuery() { Id = id });
    }

    // POST api/<OrdersController>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Post(CreateOrderCommand createOrderCommand)
    {
        var response = await mediator.Send(createOrderCommand);
        return CreatedAtAction(nameof(Get), new { id = response });
    }

    // PUT: api/<OrdersController>/5
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> Put(UpdateOrderCommand updateOrderCommand)
    {
        await mediator.Send(updateOrderCommand);
        return NoContent();
    }

    // DELETE api/<OrderController>/5
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> Delete(int id)
    {
        var command = new DeleteOrderCommand { Id = id };
        await mediator.Send(command);
        return NoContent();
    }
}