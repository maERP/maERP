#nullable disable
using maERP.Application.Contracts.Persistence;
using maERP.Domain.Enums;
using maERP.SalesChannels.Abstractions;
using maERP.SalesChannels.Connectors.Common;
using maERP.SalesChannels.Contracts;
using maERP.SalesChannels.Models;
using Microsoft.Extensions.Logging;
using WooCommerceNET;
using WooCommerceNET.WooCommerce.v3;

namespace maERP.SalesChannels.Connectors.WooCommerce;

/// <summary>
/// WooCommerce REST API connector via the <c>WooCommerceNET</c> SDK. Migrated from
/// <c>WooCommerceProductImportTask</c> + <c>WooCommerceOrderImportTask</c>; same SDK, same
/// mappings, but routed through the connector contract so the orchestrator owns scheduling.
/// </summary>
public sealed class WooCommerceConnector : ConnectorBase
{
    private readonly IProductImportRepository _productImportRepository;
    private readonly IOrderImportRepository _orderImportRepository;
    private readonly ILogger<WooCommerceConnector> _logger;

    public WooCommerceConnector(
        IProductImportRepository productImportRepository,
        IOrderImportRepository orderImportRepository,
        ILogger<WooCommerceConnector> logger)
    {
        _productImportRepository = productImportRepository;
        _orderImportRepository = orderImportRepository;
        _logger = logger;
    }

    public override SalesChannelType Type => SalesChannelType.WooCommerce;

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
            var wc = BuildClient(context);
            await wc.Product.GetAll(new Dictionary<string, string> { ["per_page"] = "1" });
            return new ConnectionTestResult(true);
        }
        catch (Exception ex)
        {
            return new ConnectionTestResult(false, ex.Message);
        }
    }

    public override async Task<SyncResult> ImportProductsAsync(SalesChannelContext context)
    {
        try
        {
            SalesChannelUrlValidator.Validate(context.SalesChannel.Url);
        }
        catch (ArgumentException ex)
        {
            return SyncResult.Failed($"Invalid sales channel URL: {ex.Message}");
        }

        var processed = 0;
        var failed = 0;

        try
        {
            var wc = BuildClient(context);
            var remoteProducts = await wc.Product.GetAll();
            foreach (var remoteProduct in remoteProducts)
            {
                if (string.IsNullOrEmpty(remoteProduct.sku))
                {
                    _logger.LogDebug("Product {Name} has no SKU, skipping", remoteProduct.name);
                    continue;
                }

                try
                {
                    await _productImportRepository.ImportOrUpdateFromSalesChannel(context.SalesChannel.Id, new SalesChannelImportProduct
                    {
                        Name = remoteProduct.name,
                        Price = (decimal)remoteProduct.price!,
                        Sku = remoteProduct.sku,
                        TaxRate = 19,
                        Description = remoteProduct.description,
                    });
                    processed++;
                }
                catch (Exception ex)
                {
                    failed++;
                    _logger.LogError(ex, "WooCommerce product import failed for SKU {Sku}", remoteProduct.sku);
                }
            }
        }
        catch (Exception ex)
        {
            return SyncResult.Failed(ex.Message);
        }

        return new SyncResult(processed, failed);
    }

    public override async Task<SyncResult> ImportOrdersAsync(SalesChannelContext context)
    {
        try
        {
            SalesChannelUrlValidator.Validate(context.SalesChannel.Url);
        }
        catch (ArgumentException ex)
        {
            return SyncResult.Failed($"Invalid sales channel URL: {ex.Message}");
        }

        var processed = 0;
        var failed = 0;

        try
        {
            var wc = BuildClient(context);
            var remoteOrders = await wc.Order.GetAll();

            foreach (var remoteOrder in remoteOrders)
            {
                try
                {
                    decimal subtotal = (remoteOrder.total ?? 0) - (remoteOrder.total_tax ?? 0) - (remoteOrder.shipping_total ?? 0);

                    var importOrder = new SalesChannelImportOrder
                    {
                        RemoteOrderId = remoteOrder.id.ToString(),
                        DateOrdered = remoteOrder.date_created ?? DateTime.UtcNow,
                        Status = MapOrderStatus(remoteOrder.status),
                        PaymentStatus = PaymentStatus.Unknown,
                        PaymentMethod = remoteOrder.payment_method,
                        PaymentProvider = remoteOrder.payment_method_title,
                        PaymentTransactionId = remoteOrder.transaction_id,
                        Subtotal = subtotal,
                        ShippingCost = remoteOrder.shipping_total ?? 0,
                        TotalTax = remoteOrder.total_tax ?? 0,
                        Total = remoteOrder.total ?? 0,
                        Customer = new SalesChannelImportCustomer
                        {
                            Firstname = remoteOrder.billing.first_name,
                            Lastname = remoteOrder.billing.last_name,
                            CompanyName = remoteOrder.billing.company,
                            Email = remoteOrder.billing.email,
                            Phone = remoteOrder.billing.phone,
                            DateEnrollment = remoteOrder.date_created_gmt ?? DateTime.UtcNow,
                        },
                        BillingAddress = new SalesChannelImportCustomerAddress
                        {
                            Firstname = remoteOrder.billing.first_name,
                            Lastname = remoteOrder.billing.last_name,
                            CompanyName = remoteOrder.billing.company,
                            Street = remoteOrder.billing.address_1,
                            City = remoteOrder.billing.city,
                            Zip = remoteOrder.billing.postcode,
                            Country = remoteOrder.billing.country,
                        },
                        ShippingAddress = new SalesChannelImportCustomerAddress
                        {
                            Firstname = remoteOrder.shipping.first_name,
                            Lastname = remoteOrder.shipping.last_name,
                            CompanyName = remoteOrder.shipping.company,
                            Street = remoteOrder.shipping.address_1,
                            City = remoteOrder.shipping.city,
                            Zip = remoteOrder.shipping.postcode,
                            Country = remoteOrder.shipping.country,
                        },
                        OrderItems = remoteOrder.line_items.Select(item => new SalesChannelImportOrderItem
                        {
                            Name = item.name,
                            Sku = item.sku,
                            Quantity = (double)item.quantity!,
                            Price = (decimal)item.price!,
                            TaxRate = string.IsNullOrEmpty(item.tax_class) ? 0 : Convert.ToDouble(item.tax_class),
                        }).ToList(),
                    };

                    await _orderImportRepository.ImportOrUpdateFromSalesChannel(context.SalesChannel, importOrder);
                    processed++;
                }
                catch (Exception ex)
                {
                    failed++;
                    _logger.LogError(ex, "WooCommerce order import failed for {Id}", remoteOrder.id);
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
        if (string.IsNullOrEmpty(payload.RemoteProductId) || !uint.TryParse(payload.RemoteProductId, out var productId))
        {
            return ExportResult.Fail("WooCommerce product id (numeric RemoteProductId) is required for stock updates");
        }

        try
        {
            var wc = BuildClient(context);
            await wc.Product.Update(productId, new Product
            {
                stock_quantity = payload.Quantity,
                manage_stock = true,
            });
            return ExportResult.Ok(payload.RemoteProductId);
        }
        catch (Exception ex)
        {
            return ExportResult.Fail(ex.Message);
        }
    }

    public override async Task<ExportResult> UpdatePriceAsync(SalesChannelContext context, PriceUpdatePayload payload)
    {
        if (string.IsNullOrEmpty(payload.RemoteProductId) || !uint.TryParse(payload.RemoteProductId, out var productId))
        {
            return ExportResult.Fail("WooCommerce product id (numeric RemoteProductId) is required for price updates");
        }

        try
        {
            var wc = BuildClient(context);
            await wc.Product.Update(productId, new Product
            {
                regular_price = payload.Price,
            });
            return ExportResult.Ok(payload.RemoteProductId);
        }
        catch (Exception ex)
        {
            return ExportResult.Fail(ex.Message);
        }
    }

    private static WCObject BuildClient(SalesChannelContext context)
    {
        var sc = context.SalesChannel;
        var url = sc.Url.TrimEnd('/') + "/wp-json/wc/v3/";
        var rest = new RestAPI(url, sc.Username, context.Password);
        return new WCObject(rest);
    }

    private static OrderStatus MapOrderStatus(string orderStatus) => orderStatus switch
    {
        "pending" => OrderStatus.Pending,
        "processing" => OrderStatus.Processing,
        "on-hold" => OrderStatus.OnHold,
        "completed" => OrderStatus.Completed,
        "cancelled" => OrderStatus.Cancelled,
        "refunded" => OrderStatus.Refunded,
        "failed" => OrderStatus.Failed,
        _ => OrderStatus.Unknown,
    };
}
