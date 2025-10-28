using maERP.Application.Features.Statistic.Queries.StatisticOrderCustomerChart;
using maERP.Domain.Dtos.Statistic;
using maERP.Domain.Wrapper;

namespace maERP.Client.Services.Api.Clients;

/// <summary>
/// Implementation of statistics API client
/// </summary>
public class StatisticsApiClient : ApiClientBase, IStatisticsApiClient
{
    private const string BaseEndpoint = "api/v1/Statistics";

    public StatisticsApiClient(HttpClient httpClient, ILogger<StatisticsApiClient> logger)
        : base(httpClient, logger)
    {
    }

    public async Task<Result<StatisticOrderDto>?> GetOrderStatisticsAsync(CancellationToken cancellationToken = default)
    {
        return await GetAsync<Result<StatisticOrderDto>>($"{BaseEndpoint}/OrderStatistic", cancellationToken);
    }

    public async Task<Result<StatisticProductDto>?> GetProductStatisticsAsync(CancellationToken cancellationToken = default)
    {
        return await GetAsync<Result<StatisticProductDto>>($"{BaseEndpoint}/ProductStatistic", cancellationToken);
    }

    public async Task<Result<StatisticOrderCustomerChartResponse>?> GetOrderCustomerChartAsync(CancellationToken cancellationToken = default)
    {
        return await GetAsync<Result<StatisticOrderCustomerChartResponse>>($"{BaseEndpoint}/OrderCustomerChart", cancellationToken);
    }

    public async Task<Result<StatisticSalesDto>?> GetSalesStatisticsAsync(CancellationToken cancellationToken = default)
    {
        return await GetAsync<Result<StatisticSalesDto>>($"{BaseEndpoint}/SalesStatistic", cancellationToken);
    }

    public async Task<Result<StatisticMostSellingProductsDto>?> GetMostSellingProductsAsync(CancellationToken cancellationToken = default)
    {
        return await GetAsync<Result<StatisticMostSellingProductsDto>>($"{BaseEndpoint}/MostSellingProducts", cancellationToken);
    }
}
