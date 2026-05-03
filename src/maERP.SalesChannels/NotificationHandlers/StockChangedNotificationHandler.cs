using maERP.Application.Mediator;
using maERP.Application.Notifications;
using maERP.Domain.Enums;
using maERP.Persistence.DatabaseContext;
using maERP.SalesChannels.Orchestration;
using Microsoft.EntityFrameworkCore;

namespace maERP.SalesChannels.NotificationHandlers;

public sealed class StockChangedNotificationHandler : INotificationHandler<StockChangedNotification>
{
    private readonly ApplicationDbContext _context;
    private readonly ChannelExportOutboxEnqueuer _enqueuer;

    public StockChangedNotificationHandler(ApplicationDbContext context, ChannelExportOutboxEnqueuer enqueuer)
    {
        _context = context;
        _enqueuer = enqueuer;
    }

    public async Task Handle(StockChangedNotification notification, CancellationToken cancellationToken)
    {
        // Only push stock to channels that (a) have UpdateStock-type capability via ExportProducts,
        // (b) are linked to the warehouse where stock changed, AND (c) have the product listed.
        var channelIds = await _context.ProductSalesChannel
            .IgnoreQueryFilters()
            .Where(psc => psc.ProductId == notification.ProductId
                          && psc.IsListed
                          && psc.SalesChannel != null
                          && psc.SalesChannel.IsEnabled
                          && psc.SalesChannel.ExportProducts
                          && psc.SalesChannel.Warehouses.Any(w => w.Id == notification.WarehouseId))
            .Select(psc => psc.SalesChannelId)
            .Distinct()
            .ToListAsync(cancellationToken);

        if (channelIds.Count == 0)
        {
            return;
        }

        await _enqueuer.EnqueueAsync(
            channelIds,
            ChannelSyncOperation.UpdateStock,
            ChannelOutboxAggregateType.Stock,
            notification.ProductId,
            notification.TenantId,
            cancellationToken);
    }
}
