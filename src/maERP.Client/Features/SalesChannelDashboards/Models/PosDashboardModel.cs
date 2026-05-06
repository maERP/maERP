using maERP.Client.Core.Models;
using maERP.Client.Features.Customers.Services;
using maERP.Client.Features.Dashboard.Models;
using maERP.Client.Features.Saless;
using maERP.Client.Features.Saless.Models;
using maERP.Client.Features.Saless.Services;
using maERP.Client.Features.Products.Services;
using maERP.Client.Features.SalesChannelDashboards.Services;
using maERP.Domain.Dtos.Customer;
using maERP.Domain.Dtos.Sales;
using maERP.Domain.Dtos.Product;
using maERP.Domain.Entities;
using maERP.Domain.Enums;
using Microsoft.Extensions.Logging;

namespace maERP.Client.Features.SalesChannelDashboards.Models;

/// <summary>
/// Model for POS Dashboard page using MVUX pattern.
/// Provides KPIs, Quick Sale, and Recent Sales for a specific SalesChannel.
/// </summary>
public partial record PosDashboardModel
{
    private readonly ISalesChannelStatisticsService _statisticsService;
    private readonly ISalesService _salesService;
    private readonly ICustomerService _customerService;
    private readonly IProductService _productService;
    private readonly INavigator _navigator;
    private readonly IStringLocalizer _localizer;
    private readonly ILogger<PosDashboardModel> _logger;
    private readonly Guid _salesChannelId;

    public string Title { get; }

    public PosDashboardModel(
        ISalesChannelStatisticsService statisticsService,
        ISalesService salesService,
        ICustomerService customerService,
        IProductService productService,
        INavigator navigator,
        IStringLocalizer localizer,
        ILogger<PosDashboardModel> logger,
        SalesChannelDashboardData? data = null)
    {
        _statisticsService = statisticsService;
        _salesService = salesService;
        _customerService = customerService;
        _productService = productService;
        _navigator = navigator;
        _localizer = localizer;
        _logger = logger;
        _salesChannelId = data?.SalesChannelId ?? Guid.Empty;
        Title = data?.SalesChannelName ?? "POS";
    }

    // Tab 1: Dashboard KPIs
    public IFeed<RevenueKpiData> RevenueData => Feed.Async(LoadRevenueDataAsync);
    public IFeed<SalessKpiData> SalessData => Feed.Async(LoadSalessDataAsync);

    // Tab 2: Quick Sale - Search
    public IState<string> CustomerSearchQuery => State<string>.Value(this, () => string.Empty);
    public IState<string> ProductSearchQuery => State<string>.Value(this, () => string.Empty);

    // Tab 2: Quick Sale - Selection
    public IState<PosCustomerSelection> SelectedCustomer => State<PosCustomerSelection>.Empty(this);
    public IState<IImmutableList<PosCartItem>> CartItems => State<IImmutableList<PosCartItem>>.Value(this, () => (IImmutableList<PosCartItem>)ImmutableList<PosCartItem>.Empty);
    public IState<string> PosErrorMessage => State<string>.Value(this, () => string.Empty);
    public IState<string> PosSuccessMessage => State<string>.Value(this, () => string.Empty);
    public IState<bool> IsProcessingSale => State<bool>.Value(this, () => false);
    public IState<string> CartTotalFormatted => State<string>.Value(this, () => 0m.ToString("C2"));
    public IState<string> CartTaxTotalFormatted => State<string>.Value(this, () => 0m.ToString("C2"));
    public IState<string> CartGrandTotalFormatted => State<string>.Value(this, () => 0m.ToString("C2"));
    public IState<IImmutableList<PosTaxLineItem>> TaxBreakdown => State<IImmutableList<PosTaxLineItem>>.Value(this, () => (IImmutableList<PosTaxLineItem>)ImmutableList<PosTaxLineItem>.Empty);
    public string InvoiceDateFormatted => DateTime.Now.ToString("d");

    // Tab 2: Quick Sale - Search Results
    public IListFeed<CustomerListWithAddressDto> CustomerSearchResults => CustomerSearchQuery
        .Where(q => !string.IsNullOrWhiteSpace(q) && q.Length >= 2)
        .SelectAsync(SearchCustomersAsync)
        .AsListFeed();

    public IListFeed<ProductListDto> ProductSearchResults => ProductSearchQuery
        .Where(q => !string.IsNullOrWhiteSpace(q) && q.Length >= 2)
        .SelectAsync(SearchProductsAsync)
        .AsListFeed();

    // Tab 3: Recent Sales
    public IListFeed<RecentSalesItem> RecentSaless => ListFeed.Async(LoadRecentSalessAsync);

    // Navigation
    public async ValueTask ViewSales(RecentSalesItem sales)
    {
        await _navigator.NavigateDataAsync(this, new SalesDetailData(sales.Id));
    }

    // Tab 2: Quick Sale - Actions
    public async ValueTask SelectCustomer(CustomerListWithAddressDto customer)
    {
        await SelectedCustomer.UpdateAsync(_ => new PosCustomerSelection(customer.Id, customer.CustomerId, customer.FullName, customer.Email, customer.InvoiceAddress));
        await CustomerSearchQuery.UpdateAsync(_ => string.Empty);
    }

    public async ValueTask ClearCustomer()
    {
        await SelectedCustomer.UpdateAsync(_ => null!);
    }

    public async ValueTask AddToCart(ProductListDto product)
    {
        await CartItems.UpdateAsync(items =>
        {
            var existing = items.FirstOrDefault(i => i.ProductId == product.Id);
            if (existing != null)
            {
                return items.Replace(existing, existing with { Quantity = existing.Quantity + 1 });
            }
            return items.Add(new PosCartItem(product.Id, product.Name, product.Sku, product.Price, 1, product.TaxRate));
        });
        await RefreshCartTotal();
    }

    public async ValueTask ClearProductSearch()
    {
        await ProductSearchQuery.UpdateAsync(_ => string.Empty);
    }

    public async ValueTask RemoveFromCart(PosCartItem item)
    {
        await CartItems.UpdateAsync(items => items.Remove(item));
        await RefreshCartTotal();
    }

    public async ValueTask UpdateCartItemQuantity(PosCartItem item, int newQuantity)
    {
        if (newQuantity <= 0)
        {
            await RemoveFromCart(item);
            return;
        }
        await CartItems.UpdateAsync(items => items.Replace(item, item with { Quantity = newQuantity }));
        await RefreshCartTotal();
    }

    public async ValueTask UpdateCartItemPrice(PosCartItem item, decimal newPrice)
    {
        if (newPrice < 0) return;
        await CartItems.UpdateAsync(items => items.Replace(item, item with { UnitPrice = newPrice }));
        await RefreshCartTotal();
    }

    public async ValueTask ProcessSale()
    {
        await IsProcessingSale.UpdateAsync(_ => true);
        await PosErrorMessage.UpdateAsync(_ => string.Empty);
        await PosSuccessMessage.UpdateAsync(_ => string.Empty);

        try
        {
            var customer = await SelectedCustomer;
            var cartItems = await CartItems;

            if (cartItems == null || cartItems.Count == 0)
            {
                await PosErrorMessage.UpdateAsync(_ => _localizer["PosDashboard.QuickSale.EmptyCart.Text"]);
                return;
            }

            var subtotal = cartItems.Sum(i => i.LineTotal);
            var totalTax = cartItems.Sum(i => i.LineTax);
            var total = subtotal + totalTax;

            var salesInput = new SalesInputDto
            {
                SalesChannelId = _salesChannelId,
                CustomerId = customer?.CustomerId ?? 0,
                Status = SalesStatus.Completed,
                PaymentStatus = PaymentStatus.CompletelyPaid,
                PaymentMethod = "POS",
                DateSalesed = DateTime.UtcNow,
                Subtotal = subtotal,
                TotalTax = totalTax,
                Total = total,
                SalesItems = cartItems.Select(i => new SalesItem
                {
                    ProductId = i.ProductId,
                    Name = i.ProductName,
                    Price = i.UnitPrice,
                    Quantity = i.Quantity,
                    TaxRate = i.TaxRate
                }).ToList(),
                // Populate minimal address from customer name
                InvoiceAddressFirstName = customer != null ? customer.DisplayName.Split(' ').FirstOrDefault() ?? "" : "POS",
                InvoiceAddressLastName = customer != null ? string.Join(" ", customer.DisplayName.Split(' ').Skip(1)) : "Customer",
                DeliveryAddressFirstName = customer != null ? customer.DisplayName.Split(' ').FirstOrDefault() ?? "" : "POS",
                DeliveryAddressLastName = customer != null ? string.Join(" ", customer.DisplayName.Split(' ').Skip(1)) : "Customer"
            };

            await _salesService.CreateSalesAsync(salesInput);

            // Success: clear cart and show message
            await CartItems.UpdateAsync(_ => ImmutableList<PosCartItem>.Empty);
            await SelectedCustomer.UpdateAsync(_ => null!);
            await PosSuccessMessage.UpdateAsync(_ => _localizer["PosDashboard.QuickSale.SaleSuccess.Text"]);
            await RefreshCartTotal();

            _logger.LogInformation("POS sale completed for SalesChannel {SalesChannelId}", _salesChannelId);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error processing POS sale for SalesChannel {SalesChannelId}", _salesChannelId);
            await PosErrorMessage.UpdateAsync(_ => ex.Message);
        }
        finally
        {
            await IsProcessingSale.UpdateAsync(_ => false);
        }
    }

    // Private helpers
    private async ValueTask RefreshCartTotal()
    {
        var items = await CartItems;
        var subtotal = items?.Sum(i => i.LineTotal) ?? 0m;
        var totalTax = items?.Sum(i => i.LineTax) ?? 0m;
        var grandTotal = subtotal + totalTax;

        var taxLines = items?
            .Where(i => i.TaxRate > 0)
            .GroupBy(i => i.TaxRate)
            .OrderBy(g => g.Key)
            .Select(g => new PosTaxLineItem(g.Key, g.Sum(i => i.LineTax)))
            .ToImmutableList()
            ?? ImmutableList<PosTaxLineItem>.Empty;

        await CartTotalFormatted.UpdateAsync(_ => subtotal.ToString("C2"));
        await CartTaxTotalFormatted.UpdateAsync(_ => totalTax.ToString("C2"));
        await CartGrandTotalFormatted.UpdateAsync(_ => grandTotal.ToString("C2"));
        await TaxBreakdown.UpdateAsync(_ => taxLines);
    }

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

    private async ValueTask<SalessKpiData> LoadSalessDataAsync(CancellationToken ct)
    {
        try
        {
            var data = await _statisticsService.GetSalessTodayAsync(_salesChannelId, ct);
            if (data == null) return new SalessKpiData();

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
            _logger.LogError(ex, "Error loading saless KPI data for SalesChannel {SalesChannelId}", _salesChannelId);
            throw;
        }
    }

    private async ValueTask<IImmutableList<RecentSalesItem>> LoadRecentSalessAsync(CancellationToken ct)
    {
        try
        {
            var data = await _statisticsService.GetSalessLatestAsync(_salesChannelId, 10, ct);
            if (data == null || data.Saless.Count == 0)
                return ImmutableList<RecentSalesItem>.Empty;

            return data.Saless.Select(o => new RecentSalesItem
            {
                Id = o.Id,
                SalesNumber = o.SalesNumber,
                CustomerName = o.CustomerName,
                Amount = o.Amount,
                Status = o.Status,
                SalesDate = o.SalesDate
            }).ToImmutableList();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error loading recent saless for SalesChannel {SalesChannelId}", _salesChannelId);
            throw;
        }
    }

    private async ValueTask<IImmutableList<CustomerListWithAddressDto>> SearchCustomersAsync(string query, CancellationToken ct)
    {
        try
        {
            var parameters = new QueryParameters { SearchString = query, PageSize = 10 };
            var response = await _customerService.SearchCustomersWithAddressAsync(parameters, ct);
            return response.Data.ToImmutableList();
        }
        catch (OperationCanceledException) when (ct.IsCancellationRequested)
        {
            throw; // Let MVUX handle cancellation for proper feed state management
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error searching customers: {Query}", query);
            return ImmutableList<CustomerListWithAddressDto>.Empty;
        }
    }

    private async ValueTask<IImmutableList<ProductListDto>> SearchProductsAsync(string query, CancellationToken ct)
    {
        try
        {
            var parameters = new QueryParameters { SearchString = query, PageSize = 10 };
            var response = await _productService.GetProductsAsync(parameters, ct);
            return response.Data.ToImmutableList();
        }
        catch (OperationCanceledException) when (ct.IsCancellationRequested)
        {
            throw; // Let MVUX handle cancellation for proper feed state management
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error searching products: {Query}", query);
            return ImmutableList<ProductListDto>.Empty;
        }
    }
}

/// <summary>
/// Selected customer for POS sale.
/// </summary>
public partial record PosCustomerSelection(Guid Id, int CustomerId, string DisplayName, string Email, string InvoiceAddress);

/// <summary>
/// Cart item for POS sale.
/// </summary>
public record PosCartItem(Guid ProductId, string ProductName, string Sku, decimal UnitPrice, int Quantity, double TaxRate)
{
    public decimal LineTotal => UnitPrice * Quantity;
    public decimal LineTax => LineTotal * (decimal)(TaxRate / 100.0);
    public string LineTotalFormatted => LineTotal.ToString("C2");
    public string UnitPriceFormatted => UnitPrice.ToString("C2");
    public string UnitPriceEditable => UnitPrice.ToString("F2");
}

/// <summary>
/// Tax breakdown line for display (grouped by tax rate).
/// </summary>
public record PosTaxLineItem(double TaxRate, decimal TaxAmount)
{
    public string Label => $"MwSt {TaxRate:0.##}%";
    public string AmountFormatted => TaxAmount.ToString("C2");
}
