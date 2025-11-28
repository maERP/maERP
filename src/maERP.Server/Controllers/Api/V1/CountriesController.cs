using Asp.Versioning;
using maERP.Application.Features.Country.Queries.CountryList;
using maERP.Domain.Dtos.Country;
using maERP.Domain.Wrapper;
using maERP.Application.Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace maERP.Server.Controllers.Api.V1;

[ApiController]
[Authorize]
[ApiVersion(1.0)]
[Route("/api/v{version:apiVersion}/[controller]")]
public class CountriesController(IMediator mediator) : ControllerBase
{
    // GET: api/v1/<CountriesController>
    [HttpGet]
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

        var response = await mediator.Send(new CountryListQuery(pageNumber, pageSize, searchString, orderBy));
        return StatusCode((int)response.StatusCode, response);
    }
}
