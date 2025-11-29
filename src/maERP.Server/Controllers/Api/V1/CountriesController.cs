using Asp.Versioning;
using maERP.Application.Features.Country.Commands.CountryCreate;
using maERP.Application.Features.Country.Commands.CountryDelete;
using maERP.Application.Features.Country.Commands.CountryUpdate;
using maERP.Application.Features.Country.Queries.CountryDetail;
using maERP.Application.Features.Country.Queries.CountryList;
using maERP.Domain.Dtos.Country;
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
public class CountriesController : ControllerBase
{
    private readonly IMediator _mediator;

    public CountriesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    // GET: api/v1/<CountriesController>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<PaginatedResult<CountryListDto>>> GetAll(
        int pageNumber = 0,
        int pageSize = 300,
        string searchString = "",
        string orderBy = "")
    {
        if (string.IsNullOrEmpty(orderBy))
        {
            orderBy = "Name";
        }

        var response = await _mediator.Send(new CountryListQuery(pageNumber, pageSize, searchString, orderBy));
        return response.ToActionResult();
    }

    // GET api/<CountriesController>/5
    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CountryDetailDto>> GetDetails(Guid id)
    {
        var response = await _mediator.Send(new CountryDetailQuery { Id = id });
        return response.ToActionResult();
    }

    // POST: api/v1/<CountriesController>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Guid>> Create(CountryCreateCommand countryCreateCommand)
    {
        var response = await _mediator.Send(countryCreateCommand);
        return response.ToActionResult();
    }

    // PUT: api/v1/<CountriesController>/5
    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> Update(Guid id, CountryUpdateCommand countryUpdateCommand)
    {
        countryUpdateCommand.Id = id;
        var response = await _mediator.Send(countryUpdateCommand);
        return response.ToActionResult();
    }

    // DELETE: api/v1/<CountriesController>/5
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> Delete(Guid id)
    {
        var command = new CountryDeleteCommand { Id = id };
        var response = await _mediator.Send(command);
        return response.ToActionResult();
    }
}
