using Asp.Versioning;
using maERP.Application.Features.Statistic.Queries.CustomersToday;
using maERP.Application.Features.Statistic.Queries.OrdersLatest;
using maERP.Application.Features.Statistic.Queries.OrdersToday;
using maERP.Application.Features.Statistic.Queries.ProductsBestSelling;
using maERP.Application.Features.Statistic.Queries.ProductsToday;
using maERP.Application.Features.Statistic.Queries.SalesToday;
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
    // GET: api/v1/<StatisticsController>/SalesToday
    [HttpGet("SalesToday")]
    public async Task<ActionResult<Result<SalesTodayDto>>> SalesToday([FromQuery] Guid? salesChannelId = null)
    {
        var result = await mediator.Send(new SalesTodayQuery(salesChannelId));

        if (!result.Succeeded)
            return StatusCode((int)result.StatusCode, result);

        return Ok(result);
    }

    // GET: api/v1/<StatisticsController>/OrdersToday
    [HttpGet("OrdersToday")]
    public async Task<ActionResult<Result<OrdersTodayDto>>> OrdersToday([FromQuery] Guid? salesChannelId = null)
    {
        var result = await mediator.Send(new OrdersTodayQuery(salesChannelId));

        if (!result.Succeeded)
            return StatusCode((int)result.StatusCode, result);

        return Ok(result);
    }

    // GET: api/v1/<StatisticsController>/CustomersToday
    [HttpGet("CustomersToday")]
    public async Task<ActionResult<Result<CustomersTodayDto>>> CustomersToday()
    {
        var result = await mediator.Send(new CustomersTodayQuery());

        if (!result.Succeeded)
            return StatusCode((int)result.StatusCode, result);

        return Ok(result);
    }

    // GET: api/v1/<StatisticsController>/ProductsToday
    [HttpGet("ProductsToday")]
    public async Task<ActionResult<Result<ProductsTodayDto>>> ProductsToday()
    {
        var result = await mediator.Send(new ProductsTodayQuery());

        if (!result.Succeeded)
            return StatusCode((int)result.StatusCode, result);

        return Ok(result);
    }

    // GET: api/v1/<StatisticsController>/OrdersLatest
    [HttpGet("OrdersLatest")]
    public async Task<ActionResult<Result<OrdersLatestDto>>> OrdersLatest([FromQuery] int count = 5, [FromQuery] Guid? salesChannelId = null)
    {
        var result = await mediator.Send(new OrdersLatestQuery(count, salesChannelId));

        if (!result.Succeeded)
            return StatusCode((int)result.StatusCode, result);

        return Ok(result);
    }

    // GET: api/v1/<StatisticsController>/ProductsBestSelling
    [HttpGet("ProductsBestSelling")]
    public async Task<ActionResult<Result<ProductsBestSellingDto>>> ProductsBestSelling([FromQuery] int count = 5)
    {
        var result = await mediator.Send(new ProductsBestSellingQuery(count));

        if (!result.Succeeded)
            return StatusCode((int)result.StatusCode, result);

        return Ok(result);
    }

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