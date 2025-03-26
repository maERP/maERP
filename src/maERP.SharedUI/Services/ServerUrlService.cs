using Blazored.LocalStorage;
using maERP.SharedUI.Contracts;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace maERP.SharedUI.Services;

/// <summary>
/// Service for managing server URL configuration
/// </summary>
public class ServerUrlService : IServerUrlService
{
    private readonly ILocalStorageService _localStorageService;
    private readonly ILogger<ServerUrlService> _logger;
    private readonly ServerUrlProvider _serverUrlProvider;

    public ServerUrlService(
        ILocalStorageService localStorageService, 
        ILogger<ServerUrlService> logger,
        ServerUrlProvider serverUrlProvider)
    {
        _localStorageService = localStorageService;
        _logger = logger;
        _serverUrlProvider = serverUrlProvider;
    }

    /// <summary>
    /// Gets the current server URL from the provider
    /// </summary>
    public Uri ServerUrl => _serverUrlProvider.ServerUrl;

    /// <summary>
    /// Initializes the service by loading the server URL from local storage if available
    /// </summary>
    public async Task InitializeAsync()
    {
        try
        {
            if (await _localStorageService.ContainKeyAsync("server"))
            {
                var savedUrl = await _localStorageService.GetItemAsStringAsync("server");
                if (!string.IsNullOrEmpty(savedUrl))
                {
                    // Update the provider with the URL from storage
                    _serverUrlProvider.SetServerUrl(savedUrl);
                    _logger.LogInformation("Loaded server URL from storage: {Url}", savedUrl);
                }
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error loading server URL from storage");
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
            _logger.LogInformation("Server URL set to: {Url}", url);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error setting server URL: {Url}", url);
            throw;
        }
    }
}
