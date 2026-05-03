using maERP.Domain.Enums;

namespace maERP.SalesChannels.Abstractions;

/// <summary>
/// Contract every channel integration implements. Connectors are stateless — the orchestrator
/// builds a <see cref="SalesChannelContext"/> per dispatch (credentials, HttpClient, sync run)
/// and hands it in. Methods return concise <see cref="SyncResult"/>/<see cref="ExportResult"/>
/// records that map cleanly to <see cref="Domain.Entities.ChannelSyncRun"/> and
/// <see cref="Domain.Entities.ChannelExportOutbox"/> rows.
/// </summary>
public interface ISalesChannelConnector
{
    SalesChannelType Type { get; }
    SalesChannelCapabilities Capabilities { get; }

    Task<ConnectionTestResult> TestConnectionAsync(SalesChannelContext context);

    Task<SyncResult> ImportProductsAsync(SalesChannelContext context);
    Task<SyncResult> ImportOrdersAsync(SalesChannelContext context);
    Task<SyncResult> ImportCustomersAsync(SalesChannelContext context);

    Task<ExportResult> ExportProductAsync(SalesChannelContext context, ProductExportPayload payload);
    Task<ExportResult> UpdateStockAsync(SalesChannelContext context, StockUpdatePayload payload);
    Task<ExportResult> UpdatePriceAsync(SalesChannelContext context, PriceUpdatePayload payload);
    Task<ExportResult> UpdateOrderAsync(SalesChannelContext context, OrderUpdatePayload payload);
    Task<ExportResult> DelistProductAsync(SalesChannelContext context, DelistPayload payload);
}
