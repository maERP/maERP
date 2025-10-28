namespace maERP.Client.Services.Authentication;

/// <summary>
/// Implementation of authentication state service using in-memory storage
/// TODO: Replace with platform-specific secure storage implementation
/// </summary>
public class AuthenticationStateService : IAuthenticationStateService
{
    private readonly ILogger<AuthenticationStateService> _logger;
    private string? _cachedAccessToken;
    private string? _cachedRefreshToken;
    private string? _cachedServerUrl;

    public AuthenticationStateService(ILogger<AuthenticationStateService> logger)
    {
        _logger = logger;
    }

    public Task<string?> GetAccessTokenAsync()
    {
        return Task.FromResult(_cachedAccessToken);
    }

    public Task SetAccessTokenAsync(string token)
    {
        _cachedAccessToken = token;
        _logger.LogDebug("Access token stored successfully");
        return Task.CompletedTask;
    }

    public Task<string?> GetRefreshTokenAsync()
    {
        return Task.FromResult(_cachedRefreshToken);
    }

    public Task SetRefreshTokenAsync(string token)
    {
        _cachedRefreshToken = token;
        _logger.LogDebug("Refresh token stored successfully");
        return Task.CompletedTask;
    }

    public Task ClearTokensAsync()
    {
        _cachedAccessToken = null;
        _cachedRefreshToken = null;
        // Note: We intentionally keep _cachedServerUrl so user doesn't have to re-enter it on next login
        _logger.LogDebug("Authentication tokens cleared");
        return Task.CompletedTask;
    }

    public async Task<bool> IsAuthenticatedAsync()
    {
        var token = await GetAccessTokenAsync();
        // Simple check: if we have a token, we consider the user authenticated
        // The server will validate the token on each request
        // If the token is invalid/expired, the AuthenticationHandler will clear it on 401 response
        return !string.IsNullOrEmpty(token);
    }

    public async Task<string?> GetUserIdAsync()
    {
        // User ID extraction from JWT token requires System.IdentityModel.Tokens.Jwt
        // For now, return null - this can be enhanced later if needed
        // The server knows the user ID from the token
        await Task.CompletedTask;
        return null;
    }

    public Task<string?> GetServerUrlAsync()
    {
        return Task.FromResult(_cachedServerUrl);
    }

    public Task SetServerUrlAsync(string serverUrl)
    {
        _cachedServerUrl = serverUrl;
        _logger.LogDebug("Server URL stored successfully");
        return Task.CompletedTask;
    }
}
