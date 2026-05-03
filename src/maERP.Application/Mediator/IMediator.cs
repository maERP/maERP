namespace maERP.Application.Mediator;

/// <summary>
/// Mediator interface for sending requests to handlers and publishing notifications.
/// </summary>
public interface IMediator
{
    /// <summary>
    /// Send a request to its handler
    /// </summary>
    /// <typeparam name="TResponse">The type of response expected</typeparam>
    /// <param name="request">The request to send</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The response from the handler</returns>
    Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Publish a notification to all registered <see cref="INotificationHandler{TNotification}"/> instances.
    /// Handlers run sequentially; exceptions are aggregated and rethrown after all handlers complete,
    /// so one failing handler does not prevent the others from running.
    /// </summary>
    Task Publish<TNotification>(TNotification notification, CancellationToken cancellationToken = default)
        where TNotification : INotification;
}