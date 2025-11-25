using Windows.Storage;

namespace maERP.Client.Features.Auth.Services;

public class TokenStorageService : ITokenStorageService
{
    private const string TokenKey = "auth_token";
    private const string ServerUrlKey = "server_url";
    private const string TenantIdKey = "current_tenant_id";

    public async Task<string?> GetTokenAsync()
    {
        try
        {
            var localSettings = ApplicationData.Current.LocalSettings;
            if (localSettings.Values.TryGetValue(TokenKey, out var token))
            {
                return token?.ToString();
            }
            return null;
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error getting token: {ex.Message}");
            return null;
        }
    }

    public async Task SetTokenAsync(string token)
    {
        try
        {
            var localSettings = ApplicationData.Current.LocalSettings;
            localSettings.Values[TokenKey] = token;
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error setting token: {ex.Message}");
        }
        await Task.CompletedTask;
    }

    public async Task ClearTokenAsync()
    {
        try
        {
            var localSettings = ApplicationData.Current.LocalSettings;
            localSettings.Values.Remove(TokenKey);
            localSettings.Values.Remove(TenantIdKey);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error clearing token: {ex.Message}");
        }
        await Task.CompletedTask;
    }

    public async Task<string?> GetServerUrlAsync()
    {
        try
        {
            var localSettings = ApplicationData.Current.LocalSettings;
            if (localSettings.Values.TryGetValue(ServerUrlKey, out var serverUrl))
            {
                return serverUrl?.ToString();
            }
            return null;
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error getting server URL: {ex.Message}");
            return null;
        }
    }

    public async Task SetServerUrlAsync(string serverUrl)
    {
        try
        {
            var localSettings = ApplicationData.Current.LocalSettings;
            localSettings.Values[ServerUrlKey] = serverUrl;
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error setting server URL: {ex.Message}");
        }
        await Task.CompletedTask;
    }

    public async Task<Guid?> GetCurrentTenantIdAsync()
    {
        try
        {
            var localSettings = ApplicationData.Current.LocalSettings;
            if (localSettings.Values.TryGetValue(TenantIdKey, out var tenantId))
            {
                if (Guid.TryParse(tenantId?.ToString(), out var parsedGuid))
                {
                    return parsedGuid;
                }
            }
            return null;
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error getting tenant ID: {ex.Message}");
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
                localSettings.Values[TenantIdKey] = tenantId.Value.ToString();
            }
            else
            {
                localSettings.Values.Remove(TenantIdKey);
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error setting tenant ID: {ex.Message}");
        }
        await Task.CompletedTask;
    }
}
