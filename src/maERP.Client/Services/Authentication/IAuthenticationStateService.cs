namespace maERP.Client.Services.Authentication;

/// <summary>
/// Service for managing JWT authentication state (token storage and retrieval)
/// </summary>
public interface IAuthenticationStateService
{
    /// <summary>
    /// Gets the current JWT access token
    /// </summary>
    Task<string?> GetAccessTokenAsync();

    /// <summary>
    /// Sets the JWT access token
    /// </summary>
    Task SetAccessTokenAsync(string token);

    /// <summary>
    /// Gets the refresh token
    /// </summary>
    Task<string?> GetRefreshTokenAsync();

    /// <summary>
    /// Sets the refresh token
    /// </summary>
    Task SetRefreshTokenAsync(string token);

    /// <summary>
    /// Clears all authentication tokens
    /// </summary>
    Task ClearTokensAsync();

    /// <summary>
    /// Checks if user is authenticated (has valid token)
    /// </summary>
    Task<bool> IsAuthenticatedAsync();

    /// <summary>
    /// Gets the user ID from the token
    /// </summary>
    Task<string?> GetUserIdAsync();
}
