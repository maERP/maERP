using Asp.Versioning;
using maERP.Application.Features.Statistic.Queries.StatisticOrder;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace maERP.Server.Controllers.Api.V1;

[ApiController]
[Authorize]
[ApiVersion(1.0)]
[Route("/api/v{version:apiVersion}/[controller]")]
public class StatisticController(IMediator _mediator) : ControllerBase
{
    // GET: api/<StatisticController>
    [HttpGet("OrderStatistic")]
    public async Task<ActionResult<StatisticOrderResponse>> OrderStatistic()
    {
        var statistic = await _mediator.Send(new StatisticOrderQuery());
        return Ok(statistic);
    }
}