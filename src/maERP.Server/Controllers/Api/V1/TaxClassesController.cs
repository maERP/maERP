using Asp.Versioning;
using maERP.Application.Features.TaxClass.Commands.TaxClassCreate;
using maERP.Application.Features.TaxClass.Commands.TaxClassDelete;
using maERP.Application.Features.TaxClass.Commands.TaxClassUpdate;
using maERP.Application.Features.TaxClass.Queries.TaxClassDetail;
using maERP.Application.Features.TaxClass.Queries.TaxClassList;
using maERP.Domain.Dtos.TaxClass;
using maERP.Domain.Wrapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
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
    public async Task<ActionResult<PaginatedResult<TaxClassListDto>>> GetAll(int pageNumber = 0, int pageSize = 10, string searchString = "", string orderBy = "")
    {
        if (string.IsNullOrEmpty(orderBy))
        {
            orderBy = "DateCreated Descending";
        }

        var response = await _mediator.Send(new TaxClassListQuery(pageNumber, pageSize, searchString, orderBy));
        return StatusCode((int)response.StatusCode, response);
    }

    // GET api/TaxClassesController>/5
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TaxClassDetailDto>> GetDetails(int id)
    {
        var response = await _mediator.Send(new TaxClassDetailQuery { Id = id });
        return StatusCode((int)response.StatusCode, response);
    }

    // POST api/<TaxClassesController>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<int>> Create(TaxClassCreateCommand taxClassCreateCommand)
    {
        var response = await _mediator.Send(taxClassCreateCommand);
        return StatusCode((int)response.StatusCode, response);
    }

    // PUT api/<TaxClassesController>/5
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> Update(int id, TaxClassInputCommand taxClassInputCommand)
    {
        taxClassInputCommand.Id = id;
        var response = await _mediator.Send(taxClassInputCommand);
        return StatusCode((int)response.StatusCode, response);
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
