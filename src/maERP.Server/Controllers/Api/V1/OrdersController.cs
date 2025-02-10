using Asp.Versioning;
using maERP.Application.Exceptions;
using maERP.Application.Features.Order.Commands.OrderCreate;
using maERP.Application.Features.Order.Commands.OrderDelete;
using maERP.Application.Features.Order.Commands.OrderUpdate;
using maERP.Application.Features.Order.Queries.OrderDetail;
using maERP.Application.Features.Order.Queries.OrderList;
using maERP.Domain.Dtos.Order;
using maERP.Domain.Wrapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace maERP.Server.Controllers.Api.V1;

[ApiController]
[Authorize]
[ApiVersion(1.0)]
[Route("/api/v{version:apiVersion}/[controller]")]
public class OrdersController(IMediator mediator) : ControllerBase
{
    // GET: api/<OrdersController>
    [HttpGet]
    public async Task<ActionResult<PaginatedResult<OrderListDto>>> GetAll(int pageNumber = 0, int pageSize = 10, string searchString = "", string orderBy = "")
    {
        if (string.IsNullOrEmpty(orderBy))
        {
            orderBy = "DateOrdered Descending";
        }

        var orders = await mediator.Send(new OrderListQuery(pageNumber, pageSize, searchString, orderBy));
        return Ok(orders);
    }

    // GET api/<OrdersController>/5
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<OrderDetailDto>> GetDetails(int id)
    {
        try 
        {
            var order = await mediator.Send(new OrderDetailQuery { Id = id });
            return Ok(order);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    // POST api/<OrdersController>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<int>> Create(OrderCreateCommand orderCreateCommand)
    {
        var response = await mediator.Send(orderCreateCommand);
        return CreatedAtAction(nameof(GetDetails), new { id = response });
    }

    // PUT: api/<OrdersController>/5
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> Update(int id, OrderUpdateCommand orderUpdateCommand)
    {
        orderUpdateCommand.Id = id;
        await mediator.Send(orderUpdateCommand);
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