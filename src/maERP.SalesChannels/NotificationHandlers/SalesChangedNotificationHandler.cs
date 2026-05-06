using maERP.Application.Mediator;
using maERP.Application.Notifications;
using maERP.Domain.Enums;
using maERP.Persistence.DatabaseContext;
using maERP.SalesChannels.Orchestration;
using Microsoft.EntityFrameworkCore;

namespace maERP.SalesChannels.NotificationHandlers;

/// <summary>
/// Sales changes only propagate back to the originating channel — we don't broadcast an sales
/// to other marketplaces. Confirms shipment/cancel/refund updates only.
/// </summary>
public sealed class SalesChangedNotificationHandler : INotificationHandler<SalesChangedNotification>
{
    private readonly ApplicationDbContext _context;
    private readonly ChannelExportOutboxEnqueuer _enqueuer;

    public SalesChangedNotificationHandler(ApplicationDbContext context, ChannelExportOutboxEnqueuer enqueuer)
    {
        _context = context;
        _enqueuer = enqueuer;
    }

    public async Task Handle(SalesChangedNotification notification, CancellationToken cancellationToken)
    {
        var originSalesChannelId = await _context.Sales
            .IgnoreQueryFilters()
            .Where(o => o.Id == notification.SalesId)
            .Select(o => (Guid?)o.SalesChannelId)
            .FirstOrDefaultAsync(cancellationToken);

        if (originSalesChannelId is null)
        {
            return;
        }

        var canExport = await _context.SalesChannel
            .IgnoreQueryFilters()
            .AnyAsync(s => s.Id == originSalesChannelId && s.IsEnabled && s.ExportSaless, cancellationToken);

        if (!canExport)
        {
            return;
        }

        await _enqueuer.EnqueueAsync(
            new[] { originSalesChannelId.Value },
            ChannelSyncOperation.UpdateSales,
            ChannelOutboxAggregateType.Sales,
            notification.SalesId,
            notification.TenantId,
            cancellationToken);
    }
}
