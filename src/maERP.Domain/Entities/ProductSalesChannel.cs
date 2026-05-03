using maERP.Domain.Entities.Common;
using maERP.Domain.Enums;

namespace maERP.Domain.Entities;

public class ProductSalesChannel : BaseEntity, IBaseEntity
{
    public SalesChannel SalesChannel { get; set; } = new();
    public Guid SalesChannelId { get; set; }
    public Guid ProductId { get; set; } = new();
    public Product Product { get; set; } = new();

    /// <summary>
    /// Channel-side identifier for this listing (eBay SKU, Amazon ASIN/SKU, Shopware product number, ...).
    /// Stored as string because most channels do not use GUIDs.
    /// </summary>
    public string? RemoteProductId { get; set; }

    /// <summary>
    /// Channel-side listing/offer id once the product has been published (eBay offerId,
    /// Amazon listing-id, ...). Independent of <see cref="RemoteProductId"/> because
    /// some channels separate the catalog item from the offer.
    /// </summary>
    public string? ExternalListingId { get; set; }

    /// <summary>
    /// Whether this product should be listed on the channel. Lets a product exist in the
    /// PSC table without being pushed to the channel (e.g. drafted but not yet enabled).
    /// </summary>
    public bool IsListed { get; set; }

    /// <summary>
    /// Final selling price on this channel. Final price formula:
    /// <c>clamp(BasePrice + Surcharge(MinimumProfit, MinimumProfitUnit), MinPrice, MaxPrice)</c>.
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>Repricing floor — the engine must never set a price below this.</summary>
    public decimal? MinPrice { get; set; }

    /// <summary>Repricing ceiling — the engine must never set a price above this.</summary>
    public decimal? MaxPrice { get; set; }

    /// <summary>ISO 4217 currency code for this channel's price (e.g. "EUR", "USD").</summary>
    public string? Currency { get; set; }

    public RepricingType RepricingType { get; set; }
    public decimal MinimumProfit { get; set; }
    public MinimumProfitUnit MinimumProfitUnit { get; set; }

    /// <summary>
    /// Subtracted from the warehouse-summed stock before pushing to the channel — protects
    /// against oversell while keeping ERP stock as the single source of truth.
    /// </summary>
    public int StockBuffer { get; set; }

    public FulfillmentChannel FulfillmentChannel { get; set; }

    public SalesChannelSyncStatus SyncStatus { get; set; }

    public DateTime? LastSyncedAt { get; set; }

    /// <summary>SHA-256 of the last exported payload — skip re-export if unchanged.</summary>
    public string? LastExportHash { get; set; }

    public string? LastErrorMessage { get; set; }

    /// <summary>
    /// Channel-specific metadata (eBay item-specifics, Amazon productType attributes, browse-node ids, ...).
    /// Schema is owned by the connector; the domain treats it as opaque JSON.
    /// </summary>
    public string? MetadataJson { get; set; }
}
