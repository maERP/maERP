using Asp.Versioning;
using maERP.Application.Features.Invoice.Commands.InvoiceCreate;
using maERP.Application.Features.Invoice.Commands.InvoiceDelete;
using maERP.Application.Features.Invoice.Commands.InvoiceUpdate;
using maERP.Application.Features.Invoice.Queries.InvoiceDetail;
using maERP.Application.Features.Invoice.Queries.InvoiceList;
using maERP.Application.Features.Invoice.Queries.InvoicePdf;
using maERP.Domain.Dtos.Invoice;
using maERP.Domain.Wrapper;
using maERP.Application.Mediator;
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
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<PaginatedResult<InvoiceListDto>>> GetAll(int pageNumber = 0, int pageSize = 10, string searchString = "", string orderBy = "")
    {
        // Validate pagination parameters
        if (pageNumber < 0)
        {
            var errorResult = new Result<PaginatedResult<InvoiceListDto>>();
            errorResult.Succeeded = false;
            errorResult.StatusCode = ResultStatusCode.BadRequest;
            errorResult.Messages.Add("PageNumber muss größer oder gleich 0 sein.");
            return BadRequest(errorResult);
        }

        if (pageSize < 1)
        {
            var errorResult = new Result<PaginatedResult<InvoiceListDto>>();
            errorResult.Succeeded = false;
            errorResult.StatusCode = ResultStatusCode.BadRequest;
            errorResult.Messages.Add("PageSize muss größer als 0 sein.");
            return BadRequest(errorResult);
        }

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
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<InvoiceDetailDto>> GetDetails(string id)
    {
        if (!Guid.TryParse(id, out var guidId))
        {
            var errorResult = new Result<InvoiceDetailDto>();
            errorResult.Succeeded = false;
            errorResult.StatusCode = ResultStatusCode.BadRequest;
            errorResult.Messages.Add("Ungültige ID-Format. Eine gültige GUID ist erforderlich.");
            return BadRequest(errorResult);
        }

        var response = await mediator.Send(new InvoiceDetailQuery { Id = guidId });
        return StatusCode((int)response.StatusCode, response);
    }

    // GET: api/v1/<InvoiceController>/5/pdf
    [HttpGet("{id}/pdf")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetPdf(string id)
    {
        if (!Guid.TryParse(id, out var guidId))
        {
            var errorResult = new Result<byte[]>();
            errorResult.Succeeded = false;
            errorResult.StatusCode = ResultStatusCode.BadRequest;
            errorResult.Messages.Add("Ungültige ID-Format. Eine gültige GUID ist erforderlich.");
            return BadRequest(errorResult);
        }

        var response = await mediator.Send(new InvoicePdfQuery { Id = guidId });

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
    public async Task<ActionResult> Update(string id, InvoiceUpdateCommand invoiceUpdateCommand)
    {
        if (!Guid.TryParse(id, out var guidId))
        {
            var errorResult = new Result();
            errorResult.Succeeded = false;
            errorResult.StatusCode = ResultStatusCode.BadRequest;
            errorResult.Messages.Add("Ungültige ID-Format. Eine gültige GUID ist erforderlich.");
            return BadRequest(errorResult);
        }

        if (invoiceUpdateCommand.Id != Guid.Empty && invoiceUpdateCommand.Id != guidId)
        {
            var mismatchResult = await Result<Guid>.FailAsync(ResultStatusCode.BadRequest, "Die in der Anfrage angegebene ID stimmt nicht mit der URL überein.");
            return BadRequest(mismatchResult);
        }

        invoiceUpdateCommand.Id = guidId;
        var response = await mediator.Send(invoiceUpdateCommand);
        return StatusCode((int)response.StatusCode, response);
    }

    // DELETE: api/v1/<InvoiceController>/5
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> Delete(string id)
    {
        if (!Guid.TryParse(id, out var guidId))
        {
            var errorResult = new Result();
            errorResult.Succeeded = false;
            errorResult.StatusCode = ResultStatusCode.BadRequest;
            errorResult.Messages.Add("Ungültige ID-Format. Eine gültige GUID ist erforderlich.");
            return BadRequest(errorResult);
        }

        var command = new InvoiceDeleteCommand { Id = guidId };
        var response = await mediator.Send(command);
        return StatusCode((int)response.StatusCode, response);
    }
}
