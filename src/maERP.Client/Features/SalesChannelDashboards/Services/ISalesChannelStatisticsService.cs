using maERP.Domain.Dtos.Statistic;

namespace maERP.Client.Features.SalesChannelDashboards.Services;

/// <summary>
/// Service interface for SalesChannel-filtered statistics API operations.
/// </summary>
public interface ISalesChannelStatisticsService
{
    /// <summary>
    /// Gets the revenue/sales statistics for today, filtered by SalesChannel.
    /// </summary>
    Task<SalesTodayDto?> GetSalesTodayAsync(Guid salesChannelId, CancellationToken ct = default);

    /// <summary>
    /// Gets the orders statistics for today, filtered by SalesChannel.
    /// </summary>
    Task<OrdersTodayDto?> GetOrdersTodayAsync(Guid salesChannelId, CancellationToken ct = default);

    /// <summary>
    /// Gets the latest orders, filtered by SalesChannel.
    /// </summary>
    Task<OrdersLatestDto?> GetOrdersLatestAsync(Guid salesChannelId, int count = 5, CancellationToken ct = default);
}
