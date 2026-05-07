using maERP.Client.Core.Models;
using maERP.Client.Features.SalesChannels.Services;
using maERP.Client.Features.SalesChannelDashboards.Models;
using maERP.Client.Features.SalesChannelDashboards.Services;
using maERP.Domain.Dtos.SalesChannel;
using maERP.Domain.Enums;
using Microsoft.Extensions.Logging;
using Microsoft.UI;
using Microsoft.UI.Xaml.Media;
using Windows.UI;

namespace maERP.Client.Features.SalesChannels.Models;

/// <summary>
/// Model for the SalesChannel overview dashboard.
/// Aggregates revenue/sales KPIs across all sales channels and provides
/// per-channel cards that drill down into the channel-specific dashboard.
/// </summary>
public partial record SalesChannelOverviewModel
{
    private readonly ISalesChannelService _channelService;
    private readonly ISalesChannelStatisticsService _statisticsService;
    private readonly INavigator _navigator;
    private readonly ILogger<SalesChannelOverviewModel> _logger;

    public SalesChannelOverviewModel(
        ISalesChannelService channelService,
        ISalesChannelStatisticsService statisticsService,
        INavigator navigator,
        ILogger<SalesChannelOverviewModel> logger)
    {
        _channelService = channelService;
        _statisticsService = statisticsService;
        _navigator = navigator;
        _logger = logger;
    }

    public IFeed<SalesChannelOverviewData> Overview => Feed.Async(LoadOverviewAsync);

    public async ValueTask OpenChannel(SalesChannelOverviewItem item)
    {
        var data = new SalesChannelDashboardData(item.Id, item.Name, item.Type);
        switch (item.Type)
        {
            case SalesChannelType.PointOfSale:
                await _navigator.NavigateViewModelAsync<PosDashboardModel>(this, data: data);
                break;
            case SalesChannelType.Shopware5:
                await _navigator.NavigateViewModelAsync<Shopware5DashboardModel>(this, data: data);
                break;
            default:
                await _navigator.NavigateDataAsync(this, new SalesChannelDetailData(item.Id));
                break;
        }
    }

    public async ValueTask OpenChannelList()
    {
        await _navigator.NavigateViewModelAsync<SalesChannelListModel>(this);
    }

    private async ValueTask<SalesChannelOverviewData> LoadOverviewAsync(CancellationToken ct)
    {
        try
        {
            var channelsResponse = await _channelService.GetSalesChannelsAsync(
                new QueryParameters
                {
                    PageNumber = 0,
                    PageSize = 200,
                    SalesBy = "Name Ascending"
                },
                ct);

            var channels = channelsResponse.Data ?? new List<SalesChannelListDto>();
            if (channels.Count == 0)
            {
                return new SalesChannelOverviewData
                {
                    Channels = ImmutableList<SalesChannelOverviewItem>.Empty,
                    TotalChannels = 0,
                };
            }

            var channelStats = await Task.WhenAll(channels.Select(async ch =>
            {
                var sales = await _statisticsService.GetSalesTodayAsync(ch.Id, ct);
                var saless = await _statisticsService.GetSalessTodayAsync(ch.Id, ct);
                return (Channel: ch, Sales: sales, Saless: saless);
            }));

            var totalRevenueToday = channelStats.Sum(s => s.Sales?.RevenueToday ?? 0m);
            var totalRevenueThisWeek = channelStats.Sum(s => s.Sales?.RevenueThisWeek ?? 0m);
            var totalRevenueThisMonth = channelStats.Sum(s => s.Sales?.RevenueThisMonth ?? 0m);
            var totalSalessToday = channelStats.Sum(s => s.Saless?.SalessToday ?? 0);
            var totalSalessThisWeek = channelStats.Sum(s => s.Saless?.SalessThisWeek ?? 0);
            var totalSalessPending = channelStats.Sum(s => s.Saless?.SalessPending ?? 0);

            var items = channelStats
                .Select(s => new SalesChannelOverviewItem(
                    s.Channel.Id,
                    s.Channel.Name,
                    s.Channel.SalesChannelType,
                    s.Channel.Url,
                    s.Sales?.RevenueToday ?? 0m,
                    s.Sales?.RevenueThisWeek ?? 0m,
                    s.Sales?.RevenueThisMonth ?? 0m,
                    s.Sales?.RevenueChangePercent ?? 0m,
                    s.Saless?.SalessToday ?? 0,
                    s.Saless?.SalessThisWeek ?? 0,
                    s.Saless?.SalessPending ?? 0,
                    s.Saless?.SalessChangePercent ?? 0m,
                    totalRevenueThisMonth))
                .OrderByDescending(i => i.RevenueThisMonth)
                .ThenBy(i => i.Name)
                .ToImmutableList();

            var top = items.FirstOrDefault();
            var activeCount = items.Count(i => i.RevenueThisMonth > 0 || i.SalessThisWeek > 0);

            return new SalesChannelOverviewData
            {
                Channels = items,
                TotalChannels = channels.Count,
                ActiveChannels = activeCount,
                TotalRevenueToday = totalRevenueToday,
                TotalRevenueThisWeek = totalRevenueThisWeek,
                TotalRevenueThisMonth = totalRevenueThisMonth,
                TotalSalessToday = totalSalessToday,
                TotalSalessThisWeek = totalSalessThisWeek,
                TotalSalessPending = totalSalessPending,
                TopChannelName = top?.Name ?? string.Empty,
                TopChannelRevenueThisMonth = top?.RevenueThisMonth ?? 0m,
                TopChannelTypeLabel = top != null ? FormatType(top.Type) : string.Empty,
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error loading SalesChannel overview");
            throw;
        }
    }

    internal static string FormatType(SalesChannelType type) => type switch
    {
        SalesChannelType.PointOfSale => "POS",
        SalesChannelType.Shopware5 => "Shopware 5",
        SalesChannelType.Shopware6 => "Shopware 6",
        SalesChannelType.WooCommerce => "WooCommerce",
        SalesChannelType.eBay => "eBay",
        SalesChannelType.Amazon => "Amazon",
        _ => type.ToString()
    };
}

/// <summary>
/// Aggregate dashboard data for the SalesChannel overview page.
/// </summary>
public record SalesChannelOverviewData
{
    public IImmutableList<SalesChannelOverviewItem> Channels { get; init; } = ImmutableList<SalesChannelOverviewItem>.Empty;
    public int TotalChannels { get; init; }
    public int ActiveChannels { get; init; }
    public decimal TotalRevenueToday { get; init; }
    public decimal TotalRevenueThisWeek { get; init; }
    public decimal TotalRevenueThisMonth { get; init; }
    public int TotalSalessToday { get; init; }
    public int TotalSalessThisWeek { get; init; }
    public int TotalSalessPending { get; init; }
    public string TopChannelName { get; init; } = string.Empty;
    public decimal TopChannelRevenueThisMonth { get; init; }
    public string TopChannelTypeLabel { get; init; } = string.Empty;

    public bool HasChannels => TotalChannels > 0;
    public bool HasNoChannels => TotalChannels == 0;

    public string TotalRevenueTodayFormatted => TotalRevenueToday.ToString("C0");
    public string TotalRevenueThisWeekFormatted => TotalRevenueThisWeek.ToString("C0");
    public string TotalRevenueThisMonthFormatted => TotalRevenueThisMonth.ToString("C0");
    public string TopChannelRevenueFormatted => TopChannelRevenueThisMonth.ToString("C0");
    public string ActiveOfTotalFormatted => $"{ActiveChannels} / {TotalChannels}";
}

/// <summary>
/// Per-channel card data on the overview dashboard.
/// </summary>
public partial record SalesChannelOverviewItem(
    Guid Id,
    string Name,
    SalesChannelType Type,
    string Url,
    decimal RevenueToday,
    decimal RevenueThisWeek,
    decimal RevenueThisMonth,
    decimal RevenueChangePercent,
    int SalessToday,
    int SalessThisWeek,
    int SalessPending,
    decimal SalessChangePercent,
    decimal TotalRevenueThisMonthAcrossChannels)
{
    public string TypeLabel => SalesChannelOverviewModel.FormatType(Type);

    public string RevenueTodayFormatted => RevenueToday.ToString("C0");
    public string RevenueThisWeekFormatted => RevenueThisWeek.ToString("C0");
    public string RevenueThisMonthFormatted => RevenueThisMonth.ToString("C0");
    public string RevenueChangeFormatted =>
        $"{(RevenueChangePercent >= 0 ? "+" : string.Empty)}{RevenueChangePercent:F1}%";
    public bool RevenueChangePositive => RevenueChangePercent >= 0;

    public string SalessChangeFormatted =>
        $"{(SalessChangePercent >= 0 ? "+" : string.Empty)}{SalessChangePercent:F1}%";
    public bool SalessChangePositive => SalessChangePercent >= 0;

    public double SharePercent => TotalRevenueThisMonthAcrossChannels > 0
        ? (double)(RevenueThisMonth / TotalRevenueThisMonthAcrossChannels) * 100.0
        : 0.0;
    public string SharePercentFormatted => $"{SharePercent:F1}%";

    public SolidColorBrush TypeAccentBrush => Type switch
    {
        SalesChannelType.PointOfSale => new SolidColorBrush(Color.FromArgb(0xFF, 0x63, 0x66, 0xF1)),
        SalesChannelType.Shopware5 => new SolidColorBrush(Color.FromArgb(0xFF, 0x0E, 0xA5, 0xE9)),
        SalesChannelType.Shopware6 => new SolidColorBrush(Color.FromArgb(0xFF, 0x02, 0x84, 0xC7)),
        SalesChannelType.WooCommerce => new SolidColorBrush(Color.FromArgb(0xFF, 0x7C, 0x3A, 0xED)),
        SalesChannelType.eBay => new SolidColorBrush(Color.FromArgb(0xFF, 0xEA, 0xB3, 0x08)),
        SalesChannelType.Amazon => new SolidColorBrush(Color.FromArgb(0xFF, 0xF9, 0x73, 0x16)),
        _ => new SolidColorBrush(Color.FromArgb(0xFF, 0x64, 0x74, 0x8B))
    };
}
