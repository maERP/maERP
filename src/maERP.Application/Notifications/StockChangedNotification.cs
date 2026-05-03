using maERP.Application.Mediator;

namespace maERP.Application.Notifications;

/// <summary>
/// Raised when ProductStock changes. Triggers <c>UpdateStock</c> exports per channel.
/// </summary>
public sealed record StockChangedNotification(Guid ProductId, Guid WarehouseId, Guid? TenantId) : INotification;
