namespace maERP.Application.Mediator;

/// <summary>
/// Marker interface for requests that return a response
/// </summary>
/// <typeparam name="TResponse">The type of response returned</typeparam>
public interface IRequest<out TResponse>
{
}