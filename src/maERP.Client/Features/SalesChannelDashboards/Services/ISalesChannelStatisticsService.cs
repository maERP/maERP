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
    /// Gets the saless statistics for today, filtered by SalesChannel.
    /// </summary>
    Task<SalessTodayDto?> GetSalessTodayAsync(Guid salesChannelId, CancellationToken ct = default);

    /// <summary>
    /// Gets the latest saless, filtered by SalesChannel.
    /// </summary>
    Task<SalessLatestDto?> GetSalessLatestAsync(Guid salesChannelId, int count = 5, CancellationToken ct = default);
}
