using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Web;
using maERP.Application.Contracts.Persistence;
using maERP.Domain.Enums;
using maERP.SalesChannels.Abstractions;
using maERP.SalesChannels.Connectors.Common;
using maERP.SalesChannels.Contracts;
using maERP.SalesChannels.Models;
using maERP.SalesChannels.Models.Amazon;
using Microsoft.Extensions.Logging;

namespace maERP.SalesChannels.Connectors.Amazon;

/// <summary>
/// Amazon Selling Partner API (SP-API) connector. One <see cref="SalesChannel"/> row maps to a
/// single (region × marketplace) — multi-marketplace sellers create one channel per marketplace.
///
/// Auth: LWA refresh-token via <see cref="AmazonAuthHelper"/>. AWS SigV4 is no longer required
/// since 2023 — bearer access token alone authenticates regular calls.
///
/// PR 13 ships <see cref="ImportSalessAsync"/> and the export plumbing (Listings PATCH for stock
/// and price); deeper coverage (full ExportProduct / FBA inventory / shipment confirmation feeds)
/// follows the same pattern and lands incrementally.
/// </summary>
public sealed class AmazonConnector : ConnectorBase
{
    private static readonly TimeSpan ImportWindow = TimeSpan.FromDays(30);

    private readonly AmazonAuthHelper _auth;
    private readonly ISalesImportRepository _salesImportRepository;
    private readonly ICustomerImportRepository _customerImportRepository;
    private readonly ILogger<AmazonConnector> _logger;

    public AmazonConnector(
        AmazonAuthHelper auth,
        ISalesImportRepository salesImportRepository,
        ICustomerImportRepository customerImportRepository,
        ILogger<AmazonConnector> logger)
    {
        _auth = auth;
        _salesImportRepository = salesImportRepository;
        _customerImportRepository = customerImportRepository;
        _logger = logger;
    }

    public override SalesChannelType Type => SalesChannelType.Amazon;

    public override SalesChannelCapabilities Capabilities =>
        SalesChannelCapabilities.ImportSaless |
        SalesChannelCapabilities.ImportCustomers |
        SalesChannelCapabilities.UpdateStock |
        SalesChannelCapabilities.UpdatePrice |
        SalesChannelCapabilities.OAuth |
        SalesChannelCapabilities.RequiresMarketplaceId;

    public override async Task<ConnectionTestResult> TestConnectionAsync(SalesChannelContext context)
    {
        try
        {
            var (config, accessToken) = await PrepareAsync(context);
            ConfigureBearer(context, accessToken, config);

            // Smoke-test: fetch the seller's marketplace participation.
            var url = $"{config.GetEndpointBaseUrl()}/sellers/v1/marketplaceParticipations";
            var response = await context.HttpClient.GetAsync(url, context.CancellationToken);
            if (response.IsSuccessStatusCode) return new ConnectionTestResult(true);

            var body = await response.Content.ReadAsStringAsync(context.CancellationToken);
            return new ConnectionTestResult(false, $"HTTP {(int)response.StatusCode}: {Truncate(body, 250)}");
        }
        catch (Exception ex)
        {
            return new ConnectionTestResult(false, ex.Message);
        }
    }

    public override async Task<SyncResult> ImportSalessAsync(SalesChannelContext context)
    {
        var salesChannel = context.SalesChannel;
        if (string.IsNullOrEmpty(salesChannel.MarketplaceId))
        {
            return SyncResult.Failed("MarketplaceId is required for Amazon imports");
        }

        var processed = 0;
        var failed = 0;

        try
        {
            var (config, accessToken) = await PrepareAsync(context);
            ConfigureBearer(context, accessToken, config);

            var createdAfter = DateTime.UtcNow - ImportWindow;
            string? nextToken = null;
            var baseUrl = config.GetEndpointBaseUrl();

            do
            {
                var url = $"{baseUrl}/saless/v0/saless" +
                          $"?MarketplaceIds={HttpUtility.UrlEncode(salesChannel.MarketplaceId)}" +
                          $"&CreatedAfter={createdAfter:O}";
                if (!string.IsNullOrEmpty(nextToken))
                {
                    url += $"&NextToken={HttpUtility.UrlEncode(nextToken)}";
                }

                var response = await context.HttpClient.GetAsync(url, context.CancellationToken);
                if (!response.IsSuccessStatusCode)
                {
                    var body = await response.Content.ReadAsStringAsync(context.CancellationToken);
                    _logger.LogError("Amazon saless HTTP {Status}: {Body}", (int)response.StatusCode, Truncate(body, 500));
                    failed++;
                    break;
                }

                var raw = await response.Content.ReadAsStringAsync(context.CancellationToken);
                var salessResponse = JsonSerializer.Deserialize<AmazonSalessResponse>(raw);
                var payload = salessResponse?.Payload;
                if (payload?.Saless is null || payload.Saless.Count == 0) break;

                foreach (var sales in payload.Saless)
                {
                    try
                    {
                        var importSales = MapSales(sales);
                        await _salesImportRepository.ImportOrUpdateFromSalesChannel(salesChannel, importSales);
                        processed++;
                    }
                    catch (Exception ex)
                    {
                        failed++;
                        _logger.LogError(ex, "Amazon sales import failed for {Id}", sales.AmazonSalesId);
                    }
                }

                nextToken = payload.NextToken;
            }
            while (!string.IsNullOrEmpty(nextToken));
        }
        catch (Exception ex)
        {
            return SyncResult.Failed(ex.Message);
        }

        return new SyncResult(processed, failed);
    }

    public override async Task<SyncResult> ImportCustomersAsync(SalesChannelContext context)
    {
        // Amazon does not expose a customer-list endpoint — customers are derived from saless.
        // We piggyback on the sales endpoint and persist buyers via the customer-import path.
        var salesChannel = context.SalesChannel;
        if (string.IsNullOrEmpty(salesChannel.MarketplaceId))
        {
            return SyncResult.Failed("MarketplaceId is required for Amazon imports");
        }

        var processed = 0;
        var failed = 0;

        try
        {
            var (config, accessToken) = await PrepareAsync(context);
            ConfigureBearer(context, accessToken, config);

            var createdAfter = DateTime.UtcNow - ImportWindow;
            var url = $"{config.GetEndpointBaseUrl()}/saless/v0/saless" +
                      $"?MarketplaceIds={HttpUtility.UrlEncode(salesChannel.MarketplaceId)}" +
                      $"&CreatedAfter={createdAfter:O}";

            var response = await context.HttpClient.GetAsync(url, context.CancellationToken);
            if (!response.IsSuccessStatusCode) return SyncResult.Failed($"Amazon saless HTTP {(int)response.StatusCode}");

            var raw = await response.Content.ReadAsStringAsync(context.CancellationToken);
            var salessResponse = JsonSerializer.Deserialize<AmazonSalessResponse>(raw);
            var saless = salessResponse?.Payload?.Saless ?? new List<AmazonSales>();

            foreach (var sales in saless)
            {
                if (sales.BuyerInfo is null && sales.ShippingAddress is null) continue;

                try
                {
                    var importCustomer = MapCustomer(sales);
                    await _customerImportRepository.ImportOrUpdateFromSalesChannel(salesChannel, importCustomer);
                    processed++;
                }
                catch (Exception ex)
                {
                    failed++;
                    _logger.LogError(ex, "Amazon customer import failed for sales {Id}", sales.AmazonSalesId);
                }
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
        try
        {
            var (config, accessToken) = await PrepareAsync(context);
            ConfigureBearer(context, accessToken, config);

            var url = $"{config.GetEndpointBaseUrl()}/listings/2021-08-01/items/{Uri.EscapeDataString(config.SellerId)}/{Uri.EscapeDataString(payload.Sku)}" +
                      $"?marketplaceIds={HttpUtility.UrlEncode(context.SalesChannel.MarketplaceId)}";

            var body = new
            {
                productType = "PRODUCT",
                patches = new[]
                {
                    new
                    {
                        op = "replace",
                        path = "/attributes/fulfillment_availability",
                        value = new[]
                        {
                            new
                            {
                                fulfillment_channel_code = "DEFAULT",
                                quantity = payload.Quantity,
                            },
                        },
                    },
                },
            };

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
        try
        {
            var (config, accessToken) = await PrepareAsync(context);
            ConfigureBearer(context, accessToken, config);

            var url = $"{config.GetEndpointBaseUrl()}/listings/2021-08-01/items/{Uri.EscapeDataString(config.SellerId)}/{Uri.EscapeDataString(payload.Sku)}" +
                      $"?marketplaceIds={HttpUtility.UrlEncode(context.SalesChannel.MarketplaceId)}";

            var body = new
            {
                productType = "PRODUCT",
                patches = new[]
                {
                    new
                    {
                        op = "replace",
                        path = "/attributes/purchasable_offer",
                        value = new[]
                        {
                            new
                            {
                                marketplace_id = context.SalesChannel.MarketplaceId,
                                currency = payload.Currency ?? "EUR",
                                our_price = new[]
                                {
                                    new { schedule = new[] { new { value_with_tax = payload.Price } } },
                                },
                            },
                        },
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

    private async Task<(AmazonChannelConfig config, string accessToken)> PrepareAsync(SalesChannelContext context)
    {
        // Channel-specific config (region, sellerId, sandbox flag, ...) still lives in
        // AdditionalConfigJson. App credentials (LWA client_id/secret) are now resolved by the
        // helper itself via IOAuthAppSettingsService — tenant override → system fallback.
        var config = AmazonChannelConfig.FromSalesChannel(context.SalesChannel);
        if (string.IsNullOrEmpty(config.SellerId))
        {
            throw new InvalidOperationException(
                "Amazon channel is missing SellerId in AdditionalConfigJson");
        }

        var accessToken = await _auth.GetAccessTokenAsync(context.SalesChannel, context.CancellationToken);

        return (config, accessToken);
    }

    private static void ConfigureBearer(SalesChannelContext context, string accessToken, AmazonChannelConfig config)
    {
        context.HttpClient.DefaultRequestHeaders.Accept.Clear();
        context.HttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        context.HttpClient.DefaultRequestHeaders.Remove("x-amz-access-token");
        context.HttpClient.DefaultRequestHeaders.Add("x-amz-access-token", accessToken);
        context.HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
    }

    private static SalesChannelImportSales MapSales(AmazonSales sales)
    {
        var total = decimal.TryParse(sales.SalesTotal?.Amount, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out var t) ? t : 0m;

        var billingAddress = sales.ShippingAddress is null
            ? new SalesChannelImportCustomerAddress()
            : new SalesChannelImportCustomerAddress
            {
                Street = sales.ShippingAddress.AddressLine1 ?? string.Empty,
                City = sales.ShippingAddress.City ?? string.Empty,
                Zip = sales.ShippingAddress.PostalCode ?? string.Empty,
                Country = sales.ShippingAddress.CountryCode ?? string.Empty,
            };

        var importSales = new SalesChannelImportSales
        {
            RemoteSalesId = sales.AmazonSalesId,
            DateSalesed = sales.PurchaseDate,
            Status = MapSalesStatus(sales.SalesStatus),
            PaymentStatus = PaymentStatus.CompletelyPaid,
            PaymentMethod = "Amazon",
            PaymentProvider = "Amazon",
            Subtotal = total,
            Total = total,
            ShippingCost = 0m,
            TotalTax = 0m,
            CustomerNote = string.Empty,
            SalesItems = new List<SalesChannelImportSalesItem>(),
            Customer = new SalesChannelImportCustomer
            {
                Email = sales.BuyerInfo?.BuyerEmail ?? string.Empty,
                Firstname = sales.BuyerInfo?.BuyerName ?? string.Empty,
                Lastname = string.Empty,
                Phone = sales.ShippingAddress?.Phone ?? string.Empty,
                CustomerStatus = CustomerStatus.Active,
                DateEnrollment = DateTime.UtcNow,
                BillingAddress = billingAddress,
                ShippingAddress = billingAddress,
            },
            BillingAddress = billingAddress,
            ShippingAddress = billingAddress,
        };

        return importSales;
    }

    private static SalesChannelImportCustomer MapCustomer(AmazonSales sales)
    {
        var addr = sales.ShippingAddress is null
            ? new SalesChannelImportCustomerAddress()
            : new SalesChannelImportCustomerAddress
            {
                Street = sales.ShippingAddress.AddressLine1 ?? string.Empty,
                City = sales.ShippingAddress.City ?? string.Empty,
                Zip = sales.ShippingAddress.PostalCode ?? string.Empty,
                Country = sales.ShippingAddress.CountryCode ?? string.Empty,
            };

        return new SalesChannelImportCustomer
        {
            RemoteCustomerId = sales.BuyerInfo?.BuyerEmail ?? sales.AmazonSalesId,
            Email = sales.BuyerInfo?.BuyerEmail ?? string.Empty,
            Firstname = sales.BuyerInfo?.BuyerName ?? string.Empty,
            Lastname = string.Empty,
            Phone = sales.ShippingAddress?.Phone ?? string.Empty,
            CustomerStatus = CustomerStatus.Active,
            DateEnrollment = DateTime.UtcNow,
            BillingAddress = addr,
            ShippingAddress = addr,
        };
    }

    private static SalesStatus MapSalesStatus(string status) => status?.ToLowerInvariant() switch
    {
        "pending" => SalesStatus.Pending,
        "unshipped" => SalesStatus.Processing,
        "partiallyshipped" => SalesStatus.Processing,
        "shipped" => SalesStatus.Completed,
        "canceled" or "cancelled" => SalesStatus.Cancelled,
        _ => SalesStatus.Unknown,
    };

    private static string Truncate(string value, int max)
    {
        if (string.IsNullOrEmpty(value)) return value ?? string.Empty;
        return value.Length <= max ? value : value[..max];
    }
}
