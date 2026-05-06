using Asp.Versioning;
using maERP.Application.Features.Sales.Commands.SalesCreate;
using maERP.Application.Features.Sales.Commands.SalesDelete;
using maERP.Application.Features.Sales.Commands.SalesUpdate;
using maERP.Application.Features.Sales.Queries.SalesCustomerList;
using maERP.Application.Features.Sales.Queries.SalesDetail;
using maERP.Application.Features.Sales.Queries.SalesList;
using maERP.Application.Features.Sales.Queries.SalesNotPaidList;
using maERP.Application.Features.Sales.Queries.SalesReadyForDeliveryList;
using maERP.Domain.Dtos.Sales;
using maERP.Domain.Wrapper;
using maERP.Application.Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace maERP.Server.Controllers.Api.V1;

[ApiController]
[Authorize]
[ApiVersion(1.0)]
[Route("/api/v{version:apiVersion}/[controller]")]
public class SalessController(IMediator mediator) : ControllerBase
{
    // GET: api/v1/<SalessController>
    [HttpGet]
    public async Task<ActionResult<PaginatedResult<SalesListDto>>> GetAll(int pageNumber = 0, int pageSize = 10, string searchString = "", string salesBy = "", [FromQuery] Guid? salesChannelId = null)
    {
        if (string.IsNullOrEmpty(salesBy))
        {
            salesBy = "DateSalesed Descending";
        }

        var saless = await mediator.Send(new SalesListQuery(pageNumber, pageSize, searchString, salesBy, salesChannelId));
        return Ok(saless);
    }

    // GET: api/v1/<SalessController>/customer/{customerId}
    [HttpGet("customer/{customerId:int}")]
    public async Task<ActionResult<PaginatedResult<SalesListDto>>> GetByCustomer(int customerId, int pageNumber = 0, int pageSize = 10, string searchString = "", string salesBy = "")
    {
        if (string.IsNullOrEmpty(salesBy))
        {
            salesBy = "DateSalesed Descending";
        }

        var saless = await mediator.Send(new SalesCustomerListQuery(customerId, pageNumber, pageSize, searchString, salesBy));
        return Ok(saless);
    }

    // GET: api/v1/<SalessController>/ready-for-delivery
    [HttpGet("ready-for-delivery")]
    public async Task<ActionResult<PaginatedResult<SalesListDto>>> GetReadyForDelivery(int pageNumber = 0, int pageSize = 10, string salesBy = "")
    {
        if (string.IsNullOrEmpty(salesBy))
        {
            salesBy = "DateSalesed Descending";
        }

        var saless = await mediator.Send(new SalesReadyForDeliveryListQuery(pageNumber, pageSize, salesBy));
        return Ok(saless);
    }

    // GET: api/v1/<SalessController>/not-paid
    [HttpGet("not-paid")]
    public async Task<ActionResult<PaginatedResult<SalesListDto>>> GetNotPaid(int pageNumber = 0, int pageSize = 10, string salesBy = "")
    {
        if (string.IsNullOrEmpty(salesBy))
        {
            salesBy = "DateSalesed Descending";
        }

        var saless = await mediator.Send(new SalesNotPaidListQuery(pageNumber, pageSize, salesBy));
        return Ok(saless);
    }

    // GET: api/v1/<SalessController>/5
    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<SalesDetailDto>> GetDetails(Guid id)
    {
        var response = await mediator.Send(new SalesDetailQuery { Id = id });
        return StatusCode((int)response.StatusCode, response);
    }

    // POST: api/v1/<SalessController>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<int>> Create(SalesCreateCommand salesCreateCommand)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        var response = await mediator.Send(salesCreateCommand);
        return StatusCode((int)response.StatusCode, response);
    }

    // PUT: api/v1/<SalessController>/5
    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> Update(Guid id, SalesUpdateCommand salesUpdateCommand)
    {
        // Check for ID mismatch
        if (salesUpdateCommand.Id != Guid.Empty && salesUpdateCommand.Id != id)
        {
            return BadRequest("ID mismatch between route and payload");
        }
        
        salesUpdateCommand.Id = id;
        var response = await mediator.Send(salesUpdateCommand);
        return StatusCode((int)response.StatusCode, response);
    }

    // DELETE: api/v1/<SalesController>/5
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> Delete(Guid id)
    {
        var command = new DeleteSalesCommand { Id = id };
        await mediator.Send(command);
        return NoContent();
    }
}