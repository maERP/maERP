using System.Text;
using System.Text.Json;
using Windows.Storage;

namespace maERP.Client.Features.Auth.Services;

public class TokenStorageService : ITokenStorageService
{
    private const string TokenKey = "auth_token";
    private const string ServerUrlKey = "server_url";
    private const string TenantIdKey = "current_tenant_id";
    private const string RoleClaimType = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role";

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

    public async Task<IReadOnlyList<string>> GetRolesAsync()
    {
        try
        {
            var token = await GetTokenAsync();
            if (string.IsNullOrEmpty(token))
            {
                _logger.LogDebug("No token available to extract roles");
                return Array.Empty<string>();
            }

            var roles = ParseRolesFromJwt(token);
            _logger.LogDebug("Extracted roles from token: {Roles}", string.Join(", ", roles));
            return roles;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error extracting roles from token");
            return Array.Empty<string>();
        }
    }

    public async Task<bool> IsInRoleAsync(string role)
    {
        var roles = await GetRolesAsync();
        return roles.Contains(role, StringComparer.OrdinalIgnoreCase);
    }

    public async Task<DateTimeOffset?> GetTokenExpiryAsync()
    {
        try
        {
            var token = await GetTokenAsync();
            if (string.IsNullOrEmpty(token))
            {
                return null;
            }

            using var document = DecodeJwtPayload(token);
            if (document == null)
            {
                return null;
            }

            if (document.RootElement.TryGetProperty("exp", out var expElement)
                && expElement.TryGetInt64(out var expUnix))
            {
                return DateTimeOffset.FromUnixTimeSeconds(expUnix);
            }

            _logger.LogDebug("No 'exp' claim found in JWT token");
            return null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error reading token expiry");
            return null;
        }
    }

    private JsonDocument? DecodeJwtPayload(string token)
    {
        // JWT format: header.payload.signature
        var parts = token.Split('.');
        if (parts.Length != 3)
        {
            _logger.LogWarning("Invalid JWT token format");
            return null;
        }

        // Decode the payload (second part)
        var payload = parts[1];

        // Add padding if needed for Base64 decoding
        payload = payload.Replace('-', '+').Replace('_', '/');
        switch (payload.Length % 4)
        {
            case 2: payload += "=="; break;
            case 3: payload += "="; break;
        }

        var payloadBytes = Convert.FromBase64String(payload);
        var payloadJson = Encoding.UTF8.GetString(payloadBytes);

        return JsonDocument.Parse(payloadJson);
    }

    private IReadOnlyList<string> ParseRolesFromJwt(string token)
    {
        var roles = new List<string>();

        try
        {
            using var document = DecodeJwtPayload(token);
            if (document == null)
            {
                return roles;
            }

            var root = document.RootElement;

            // Try standard role claim
            if (root.TryGetProperty(RoleClaimType, out var roleElement))
            {
                ExtractRolesFromElement(roleElement, roles);
            }

            // Also try simple "role" claim
            if (root.TryGetProperty("role", out var simpleRoleElement))
            {
                ExtractRolesFromElement(simpleRoleElement, roles);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error parsing JWT token for roles");
        }

        return roles;
    }

    private static void ExtractRolesFromElement(JsonElement element, List<string> roles)
    {
        if (element.ValueKind == JsonValueKind.Array)
        {
            foreach (var item in element.EnumerateArray())
            {
                if (item.ValueKind == JsonValueKind.String)
                {
                    var role = item.GetString();
                    if (!string.IsNullOrEmpty(role) && !roles.Contains(role))
                    {
                        roles.Add(role);
                    }
                }
            }
        }
        else if (element.ValueKind == JsonValueKind.String)
        {
            var role = element.GetString();
            if (!string.IsNullOrEmpty(role) && !roles.Contains(role))
            {
                roles.Add(role);
            }
        }
    }
}
