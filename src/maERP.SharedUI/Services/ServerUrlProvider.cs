using Microsoft.Extensions.Logging;

namespace maERP.SharedUI.Services;

/// <summary>
/// A simple provider for the server URL that can be safely used from singleton services
/// </summary>
public class ServerUrlProvider
{
    private readonly ILogger<ServerUrlProvider> _logger;
    private Uri _serverUrl;
    private readonly string _defaultServerUrl = "https://localhost:8443/";

    public ServerUrlProvider(ILogger<ServerUrlProvider> logger)
    {
        _logger = logger;
        
        // Versuche, die Server-URL aus der Umgebungsvariable zu lesen
        var envServerUrl = Environment.GetEnvironmentVariable("MAERP_SERVER_BASE_URL");
        if (!string.IsNullOrEmpty(envServerUrl))
        {
            try
            {
                _serverUrl = new Uri(envServerUrl);
                _logger.LogInformation("Server URL aus Umgebungsvariable gesetzt: {Url}", _serverUrl);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Fehler beim Setzen der Server-URL aus Umgebungsvariable: {Url}", envServerUrl);
                _serverUrl = new Uri(_defaultServerUrl);
            }
        }
        else
        {
            _serverUrl = new Uri(_defaultServerUrl);
        }
    }

    /// <summary>
    /// Gets the current server URL
    /// </summary>
    public Uri ServerUrl => _serverUrl;

    /// <summary>
    /// Sets the server URL
    /// </summary>
    public void SetServerUrl(string url)
    {
        try
        {
            _serverUrl = new Uri(url);
            _logger.LogInformation("Server URL set to: {Url}", _serverUrl);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error setting server URL: {Url}", url);
            _serverUrl = new Uri(_defaultServerUrl);
        }
    }
}
