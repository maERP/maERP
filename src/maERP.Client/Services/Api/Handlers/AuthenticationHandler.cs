using maERP.Client.Services.Authentication;

namespace maERP.Client.Services.Api.Handlers;

/// <summary>
/// HTTP message handler that automatically adds JWT Bearer token to requests
/// </summary>
public class AuthenticationHandler : DelegatingHandler
{
    private readonly IAuthenticationStateService _authStateService;
    private readonly ILogger<AuthenticationHandler> _logger;

    public AuthenticationHandler(
        IAuthenticationStateService authStateService,
        ILogger<AuthenticationHandler> logger)
    {
        _authStateService = authStateService;
        _logger = logger;
    }

    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        // Skip adding token for authentication endpoints
        if (IsAuthenticationEndpoint(request.RequestUri))
        {
            _logger.LogDebug("Skipping authentication header for auth endpoint: {Uri}", request.RequestUri);
            return await base.SendAsync(request, cancellationToken);
        }

        // Get access token and add to request
        var accessToken = await _authStateService.GetAccessTokenAsync();
        if (!string.IsNullOrEmpty(accessToken))
        {
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
            _logger.LogDebug("Added Bearer token to request: {Method} {Uri}", request.Method, request.RequestUri);
        }
        else
        {
            _logger.LogWarning("No access token available for request: {Method} {Uri}", request.Method, request.RequestUri);
        }

        var response = await base.SendAsync(request, cancellationToken);

        // If unauthorized, clear tokens (they might be expired)
        if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
        {
            _logger.LogInformation("Received 401 Unauthorized, clearing authentication tokens");
            await _authStateService.ClearTokensAsync();
        }

        return response;
    }

    private static bool IsAuthenticationEndpoint(Uri? uri)
    {
        if (uri == null) return false;

        var path = uri.AbsolutePath.ToLowerInvariant();
        return path.Contains("/auth/login") ||
               path.Contains("/auth/register") ||
               path.Contains("/auth/refresh") ||
               path.Contains("/auth/forgot-password") ||
               path.Contains("/auth/reset-password");
    }
}
