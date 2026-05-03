#nullable disable
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using maERP.Application.Contracts.Persistence;
using maERP.Domain.Enums;
using maERP.SalesChannels.Abstractions;
using maERP.SalesChannels.Connectors.Common;
using maERP.SalesChannels.Contracts;
using maERP.SalesChannels.Models;
using maERP.SalesChannels.Models.eBay;
using Microsoft.Extensions.Logging;

namespace maERP.SalesChannels.Connectors.Ebay;

/// <summary>
/// eBay Sell-API connector. Migrates the legacy <c>EbayProductImportTask</c>,
/// <c>EbayOrderImportTask</c> and <c>EbayCustomerImportTask</c> behind the connector contract.
/// Customers are derived from orders (eBay does not expose a customer list endpoint), so
/// <see cref="ImportCustomersAsync"/> walks the same fulfillment endpoint as
/// <see cref="ImportOrdersAsync"/> and just keeps buyers.
///
/// Export operations (publish offer, update stock/price, mark order shipped) are not yet
/// implemented — they land in PR 12 when the outbox drainer is wired up.
/// </summary>
public sealed class EbayConnector : ConnectorBase
{
    private const int InventoryPageSize = 100;
    private const int OrderPageSize = 50;

    private readonly EbayAuthHelper _authHelper;
    private readonly IProductImportRepository _productImportRepository;
    private readonly IOrderImportRepository _orderImportRepository;
    private readonly ICustomerImportRepository _customerImportRepository;
    private readonly ISalesChannelRepository _salesChannelRepository;
    private readonly ILogger<EbayConnector> _logger;

    public EbayConnector(
        EbayAuthHelper authHelper,
        IProductImportRepository productImportRepository,
        IOrderImportRepository orderImportRepository,
        ICustomerImportRepository customerImportRepository,
        ISalesChannelRepository salesChannelRepository,
        ILogger<EbayConnector> logger)
    {
        _authHelper = authHelper;
        _productImportRepository = productImportRepository;
        _orderImportRepository = orderImportRepository;
        _customerImportRepository = customerImportRepository;
        _salesChannelRepository = salesChannelRepository;
        _logger = logger;
    }

    public override SalesChannelType Type => SalesChannelType.eBay;

    public override SalesChannelCapabilities Capabilities =>
        SalesChannelCapabilities.ImportProducts |
        SalesChannelCapabilities.ImportOrders |
        SalesChannelCapabilities.ImportCustomers |
        SalesChannelCapabilities.UpdateStock |
        SalesChannelCapabilities.UpdatePrice |
        SalesChannelCapabilities.OAuth;

    public override async Task<ConnectionTestResult> TestConnectionAsync(SalesChannelContext context)
    {
        try
        {
            var token = await _authHelper.GetAccessTokenAsync(context.SalesChannel);
            return string.IsNullOrEmpty(token)
                ? new ConnectionTestResult(false, "No access token returned")
                : new ConnectionTestResult(true);
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
        var offset = 0;
        var total = 0;

        try
        {
            var accessToken = await _authHelper.GetAccessTokenAsync(salesChannel);
            ConfigureBearer(context, accessToken);

            do
            {
                var requestUrl = $"{salesChannel.Url}/sell/inventory/v1/inventory_item?limit={InventoryPageSize}&offset={offset}";
                var response = await context.HttpClient.GetAsync(requestUrl, context.CancellationToken);
                if (!response.IsSuccessStatusCode)
                {
                    var body = await response.Content.ReadAsStringAsync(context.CancellationToken);
                    _logger.LogError("eBay inventory list HTTP {Status}: {Body}", (int)response.StatusCode, body);
                    failed++;
                    break;
                }

                var raw = await response.Content.ReadAsStringAsync(context.CancellationToken);
                var inventoryItems = JsonSerializer.Deserialize<EbayInventoryItemResponse>(raw);
                if (inventoryItems is null) break;
                total = inventoryItems.Total;

                foreach (var item in inventoryItems.InventoryItems)
                {
                    try
                    {
                        var offerUrl = $"{salesChannel.Url}/sell/inventory/v1/offer?sku={item.Sku}";
                        var offerResponse = await context.HttpClient.GetAsync(offerUrl, context.CancellationToken);
                        if (!offerResponse.IsSuccessStatusCode)
                        {
                            failed++;
                            continue;
                        }

                        var offerRaw = await offerResponse.Content.ReadAsStringAsync(context.CancellationToken);
                        var offers = JsonSerializer.Deserialize<EbayOfferResponse>(offerRaw);
                        if (offers?.Offers is null || offers.Offers.Length == 0)
                        {
                            continue;
                        }

                        var offer = offers.Offers[0];
                        var description = item.Product.Description ?? string.Empty;
                        if (description.Length > 4000) description = description.Substring(0, 4000);

                        var importProduct = new SalesChannelImportProduct
                        {
                            Name = item.Product.Title,
                            Sku = item.Sku,
                            Ean = item.Product.Ean is { Length: > 0 } ? item.Product.Ean[0] : string.Empty,
                            Price = offer.PricingSummary.Price.Value,
                            TaxRate = 19,
                            Description = description,
                        };

                        await _productImportRepository.ImportOrUpdateFromSalesChannel(salesChannel.Id, importProduct);
                        processed++;
                    }
                    catch (Exception ex)
                    {
                        failed++;
                        _logger.LogError(ex, "eBay product import failed for SKU {Sku}", item.Sku);
                    }
                }

                offset += InventoryPageSize;
            }
            while (total > 0 && offset < total);
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
        var offset = 0;

        try
        {
            var accessToken = await _authHelper.GetAccessTokenAsync(salesChannel);
            ConfigureBearer(context, accessToken);

            while (true)
            {
                var url = $"{salesChannel.Url}/sell/fulfillment/v1/order?limit={OrderPageSize}&offset={offset}";
                var response = await context.HttpClient.GetAsync(url, context.CancellationToken);
                if (!response.IsSuccessStatusCode)
                {
                    var body = await response.Content.ReadAsStringAsync(context.CancellationToken);
                    _logger.LogError("eBay orders list HTTP {Status}: {Body}", (int)response.StatusCode, body);
                    failed++;
                    break;
                }

                var raw = await response.Content.ReadAsStringAsync(context.CancellationToken);
                var ordersResponse = JsonSerializer.Deserialize<EbayOrderResponse>(raw);
                if (ordersResponse is null || ordersResponse.Total == 0 || ordersResponse.LineItems is null || ordersResponse.LineItems.Length == 0)
                {
                    break;
                }

                foreach (var lineItem in ordersResponse.LineItems)
                {
                    try
                    {
                        var importOrder = MapOrder(ordersResponse, lineItem);
                        await _orderImportRepository.ImportOrUpdateFromSalesChannel(salesChannel, importOrder);
                        processed++;
                    }
                    catch (Exception ex)
                    {
                        failed++;
                        _logger.LogError(ex, "eBay order import failed for line item {Id}", lineItem.LineItemId);
                    }
                }

                offset += OrderPageSize;
            }
        }
        catch (Exception ex)
        {
            return SyncResult.Failed(ex.Message);
        }

        return new SyncResult(processed, failed);
    }

    public override async Task<SyncResult> ImportCustomersAsync(SalesChannelContext context)
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
        var offset = 0;

        try
        {
            var accessToken = await _authHelper.GetAccessTokenAsync(salesChannel);
            ConfigureBearer(context, accessToken);

            while (true)
            {
                var url = $"{salesChannel.Url}/sell/fulfillment/v1/order?limit={OrderPageSize}&offset={offset}";
                var response = await context.HttpClient.GetAsync(url, context.CancellationToken);
                if (!response.IsSuccessStatusCode)
                {
                    failed++;
                    break;
                }

                var raw = await response.Content.ReadAsStringAsync(context.CancellationToken);
                var ordersResponse = JsonSerializer.Deserialize<EbayOrderResponse>(raw);
                if (ordersResponse is null || ordersResponse.Total == 0 || ordersResponse.LineItems is null || ordersResponse.LineItems.Length == 0)
                {
                    break;
                }

                var buyer = ordersResponse.Buyer;
                if (buyer?.TaxAddress is not null)
                {
                    try
                    {
                        var importCustomer = MapCustomer(ordersResponse);
                        await _customerImportRepository.ImportOrUpdateFromSalesChannel(salesChannel, importCustomer);
                        processed++;
                    }
                    catch (Exception ex)
                    {
                        failed++;
                        _logger.LogError(ex, "eBay customer import failed");
                    }
                }

                offset += OrderPageSize;
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
        if (string.IsNullOrEmpty(payload.Sku))
        {
            return ExportResult.Fail("eBay SKU is required for stock updates");
        }

        try
        {
            var accessToken = await _authHelper.GetAccessTokenAsync(context.SalesChannel);
            ConfigureBearer(context, accessToken);

            var url = $"{context.SalesChannel.Url}/sell/inventory/v1/inventory_item/{Uri.EscapeDataString(payload.Sku)}";
            var body = new
            {
                availability = new
                {
                    shipToLocationAvailability = new { quantity = payload.Quantity },
                },
            };
            var request = new HttpRequestMessage(HttpMethod.Put, url) { Content = JsonContent.Create(body) };
            var response = await context.HttpClient.SendAsync(request, context.CancellationToken);
            return response.IsSuccessStatusCode
                ? ExportResult.Ok(payload.Sku)
                : ExportResult.Fail($"HTTP {(int)response.StatusCode}");
        }
        catch (Exception ex)
        {
            return ExportResult.Fail(ex.Message);
        }
    }

    public override async Task<ExportResult> UpdatePriceAsync(SalesChannelContext context, PriceUpdatePayload payload)
    {
        if (string.IsNullOrEmpty(payload.ExternalListingId))
        {
            return ExportResult.Fail("eBay offerId (ExternalListingId) is required for price updates");
        }

        try
        {
            var accessToken = await _authHelper.GetAccessTokenAsync(context.SalesChannel);
            ConfigureBearer(context, accessToken);

            var url = $"{context.SalesChannel.Url}/sell/inventory/v1/offer/{Uri.EscapeDataString(payload.ExternalListingId)}";
            var body = new
            {
                pricingSummary = new
                {
                    price = new
                    {
                        currency = payload.Currency ?? "EUR",
                        value = payload.Price.ToString(System.Globalization.CultureInfo.InvariantCulture),
                    },
                },
            };
            var request = new HttpRequestMessage(HttpMethod.Put, url) { Content = JsonContent.Create(body) };
            var response = await context.HttpClient.SendAsync(request, context.CancellationToken);
            return response.IsSuccessStatusCode
                ? ExportResult.Ok(payload.RemoteProductId, payload.ExternalListingId)
                : ExportResult.Fail($"HTTP {(int)response.StatusCode}");
        }
        catch (Exception ex)
        {
            return ExportResult.Fail(ex.Message);
        }
    }

    private static void ConfigureBearer(SalesChannelContext context, string accessToken)
    {
        context.HttpClient.DefaultRequestHeaders.Accept.Clear();
        context.HttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        context.HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
    }

    private static SalesChannelImportOrder MapOrder(EbayOrderResponse o, EbayLineItem line)
    {
        var order = new SalesChannelImportOrder
        {
            RemoteOrderId = line.LineItemId,
            RemoteCustomerId = o.Buyer?.Username ?? string.Empty,
            DateOrdered = o.CreationDate,
            Subtotal = o.PricingSummary?.Subtotal?.Value ?? 0m,
            Total = o.PricingSummary?.Total?.Value ?? line.LineItemCost.Value,
            TotalTax = o.PricingSummary?.TotalTaxAmount?.Value ?? 0m,
            ShippingCost = CalculateShippingCost(o),
            Status = MapOrderStatus(o.OrderFulfillmentStatus),
            PaymentStatus = MapPaymentStatus(o.OrderPaymentStatus),
            PaymentMethod = "eBay",
            PaymentProvider = "eBay",
            CustomerNote = string.Empty,
            OrderItems = new List<SalesChannelImportOrderItem>(),
            Customer = new SalesChannelImportCustomer(),
        };

        var buyer = o.Buyer;
        if (buyer?.TaxAddress is not null)
        {
            order.Customer = new SalesChannelImportCustomer
            {
                Email = buyer.TaxAddress.Email ?? string.Empty,
                RemoteCustomerId = buyer.Username ?? string.Empty,
                Firstname = buyer.TaxAddress.FirstName ?? string.Empty,
                Lastname = buyer.TaxAddress.LastName ?? string.Empty,
                Phone = buyer.TaxAddress.Phone ?? string.Empty,
                CustomerStatus = CustomerStatus.Active,
                DateEnrollment = DateTime.UtcNow,
            };

            var billingAddress = new SalesChannelImportCustomerAddress
            {
                Firstname = buyer.TaxAddress.FirstName ?? string.Empty,
                Lastname = buyer.TaxAddress.LastName ?? string.Empty,
                Street = buyer.TaxAddress.AddressLine1 ?? string.Empty,
                City = buyer.TaxAddress.City ?? string.Empty,
                Zip = buyer.TaxAddress.PostalCode ?? string.Empty,
                Country = buyer.TaxAddress.CountryCode ?? string.Empty,
            };
            order.Customer.BillingAddress = billingAddress;
            order.BillingAddress = billingAddress;

            var shipTo = GetShippingAddress(o);
            if (shipTo is not null)
            {
                var shippingAddress = new SalesChannelImportCustomerAddress
                {
                    Firstname = shipTo.FirstName ?? string.Empty,
                    Lastname = shipTo.LastName ?? string.Empty,
                    Street = shipTo.AddressLine1 ?? string.Empty,
                    City = shipTo.City ?? string.Empty,
                    Zip = shipTo.PostalCode ?? string.Empty,
                    Country = shipTo.CountryCode ?? string.Empty,
                };
                order.Customer.ShippingAddress = shippingAddress;
                order.ShippingAddress = shippingAddress;
            }
            else
            {
                order.Customer.ShippingAddress = billingAddress;
                order.ShippingAddress = billingAddress;
            }
        }

        order.OrderItems.Add(new SalesChannelImportOrderItem
        {
            Name = line.Title ?? string.Empty,
            Sku = line.Sku ?? string.Empty,
            Quantity = line.Quantity,
            Price = line.LineItemCost.Value / Math.Max(1, line.Quantity),
            TaxRate = 19,
        });

        return order;
    }

    private static SalesChannelImportCustomer MapCustomer(EbayOrderResponse o)
    {
        var buyer = o.Buyer;
        var importCustomer = new SalesChannelImportCustomer
        {
            RemoteCustomerId = buyer.Username ?? string.Empty,
            Email = buyer.TaxAddress.Email ?? string.Empty,
            Firstname = buyer.TaxAddress.FirstName ?? string.Empty,
            Lastname = buyer.TaxAddress.LastName ?? string.Empty,
            Phone = buyer.TaxAddress.Phone ?? string.Empty,
            CustomerStatus = CustomerStatus.Active,
            DateEnrollment = DateTime.UtcNow,
            BillingAddress = new SalesChannelImportCustomerAddress
            {
                Firstname = buyer.TaxAddress.FirstName ?? string.Empty,
                Lastname = buyer.TaxAddress.LastName ?? string.Empty,
                Street = buyer.TaxAddress.AddressLine1 ?? string.Empty,
                City = buyer.TaxAddress.City ?? string.Empty,
                Zip = buyer.TaxAddress.PostalCode ?? string.Empty,
                Country = buyer.TaxAddress.CountryCode ?? string.Empty,
            },
        };

        var shipTo = GetShippingAddress(o);
        importCustomer.ShippingAddress = shipTo is null
            ? importCustomer.BillingAddress
            : new SalesChannelImportCustomerAddress
            {
                Firstname = shipTo.FirstName ?? string.Empty,
                Lastname = shipTo.LastName ?? string.Empty,
                Street = shipTo.AddressLine1 ?? string.Empty,
                City = shipTo.City ?? string.Empty,
                Zip = shipTo.PostalCode ?? string.Empty,
                Country = shipTo.CountryCode ?? string.Empty,
            };

        return importCustomer;
    }

    private static EbayAddress GetShippingAddress(EbayOrderResponse o)
    {
        var instructions = o.FulfillmentStartInstructions;
        if (instructions is { Length: > 0 } && instructions[0].ShippingStep is not null)
        {
            return instructions[0].ShippingStep.ShipTo;
        }
        return null;
    }

    private static decimal CalculateShippingCost(EbayOrderResponse o)
    {
        if (o.PricingSummary?.Total is null || o.PricingSummary.Subtotal is null)
        {
            return 0m;
        }
        var total = o.PricingSummary.Total.Value;
        var subtotal = o.PricingSummary.Subtotal.Value;
        var tax = o.PricingSummary.TotalTaxAmount?.Value ?? 0m;
        return Math.Max(0, total - subtotal - tax);
    }

    private static OrderStatus MapOrderStatus(string ebayStatus) => ebayStatus?.ToLower() switch
    {
        "not_started" => OrderStatus.Pending,
        "in_progress" => OrderStatus.Processing,
        "fulfilled" => OrderStatus.Completed,
        "failed" => OrderStatus.Failed,
        _ => OrderStatus.Unknown,
    };

    private static PaymentStatus MapPaymentStatus(string status) => status?.ToLower() switch
    {
        "paid" => PaymentStatus.CompletelyPaid,
        "partially_paid" => PaymentStatus.PartiallyPaid,
        "not_paid" => PaymentStatus.Invoiced,
        _ => PaymentStatus.Unknown,
    };
}
