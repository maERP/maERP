using Windows.Storage;

namespace maERP.Client.Features.Auth.Services;

public class TokenStorageService : ITokenStorageService
{
    private const string TokenKey = "auth_token";
    private const string ServerUrlKey = "server_url";
    private const string TenantIdKey = "current_tenant_id";

    private readonly ILogger<TokenStorageService> _logger;

    public TokenStorageService(ILogger<TokenStorageService> logger)
    {
        _logger = logger;
    }

    public async Task<string?> GetTokenAsync()
    {
        try
        {
            _logger.LogDebug("Retrieving authentication token from local storage");
            var localSettings = ApplicationData.Current.LocalSettings;
            if (localSettings.Values.TryGetValue(TokenKey, out var token))
            {
                _logger.LogDebug("Token found in local storage");
                return token?.ToString();
            }
            _logger.LogDebug("No token found in local storage");
            return null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving token from local storage");
            return null;
        }
    }

    public async Task SetTokenAsync(string token)
    {
        try
        {
            _logger.LogDebug("Storing authentication token in local storage");
            var localSettings = ApplicationData.Current.LocalSettings;
            localSettings.Values[TokenKey] = token;
            _logger.LogInformation("Authentication token stored successfully");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error storing token in local storage");
        }
        await Task.CompletedTask;
    }

    public async Task ClearTokenAsync()
    {
        try
        {
            _logger.LogDebug("Clearing authentication data from local storage");
            var localSettings = ApplicationData.Current.LocalSettings;
            localSettings.Values.Remove(TokenKey);
            localSettings.Values.Remove(TenantIdKey);
            _logger.LogInformation("Authentication data cleared from local storage");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error clearing authentication data from local storage");
        }
        await Task.CompletedTask;
    }

    public async Task<string?> GetServerUrlAsync()
    {
        try
        {
            _logger.LogDebug("Retrieving server URL from local storage");
            var localSettings = ApplicationData.Current.LocalSettings;
            if (localSettings.Values.TryGetValue(ServerUrlKey, out var serverUrl))
            {
                var url = serverUrl?.ToString();
                _logger.LogDebug("Server URL retrieved: {ServerUrl}", url);
                return url;
            }
            _logger.LogDebug("No server URL found in local storage");
            return null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving server URL from local storage");
            return null;
        }
    }

    public async Task SetServerUrlAsync(string serverUrl)
    {
        try
        {
            _logger.LogDebug("Storing server URL in local storage: {ServerUrl}", serverUrl);
            var localSettings = ApplicationData.Current.LocalSettings;
            localSettings.Values[ServerUrlKey] = serverUrl;
            _logger.LogInformation("Server URL stored: {ServerUrl}", serverUrl);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error storing server URL in local storage");
        }
        await Task.CompletedTask;
    }

    public async Task<Guid?> GetCurrentTenantIdAsync()
    {
        try
        {
            _logger.LogDebug("Retrieving tenant ID from local storage");
            var localSettings = ApplicationData.Current.LocalSettings;
            if (localSettings.Values.TryGetValue(TenantIdKey, out var tenantId))
            {
                if (Guid.TryParse(tenantId?.ToString(), out var parsedGuid))
                {
                    _logger.LogDebug("Tenant ID retrieved: {TenantId}", parsedGuid);
                    return parsedGuid;
                }
                _logger.LogWarning("Invalid tenant ID format in local storage: {Value}", tenantId);
            }
            _logger.LogDebug("No tenant ID found in local storage");
            return null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving tenant ID from local storage");
            return null;
        }
    }

    public async Task SetCurrentTenantIdAsync(Guid? tenantId)
    {
        try
        {
            var localSettings = ApplicationData.Current.LocalSettings;
            if (tenantId.HasValue)
            {
                _logger.LogDebug("Storing tenant ID in local storage: {TenantId}", tenantId.Value);
                localSettings.Values[TenantIdKey] = tenantId.Value.ToString();
                _logger.LogInformation("Tenant ID stored: {TenantId}", tenantId.Value);
            }
            else
            {
                _logger.LogDebug("Removing tenant ID from local storage");
                localSettings.Values.Remove(TenantIdKey);
                _logger.LogInformation("Tenant ID removed from local storage");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error storing tenant ID in local storage");
        }
        await Task.CompletedTask;
    }
}
