using Asp.Versioning;
using maERP.Application.Features.Statistic.Queries.StatisticOrder;
using maERP.Application.Features.Statistic.Queries.StatisticProduct;
using maERP.Domain.Dtos.Statistic;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace maERP.Server.Controllers.Api.V1;

[ApiController]
[Authorize]
[ApiVersion(1.0)]
[Route("/api/v{version:apiVersion}/[controller]")]
public class StatisticController(IMediator mediator) : ControllerBase
{
    // GET: api/<StatisticController>
    [HttpGet("OrderStatistic")]
    public async Task<ActionResult<StatisticOrderDto>> OrderStatistic()
    {
        var statistic = await mediator.Send(new StatisticOrderQuery());
        return Ok(statistic);
    }

    // GET: api/<StatisticController>
    [HttpGet("ProductStatistic")]
    public async Task<ActionResult<StatisticProductDto>> ProductStatistic()
    {
        var statistic = await mediator.Send(new StatisticProductQuery());
        return Ok(statistic);
    }
}