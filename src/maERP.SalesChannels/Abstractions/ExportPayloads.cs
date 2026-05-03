namespace maERP.SalesChannels.Abstractions;

/// <summary>
/// Channel-agnostic payload shapes drained from <c>ChannelExportOutbox</c> and translated by
/// each connector to its native API DTO. The orchestrator hydrates these from the stored
/// JSON before invoking the connector method.
/// </summary>
public sealed record ProductExportPayload(
    Guid ProductId,
    Guid ProductSalesChannelId,
    string Sku,
    string Name,
    string? Description,
    decimal Price,
    decimal? MinPrice,
    decimal? MaxPrice,
    string? Currency,
    int Stock,
    string? Ean,
    string? Gtin,
    string? Mpn,
    string? Brand,
    string? RemoteProductId,
    string? ExternalListingId,
    string? MetadataJson);

public sealed record StockUpdatePayload(
    Guid ProductId,
    Guid ProductSalesChannelId,
    string Sku,
    int Quantity,
    string? RemoteProductId);

public sealed record PriceUpdatePayload(
    Guid ProductId,
    Guid ProductSalesChannelId,
    string Sku,
    decimal Price,
    string? Currency,
    string? RemoteProductId,
    string? ExternalListingId);

public sealed record OrderUpdatePayload(
    Guid OrderId,
    string? RemoteOrderId,
    string Status,
    string? TrackingNumber,
    string? ShippingProvider);

public sealed record DelistPayload(
    Guid ProductSalesChannelId,
    string Sku,
    string? RemoteProductId,
    string? ExternalListingId);
