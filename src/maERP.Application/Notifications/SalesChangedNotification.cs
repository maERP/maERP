using maERP.Application.Mediator;

namespace maERP.Application.Notifications;

/// <summary>
/// Raised on sales create / status change / cancel. Routes to <c>UpdateSales</c> exports.
/// </summary>
public sealed record SalesChangedNotification(Guid SalesId, Guid? TenantId, SalesChangeKind Kind) : INotification;

public enum SalesChangeKind
{
    Created = 0,
    StatusChanged = 1,
    Cancelled = 2,
    Refunded = 3,
}
