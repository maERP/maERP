using Asp.Versioning;
using maERP.Application.Features.Order.Queries.OrderList;
using maERP.Application.Features.TaxClass.Commands.TaxClassCreate;
using maERP.Application.Features.TaxClass.Commands.TaxClassDelete;
using maERP.Application.Features.TaxClass.Commands.TaxClassUpdate;
using maERP.Application.Features.TaxClass.Queries.TaxClassDetail;
using maERP.Application.Features.TaxClass.Queries.TaxClassList;
using maERP.Shared.Wrapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;

namespace maERP.Server.Controllers.Api.V1;

[ApiController]
[Authorize]
[ApiVersion(1.0)]
[Route("/api/v{version:apiVersion}/[controller]")]
public class TaxClassesController : ControllerBase
{
    private readonly IMediator _mediator;

    public TaxClassesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    // GET: api/<TaxClassesController>
    [HttpGet]
    public async Task<ActionResult<PaginatedResult<TaxClassListResponse>>> GetAll(int pageNumber = 0, int pageSize = 10, string searchString = "", string orderBy = "")
    {
        if (string.IsNullOrEmpty(orderBy))
        {
            orderBy = "DateCreated Descending";
        }

        var taxClasses = await _mediator.Send(new TaxClassListQuery(pageNumber, pageSize, searchString, orderBy));
        return Ok(taxClasses);
    }

    // GET api/TaxClassesController>/5
    [HttpGet("{id}")]
    public async Task<ActionResult<TaxClassDetailResponse>> GetDetails(int id)
    {
        var taxClass = await _mediator.Send(new TaxClassDetailQuery { Id = id });
        return Ok(taxClass);
    }

    // POST api/<TaxClassesController>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<int>> Create(TaxClassCreateCommand taxClassCreateCommand)
    {
        var response = await _mediator.Send(taxClassCreateCommand);
        return CreatedAtAction(nameof(GetDetails), new { id = response });
    }

    // PUT api/<TaxClassesController>/5
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> Update(int id, TaxClassUpdateCommand taxClassUpdateCommand)
    {
        taxClassUpdateCommand.Id = id;
        await _mediator.Send(taxClassUpdateCommand);
        return NoContent();
    }

    // DELETE api/<TaxClassesController>/5
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> Delete(int id)
    {
        var command = new TaxClassDeleteCommand { Id = id };
        await _mediator.Send(command);
        return NoContent();
    }
}
