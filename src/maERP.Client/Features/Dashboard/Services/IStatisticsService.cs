using maERP.Domain.Dtos.Statistic;

namespace maERP.Client.Features.Dashboard.Services;

/// <summary>
/// Service interface for statistics-related API operations.
/// </summary>
public interface IStatisticsService
{
    /// <summary>
    /// Gets the revenue/sales statistics for today.
    /// </summary>
    Task<SalesTodayDto?> GetSalesTodayAsync(CancellationToken ct = default);

    /// <summary>
    /// Gets the orders statistics for today.
    /// </summary>
    Task<OrdersTodayDto?> GetOrdersTodayAsync(CancellationToken ct = default);

    /// <summary>
    /// Gets the customer statistics.
    /// </summary>
    Task<CustomersTodayDto?> GetCustomersTodayAsync(CancellationToken ct = default);

    /// <summary>
    /// Gets the product/inventory statistics.
    /// </summary>
    Task<ProductsTodayDto?> GetProductsTodayAsync(CancellationToken ct = default);

    /// <summary>
    /// Gets the latest orders.
    /// </summary>
    Task<OrdersLatestDto?> GetOrdersLatestAsync(int count = 5, CancellationToken ct = default);

    /// <summary>
    /// Gets the best-selling products.
    /// </summary>
    Task<ProductsBestSellingDto?> GetProductsBestSellingAsync(int count = 5, CancellationToken ct = default);
}
