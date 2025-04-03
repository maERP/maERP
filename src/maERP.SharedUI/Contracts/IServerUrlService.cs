namespace maERP.SharedUI.Contracts;

/// <summary>
/// Interface for services that manage server URL configuration
/// </summary>
public interface IServerUrlService
{
    /// <summary>
    /// Gets the current server URL
    /// </summary>
    Uri ServerUrl { get; }

    /// <summary>
    /// Initializes the service by loading the server URL from storage
    /// </summary>
    Task InitializeAsync();

    /// <summary>
    /// Sets the server URL
    /// </summary>
    Task SetServerUrlAsync(string url);
}
