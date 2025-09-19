using Microsoft.Extensions.DependencyInjection;
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
}