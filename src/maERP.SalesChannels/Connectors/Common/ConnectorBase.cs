using maERP.Domain.Enums;
using maERP.SalesChannels.Abstractions;

namespace maERP.SalesChannels.Connectors.Common;

/// <summary>
/// Base class for connectors. Provides default <c>NotSupported</c> implementations for every
/// method on <see cref="ISalesChannelConnector"/> so a concrete connector only overrides what
/// it actually supports — the bitmask in <see cref="Capabilities"/> reflects the truth.
/// </summary>
public abstract class ConnectorBase : ISalesChannelConnector
{
    public abstract SalesChannelType Type { get; }
    public abstract SalesChannelCapabilities Capabilities { get; }

    public virtual Task<ConnectionTestResult> TestConnectionAsync(SalesChannelContext context)
        => Task.FromResult(new ConnectionTestResult(true));

    public virtual Task<SyncResult> ImportProductsAsync(SalesChannelContext context)
        => Task.FromResult(SyncResult.Empty);

    public virtual Task<SyncResult> ImportOrdersAsync(SalesChannelContext context)
        => Task.FromResult(SyncResult.Empty);

    public virtual Task<SyncResult> ImportCustomersAsync(SalesChannelContext context)
        => Task.FromResult(SyncResult.Empty);

    public virtual Task<ExportResult> ExportProductAsync(SalesChannelContext context, ProductExportPayload payload)
        => Task.FromResult(ExportResult.Fail($"{Type} does not support ExportProduct"));

    public virtual Task<ExportResult> UpdateStockAsync(SalesChannelContext context, StockUpdatePayload payload)
        => Task.FromResult(ExportResult.Fail($"{Type} does not support UpdateStock"));

    public virtual Task<ExportResult> UpdatePriceAsync(SalesChannelContext context, PriceUpdatePayload payload)
        => Task.FromResult(ExportResult.Fail($"{Type} does not support UpdatePrice"));

    public virtual Task<ExportResult> UpdateOrderAsync(SalesChannelContext context, OrderUpdatePayload payload)
        => Task.FromResult(ExportResult.Fail($"{Type} does not support UpdateOrder"));

    public virtual Task<ExportResult> DelistProductAsync(SalesChannelContext context, DelistPayload payload)
        => Task.FromResult(ExportResult.Fail($"{Type} does not support DelistProduct"));
}
