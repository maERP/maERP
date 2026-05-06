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
/// <c>WooCommerceProductImportTask</c> + <c>WooCommerceSalesImportTask</c>; same SDK, same
/// mappings, but routed through the connector contract so the orchestrator owns scheduling.
/// </summary>
public sealed class WooCommerceConnector : ConnectorBase
{
    private readonly IProductImportRepository _productImportRepository;
    private readonly ISalesImportRepository _salesImportRepository;
    private readonly ILogger<WooCommerceConnector> _logger;

    public WooCommerceConnector(
        IProductImportRepository productImportRepository,
        ISalesImportRepository salesImportRepository,
        ILogger<WooCommerceConnector> logger)
    {
        _productImportRepository = productImportRepository;
        _salesImportRepository = salesImportRepository;
        _logger = logger;
    }

    public override SalesChannelType Type => SalesChannelType.WooCommerce;

    public override SalesChannelCapabilities Capabilities =>
        SalesChannelCapabilities.ImportProducts |
        SalesChannelCapabilities.ImportSaless |
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

    public override async Task<SyncResult> ImportSalessAsync(SalesChannelContext context)
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
            var remoteSaless = await wc.Order.GetAll();

            foreach (var remoteSales in remoteSaless)
            {
                try
                {
                    decimal subtotal = (remoteSales.total ?? 0) - (remoteSales.total_tax ?? 0) - (remoteSales.shipping_total ?? 0);

                    var importSales = new SalesChannelImportSales
                    {
                        RemoteSalesId = remoteSales.id.ToString(),
                        DateSalesed = remoteSales.date_created ?? DateTime.UtcNow,
                        Status = MapSalesStatus(remoteSales.status),
                        PaymentStatus = PaymentStatus.Unknown,
                        PaymentMethod = remoteSales.payment_method,
                        PaymentProvider = remoteSales.payment_method_title,
                        PaymentTransactionId = remoteSales.transaction_id,
                        Subtotal = subtotal,
                        ShippingCost = remoteSales.shipping_total ?? 0,
                        TotalTax = remoteSales.total_tax ?? 0,
                        Total = remoteSales.total ?? 0,
                        Customer = new SalesChannelImportCustomer
                        {
                            Firstname = remoteSales.billing.first_name,
                            Lastname = remoteSales.billing.last_name,
                            CompanyName = remoteSales.billing.company,
                            Email = remoteSales.billing.email,
                            Phone = remoteSales.billing.phone,
                            DateEnrollment = remoteSales.date_created_gmt ?? DateTime.UtcNow,
                        },
                        BillingAddress = new SalesChannelImportCustomerAddress
                        {
                            Firstname = remoteSales.billing.first_name,
                            Lastname = remoteSales.billing.last_name,
                            CompanyName = remoteSales.billing.company,
                            Street = remoteSales.billing.address_1,
                            City = remoteSales.billing.city,
                            Zip = remoteSales.billing.postcode,
                            Country = remoteSales.billing.country,
                        },
                        ShippingAddress = new SalesChannelImportCustomerAddress
                        {
                            Firstname = remoteSales.shipping.first_name,
                            Lastname = remoteSales.shipping.last_name,
                            CompanyName = remoteSales.shipping.company,
                            Street = remoteSales.shipping.address_1,
                            City = remoteSales.shipping.city,
                            Zip = remoteSales.shipping.postcode,
                            Country = remoteSales.shipping.country,
                        },
                        SalesItems = remoteSales.line_items.Select(item => new SalesChannelImportSalesItem
                        {
                            Name = item.name,
                            Sku = item.sku,
                            Quantity = (double)item.quantity!,
                            Price = (decimal)item.price!,
                            TaxRate = string.IsNullOrEmpty(item.tax_class) ? 0 : Convert.ToDouble(item.tax_class),
                        }).ToList(),
                    };

                    await _salesImportRepository.ImportOrUpdateFromSalesChannel(context.SalesChannel, importSales);
                    processed++;
                }
                catch (Exception ex)
                {
                    failed++;
                    _logger.LogError(ex, "WooCommerce sales import failed for {Id}", remoteSales.id);
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

    private static SalesStatus MapSalesStatus(string salesStatus) => salesStatus switch
    {
        "pending" => SalesStatus.Pending,
        "processing" => SalesStatus.Processing,
        "on-hold" => SalesStatus.OnHold,
        "completed" => SalesStatus.Completed,
        "cancelled" => SalesStatus.Cancelled,
        "refunded" => SalesStatus.Refunded,
        "failed" => SalesStatus.Failed,
        _ => SalesStatus.Unknown,
    };
}
