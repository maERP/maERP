using Asp.Versioning;
using maERP.Application.Features.TaxClass.Commands.TaxClassCreate;
using maERP.Application.Features.TaxClass.Commands.TaxClassDelete;
using maERP.Application.Features.TaxClass.Commands.TaxClassUpdate;
using maERP.Application.Features.TaxClass.Queries.TaxClassDetail;
using maERP.Application.Features.TaxClass.Queries.TaxClassList;
using maERP.Domain.Dtos.TaxClass;
using maERP.Domain.Wrapper;
using maERP.Application.Mediator;
using maERP.Server.Extensions;
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

    // GET: api/v1/<TaxClassesController>
    [HttpGet]
    public async Task<ActionResult<PaginatedResult<TaxClassListDto>>> GetAll(int pageNumber = 0, int pageSize = 10, string searchString = "", string orderBy = "")
    {
        if (string.IsNullOrEmpty(orderBy))
        {
            orderBy = "Id Ascending";
        }

        var response = await _mediator.Send(new TaxClassListQuery(pageNumber, pageSize, searchString, orderBy));
        return StatusCode((int)response.StatusCode, response);
    }

    // GET api/TaxClassesController>/5
    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TaxClassDetailDto>> GetDetails(Guid id)
    {
        var response = await _mediator.Send(new TaxClassDetailQuery { Id = id });
        return StatusCode((int)response.StatusCode, response);
    }

    // POST: api/v1/<TaxClassesController>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<int>> Create(TaxClassCreateCommand taxClassCreateCommand)
    {
        var response = await _mediator.Send(taxClassCreateCommand);
        return StatusCode((int)response.StatusCode, response);
    }

    // PUT: api/v1/<TaxClassesController>/5
    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> Update(Guid id, TaxClassUpdateCommand taxClassUpdateCommand)
    {
        taxClassUpdateCommand.Id = id;
        var response = await _mediator.Send(taxClassUpdateCommand);
        return StatusCode((int)response.StatusCode, response);
    }

    // DELETE: api/v1/<TaxClassesController>/5
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> Delete(Guid id)
    {
        var command = new TaxClassDeleteCommand { Id = id };
        var response = await _mediator.Send(command);
        return response.ToActionResult();
    }
}
