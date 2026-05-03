using maERP.Application.Mediator;
using maERP.Application.Notifications;
using maERP.Domain.Enums;
using maERP.Persistence.DatabaseContext;
using maERP.SalesChannels.Orchestration;
using Microsoft.EntityFrameworkCore;

namespace maERP.SalesChannels.NotificationHandlers;

/// <summary>
/// On Product create/update, enqueue an <c>ExportProduct</c> outbox row for every channel where
/// the product is listed (PSC.IsListed=true) AND the channel exports products AND IsEnabled.
/// Deletion routes to <c>DelistProduct</c> on every previously-listed channel.
/// </summary>
public sealed class ProductChangedNotificationHandler : INotificationHandler<ProductChangedNotification>
{
    private readonly ApplicationDbContext _context;
    private readonly ChannelExportOutboxEnqueuer _enqueuer;

    public ProductChangedNotificationHandler(ApplicationDbContext context, ChannelExportOutboxEnqueuer enqueuer)
    {
        _context = context;
        _enqueuer = enqueuer;
    }

    public async Task Handle(ProductChangedNotification notification, CancellationToken cancellationToken)
    {
        var channelIds = await _context.ProductSalesChannel
            .IgnoreQueryFilters()
            .Where(psc => psc.ProductId == notification.ProductId
                          && psc.IsListed
                          && psc.SalesChannel != null
                          && psc.SalesChannel.IsEnabled
                          && psc.SalesChannel.ExportProducts)
            .Select(psc => psc.SalesChannelId)
            .Distinct()
            .ToListAsync(cancellationToken);

        if (channelIds.Count == 0)
        {
            return;
        }

        var operation = notification.Kind == ProductChangeKind.Deleted
            ? ChannelSyncOperation.DelistProduct
            : ChannelSyncOperation.ExportProduct;

        await _enqueuer.EnqueueAsync(
            channelIds,
            operation,
            ChannelOutboxAggregateType.Product,
            notification.ProductId,
            notification.TenantId,
            cancellationToken);
    }
}
