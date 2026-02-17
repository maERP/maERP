using maERP.Client.Features.Dashboard.Models;
using maERP.Client.Features.SalesChannelDashboards.Services;
using Microsoft.Extensions.Logging;

namespace maERP.Client.Features.SalesChannelDashboards.Models;

/// <summary>
/// Model for Shopware5 Dashboard page using MVUX pattern.
/// Provides KPIs for a specific Shopware5 SalesChannel.
/// </summary>
public partial record Shopware5DashboardModel
{
    private readonly ISalesChannelStatisticsService _statisticsService;
    private readonly INavigator _navigator;
    private readonly IStringLocalizer _localizer;
    private readonly ILogger<Shopware5DashboardModel> _logger;
    private readonly Guid _salesChannelId;

    public string Title { get; }

    public Shopware5DashboardModel(
        ISalesChannelStatisticsService statisticsService,
        INavigator navigator,
        IStringLocalizer localizer,
        ILogger<Shopware5DashboardModel> logger,
        SalesChannelDashboardData? data = null)
    {
        _statisticsService = statisticsService;
        _navigator = navigator;
        _localizer = localizer;
        _logger = logger;
        _salesChannelId = data?.SalesChannelId ?? Guid.Empty;
        Title = data?.SalesChannelName ?? "Shopware 5";
    }

    // Tab 1: Dashboard KPIs
    public IFeed<RevenueKpiData> RevenueData => Feed.Async(LoadRevenueDataAsync);
    public IFeed<OrdersKpiData> OrdersData => Feed.Async(LoadOrdersDataAsync);

    private async ValueTask<RevenueKpiData> LoadRevenueDataAsync(CancellationToken ct)
    {
        try
        {
            var data = await _statisticsService.GetSalesTodayAsync(_salesChannelId, ct);
            if (data == null) return new RevenueKpiData();

            return new RevenueKpiData
            {
                RevenueToday = data.RevenueToday,
                RevenueThisWeek = data.RevenueThisWeek,
                RevenueThisMonth = data.RevenueThisMonth,
                RevenueChange = data.RevenueChangePercent
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error loading revenue KPI data for SalesChannel {SalesChannelId}", _salesChannelId);
            throw;
        }
    }

    private async ValueTask<OrdersKpiData> LoadOrdersDataAsync(CancellationToken ct)
    {
        try
        {
            var data = await _statisticsService.GetOrdersTodayAsync(_salesChannelId, ct);
            if (data == null) return new OrdersKpiData();

            return new OrdersKpiData
            {
                OrdersToday = data.OrdersToday,
                OrdersPending = data.OrdersPending,
                OrdersThisWeek = data.OrdersThisWeek,
                OrdersChange = data.OrdersChangePercent
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error loading orders KPI data for SalesChannel {SalesChannelId}", _salesChannelId);
            throw;
        }
    }
}
