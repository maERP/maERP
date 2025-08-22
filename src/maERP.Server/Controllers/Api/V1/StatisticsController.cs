using Asp.Versioning;
using maERP.Application.Features.Statistic.Queries.StatisticOrder;
using maERP.Application.Features.Statistic.Queries.StatisticOrderCustomerChart;
using maERP.Application.Features.Statistic.Queries.StatisticProduct;
using maERP.Application.Features.Statistic.Queries.StatisticSales;
using maERP.Application.Features.Statistic.Queries.StatisticMostSellingProducts;
using maERP.Domain.Dtos.Statistic;
using maERP.Domain.Wrapper;
using maERP.Application.Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace maERP.Server.Controllers.Api.V1;

[ApiController]
[Authorize]
[ApiVersion(1.0)]
[Route("/api/v{version:apiVersion}/[controller]")]
public class StatisticsController(IMediator mediator) : ControllerBase
{
    // GET: api/v1/<StatisticsController>
    [HttpGet("OrderStatistic")]
    public async Task<ActionResult<Result<StatisticOrderDto>>> OrderStatistic()
    {
        var result = await mediator.Send(new StatisticOrderQuery());

        if (!result.Succeeded)
            return StatusCode((int)result.StatusCode, result);

        return Ok(result);
    }

    // GET: api/v1/<StatisticsController>
    [HttpGet("ProductStatistic")]
    public async Task<ActionResult<Result<StatisticProductDto>>> ProductStatistic()
    {
        var result = await mediator.Send(new StatisticProductQuery());

        if (!result.Succeeded)
            return StatusCode((int)result.StatusCode, result);

        return Ok(result);
    }

    // GET: api/v1/<StatisticsController>
    [HttpGet("OrderCustomerChart")]
    public async Task<ActionResult<Result<StatisticOrderCustomerChartResponse>>> OrderCustomerChart()
    {
        var result = await mediator.Send(new StatisticOrderCustomerChartQuery());

        if (!result.Succeeded)
            return StatusCode((int)result.StatusCode, result);

        return Ok(result);
    }

    // GET: api/v1/<StatisticsController>
    [HttpGet("SalesStatistic")]
    public async Task<ActionResult<Result<StatisticSalesDto>>> SalesStatistic()
    {
        var result = await mediator.Send(new StatisticSalesQuery());

        if (!result.Succeeded)
            return StatusCode((int)result.StatusCode, result);

        return Ok(result);
    }

    // GET: api/v1/<StatisticsController>
    [HttpGet("MostSellingProducts")]
    public async Task<ActionResult<Result<StatisticMostSellingProductsDto>>> MostSellingProducts()
    {
        var result = await mediator.Send(new StatisticMostSellingProductsQuery());

        if (!result.Succeeded)
            return StatusCode((int)result.StatusCode, result);

        return Ok(result);
    }
}