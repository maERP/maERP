using maERP.Application.Features.Statistic.Queries.StatisticOrderCustomerChart;
using maERP.Domain.Dtos.Statistic;
using maERP.Domain.Wrapper;

namespace maERP.Client.Services.Api.Clients;

/// <summary>
/// API client for statistics operations
/// </summary>
public interface IStatisticsApiClient
{
    /// <summary>
    /// Get order statistics
    /// </summary>
    Task<Result<StatisticOrderDto>?> GetOrderStatisticsAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Get product statistics
    /// </summary>
    Task<Result<StatisticProductDto>?> GetProductStatisticsAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Get order customer chart data
    /// </summary>
    Task<Result<StatisticOrderCustomerChartResponse>?> GetOrderCustomerChartAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Get sales statistics
    /// </summary>
    Task<Result<StatisticSalesDto>?> GetSalesStatisticsAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Get most selling products
    /// </summary>
    Task<Result<StatisticMostSellingProductsDto>?> GetMostSellingProductsAsync(CancellationToken cancellationToken = default);
}
