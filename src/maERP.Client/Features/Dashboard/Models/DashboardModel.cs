using maERP.Client.Features.Dashboard.Services;
using maERP.Client.Features.Saless;
using maERP.Client.Features.Saless.Models;
using maERP.Client.Features.Products.Models;
using maERP.Domain.Enums;
using Microsoft.Extensions.Logging;

namespace maERP.Client.Features.Dashboard.Models;

/// <summary>
/// Model for dashboard page using MVUX pattern.
/// Provides KPIs, recent activity, and quick actions for ERP overview.
/// </summary>
public partial record DashboardModel
{
    private readonly IStringLocalizer _localizer;
    private readonly IStatisticsService _statisticsService;
    private readonly INavigator _navigator;
    private readonly ILogger<DashboardModel> _logger;

    public DashboardModel(
        IStringLocalizer localizer,
        IStatisticsService statisticsService,
        INavigator navigator,
        ILogger<DashboardModel> logger)
    {
        _localizer = localizer;
        _statisticsService = statisticsService;
        _navigator = navigator;
        _logger = logger;
    }

    public async ValueTask NavigateToSalesList()
    {
        await _navigator.NavigateViewModelAsync<SalesListModel>(this);
    }

    public async ValueTask NavigateToProductList()
    {
        await _navigator.NavigateViewModelAsync<ProductListModel>(this);
    }

    public async ValueTask ViewSales(RecentSalesItem sales)
    {
        await _navigator.NavigateDataAsync(this, new SalesDetailData(sales.Id));
    }

    // KPI Data Feeds - four separate feeds for parallel loading
    public IFeed<RevenueKpiData> RevenueData => Feed.Async(LoadRevenueDataAsync);
    public IFeed<SalessKpiData> SalessData => Feed.Async(LoadSalessDataAsync);
    public IFeed<CustomersKpiData> CustomersData => Feed.Async(LoadCustomersDataAsync);
    public IFeed<ProductsKpiData> ProductsData => Feed.Async(LoadProductsDataAsync);

    // Recent Saless Feed
    public IListFeed<RecentSalesItem> RecentSaless => ListFeed.Async(LoadRecentSalessAsync);

    // Top Selling Products Feed
    public IListFeed<TopProductItem> TopProducts => ListFeed.Async(LoadTopProductsAsync);

    // Low Stock Alerts Feed
    public IListFeed<LowStockItem> LowStockAlerts => ListFeed.Async(LoadLowStockAlertsAsync);

    private async ValueTask<RevenueKpiData> LoadRevenueDataAsync(CancellationToken ct)
    {
        try
        {
            _logger.LogInformation("Loading revenue KPI data");
            var data = await _statisticsService.GetSalesTodayAsync(ct);

            if (data == null)
            {
                _logger.LogWarning("SalesToday service returned null");
                return new RevenueKpiData();
            }

            _logger.LogInformation("Revenue KPI loaded - RevenueToday: {RevenueToday}", data.RevenueToday);

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
            _logger.LogError(ex, "Error loading revenue KPI data");
            throw;
        }
    }

    private async ValueTask<SalessKpiData> LoadSalessDataAsync(CancellationToken ct)
    {
        try
        {
            _logger.LogInformation("Loading saless KPI data");
            var data = await _statisticsService.GetSalessTodayAsync(ct);

            if (data == null)
            {
                _logger.LogWarning("SalessToday service returned null");
                return new SalessKpiData();
            }

            _logger.LogInformation("Saless KPI loaded - SalessToday: {SalessToday}", data.SalessToday);

            return new SalessKpiData
            {
                SalessToday = data.SalessToday,
                SalessPending = data.SalessPending,
                SalessThisWeek = data.SalessThisWeek,
                SalessChange = data.SalessChangePercent
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error loading saless KPI data");
            throw;
        }
    }

    private async ValueTask<CustomersKpiData> LoadCustomersDataAsync(CancellationToken ct)
    {
        try
        {
            _logger.LogInformation("Loading customers KPI data");
            var data = await _statisticsService.GetCustomersTodayAsync(ct);

            if (data == null)
            {
                _logger.LogWarning("CustomersToday service returned null");
                return new CustomersKpiData();
            }

            _logger.LogInformation("Customers KPI loaded - CustomersTotal: {CustomersTotal}", data.CustomersTotal);

            return new CustomersKpiData
            {
                CustomersTotal = data.CustomersTotal,
                CustomersNewThisMonth = data.CustomersNewThisMonth,
                CustomersChange = data.CustomersChangePercent
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error loading customers KPI data");
            throw;
        }
    }

    private async ValueTask<ProductsKpiData> LoadProductsDataAsync(CancellationToken ct)
    {
        try
        {
            _logger.LogInformation("Loading products KPI data");
            var data = await _statisticsService.GetProductsTodayAsync(ct);

            if (data == null)
            {
                _logger.LogWarning("ProductsToday service returned null");
                return new ProductsKpiData();
            }

            _logger.LogInformation("Products KPI loaded - ProductsTotal: {ProductsTotal}", data.ProductsTotal);

            return new ProductsKpiData
            {
                ProductsInStock = data.ProductsInStock,
                ProductsTotal = data.ProductsTotal,
                ProductsLowStock = data.ProductsLowStock,
                ProductsChange = data.ProductsChangePercent
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error loading products KPI data");
            throw;
        }
    }

    private async ValueTask<IImmutableList<RecentSalesItem>> LoadRecentSalessAsync(CancellationToken ct)
    {
        try
        {
            _logger.LogInformation("Loading recent saless");
            var data = await _statisticsService.GetSalessLatestAsync(5, ct);

            if (data == null || data.Saless.Count == 0)
            {
                _logger.LogWarning("SalessLatest service returned null or empty");
                return ImmutableList<RecentSalesItem>.Empty;
            }

            var saless = data.Saless.Select(o => new RecentSalesItem
            {
                Id = o.Id,
                SalesNumber = o.SalesNumber,
                CustomerName = o.CustomerName,
                Amount = o.Amount,
                Status = o.Status,
                SalesDate = o.SalesDate
            }).ToImmutableList();

            _logger.LogInformation("Successfully loaded {Count} recent saless", saless.Count);
            return saless;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error loading recent saless");
            throw;
        }
    }

    private async ValueTask<IImmutableList<TopProductItem>> LoadTopProductsAsync(CancellationToken ct)
    {
        try
        {
            _logger.LogInformation("Loading top products");
            var data = await _statisticsService.GetProductsBestSellingAsync(5, ct);

            if (data == null || data.Products.Count == 0)
            {
                _logger.LogWarning("ProductsBestSelling service returned null or empty");
                return ImmutableList<TopProductItem>.Empty;
            }

            var products = data.Products.Select(p => new TopProductItem
            {
                Rank = p.Rank,
                ProductName = p.ProductName,
                Sku = p.Sku,
                QuantitySold = p.QuantitySold,
                Revenue = p.Revenue
            }).ToImmutableList();

            _logger.LogInformation("Successfully loaded {Count} top products", products.Count);
            return products;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error loading top products");
            throw;
        }
    }

    private async ValueTask<IImmutableList<LowStockItem>> LoadLowStockAlertsAsync(CancellationToken ct)
    {
        // Simulate API call delay
        await Task.Delay(350, ct);

        // Return dummy data
        var alerts = new List<LowStockItem>
        {
            new() { ProductName = "Wireless Mouse Pro", Sku = "WMP-012", CurrentStock = 3, MinimumStock = 20, Severity = AlertSeverity.Critical },
            new() { ProductName = "HDMI Cable 3m", Sku = "HDM-023", CurrentStock = 8, MinimumStock = 25, Severity = AlertSeverity.Warning },
            new() { ProductName = "USB Hub 4-Port", Sku = "USH-034", CurrentStock = 12, MinimumStock = 30, Severity = AlertSeverity.Warning },
            new() { ProductName = "Webcam HD 1080p", Sku = "WCH-045", CurrentStock = 5, MinimumStock = 15, Severity = AlertSeverity.Critical }
        };

        return alerts.ToImmutableList();
    }
}

/// <summary>
/// Revenue KPI data for the first dashboard card.
/// </summary>
public record RevenueKpiData
{
    public decimal RevenueToday { get; init; }
    public decimal RevenueThisWeek { get; init; }
    public decimal RevenueThisMonth { get; init; }
    public decimal RevenueChange { get; init; }

    public string RevenueTodayFormatted => RevenueToday.ToString("C0");
    public string RevenueThisWeekFormatted => RevenueThisWeek.ToString("C0");
    public string RevenueChangeFormatted => $"{(RevenueChange >= 0 ? "+" : "")}{RevenueChange:F1}%";
    public bool RevenueChangePositive => RevenueChange >= 0;
}

/// <summary>
/// Saless KPI data for the second dashboard card.
/// </summary>
public record SalessKpiData
{
    public int SalessToday { get; init; }
    public int SalessPending { get; init; }
    public int SalessThisWeek { get; init; }
    public decimal SalessChange { get; init; }

    public string SalessChangeFormatted => $"{(SalessChange >= 0 ? "+" : "")}{SalessChange:F1}%";
    public bool SalessChangePositive => SalessChange >= 0;
}

/// <summary>
/// Customers KPI data for the third dashboard card.
/// </summary>
public record CustomersKpiData
{
    public int CustomersTotal { get; init; }
    public int CustomersNewThisMonth { get; init; }
    public decimal CustomersChange { get; init; }

    public string CustomersChangeFormatted => $"{(CustomersChange >= 0 ? "+" : "")}{CustomersChange:F1}%";
    public bool CustomersChangePositive => CustomersChange >= 0;
}

/// <summary>
/// Products/Inventory KPI data for the fourth dashboard card.
/// </summary>
public record ProductsKpiData
{
    public int ProductsInStock { get; init; }
    public int ProductsTotal { get; init; }
    public int ProductsLowStock { get; init; }
    public decimal ProductsChange { get; init; }

    public string ProductsChangeFormatted => $"{(ProductsChange >= 0 ? "+" : "")}{ProductsChange:F1}%";
    public bool ProductsChangePositive => ProductsChange >= 0;
    public string StockRatio => $"{ProductsInStock} / {ProductsTotal}";
}

/// <summary>
/// Recent sales item for the activity list.
/// </summary>
public partial record RecentSalesItem
{
    public Guid Id { get; init; }
    public string SalesNumber { get; init; } = string.Empty;
    public string CustomerName { get; init; } = string.Empty;
    public decimal Amount { get; init; }
    public SalesStatus Status { get; init; }
    public DateTime SalesDate { get; init; }

    public string AmountFormatted => Amount.ToString("C2");
    public string DateFormatted => SalesDate.ToString("g");
    public string StatusIcon => Status switch
    {
        SalesStatus.Pending => "\uE823",           // Clock
        SalesStatus.Processing => "\uE895",        // Sync
        SalesStatus.ReadyForDelivery => "\uE7B8",  // Package
        SalesStatus.PartiallyDelivered => "\uE122", // Airplane
        SalesStatus.Completed => "\uE73E",         // Checkmark
        SalesStatus.Cancelled => "\uE711",         // Cancel
        SalesStatus.Returned => "\uE72B",          // Back
        SalesStatus.Refunded => "\uE8BB",          // Money
        SalesStatus.OnHold => "\uE769",            // Pause
        SalesStatus.Failed => "\uE783",            // Error
        _ => "\uE946"                              // Unknown
    };
}

/// <summary>
/// Top selling product item.
/// </summary>
public record TopProductItem
{
    public int Rank { get; init; }
    public string ProductName { get; init; } = string.Empty;
    public string Sku { get; init; } = string.Empty;
    public int QuantitySold { get; init; }
    public decimal Revenue { get; init; }

    public string RevenueFormatted => Revenue.ToString("C0");
    public string RankFormatted => $"#{Rank}";
}

/// <summary>
/// Low stock alert item.
/// </summary>
public record LowStockItem
{
    public string ProductName { get; init; } = string.Empty;
    public string Sku { get; init; } = string.Empty;
    public int CurrentStock { get; init; }
    public int MinimumStock { get; init; }
    public AlertSeverity Severity { get; init; }

    public string StockDisplay => $"{CurrentStock} / {MinimumStock}";
    public string SeverityIcon => Severity switch
    {
        AlertSeverity.Critical => "\uE7BA",   // Warning filled
        AlertSeverity.Warning => "\uE7BA",    // Warning
        _ => "\uE946"                         // Info
    };
}

public enum AlertSeverity
{
    Info,
    Warning,
    Critical
}
