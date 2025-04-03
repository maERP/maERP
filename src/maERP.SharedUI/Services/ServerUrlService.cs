using Blazored.LocalStorage;
using maERP.SharedUI.Contracts;
using Microsoft.Extensions.Logging;
using Microsoft.JSInterop;

namespace maERP.SharedUI.Services;

/// <summary>
/// Service for managing server URL configuration
/// </summary>
public class ServerUrlService : IServerUrlService
{
    private readonly ILocalStorageService _localStorageService;
    private readonly ILogger<ServerUrlService> _logger;
    private readonly ServerUrlProvider _serverUrlProvider;
    private readonly IJSRuntime _jsRuntime;

    public ServerUrlService(
        ILocalStorageService localStorageService, 
        ILogger<ServerUrlService> logger,
        ServerUrlProvider serverUrlProvider,
        IJSRuntime jsRuntime)
    {
        _localStorageService = localStorageService;
        _logger = logger;
        _serverUrlProvider = serverUrlProvider;
        _jsRuntime = jsRuntime;
    }

    /// <summary>
    /// Gets the current server URL from the provider
    /// </summary>
    public Uri ServerUrl => _serverUrlProvider.ServerUrl;

    /// <summary>
    /// Initializes the service by loading the server URL from various sources
    /// </summary>
    public async Task InitializeAsync()
    {
        try
        {
            // Versuche zuerst, die URL aus der JS-Konfiguration zu lesen
            try
            {
                var jsServerUrl = await _jsRuntime.InvokeAsync<string>("eval", "window.maERP?.serverUrl");
                if (!string.IsNullOrEmpty(jsServerUrl))
                {
                    // URL aus JavaScript-Konfiguration verwenden
                    _serverUrlProvider.SetServerUrl(jsServerUrl);
                    _logger.LogInformation("Server-URL aus JavaScript-Konfiguration geladen: {Url}", jsServerUrl);
                    return;
                }
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Konnte die Server-URL nicht aus der JavaScript-Konfiguration laden");
            }

            // Als Fallback lokalen Speicher verwenden
            if (await _localStorageService.ContainKeyAsync("server"))
            {
                var savedUrl = await _localStorageService.GetItemAsStringAsync("server");
                if (!string.IsNullOrEmpty(savedUrl))
                {
                    // Update the provider with the URL from storage
                    _serverUrlProvider.SetServerUrl(savedUrl);
                    _logger.LogInformation("Server-URL aus dem lokalen Speicher geladen: {Url}", savedUrl);
                }
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Fehler beim Laden der Server-URL");
        }
    }

    /// <summary>
    /// Sets the server URL
    /// </summary>
    public async Task SetServerUrlAsync(string url)
    {
        try
        {
            // Update the provider first
            _serverUrlProvider.SetServerUrl(url);
            
            // Then save to local storage
            await _localStorageService.SetItemAsStringAsync("server", url);
            _logger.LogInformation("Server-URL gesetzt auf: {Url}", url);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Fehler beim Setzen der Server-URL: {Url}", url);
            throw;
        }
    }
}
