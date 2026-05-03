using maERP.Application.Mediator;
using maERP.Application.Notifications;
using maERP.Domain.Enums;
using maERP.Persistence.DatabaseContext;
using maERP.SalesChannels.Orchestration;
using Microsoft.EntityFrameworkCore;

namespace maERP.SalesChannels.NotificationHandlers;

/// <summary>
/// Order changes only propagate back to the originating channel — we don't broadcast an order
/// to other marketplaces. Confirms shipment/cancel/refund updates only.
/// </summary>
public sealed class OrderChangedNotificationHandler : INotificationHandler<OrderChangedNotification>
{
    private readonly ApplicationDbContext _context;
    private readonly ChannelExportOutboxEnqueuer _enqueuer;

    public OrderChangedNotificationHandler(ApplicationDbContext context, ChannelExportOutboxEnqueuer enqueuer)
    {
        _context = context;
        _enqueuer = enqueuer;
    }

    public async Task Handle(OrderChangedNotification notification, CancellationToken cancellationToken)
    {
        var originSalesChannelId = await _context.Order
            .IgnoreQueryFilters()
            .Where(o => o.Id == notification.OrderId)
            .Select(o => (Guid?)o.SalesChannelId)
            .FirstOrDefaultAsync(cancellationToken);

        if (originSalesChannelId is null)
        {
            return;
        }

        var canExport = await _context.SalesChannel
            .IgnoreQueryFilters()
            .AnyAsync(s => s.Id == originSalesChannelId && s.IsEnabled && s.ExportOrders, cancellationToken);

        if (!canExport)
        {
            return;
        }

        await _enqueuer.EnqueueAsync(
            new[] { originSalesChannelId.Value },
            ChannelSyncOperation.UpdateOrder,
            ChannelOutboxAggregateType.Order,
            notification.OrderId,
            notification.TenantId,
            cancellationToken);
    }
}
