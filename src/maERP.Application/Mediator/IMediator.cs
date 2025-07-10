namespace maERP.Application.Mediator;

/// <summary>
/// Mediator interface for sending requests to handlers
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
}