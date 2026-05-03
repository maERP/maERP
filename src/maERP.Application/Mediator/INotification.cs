namespace maERP.Application.Mediator;

/// <summary>
/// Marker interface for notifications published via <see cref="IMediator.Publish{TNotification}"/>.
/// Notifications are fire-and-forget broadcasts that may be handled by zero or more
/// <see cref="INotificationHandler{TNotification}"/> instances.
/// </summary>
public interface INotification
{
}
