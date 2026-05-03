using maERP.Application.Mediator;

namespace maERP.Application.Notifications;

/// <summary>
/// Raised when a Product is created or updated. Notification handlers fan this out to all
/// SalesChannels with <c>ExportProducts=true</c> via the export outbox.
/// </summary>
public sealed record ProductChangedNotification(Guid ProductId, Guid? TenantId, ProductChangeKind Kind) : INotification;

public enum ProductChangeKind
{
    Created = 0,
    Updated = 1,
    Deleted = 2,
}
