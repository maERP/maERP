#nullable disable
using System.Globalization;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using maERP.Application.Contracts.Persistence;
using maERP.Domain.Enums;
using maERP.SalesChannels.Abstractions;
using maERP.SalesChannels.Connectors.Common;
using maERP.SalesChannels.Contracts;
using maERP.SalesChannels.Models;
using maERP.SalesChannels.Models.Shopware5;
using Microsoft.Extensions.Logging;

namespace maERP.SalesChannels.Connectors.Shopware5;

/// <summary>
/// Shopware 5 REST API connector (Basic-Auth). Migrated from <c>Shopware5ProductImportTask</c>
/// and <c>Shopware5OrderImportTask</c> in <c>Tasks/</c> — same endpoints, same payload shape,
/// same status mapping. Differences from the legacy tasks:
/// — uses the typed <see cref="HttpClient"/> from <see cref="SalesChannelContext"/> (Polly + IHttpClientFactory),
/// — paginates inside a single connector call instead of running an infinite poll loop,
/// — reports <see cref="SyncResult"/> with item counts so <see cref="Domain.Entities.ChannelSyncRun"/> reflects truth.
///
/// Export operations are not supported by Shopware 5 in our scope yet — those fall through to
/// <see cref="ConnectorBase"/>'s NotSupported responses.
/// </summary>
public sealed class Shopware5Connector : ConnectorBase
{
    private const int PageSize = 100;

    private readonly IProductImportRepository _productImportRepository;
    private readonly IOrderImportRepository _orderImportRepository;
    private readonly ISalesChannelRepository _salesChannelRepository;
    private readonly ILogger<Shopware5Connector> _logger;

    public Shopware5Connector(
        IProductImportRepository productImportRepository,
        IOrderImportRepository orderImportRepository,
        ISalesChannelRepository salesChannelRepository,
        ILogger<Shopware5Connector> logger)
    {
        _productImportRepository = productImportRepository;
        _orderImportRepository = orderImportRepository;
        _salesChannelRepository = salesChannelRepository;
        _logger = logger;
    }

    public override SalesChannelType Type => SalesChannelType.Shopware5;

    public override SalesChannelCapabilities Capabilities =>
        SalesChannelCapabilities.ImportProducts |
        SalesChannelCapabilities.ImportOrders |
        SalesChannelCapabilities.UpdateStock |
        SalesChannelCapabilities.UpdatePrice;

    public override async Task<ConnectionTestResult> TestConnectionAsync(SalesChannelContext context)
    {
        try
        {
            SalesChannelUrlValidator.Validate(context.SalesChannel.Url);
            ConfigureBasicAuth(context);
            var response = await context.HttpClient.GetAsync(
                context.SalesChannel.Url + "/api/articles?start=0&limit=1",
                context.CancellationToken).ConfigureAwait(false);

            return response.IsSuccessStatusCode
                ? new ConnectionTestResult(true)
                : new ConnectionTestResult(false, $"HTTP {(int)response.StatusCode} {response.ReasonPhrase}");
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

        ConfigureBasicAuth(context);

        var processed = 0;
        var failed = 0;
        var requestStart = 0;
        var requestMax = 0;

        do
        {
            try
            {
                var requestUrl = salesChannel.Url + $"/api/articles?start={requestStart}&limit={PageSize}";
                var response = await context.HttpClient.GetAsync(requestUrl, context.CancellationToken).ConfigureAwait(false);

                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogWarning("Shopware5 articles list HTTP {Status} for {Url}", (int)response.StatusCode, requestUrl);
                    failed++;
                    break;
                }

                var raw = await response.Content.ReadAsStringAsync(context.CancellationToken).ConfigureAwait(false);
                var remoteProducts = JsonSerializer.Deserialize<BaseListResponse<ProductResponse>>(raw);
                if (remoteProducts is null)
                {
                    failed++;
                    break;
                }
                requestMax = remoteProducts.total;

                foreach (var remoteProduct in remoteProducts.data)
                {
                    try
                    {
                        var importProduct = MapProduct(remoteProduct);
                        await _productImportRepository.ImportOrUpdateFromSalesChannel(salesChannel.Id, importProduct);
                        processed++;
                    }
                    catch (Exception ex)
                    {
                        failed++;
                        _logger.LogError(ex, "Shopware5 product import failed for SKU {Sku}", remoteProduct?.mainDetail?.number);
                    }
                }

                requestStart += PageSize;
            }
            catch (Exception ex)
            {
                failed++;
                _logger.LogError(ex, "Shopware5 product import page error");
                break;
            }
        }
        while (requestMax != 0 && requestStart <= requestMax);

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

        if (salesChannel.ImportProducts && !salesChannel.InitialProductImportCompleted)
        {
            return SyncResult.Failed("Initial product import not yet completed — skipping orders.");
        }

        try
        {
            SalesChannelUrlValidator.Validate(salesChannel.Url);
        }
        catch (ArgumentException ex)
        {
            return SyncResult.Failed($"Invalid sales channel URL: {ex.Message}");
        }

        ConfigureBasicAuth(context);

        var processed = 0;
        var failed = 0;
        var requestStart = 0;
        var requestMax = 0;

        do
        {
            try
            {
                var requestUrl = salesChannel.Url + $"/api/orders?start={requestStart}&limit={PageSize}";
                var listResponse = await context.HttpClient.GetAsync(requestUrl, context.CancellationToken).ConfigureAwait(false);
                if (!listResponse.IsSuccessStatusCode)
                {
                    _logger.LogWarning("Shopware5 orders list HTTP {Status} for {Url}", (int)listResponse.StatusCode, requestUrl);
                    failed++;
                    break;
                }

                var listRaw = await listResponse.Content.ReadAsStringAsync(context.CancellationToken).ConfigureAwait(false);
                var remoteOrders = JsonSerializer.Deserialize<BaseListResponse<OrderResponse>>(listRaw);
                if (remoteOrders?.data is null || !remoteOrders.success)
                {
                    failed++;
                    break;
                }
                requestMax = remoteOrders.total;

                foreach (var remoteOrder in remoteOrders.data)
                {
                    if (remoteOrder.orderStatusId == -1)
                    {
                        continue;
                    }

                    try
                    {
                        var detailUrl = salesChannel.Url + $"/api/orders/{remoteOrder.id}";
                        var detailResponse = await context.HttpClient.GetAsync(detailUrl, context.CancellationToken).ConfigureAwait(false);
                        if (!detailResponse.IsSuccessStatusCode)
                        {
                            failed++;
                            continue;
                        }

                        var detailRaw = await detailResponse.Content.ReadAsStringAsync(context.CancellationToken).ConfigureAwait(false);
                        var detailWrapper = JsonSerializer.Deserialize<BaseResponse<OrderDetailResponse>>(detailRaw);
                        if (detailWrapper?.data is null || !detailWrapper.success)
                        {
                            failed++;
                            continue;
                        }

                        var importOrder = MapOrder(remoteOrder, detailWrapper.data);
                        await _orderImportRepository.ImportOrUpdateFromSalesChannel(salesChannel, importOrder);
                        processed++;
                    }
                    catch (Exception ex)
                    {
                        failed++;
                        _logger.LogError(ex, "Shopware5 order import failed for {Id}", remoteOrder.id);
                    }
                }

                requestStart += PageSize;
            }
            catch (Exception ex)
            {
                failed++;
                _logger.LogError(ex, "Shopware5 order import page error");
                break;
            }
        }
        while (requestMax != 0 && requestStart <= requestMax);

        return new SyncResult(processed, failed);
    }

    public override async Task<ExportResult> UpdateStockAsync(SalesChannelContext context, StockUpdatePayload payload)
    {
        if (string.IsNullOrEmpty(payload.RemoteProductId))
        {
            return ExportResult.Fail("Shopware5 article id (RemoteProductId) is required for stock updates");
        }

        try
        {
            ConfigureBasicAuth(context);
            var url = context.SalesChannel.Url.TrimEnd('/') + $"/api/articles/{Uri.EscapeDataString(payload.RemoteProductId)}";
            var body = new { mainDetail = new { inStock = payload.Quantity } };
            var request = new HttpRequestMessage(HttpMethod.Put, url) { Content = JsonContent.Create(body) };
            var response = await context.HttpClient.SendAsync(request, context.CancellationToken);
            return response.IsSuccessStatusCode
                ? ExportResult.Ok(payload.RemoteProductId)
                : ExportResult.Fail($"HTTP {(int)response.StatusCode}");
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
            return ExportResult.Fail("Shopware5 article id (RemoteProductId) is required for price updates");
        }

        try
        {
            ConfigureBasicAuth(context);
            var url = context.SalesChannel.Url.TrimEnd('/') + $"/api/articles/{Uri.EscapeDataString(payload.RemoteProductId)}";
            var body = new
            {
                mainDetail = new
                {
                    prices = new[] { new { customerGroupKey = "EK", price = payload.Price } },
                },
            };
            var request = new HttpRequestMessage(HttpMethod.Put, url) { Content = JsonContent.Create(body) };
            var response = await context.HttpClient.SendAsync(request, context.CancellationToken);
            return response.IsSuccessStatusCode
                ? ExportResult.Ok(payload.RemoteProductId)
                : ExportResult.Fail($"HTTP {(int)response.StatusCode}");
        }
        catch (Exception ex)
        {
            return ExportResult.Fail(ex.Message);
        }
    }

    private static void ConfigureBasicAuth(SalesChannelContext context)
    {
        var sc = context.SalesChannel;
        var auth = $"{sc.Username}:{context.Password}";
        var b64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(auth));
        context.HttpClient.DefaultRequestHeaders.Accept.Clear();
        context.HttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        context.HttpClient.DefaultRequestHeaders.Remove("Authorization");
        context.HttpClient.DefaultRequestHeaders.Add("Authorization", "Basic " + b64);
    }

    private static SalesChannelImportProduct MapProduct(ProductResponse remoteProduct)
    {
        var ean = remoteProduct.mainDetail.ean ?? string.Empty;
        if (ean.Length > 13) ean = ean.Substring(0, 13);

        var description = remoteProduct.descriptionLong ?? string.Empty;
        if (description.Length > 4000) description = description.Substring(0, 4000);

        return new SalesChannelImportProduct
        {
            Name = remoteProduct.name,
            Ean = ean,
            Price = (decimal)remoteProduct.mainDetail.purchasePrice,
            Sku = remoteProduct.mainDetail.number,
            TaxRate = 19,
            Description = description,
        };
    }

    private static SalesChannelImportOrder MapOrder(OrderResponse listEntry, OrderDetailResponse detail)
    {
        if (detail.customer is null)
        {
            detail.customer = new Customer
            {
                firstLogin = DateTime.MinValue.ToString(CultureInfo.CurrentCulture),
            };
        }

        return new SalesChannelImportOrder
        {
            RemoteOrderId = listEntry.id.ToString(),
            RemoteCustomerId = detail.customer.id.ToString(),
            DateOrdered = DateTime.Parse(detail.orderTime).ToUniversalTime(),
            Status = MapOrderStatus(listEntry.orderStatusId),
            PaymentStatus = MapPaymentStatus(listEntry.paymentStatusId),
            Subtotal = listEntry.invoiceAmountNet,
            ShippingCost = listEntry.invoiceShippingNet,
            TotalTax = listEntry.invoiceAmount - listEntry.invoiceAmountNet,
            Total = listEntry.invoiceAmount,
            Customer = new SalesChannelImportCustomer
            {
                Firstname = detail.billing.firstName,
                Lastname = detail.billing.lastName,
                CompanyName = detail.billing.company,
                Email = string.IsNullOrEmpty(detail.customer.email) ? string.Empty : detail.customer.email,
                Phone = detail.billing.phone,
                DateEnrollment = DateTime.Parse(detail.customer.firstLogin).ToUniversalTime(),
            },
            BillingAddress = new SalesChannelImportCustomerAddress
            {
                Firstname = detail.billing.firstName,
                Lastname = detail.billing.lastName,
                CompanyName = detail.billing.company,
                Street = detail.billing.street,
                City = detail.billing.city,
                Zip = detail.billing.zipCode,
                Country = detail.billing.country.iso,
            },
            ShippingAddress = new SalesChannelImportCustomerAddress
            {
                Firstname = detail.shipping.firstName,
                Lastname = detail.shipping.lastName,
                CompanyName = detail.shipping.company,
                Street = detail.shipping.street,
                City = detail.shipping.city,
                Zip = detail.shipping.zipCode,
                Country = detail.shipping.country.iso,
            },
            OrderItems = detail.details.Select(item => new SalesChannelImportOrderItem
            {
                Name = item.articleName,
                Sku = item.articleNumber,
                Quantity = (double)item.quantity,
                Price = item.price,
                TaxRate = item.taxRate,
                Ean = item.ean,
            }).ToList(),
        };
    }

    private static OrderStatus MapOrderStatus(int orderStatusId) => orderStatusId switch
    {
        0 => OrderStatus.Pending,
        1 => OrderStatus.Processing,
        8 => OrderStatus.OnHold,
        7 => OrderStatus.Completed,
        4 => OrderStatus.Cancelled,
        -1 => OrderStatus.Failed,
        _ => OrderStatus.Unknown,
    };

    private static PaymentStatus MapPaymentStatus(int paymentStatusId) => paymentStatusId switch
    {
        17 => PaymentStatus.Invoiced,
        11 => PaymentStatus.PartiallyPaid,
        12 => PaymentStatus.CompletelyPaid,
        13 => PaymentStatus.FirstReminder,
        14 => PaymentStatus.SecondReminder,
        15 => PaymentStatus.ThirdReminder,
        16 => PaymentStatus.Encashment,
        18 => PaymentStatus.Reserved,
        19 => PaymentStatus.Delayed,
        20 => PaymentStatus.ReCrediting,
        21 => PaymentStatus.ReviewNecessary,
        22 => PaymentStatus.NoCreditApproved,
        23 => PaymentStatus.CreditPreliminarilyAccepted,
        _ => PaymentStatus.Unknown,
    };
}
