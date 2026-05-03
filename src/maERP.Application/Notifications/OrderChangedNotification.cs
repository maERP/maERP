using maERP.Application.Mediator;

namespace maERP.Application.Notifications;

/// <summary>
/// Raised on order create / status change / cancel. Routes to <c>UpdateOrder</c> exports.
/// </summary>
public sealed record OrderChangedNotification(Guid OrderId, Guid? TenantId, OrderChangeKind Kind) : INotification;

public enum OrderChangeKind
{
    Created = 0,
    StatusChanged = 1,
    Cancelled = 2,
    Refunded = 3,
}
