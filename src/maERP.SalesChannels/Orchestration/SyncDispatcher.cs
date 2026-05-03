using maERP.Domain.Entities;
using maERP.Domain.Enums;
using maERP.Persistence.DatabaseContext;
using maERP.SalesChannels.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace maERP.SalesChannels.Orchestration;

/// <summary>
/// Wraps a connector invocation with the <see cref="ChannelSyncRun"/> audit lifecycle:
/// open the row before dispatching, populate item counts + status from the connector's
/// <see cref="SyncResult"/>, persist on close. Connector exceptions land as Failed runs
/// instead of bubbling up to the orchestrator's tick loop.
/// </summary>
public sealed class SyncDispatcher
{
    private readonly ApplicationDbContext _context;
    private readonly ISalesChannelConnectorRegistry _registry;
    private readonly SalesChannelContextFactory _contextFactory;
    private readonly ILogger<SyncDispatcher> _logger;

    public SyncDispatcher(
        ApplicationDbContext context,
        ISalesChannelConnectorRegistry registry,
        SalesChannelContextFactory contextFactory,
        ILogger<SyncDispatcher> logger)
    {
        _context = context;
        _registry = registry;
        _contextFactory = contextFactory;
        _logger = logger;
    }

    public async Task<ChannelSyncRun> RunImportAsync(
        SalesChannel salesChannel,
        ChannelSyncOperation operation,
        ChannelSyncTriggerSource trigger,
        CancellationToken cancellationToken)
    {
        var connector = _registry.Resolve(salesChannel.Type);
        var run = await OpenRunAsync(salesChannel, operation, trigger, cancellationToken);

        if (connector is null || !ConnectorSupports(connector, operation))
        {
            await CloseRunAsync(run, ChannelSyncRunStatus.Failed, 0, 0, $"No capable connector for {salesChannel.Type}/{operation}", cancellationToken);
            return run;
        }

        try
        {
            var context = _contextFactory.Create(salesChannel, run, cancellationToken);
            var result = operation switch
            {
                ChannelSyncOperation.ImportProducts => await connector.ImportProductsAsync(context),
                ChannelSyncOperation.ImportOrders => await connector.ImportOrdersAsync(context),
                ChannelSyncOperation.ImportCustomers => await connector.ImportCustomersAsync(context),
                _ => SyncResult.Failed($"Operation {operation} is not an import"),
            };

            var status = result switch
            {
                { ErrorSummary: not null } when result.ItemsProcessed == 0 => ChannelSyncRunStatus.Failed,
                { ItemsFailed: > 0 } when result.ItemsProcessed > 0 => ChannelSyncRunStatus.PartialFailure,
                { ItemsFailed: > 0 } => ChannelSyncRunStatus.Failed,
                _ => ChannelSyncRunStatus.Success,
            };

            await CloseRunAsync(run, status, result.ItemsProcessed, result.ItemsFailed, result.ErrorSummary, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Sync dispatch failed for channel {Channel} op {Op}", salesChannel.Id, operation);
            await CloseRunAsync(run, ChannelSyncRunStatus.Failed, 0, 0, ex.Message, cancellationToken);
        }

        return run;
    }

    public async Task<ExportResult> RunExportAsync(
        SalesChannel salesChannel,
        ChannelExportOutbox outboxRow,
        CancellationToken cancellationToken)
    {
        var connector = _registry.Resolve(salesChannel.Type);
        if (connector is null)
        {
            return ExportResult.Fail($"No connector for {salesChannel.Type}");
        }

        var run = await OpenRunAsync(salesChannel, outboxRow.Operation, ChannelSyncTriggerSource.Event, cancellationToken);
        try
        {
            var context = _contextFactory.Create(salesChannel, run, cancellationToken);
            var result = await DispatchExportAsync(connector, context, outboxRow, cancellationToken);

            await CloseRunAsync(
                run,
                result.Success ? ChannelSyncRunStatus.Success : ChannelSyncRunStatus.Failed,
                result.Success ? 1 : 0,
                result.Success ? 0 : 1,
                result.ErrorMessage,
                cancellationToken);

            return result;
        }
        catch (Exception ex)
        {
            await CloseRunAsync(run, ChannelSyncRunStatus.Failed, 0, 1, ex.Message, cancellationToken);
            return ExportResult.Fail(ex.Message);
        }
    }

    private async Task<ChannelSyncRun> OpenRunAsync(
        SalesChannel salesChannel,
        ChannelSyncOperation operation,
        ChannelSyncTriggerSource trigger,
        CancellationToken cancellationToken)
    {
        var run = new ChannelSyncRun
        {
            Id = Guid.NewGuid(),
            TenantId = salesChannel.TenantId,
            SalesChannelId = salesChannel.Id,
            Operation = operation,
            TriggerSource = trigger,
            Status = ChannelSyncRunStatus.Running,
            StartedAt = DateTime.UtcNow,
            CorrelationId = Guid.NewGuid(),
        };

        _context.ChannelSyncRun.Add(run);
        await _context.SaveChangesAsync(cancellationToken);
        return run;
    }

    private async Task CloseRunAsync(
        ChannelSyncRun run,
        ChannelSyncRunStatus status,
        int itemsProcessed,
        int itemsFailed,
        string? errorSummary,
        CancellationToken cancellationToken)
    {
        run.FinishedAt = DateTime.UtcNow;
        run.Status = status;
        run.ItemsProcessed = itemsProcessed;
        run.ItemsFailed = itemsFailed;
        run.ErrorSummary = Truncate(errorSummary, 2000);

        await _context.SaveChangesAsync(cancellationToken);
    }

    private static bool ConnectorSupports(ISalesChannelConnector connector, ChannelSyncOperation operation) => operation switch
    {
        ChannelSyncOperation.ImportProducts => connector.Capabilities.HasFlag(SalesChannelCapabilities.ImportProducts),
        ChannelSyncOperation.ImportOrders => connector.Capabilities.HasFlag(SalesChannelCapabilities.ImportOrders),
        ChannelSyncOperation.ImportCustomers => connector.Capabilities.HasFlag(SalesChannelCapabilities.ImportCustomers),
        ChannelSyncOperation.ExportProduct => connector.Capabilities.HasFlag(SalesChannelCapabilities.ExportProducts),
        ChannelSyncOperation.UpdateStock => connector.Capabilities.HasFlag(SalesChannelCapabilities.UpdateStock),
        ChannelSyncOperation.UpdatePrice => connector.Capabilities.HasFlag(SalesChannelCapabilities.UpdatePrice),
        ChannelSyncOperation.UpdateOrder => connector.Capabilities.HasFlag(SalesChannelCapabilities.UpdateOrders),
        ChannelSyncOperation.DelistProduct => connector.Capabilities.HasFlag(SalesChannelCapabilities.DelistProducts),
        _ => false,
    };

    private async Task<ExportResult> DispatchExportAsync(
        ISalesChannelConnector connector,
        SalesChannelContext context,
        ChannelExportOutbox outbox,
        CancellationToken cancellationToken)
    {
        // Hydrate payload from current DB state — outbox rows store only the aggregate id, so a
        // coalesced row always carries the latest data when the drainer picks it up.
        return outbox.Operation switch
        {
            ChannelSyncOperation.ExportProduct => await ExportProductAsync(connector, context, outbox, cancellationToken),
            ChannelSyncOperation.UpdateStock => await UpdateStockAsync(connector, context, outbox, cancellationToken),
            ChannelSyncOperation.UpdatePrice => await UpdatePriceAsync(connector, context, outbox, cancellationToken),
            ChannelSyncOperation.UpdateOrder => await UpdateOrderAsync(connector, context, outbox, cancellationToken),
            ChannelSyncOperation.DelistProduct => await DelistProductAsync(connector, context, outbox, cancellationToken),
            _ => ExportResult.Fail($"Unsupported export operation {outbox.Operation}"),
        };
    }

    private async Task<ExportResult> ExportProductAsync(ISalesChannelConnector connector, SalesChannelContext context, ChannelExportOutbox outbox, CancellationToken cancellationToken)
    {
        var psc = await _context.ProductSalesChannel
            .IgnoreQueryFilters()
            .Include(p => p.Product)
            .FirstOrDefaultAsync(p => p.ProductId == outbox.AggregateId && p.SalesChannelId == outbox.SalesChannelId, cancellationToken);

        if (psc?.Product is null)
        {
            return ExportResult.Fail("ProductSalesChannel row not found at dispatch time");
        }

        var stock = await ComputeChannelStockAsync(outbox.SalesChannelId, psc.ProductId, psc.StockBuffer, cancellationToken);

        var payload = new ProductExportPayload(
            psc.ProductId,
            psc.Id,
            psc.Product.Sku,
            psc.Product.Name,
            psc.Product.Description,
            psc.Price,
            psc.MinPrice,
            psc.MaxPrice,
            psc.Currency,
            stock,
            psc.Product.Ean,
            psc.Product.Gtin,
            psc.Product.Mpn,
            psc.Product.Brand,
            psc.RemoteProductId,
            psc.ExternalListingId,
            psc.MetadataJson);

        return await connector.ExportProductAsync(context, payload);
    }

    private async Task<ExportResult> UpdateStockAsync(ISalesChannelConnector connector, SalesChannelContext context, ChannelExportOutbox outbox, CancellationToken cancellationToken)
    {
        var psc = await _context.ProductSalesChannel
            .IgnoreQueryFilters()
            .Include(p => p.Product)
            .FirstOrDefaultAsync(p => p.ProductId == outbox.AggregateId && p.SalesChannelId == outbox.SalesChannelId, cancellationToken);

        if (psc?.Product is null)
        {
            return ExportResult.Fail("ProductSalesChannel row not found at dispatch time");
        }

        var stock = await ComputeChannelStockAsync(outbox.SalesChannelId, psc.ProductId, psc.StockBuffer, cancellationToken);

        return await connector.UpdateStockAsync(context, new StockUpdatePayload(
            psc.ProductId, psc.Id, psc.Product.Sku, stock, psc.RemoteProductId));
    }

    private async Task<ExportResult> UpdatePriceAsync(ISalesChannelConnector connector, SalesChannelContext context, ChannelExportOutbox outbox, CancellationToken cancellationToken)
    {
        var psc = await _context.ProductSalesChannel
            .IgnoreQueryFilters()
            .Include(p => p.Product)
            .FirstOrDefaultAsync(p => p.ProductId == outbox.AggregateId && p.SalesChannelId == outbox.SalesChannelId, cancellationToken);

        if (psc?.Product is null)
        {
            return ExportResult.Fail("ProductSalesChannel row not found at dispatch time");
        }

        return await connector.UpdatePriceAsync(context, new PriceUpdatePayload(
            psc.ProductId, psc.Id, psc.Product.Sku, psc.Price, psc.Currency, psc.RemoteProductId, psc.ExternalListingId));
    }

    private async Task<ExportResult> UpdateOrderAsync(ISalesChannelConnector connector, SalesChannelContext context, ChannelExportOutbox outbox, CancellationToken cancellationToken)
    {
        var order = await _context.Order
            .IgnoreQueryFilters()
            .FirstOrDefaultAsync(o => o.Id == outbox.AggregateId, cancellationToken);

        if (order is null)
        {
            return ExportResult.Fail("Order not found at dispatch time");
        }

        return await connector.UpdateOrderAsync(context, new OrderUpdatePayload(
            order.Id, order.RemoteOrderId, order.Status.ToString(), null, null));
    }

    private async Task<ExportResult> DelistProductAsync(ISalesChannelConnector connector, SalesChannelContext context, ChannelExportOutbox outbox, CancellationToken cancellationToken)
    {
        var psc = await _context.ProductSalesChannel
            .IgnoreQueryFilters()
            .Include(p => p.Product)
            .FirstOrDefaultAsync(p => p.ProductId == outbox.AggregateId && p.SalesChannelId == outbox.SalesChannelId, cancellationToken);

        if (psc?.Product is null)
        {
            return ExportResult.Fail("ProductSalesChannel row not found at dispatch time");
        }

        return await connector.DelistProductAsync(context, new DelistPayload(
            psc.Id, psc.Product.Sku, psc.RemoteProductId, psc.ExternalListingId));
    }

    private async Task<int> ComputeChannelStockAsync(Guid salesChannelId, Guid productId, int stockBuffer, CancellationToken cancellationToken)
    {
        // Sum stock from the warehouses attached to this channel; subtract the per-channel buffer.
        var stock = await _context.ProductStock
            .IgnoreQueryFilters()
            .Where(ps => ps.ProductId == productId &&
                         _context.SalesChannel
                             .IgnoreQueryFilters()
                             .Where(sc => sc.Id == salesChannelId)
                             .SelectMany(sc => sc.Warehouses)
                             .Any(w => w.Id == ps.WarehouseId))
            .SumAsync(ps => (double?)ps.Stock, cancellationToken) ?? 0;

        var available = (int)Math.Floor(stock) - stockBuffer;
        return Math.Max(0, available);
    }

    private static string? Truncate(string? value, int max)
    {
        if (string.IsNullOrEmpty(value)) return value;
        return value.Length <= max ? value : value[..max];
    }
}
