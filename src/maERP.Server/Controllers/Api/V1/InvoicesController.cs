using Asp.Versioning;
using maERP.Application.Features.Invoice.Commands.InvoiceCreate;
using maERP.Application.Features.Invoice.Commands.InvoiceDelete;
using maERP.Application.Features.Invoice.Commands.InvoiceUpdate;
using maERP.Application.Features.Invoice.Queries.InvoiceDetail;
using maERP.Application.Features.Invoice.Queries.InvoiceList;
using maERP.Domain.Dtos.Invoice;
using maERP.Domain.Wrapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace maERP.Server.Controllers.Api.V1;

[ApiController]
[Authorize]
[ApiVersion(1.0)]
[Route("/api/v{version:apiVersion}/[controller]")]
public class InvoicesController(IMediator mediator) : ControllerBase
{
    // GET: api/v1/<InvoiceController>
    [HttpGet]
    public async Task<ActionResult<PaginatedResult<InvoiceListDto>>> GetAll(int pageNumber = 0, int pageSize = 10, string searchString = "", string orderBy = "")
    {
        if (string.IsNullOrEmpty(orderBy))
        {
            orderBy = "InvoiceDate Descending";
        }

        var invoices = await mediator.Send(new InvoiceListQuery(pageNumber, pageSize, searchString, orderBy));
        return Ok(invoices);
    }

    // GET: api/v1/<InvoiceController>/5
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<InvoiceDetailDto>> GetDetails(int id)
    {
        var response = await mediator.Send(new InvoiceDetailQuery { Id = id });
        return StatusCode((int)response.StatusCode, response);
    }

    // POST: api/v1/<InvoiceController>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<int>> Create(InvoiceCreateCommand invoiceCreateCommand)
    {
        var response = await mediator.Send(invoiceCreateCommand);
        return StatusCode((int)response.StatusCode, response);
    }

    // PUT: api/v1/<InvoiceController>/5
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> Update(int id, InvoiceUpdateCommand invoiceUpdateCommand)
    {
        invoiceUpdateCommand.Id = id;
        var response = await mediator.Send(invoiceUpdateCommand);
        return StatusCode((int)response.StatusCode, response);
    }

    // DELETE: api/v1/<InvoiceController>/5
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> Delete(int id)
    {
        var command = new InvoiceDeleteCommand { Id = id };
        var response = await mediator.Send(command);
        return StatusCode((int)response.StatusCode, response);
    }
}
