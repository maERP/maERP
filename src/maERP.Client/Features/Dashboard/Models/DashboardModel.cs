using maERP.Client.Features.Dashboard.Services;
using maERP.Client.Features.Orders;
using maERP.Client.Features.Orders.Models;
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

    public async ValueTask NavigateToOrderList()
    {
        await _navigator.NavigateViewModelAsync<OrderListModel>(this);
    }

    public async ValueTask NavigateToProductList()
    {
        await _navigator.NavigateViewModelAsync<ProductListModel>(this);
    }

    public async ValueTask ViewOrder(RecentOrderItem order)
    {
        await _navigator.NavigateDataAsync(this, new OrderDetailData(order.Id));
    }

    // KPI Data Feeds - four separate feeds for parallel loading
    public IFeed<RevenueKpiData> RevenueData => Feed.Async(LoadRevenueDataAsync);
    public IFeed<OrdersKpiData> OrdersData => Feed.Async(LoadOrdersDataAsync);
    public IFeed<CustomersKpiData> CustomersData => Feed.Async(LoadCustomersDataAsync);
    public IFeed<ProductsKpiData> ProductsData => Feed.Async(LoadProductsDataAsync);

    // Recent Orders Feed
    public IListFeed<RecentOrderItem> RecentOrders => ListFeed.Async(LoadRecentOrdersAsync);

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

    private async ValueTask<OrdersKpiData> LoadOrdersDataAsync(CancellationToken ct)
    {
        try
        {
            _logger.LogInformation("Loading orders KPI data");
            var data = await _statisticsService.GetOrdersTodayAsync(ct);

            if (data == null)
            {
                _logger.LogWarning("OrdersToday service returned null");
                return new OrdersKpiData();
            }

            _logger.LogInformation("Orders KPI loaded - OrdersToday: {OrdersToday}", data.OrdersToday);

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
            _logger.LogError(ex, "Error loading orders KPI data");
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

    private async ValueTask<IImmutableList<RecentOrderItem>> LoadRecentOrdersAsync(CancellationToken ct)
    {
        try
        {
            _logger.LogInformation("Loading recent orders");
            var data = await _statisticsService.GetOrdersLatestAsync(5, ct);

            if (data == null || data.Orders.Count == 0)
            {
                _logger.LogWarning("OrdersLatest service returned null or empty");
                return ImmutableList<RecentOrderItem>.Empty;
            }

            var orders = data.Orders.Select(o => new RecentOrderItem
            {
                Id = o.Id,
                OrderNumber = o.OrderNumber,
                CustomerName = o.CustomerName,
                Amount = o.Amount,
                Status = o.Status,
                OrderDate = o.OrderDate
            }).ToImmutableList();

            _logger.LogInformation("Successfully loaded {Count} recent orders", orders.Count);
            return orders;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error loading recent orders");
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
/// Orders KPI data for the second dashboard card.
/// </summary>
public record OrdersKpiData
{
    public int OrdersToday { get; init; }
    public int OrdersPending { get; init; }
    public int OrdersThisWeek { get; init; }
    public decimal OrdersChange { get; init; }

    public string OrdersChangeFormatted => $"{(OrdersChange >= 0 ? "+" : "")}{OrdersChange:F1}%";
    public bool OrdersChangePositive => OrdersChange >= 0;
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
/// Recent order item for the activity list.
/// </summary>
public partial record RecentOrderItem
{
    public Guid Id { get; init; }
    public string OrderNumber { get; init; } = string.Empty;
    public string CustomerName { get; init; } = string.Empty;
    public decimal Amount { get; init; }
    public OrderStatus Status { get; init; }
    public DateTime OrderDate { get; init; }

    public string AmountFormatted => Amount.ToString("C2");
    public string DateFormatted => OrderDate.ToString("g");
    public string StatusIcon => Status switch
    {
        OrderStatus.Pending => "\uE823",           // Clock
        OrderStatus.Processing => "\uE895",        // Sync
        OrderStatus.ReadyForDelivery => "\uE7B8",  // Package
        OrderStatus.PartiallyDelivered => "\uE122", // Airplane
        OrderStatus.Completed => "\uE73E",         // Checkmark
        OrderStatus.Cancelled => "\uE711",         // Cancel
        OrderStatus.Returned => "\uE72B",          // Back
        OrderStatus.Refunded => "\uE8BB",          // Money
        OrderStatus.OnHold => "\uE769",            // Pause
        OrderStatus.Failed => "\uE783",            // Error
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
