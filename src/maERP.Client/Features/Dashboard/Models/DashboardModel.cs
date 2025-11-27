namespace maERP.Client.Features.Dashboard.Models;

/// <summary>
/// Model for dashboard page using MVUX pattern.
/// Provides KPIs, recent activity, and quick actions for ERP overview.
/// </summary>
public partial record DashboardModel
{
    private readonly IStringLocalizer _localizer;

    public DashboardModel(IStringLocalizer localizer)
    {
        _localizer = localizer;
    }

    // KPI Data Feeds
    public IFeed<DashboardKpiData> KpiData => Feed.Async(LoadKpiDataAsync);

    // Recent Orders Feed
    public IListFeed<RecentOrderItem> RecentOrders => ListFeed.Async(LoadRecentOrdersAsync);

    // Top Selling Products Feed
    public IListFeed<TopProductItem> TopProducts => ListFeed.Async(LoadTopProductsAsync);

    // Low Stock Alerts Feed
    public IListFeed<LowStockItem> LowStockAlerts => ListFeed.Async(LoadLowStockAlertsAsync);

    private async ValueTask<DashboardKpiData> LoadKpiDataAsync(CancellationToken ct)
    {
        // Simulate API call delay
        await Task.Delay(500, ct);

        // Return dummy data
        return new DashboardKpiData
        {
            RevenueToday = 12489.50m,
            RevenueThisWeek = 87234.00m,
            RevenueThisMonth = 324567.80m,
            RevenueChange = 12.5m,

            OrdersToday = 47,
            OrdersPending = 23,
            OrdersThisWeek = 312,
            OrdersChange = 8.3m,

            CustomersTotal = 1847,
            CustomersNewThisMonth = 56,
            CustomersChange = 15.2m,

            ProductsInStock = 2341,
            ProductsTotal = 2567,
            ProductsLowStock = 12,
            ProductsChange = -3.1m
        };
    }

    private async ValueTask<IImmutableList<RecentOrderItem>> LoadRecentOrdersAsync(CancellationToken ct)
    {
        // Simulate API call delay
        await Task.Delay(300, ct);

        // Return dummy data
        var orders = new List<RecentOrderItem>
        {
            new() { OrderNumber = "ORD-2024-001247", CustomerName = "Max Mustermann", Amount = 234.50m, Status = OrderStatus.Processing, OrderDate = DateTime.Now.AddHours(-1) },
            new() { OrderNumber = "ORD-2024-001246", CustomerName = "Anna Schmidt", Amount = 89.99m, Status = OrderStatus.Shipped, OrderDate = DateTime.Now.AddHours(-3) },
            new() { OrderNumber = "ORD-2024-001245", CustomerName = "Peter Weber", Amount = 567.00m, Status = OrderStatus.Pending, OrderDate = DateTime.Now.AddHours(-5) },
            new() { OrderNumber = "ORD-2024-001244", CustomerName = "Maria Fischer", Amount = 123.45m, Status = OrderStatus.Delivered, OrderDate = DateTime.Now.AddDays(-1) },
            new() { OrderNumber = "ORD-2024-001243", CustomerName = "Thomas Bauer", Amount = 445.00m, Status = OrderStatus.Processing, OrderDate = DateTime.Now.AddDays(-1) }
        };

        return orders.ToImmutableList();
    }

    private async ValueTask<IImmutableList<TopProductItem>> LoadTopProductsAsync(CancellationToken ct)
    {
        // Simulate API call delay
        await Task.Delay(400, ct);

        // Return dummy data
        var products = new List<TopProductItem>
        {
            new() { Rank = 1, ProductName = "Wireless Bluetooth Headphones", Sku = "WBH-001", QuantitySold = 156, Revenue = 15444.00m },
            new() { Rank = 2, ProductName = "USB-C Charging Cable 2m", Sku = "UCC-002", QuantitySold = 234, Revenue = 4680.00m },
            new() { Rank = 3, ProductName = "Smartphone Case Premium", Sku = "SCP-003", QuantitySold = 189, Revenue = 5670.00m },
            new() { Rank = 4, ProductName = "LED Desk Lamp", Sku = "LDL-004", QuantitySold = 98, Revenue = 4900.00m },
            new() { Rank = 5, ProductName = "Portable Power Bank 20000mAh", Sku = "PPB-005", QuantitySold = 87, Revenue = 4350.00m }
        };

        return products.ToImmutableList();
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
/// KPI data for the dashboard overview cards.
/// </summary>
public record DashboardKpiData
{
    // Revenue KPIs
    public decimal RevenueToday { get; init; }
    public decimal RevenueThisWeek { get; init; }
    public decimal RevenueThisMonth { get; init; }
    public decimal RevenueChange { get; init; }

    // Order KPIs
    public int OrdersToday { get; init; }
    public int OrdersPending { get; init; }
    public int OrdersThisWeek { get; init; }
    public decimal OrdersChange { get; init; }

    // Customer KPIs
    public int CustomersTotal { get; init; }
    public int CustomersNewThisMonth { get; init; }
    public decimal CustomersChange { get; init; }

    // Product/Inventory KPIs
    public int ProductsInStock { get; init; }
    public int ProductsTotal { get; init; }
    public int ProductsLowStock { get; init; }
    public decimal ProductsChange { get; init; }

    // Formatted properties for display
    public string RevenueTodayFormatted => RevenueToday.ToString("C0");
    public string RevenueChangeFormatted => $"{(RevenueChange >= 0 ? "+" : "")}{RevenueChange:F1}%";
    public bool RevenueChangePositive => RevenueChange >= 0;

    public string OrdersChangeFormatted => $"{(OrdersChange >= 0 ? "+" : "")}{OrdersChange:F1}%";
    public bool OrdersChangePositive => OrdersChange >= 0;

    public string CustomersChangeFormatted => $"{(CustomersChange >= 0 ? "+" : "")}{CustomersChange:F1}%";
    public bool CustomersChangePositive => CustomersChange >= 0;

    public string ProductsChangeFormatted => $"{(ProductsChange >= 0 ? "+" : "")}{ProductsChange:F1}%";
    public bool ProductsChangePositive => ProductsChange >= 0;

    public string StockRatio => $"{ProductsInStock} / {ProductsTotal}";
}

/// <summary>
/// Recent order item for the activity list.
/// </summary>
public record RecentOrderItem
{
    public string OrderNumber { get; init; } = string.Empty;
    public string CustomerName { get; init; } = string.Empty;
    public decimal Amount { get; init; }
    public OrderStatus Status { get; init; }
    public DateTime OrderDate { get; init; }

    public string AmountFormatted => Amount.ToString("C2");
    public string DateFormatted => OrderDate.ToString("g");
    public string StatusIcon => Status switch
    {
        OrderStatus.Pending => "\uE823",      // Clock
        OrderStatus.Processing => "\uE895",   // Sync
        OrderStatus.Shipped => "\uE122",      // Airplane
        OrderStatus.Delivered => "\uE73E",    // Checkmark
        _ => "\uE946"                         // Unknown
    };
}

public enum OrderStatus
{
    Pending,
    Processing,
    Shipped,
    Delivered
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
