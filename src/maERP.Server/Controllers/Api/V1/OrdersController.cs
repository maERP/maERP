using Asp.Versioning;
using maERP.Application.Features.Order.Commands.OrderCreate;
using maERP.Application.Features.Order.Commands.OrderDelete;
using maERP.Application.Features.Order.Commands.OrderUpdate;
using maERP.Application.Features.Order.Queries.OrderCustomerList;
using maERP.Application.Features.Order.Queries.OrderDetail;
using maERP.Application.Features.Order.Queries.OrderList;
using maERP.Application.Features.Order.Queries.OrderNotPaidList;
using maERP.Application.Features.Order.Queries.OrderReadyForDeliveryList;
using maERP.Domain.Dtos.Order;
using maERP.Domain.Wrapper;
using maERP.Application.Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace maERP.Server.Controllers.Api.V1;

[ApiController]
[Authorize]
[ApiVersion(1.0)]
[Route("/api/v{version:apiVersion}/[controller]")]
public class OrdersController(IMediator mediator) : ControllerBase
{
    // GET: api/v1/<OrdersController>
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

    // GET: api/v1/<OrdersController>/customer/{customerId}
    [HttpGet("customer/{customerId:guid}")]
    public async Task<ActionResult<PaginatedResult<OrderListDto>>> GetByCustomer(Guid customerId, int pageNumber = 0, int pageSize = 10, string searchString = "", string orderBy = "")
    {
        if (string.IsNullOrEmpty(orderBy))
        {
            orderBy = "DateOrdered Descending";
        }

        var orders = await mediator.Send(new OrderCustomerListQuery(customerId, pageNumber, pageSize, searchString, orderBy));
        return Ok(orders);
    }

    // GET: api/v1/<OrdersController>/readyfordelivery
    [HttpGet("readyfordelivery")]
    public async Task<ActionResult<PaginatedResult<OrderListDto>>> GetReadyForDelivery(int pageNumber = 0, int pageSize = 10, string orderBy = "")
    {
        if (string.IsNullOrEmpty(orderBy))
        {
            orderBy = "DateOrdered Descending";
        }

        var orders = await mediator.Send(new OrderReadyForDeliveryListQuery(pageNumber, pageSize, orderBy));
        return Ok(orders);
    }

    // GET: api/v1/<OrdersController>/notpaid
    [HttpGet("notpaid")]
    public async Task<ActionResult<PaginatedResult<OrderListDto>>> GetNotPaid(int pageNumber = 0, int pageSize = 10, string orderBy = "")
    {
        if (string.IsNullOrEmpty(orderBy))
        {
            orderBy = "DateOrdered Descending";
        }

        var orders = await mediator.Send(new OrderNotPaidListQuery(pageNumber, pageSize, orderBy));
        return Ok(orders);
    }

    // GET: api/v1/<OrdersController>/5
    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<OrderDetailDto>> GetDetails(Guid id)
    {
        var response = await mediator.Send(new OrderDetailQuery { Id = id });
        return StatusCode((int)response.StatusCode, response);
    }

    // POST: api/v1/<OrdersController>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<int>> Create(OrderCreateCommand orderCreateCommand)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        var response = await mediator.Send(orderCreateCommand);
        return StatusCode((int)response.StatusCode, response);
    }

    // PUT: api/v1/<OrdersController>/5
    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> Update(Guid id, OrderUpdateCommand orderUpdateCommand)
    {
        // Check for ID mismatch
        if (orderUpdateCommand.Id != Guid.Empty && orderUpdateCommand.Id != id)
        {
            return BadRequest("ID mismatch between route and payload");
        }
        
        orderUpdateCommand.Id = id;
        var response = await mediator.Send(orderUpdateCommand);
        return StatusCode((int)response.StatusCode, response);
    }

    // DELETE: api/v1/<OrderController>/5
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> Delete(Guid id)
    {
        var command = new DeleteOrderCommand { Id = id };
        await mediator.Send(command);
        return NoContent();
    }
}