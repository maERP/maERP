using System.Collections;
using System.Reflection;

namespace maERP.Application.Mediator;

/// <summary>
/// Custom mediator implementation to replace MediatR
/// </summary>
public class CustomMediator : IMediator
{
    private readonly IServiceProvider _serviceProvider;

    public CustomMediator(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
    }

    /// <summary>
    /// Send a request to its handler
    /// </summary>
    /// <typeparam name="TResponse">The type of response expected</typeparam>
    /// <param name="request">The request to send</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The response from the handler</returns>
    public async Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default)
    {
        if (request == null)
            throw new ArgumentNullException(nameof(request));

        var requestType = request.GetType();
        var handlerType = typeof(IRequestHandler<,>).MakeGenericType(requestType, typeof(TResponse));

        var handler = _serviceProvider.GetService(handlerType);
        if (handler == null)
        {
            throw new InvalidOperationException($"No handler found for request type {requestType.Name}");
        }

        var handleMethod = handlerType.GetMethod("Handle");
        if (handleMethod == null)
        {
            throw new InvalidOperationException($"Handle method not found on handler for {requestType.Name}");
        }

        var task = (Task<TResponse>)handleMethod.Invoke(handler, new object[] { request, cancellationToken })!;
        return await task;
    }

    /// <summary>
    /// Publish a notification to all registered handlers. Handlers run sequentially; exceptions
    /// are collected and rethrown as an <see cref="AggregateException"/> after all handlers complete.
    /// Resolution uses the runtime type of the notification, so the call site can pass a concrete
    /// notification through a base type without losing handler dispatch.
    /// </summary>
    public async Task Publish<TNotification>(TNotification notification, CancellationToken cancellationToken = default)
        where TNotification : INotification
    {
        if (notification is null)
            throw new ArgumentNullException(nameof(notification));

        var notificationType = notification.GetType();
        var handlerType = typeof(INotificationHandler<>).MakeGenericType(notificationType);
        var enumerableType = typeof(IEnumerable<>).MakeGenericType(handlerType);

        var handlers = _serviceProvider.GetService(enumerableType) as IEnumerable;
        if (handlers is null)
            return;

        var handleMethod = handlerType.GetMethod("Handle")
            ?? throw new InvalidOperationException($"Handle method not found on {handlerType.Name}");

        List<Exception>? exceptions = null;
        var args = new object[] { notification, cancellationToken };

        foreach (var handler in handlers)
        {
            try
            {
                var task = (Task)handleMethod.Invoke(handler, args)!;
                await task;
            }
            catch (TargetInvocationException tie) when (tie.InnerException is not null)
            {
                (exceptions ??= new List<Exception>()).Add(tie.InnerException);
            }
            catch (Exception ex)
            {
                (exceptions ??= new List<Exception>()).Add(ex);
            }
        }

        if (exceptions is null)
            return;
        if (exceptions.Count == 1)
            throw exceptions[0];
        throw new AggregateException(exceptions);
    }
}
