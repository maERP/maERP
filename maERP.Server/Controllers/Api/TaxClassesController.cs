using MediatR;
using Microsoft.AspNetCore.Mvc;
using maERP.Application.Dtos.TaxClass;
using maERP.Application.Features.TaxClass.Commands.CreateTaxClassCommand;
using maERP.Application.Features.TaxClass.Commands.DeleteTaxClassCommand;
using maERP.Application.Features.TaxClass.Commands.UpdateTaxClassCommand;
using maERP.Application.Features.TaxClass.Queries.GetAllTaxClassesQuery;
using maERP.Application.Features.TaxClass.Queries.GetTaxClassQuery;

namespace maERP.Server.Controllers.Api;

[Route("api/[controller]")]
[ApiController]
public class TaxClassController : ControllerBase
{
    private readonly IMediator _mediator;

    public TaxClassController(IMediator mediator)
    {
        _mediator = mediator;
    }

    // GET: api/<TaxClassesController>
    [HttpGet]
    public async Task<List<TaxClassListDto>> GetAll()
    {
        var taxClasses = await _mediator.Send(new GetAllTaxClassesQuery());
        return taxClasses;
    }

    // GET api/TaxClassesController>/5
    [HttpGet("{id}")]
    public async Task<TaxClassDetailDto> GetDetails(int id)
    {
        return await _mediator.Send(new GetTaxClassQuery { Id = id });
    }

    // POST api/<TaxClassesController>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Post(CreateTaxClassesCommand taxClassCommand)
    {
        var response = await _mediator.Send(taxClassCommand);
        return CreatedAtAction(nameof(GetDetails), new { id = response });
    }

    // PUT api/<TaxClassesController>/5
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> Put(int id, [FromBody] string value)
    {
        await _mediator.Send(new UpdateTaxClassesCommand { Id = id, Name = value });
        return NoContent();
    }

    // DELETE api/<TaxClassesController>/5
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> Delete(int id)
    {
        var command = new DeleteTaxClassesCommand { Id = id };
        await _mediator.Send(command);
        return NoContent();
    }
}
