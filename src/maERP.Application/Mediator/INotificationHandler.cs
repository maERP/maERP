namespace maERP.Application.Mediator;

/// <summary>
/// Handler for a notification. Multiple handlers may be registered per notification type.
/// </summary>
/// <typeparam name="TNotification">The notification type handled by this implementation.</typeparam>
public interface INotificationHandler<in TNotification>
    where TNotification : INotification
{
    /// <summary>
    /// Handle the notification. Exceptions are aggregated by the mediator and rethrown
    /// after all handlers have completed; one failing handler does not prevent the others
    /// from running.
    /// </summary>
    Task Handle(TNotification notification, CancellationToken cancellationToken);
}
