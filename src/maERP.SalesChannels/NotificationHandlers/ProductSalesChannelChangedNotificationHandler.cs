using maERP.Application.Mediator;
using maERP.Application.Notifications;
using maERP.Domain.Enums;
using maERP.Persistence.DatabaseContext;
using maERP.SalesChannels.Orchestration;
using Microsoft.EntityFrameworkCore;

namespace maERP.SalesChannels.NotificationHandlers;

/// <summary>
/// Per-channel listing config changed (price overrides, IsListed toggled, item-specifics edited).
/// Re-export to the single channel only — no fan-out.
/// </summary>
public sealed class ProductSalesChannelChangedNotificationHandler
    : INotificationHandler<ProductSalesChannelChangedNotification>
{
    private readonly ApplicationDbContext _context;
    private readonly ChannelExportOutboxEnqueuer _enqueuer;

    public ProductSalesChannelChangedNotificationHandler(
        ApplicationDbContext context,
        ChannelExportOutboxEnqueuer enqueuer)
    {
        _context = context;
        _enqueuer = enqueuer;
    }

    public async Task Handle(ProductSalesChannelChangedNotification notification, CancellationToken cancellationToken)
    {
        var psc = await _context.ProductSalesChannel
            .IgnoreQueryFilters()
            .Include(p => p.SalesChannel)
            .FirstOrDefaultAsync(p => p.Id == notification.ProductSalesChannelId, cancellationToken);

        if (psc?.SalesChannel is null || !psc.SalesChannel.IsEnabled || !psc.SalesChannel.ExportProducts)
        {
            return;
        }

        var operation = psc.IsListed
            ? ChannelSyncOperation.ExportProduct
            : ChannelSyncOperation.DelistProduct;

        await _enqueuer.EnqueueAsync(
            new[] { notification.SalesChannelId },
            operation,
            ChannelOutboxAggregateType.Product,
            notification.ProductId,
            notification.TenantId,
            cancellationToken);
    }
}
