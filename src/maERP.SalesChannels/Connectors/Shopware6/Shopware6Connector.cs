using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using maERP.Application.Contracts.Persistence;
using maERP.Domain.Enums;
using maERP.SalesChannels.Abstractions;
using maERP.SalesChannels.Connectors.Common;
using maERP.SalesChannels.Contracts;
using maERP.SalesChannels.Models;
using maERP.SalesChannels.Models.Shopware6;
using Microsoft.Extensions.Logging;

namespace maERP.SalesChannels.Connectors.Shopware6;

/// <summary>
/// Shopware 6 Admin-API connector. Auth: OAuth client_credentials (see <see cref="Shopware6AuthHelper"/>).
/// Imports go through the <c>/api/search/{entity}</c> endpoints with cursor-style paging via
/// <c>page</c> + <c>limit</c>. Stock and price updates use the standard PATCH on <c>/api/product/{id}</c>.
/// </summary>
public sealed class Shopware6Connector : ConnectorBase
{
    private const int PageSize = 100;

    private readonly Shopware6AuthHelper _auth;
    private readonly IProductImportRepository _productImportRepository;
    private readonly IOrderImportRepository _orderImportRepository;
    private readonly ISalesChannelRepository _salesChannelRepository;
    private readonly ILogger<Shopware6Connector> _logger;

    public Shopware6Connector(
        Shopware6AuthHelper auth,
        IProductImportRepository productImportRepository,
        IOrderImportRepository orderImportRepository,
        ISalesChannelRepository salesChannelRepository,
        ILogger<Shopware6Connector> logger)
    {
        _auth = auth;
        _productImportRepository = productImportRepository;
        _orderImportRepository = orderImportRepository;
        _salesChannelRepository = salesChannelRepository;
        _logger = logger;
    }

    public override SalesChannelType Type => SalesChannelType.Shopware6;

    public override SalesChannelCapabilities Capabilities =>
        SalesChannelCapabilities.ImportProducts |
        SalesChannelCapabilities.ImportOrders |
        SalesChannelCapabilities.UpdateStock |
        SalesChannelCapabilities.UpdatePrice |
        SalesChannelCapabilities.OAuth;

    public override async Task<ConnectionTestResult> TestConnectionAsync(SalesChannelContext context)
    {
        try
        {
            SalesChannelUrlValidator.Validate(context.SalesChannel.Url);
            var (_, accessToken) = await PrepareAsync(context);
            ConfigureBearer(context, accessToken);

            var url = context.SalesChannel.Url.TrimEnd('/') + "/api/_info/version";
            var response = await context.HttpClient.GetAsync(url, context.CancellationToken);
            return response.IsSuccessStatusCode
                ? new ConnectionTestResult(true)
                : new ConnectionTestResult(false, $"HTTP {(int)response.StatusCode}");
        }
        catch (Exception ex)
        {
            return new ConnectionTestResult(false, ex.Message);
        }
    }

    public override async Task<SyncResult> ImportProductsAsync(SalesChannelContext context)
    {
        var salesChannel = context.SalesChannel;
        try
        {
            SalesChannelUrlValidator.Validate(salesChannel.Url);
        }
        catch (ArgumentException ex)
        {
            return SyncResult.Failed($"Invalid sales channel URL: {ex.Message}");
        }

        var processed = 0;
        var failed = 0;
        var page = 1;

        try
        {
            var (_, accessToken) = await PrepareAsync(context);
            ConfigureBearer(context, accessToken);
            var baseUrl = salesChannel.Url.TrimEnd('/');

            while (true)
            {
                var requestBody = new
                {
                    page,
                    limit = PageSize,
                    sort = new[] { new { field = "createdAt", order = "ASC" } },
                };
                var url = $"{baseUrl}/api/search/product";
                var response = await context.HttpClient.PostAsJsonAsync(url, requestBody, context.CancellationToken);
                if (!response.IsSuccessStatusCode)
                {
                    var body = await response.Content.ReadAsStringAsync(context.CancellationToken);
                    _logger.LogError("Shopware6 product search HTTP {Status}: {Body}", (int)response.StatusCode, body);
                    failed++;
                    break;
                }

                var raw = await response.Content.ReadAsStringAsync(context.CancellationToken);
                var result = JsonSerializer.Deserialize<Sw6SearchResult<Sw6Product>>(raw);
                if (result?.Data is null || result.Data.Count == 0) break;

                foreach (var p in result.Data)
                {
                    if (string.IsNullOrEmpty(p.ProductNumber))
                    {
                        continue;
                    }

                    try
                    {
                        var description = p.Translated?.Description ?? p.Description ?? string.Empty;
                        if (description.Length > 4000) description = description[..4000];

                        await _productImportRepository.ImportOrUpdateFromSalesChannel(salesChannel.Id, new SalesChannelImportProduct
                        {
                            Sku = p.ProductNumber!,
                            Name = p.Translated?.Name ?? p.Name ?? string.Empty,
                            Ean = p.Ean ?? string.Empty,
                            Price = p.Price.FirstOrDefault()?.Net ?? 0m,
                            TaxRate = 19,
                            Description = description,
                        });
                        processed++;
                    }
                    catch (Exception ex)
                    {
                        failed++;
                        _logger.LogError(ex, "Shopware6 product import failed for {Sku}", p.ProductNumber);
                    }
                }

                if (result.Data.Count < PageSize) break;
                page++;
            }
        }
        catch (Exception ex)
        {
            return SyncResult.Failed(ex.Message);
        }

        if (!salesChannel.InitialProductImportCompleted)
        {
            salesChannel.InitialProductImportCompleted = true;
            await _salesChannelRepository.UpdateAsync(salesChannel);
        }

        return new SyncResult(processed, failed);
    }

    public override async Task<SyncResult> ImportOrdersAsync(SalesChannelContext context)
    {
        var salesChannel = context.SalesChannel;
        try
        {
            SalesChannelUrlValidator.Validate(salesChannel.Url);
        }
        catch (ArgumentException ex)
        {
            return SyncResult.Failed($"Invalid sales channel URL: {ex.Message}");
        }

        var processed = 0;
        var failed = 0;
        var page = 1;

        try
        {
            var (_, accessToken) = await PrepareAsync(context);
            ConfigureBearer(context, accessToken);
            var baseUrl = salesChannel.Url.TrimEnd('/');

            while (true)
            {
                var requestBody = new
                {
                    page,
                    limit = PageSize,
                    associations = new
                    {
                        orderCustomer = new { },
                        billingAddress = new { associations = new { country = new { } } },
                        deliveries = new { associations = new { shippingOrderAddress = new { associations = new { country = new { } } } } },
                        lineItems = new { },
                        stateMachineState = new { },
                    },
                    sort = new[] { new { field = "orderDateTime", order = "ASC" } },
                };
                var url = $"{baseUrl}/api/search/order";
                var response = await context.HttpClient.PostAsJsonAsync(url, requestBody, context.CancellationToken);
                if (!response.IsSuccessStatusCode)
                {
                    var body = await response.Content.ReadAsStringAsync(context.CancellationToken);
                    _logger.LogError("Shopware6 order search HTTP {Status}: {Body}", (int)response.StatusCode, body);
                    failed++;
                    break;
                }

                var raw = await response.Content.ReadAsStringAsync(context.CancellationToken);
                var result = JsonSerializer.Deserialize<Sw6SearchResult<Sw6Order>>(raw);
                if (result?.Data is null || result.Data.Count == 0) break;

                foreach (var order in result.Data)
                {
                    try
                    {
                        var importOrder = MapOrder(order);
                        await _orderImportRepository.ImportOrUpdateFromSalesChannel(salesChannel, importOrder);
                        processed++;
                    }
                    catch (Exception ex)
                    {
                        failed++;
                        _logger.LogError(ex, "Shopware6 order import failed for {Id}", order.Id);
                    }
                }

                if (result.Data.Count < PageSize) break;
                page++;
            }
        }
        catch (Exception ex)
        {
            return SyncResult.Failed(ex.Message);
        }

        return new SyncResult(processed, failed);
    }

    public override async Task<ExportResult> UpdateStockAsync(SalesChannelContext context, StockUpdatePayload payload)
    {
        if (string.IsNullOrEmpty(payload.RemoteProductId))
        {
            return ExportResult.Fail("Shopware6 product id (RemoteProductId) is required for stock updates");
        }

        try
        {
            var (_, accessToken) = await PrepareAsync(context);
            ConfigureBearer(context, accessToken);

            var url = context.SalesChannel.Url.TrimEnd('/') + $"/api/product/{Uri.EscapeDataString(payload.RemoteProductId)}";
            var body = new { stock = payload.Quantity };
            var request = new HttpRequestMessage(HttpMethod.Patch, url) { Content = JsonContent.Create(body) };
            var response = await context.HttpClient.SendAsync(request, context.CancellationToken);
            if (response.IsSuccessStatusCode) return ExportResult.Ok(payload.RemoteProductId);

            var responseBody = await response.Content.ReadAsStringAsync(context.CancellationToken);
            return ExportResult.Fail($"HTTP {(int)response.StatusCode}: {Truncate(responseBody, 300)}");
        }
        catch (Exception ex)
        {
            return ExportResult.Fail(ex.Message);
        }
    }

    public override async Task<ExportResult> UpdatePriceAsync(SalesChannelContext context, PriceUpdatePayload payload)
    {
        if (string.IsNullOrEmpty(payload.RemoteProductId))
        {
            return ExportResult.Fail("Shopware6 product id (RemoteProductId) is required for price updates");
        }

        try
        {
            var (_, accessToken) = await PrepareAsync(context);
            ConfigureBearer(context, accessToken);

            var url = context.SalesChannel.Url.TrimEnd('/') + $"/api/product/{Uri.EscapeDataString(payload.RemoteProductId)}";
            var body = new
            {
                price = new[]
                {
                    new
                    {
                        gross = payload.Price,
                        net = payload.Price,
                        linked = false,
                        currencyId = "b7d2554b0ce847cd82f3ac9bd1c0dfca", // Shopware 6 default EUR currency id
                    },
                },
            };
            var request = new HttpRequestMessage(HttpMethod.Patch, url) { Content = JsonContent.Create(body) };
            var response = await context.HttpClient.SendAsync(request, context.CancellationToken);
            if (response.IsSuccessStatusCode) return ExportResult.Ok(payload.RemoteProductId, payload.ExternalListingId);

            var responseBody = await response.Content.ReadAsStringAsync(context.CancellationToken);
            return ExportResult.Fail($"HTTP {(int)response.StatusCode}: {Truncate(responseBody, 300)}");
        }
        catch (Exception ex)
        {
            return ExportResult.Fail(ex.Message);
        }
    }

    private async Task<(Shopware6ChannelConfig config, string accessToken)> PrepareAsync(SalesChannelContext context)
    {
        var config = Shopware6ChannelConfig.FromSalesChannel(context.SalesChannel);
        if (string.IsNullOrEmpty(config.ApiClientId) || string.IsNullOrEmpty(config.ApiClientSecret))
        {
            throw new InvalidOperationException(
                "Shopware6 channel is missing apiClientId / apiClientSecret in AdditionalConfigJson (or Username/Password fallback)");
        }
        var accessToken = await _auth.GetAccessTokenAsync(context.SalesChannel, config, context.CancellationToken);
        return (config, accessToken);
    }

    private static void ConfigureBearer(SalesChannelContext context, string accessToken)
    {
        context.HttpClient.DefaultRequestHeaders.Accept.Clear();
        context.HttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        context.HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
    }

    private static SalesChannelImportOrder MapOrder(Sw6Order order)
    {
        var billing = order.BillingAddress;
        var shipping = order.Deliveries.FirstOrDefault()?.ShippingOrderAddress ?? billing;

        var billingAddress = new SalesChannelImportCustomerAddress
        {
            Firstname = billing?.FirstName ?? string.Empty,
            Lastname = billing?.LastName ?? string.Empty,
            CompanyName = billing?.Company ?? string.Empty,
            Street = billing?.Street ?? string.Empty,
            City = billing?.City ?? string.Empty,
            Zip = billing?.Zipcode ?? string.Empty,
            Country = billing?.Country?.Iso ?? string.Empty,
        };

        var shippingAddress = new SalesChannelImportCustomerAddress
        {
            Firstname = shipping?.FirstName ?? string.Empty,
            Lastname = shipping?.LastName ?? string.Empty,
            CompanyName = shipping?.Company ?? string.Empty,
            Street = shipping?.Street ?? string.Empty,
            City = shipping?.City ?? string.Empty,
            Zip = shipping?.Zipcode ?? string.Empty,
            Country = shipping?.Country?.Iso ?? string.Empty,
        };

        return new SalesChannelImportOrder
        {
            RemoteOrderId = order.Id,
            RemoteCustomerId = order.OrderCustomer?.CustomerId ?? string.Empty,
            DateOrdered = order.OrderDateTime,
            Status = MapOrderStatus(order.StateMachineState?.TechnicalName),
            PaymentStatus = PaymentStatus.Unknown,
            Subtotal = order.AmountNet,
            ShippingCost = order.ShippingTotal,
            TotalTax = order.AmountTotal - order.AmountNet,
            Total = order.AmountTotal,
            Customer = new SalesChannelImportCustomer
            {
                Firstname = order.OrderCustomer?.FirstName ?? string.Empty,
                Lastname = order.OrderCustomer?.LastName ?? string.Empty,
                CompanyName = order.OrderCustomer?.Company ?? string.Empty,
                Email = order.OrderCustomer?.Email ?? string.Empty,
                Phone = billing?.PhoneNumber ?? string.Empty,
                DateEnrollment = DateTime.UtcNow,
            },
            BillingAddress = billingAddress,
            ShippingAddress = shippingAddress,
            OrderItems = order.LineItems.Select(li => new SalesChannelImportOrderItem
            {
                Name = li.Label ?? string.Empty,
                Sku = li.Payload?.ProductNumber ?? string.Empty,
                Quantity = li.Quantity,
                Price = li.UnitPrice,
                Ean = li.Payload?.Ean ?? string.Empty,
                TaxRate = 19,
            }).ToList(),
        };
    }

    private static OrderStatus MapOrderStatus(string? technicalName) => technicalName switch
    {
        "open" => OrderStatus.Pending,
        "in_progress" => OrderStatus.Processing,
        "completed" => OrderStatus.Completed,
        "cancelled" => OrderStatus.Cancelled,
        _ => OrderStatus.Unknown,
    };

    private static string Truncate(string value, int max)
    {
        if (string.IsNullOrEmpty(value)) return value ?? string.Empty;
        return value.Length <= max ? value : value[..max];
    }
}
