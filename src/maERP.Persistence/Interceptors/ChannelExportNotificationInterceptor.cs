using maERP.Application.Mediator;
using maERP.Application.Notifications;
using maERP.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace maERP.Persistence.Interceptors;

/// <summary>
/// EF Core save-changes interceptor that fans domain mutations out as notifications. Picks up
/// every Add/Update/Delete on Product, ProductSalesChannel, ProductStock, Sales and Customer
/// regardless of whether the caller went through a CQRS command-handler or wrote to the DbContext
/// directly. Notifications are published after SaveChanges succeeds, so a failed save never
/// produces phantom outbox rows.
///
/// This is the safety-net half of the export pipeline; CQRS handlers are still free to publish
/// richer notifications themselves (e.g. <c>SalesChangeKind.StatusChanged</c>), and the outbox
/// enqueuer's idempotency-key coalesces both paths.
///
/// IMediator is resolved lazily so this interceptor can also be activated in bootstrap/migration
/// contexts where no Application-layer services are registered. In that case notifications are
/// silently skipped.
/// </summary>
public sealed class ChannelExportNotificationInterceptor : SaveChangesInterceptor
{
    private readonly IServiceProvider _services;
    private readonly ILogger<ChannelExportNotificationInterceptor> _logger;
    private List<INotification>? _pending;

    public ChannelExportNotificationInterceptor(IServiceProvider services, ILogger<ChannelExportNotificationInterceptor> logger)
    {
        _services = services;
        _logger = logger;
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        if (eventData.Context is null)
        {
            return ValueTask.FromResult(result);
        }

        _pending = CollectNotifications(eventData.Context.ChangeTracker);
        return ValueTask.FromResult(result);
    }

    public override async ValueTask<int> SavedChangesAsync(
        SaveChangesCompletedEventData eventData,
        int result,
        CancellationToken cancellationToken = default)
    {
        if (_pending is null || _pending.Count == 0)
        {
            return result;
        }

        var notifications = _pending;
        _pending = null;

        foreach (var notification in notifications)
        {
            try
            {
                await PublishAsync(notification, cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Notification handler failed for {Type}", notification.GetType().Name);
            }
        }

        return result;
    }

    public override void SaveChangesFailed(DbContextErrorEventData eventData)
    {
        _pending = null;
    }

    public override Task SaveChangesFailedAsync(DbContextErrorEventData eventData, CancellationToken cancellationToken = default)
    {
        _pending = null;
        return Task.CompletedTask;
    }

    private static List<INotification> CollectNotifications(ChangeTracker changeTracker)
    {
        var notifications = new List<INotification>();

        foreach (var entry in changeTracker.Entries())
        {
            if (entry.State is not (EntityState.Added or EntityState.Modified or EntityState.Deleted))
            {
                continue;
            }

            switch (entry.Entity)
            {
                case Product product:
                    notifications.Add(new ProductChangedNotification(
                        product.Id, product.TenantId, MapProductKind(entry.State)));
                    break;

                case ProductSalesChannel psc:
                    notifications.Add(new ProductSalesChannelChangedNotification(
                        psc.Id, psc.ProductId, psc.SalesChannelId, psc.TenantId));
                    break;

                case ProductStock stock:
                    notifications.Add(new StockChangedNotification(
                        stock.ProductId, stock.WarehouseId, stock.TenantId));
                    break;

                case Sales sales:
                    notifications.Add(new SalesChangedNotification(
                        sales.Id, sales.TenantId, MapSalesKind(entry.State)));
                    break;

                case Customer customer:
                    notifications.Add(new CustomerChangedNotification(
                        customer.Id, customer.TenantId, MapCustomerKind(entry.State)));
                    break;
            }
        }

        return notifications;
    }

    private async Task PublishAsync(INotification notification, CancellationToken cancellationToken)
    {
        var mediator = _services.GetService<IMediator>();
        if (mediator is null)
            return;

        switch (notification)
        {
            case ProductChangedNotification n:
                await mediator.Publish(n, cancellationToken);
                break;
            case ProductSalesChannelChangedNotification n:
                await mediator.Publish(n, cancellationToken);
                break;
            case StockChangedNotification n:
                await mediator.Publish(n, cancellationToken);
                break;
            case SalesChangedNotification n:
                await mediator.Publish(n, cancellationToken);
                break;
            case CustomerChangedNotification n:
                await mediator.Publish(n, cancellationToken);
                break;
        }
    }

    private static ProductChangeKind MapProductKind(EntityState state) => state switch
    {
        EntityState.Added => ProductChangeKind.Created,
        EntityState.Deleted => ProductChangeKind.Deleted,
        _ => ProductChangeKind.Updated,
    };

    private static SalesChangeKind MapSalesKind(EntityState state) => state switch
    {
        EntityState.Added => SalesChangeKind.Created,
        EntityState.Deleted => SalesChangeKind.Cancelled,
        _ => SalesChangeKind.StatusChanged,
    };

    private static CustomerChangeKind MapCustomerKind(EntityState state) => state switch
    {
        EntityState.Added => CustomerChangeKind.Created,
        EntityState.Deleted => CustomerChangeKind.Deleted,
        _ => CustomerChangeKind.Updated,
    };
}
