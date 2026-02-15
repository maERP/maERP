using System.Net.Http.Headers;

namespace maERP.Client.Features.Auth.Services;

public class AuthenticationHandler : DelegatingHandler
{
    private const string TokenRetryHeader = "X-Token-Retry";
    private static readonly SemaphoreSlim _refreshLock = new(1, 1);

    private readonly ITokenStorageService _tokenStorage;
    private readonly IMaErpAuthenticationService _authService;
    private readonly ILogger<AuthenticationHandler> _logger;

    public AuthenticationHandler(
        ITokenStorageService tokenStorage,
        IMaErpAuthenticationService authService,
        ILogger<AuthenticationHandler> logger)
    {
        _tokenStorage = tokenStorage;
        _authService = authService;
        _logger = logger;
    }

    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        _logger.LogDebug("Processing authentication for request: {Method} {Uri}",
            request.Method, request.RequestUri);

        await AddAuthHeaders(request);

        var response = await base.SendAsync(request, cancellationToken);

        // 401-Retry: If unauthorized and this is not already a retry, refresh the token once and retry
        if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized
            && !request.Headers.Contains(TokenRetryHeader))
        {
            _logger.LogWarning("Unauthorized response (401) for {Method} {Uri} - attempting token refresh",
                request.Method, request.RequestUri);

            var refreshed = await TryRefreshTokenAsync(cancellationToken);
            if (refreshed)
            {
                // Clone the original request (HttpRequestMessage cannot be sent twice)
                using var retryRequest = await CloneRequestAsync(request);
                retryRequest.Headers.Add(TokenRetryHeader, "true");
                await AddAuthHeaders(retryRequest);

                response.Dispose();
                response = await base.SendAsync(retryRequest, cancellationToken);

                if (response.IsSuccessStatusCode)
                {
                    _logger.LogInformation("Token refresh retry succeeded for {Method} {Uri}",
                        request.Method, request.RequestUri);
                }
                else
                {
                    _logger.LogWarning("Token refresh retry still failed with {StatusCode} for {Method} {Uri}",
                        response.StatusCode, request.Method, request.RequestUri);
                }
            }
        }
        else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
        {
            _logger.LogWarning("Unauthorized response (401) received for {Method} {Uri} - token may be expired or invalid",
                request.Method, request.RequestUri);
        }
        else if (response.StatusCode == System.Net.HttpStatusCode.Forbidden)
        {
            _logger.LogWarning("Forbidden response (403) received for {Method} {Uri} - user may lack required permissions",
                request.Method, request.RequestUri);
        }

        return response;
    }

    private async Task AddAuthHeaders(HttpRequestMessage request)
    {
        var token = await _tokenStorage.GetTokenAsync();

        if (!string.IsNullOrEmpty(token))
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            _logger.LogDebug("Added Bearer token to request: {Uri}", request.RequestUri);
        }
        else
        {
            _logger.LogDebug("No authentication token available for request: {Uri}", request.RequestUri);
        }

        var tenantId = await _tokenStorage.GetCurrentTenantIdAsync();
        if (tenantId.HasValue)
        {
            // Remove existing header to avoid duplicates on retry
            request.Headers.Remove("X-Tenant-Id");
            request.Headers.Add("X-Tenant-Id", tenantId.Value.ToString());
            _logger.LogDebug("Added X-Tenant-Id header: {TenantId} for {Uri}",
                tenantId.Value, request.RequestUri);
        }
        else
        {
            _logger.LogDebug("No tenant ID available for request: {Uri}", request.RequestUri);
        }
    }

    private async Task<bool> TryRefreshTokenAsync(CancellationToken cancellationToken)
    {
        // Use SemaphoreSlim to ensure only one concurrent refresh across all parallel 401 responses
        await _refreshLock.WaitAsync(cancellationToken);
        try
        {
            _logger.LogDebug("Acquired refresh lock, refreshing token");
            var result = await _authService.RefreshTokenAsync(cancellationToken);
            var success = result?.Succeeded == true && !string.IsNullOrEmpty(result.Token);

            if (success)
            {
                _logger.LogInformation("Token refresh succeeded during 401-retry");
            }
            else
            {
                _logger.LogWarning("Token refresh failed during 401-retry");
            }

            return success;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Token refresh threw an exception during 401-retry");
            return false;
        }
        finally
        {
            _refreshLock.Release();
        }
    }

    private static async Task<HttpRequestMessage> CloneRequestAsync(HttpRequestMessage request)
    {
        var clone = new HttpRequestMessage(request.Method, request.RequestUri);

        // Copy content
        if (request.Content != null)
        {
            var contentBytes = await request.Content.ReadAsByteArrayAsync();
            clone.Content = new ByteArrayContent(contentBytes);

            // Copy content headers
            foreach (var header in request.Content.Headers)
            {
                clone.Content.Headers.TryAddWithoutValidation(header.Key, header.Value);
            }
        }

        // Copy request headers (except Authorization which will be re-added)
        foreach (var header in request.Headers)
        {
            if (!string.Equals(header.Key, "Authorization", StringComparison.OrdinalIgnoreCase)
                && !string.Equals(header.Key, "X-Tenant-Id", StringComparison.OrdinalIgnoreCase))
            {
                clone.Headers.TryAddWithoutValidation(header.Key, header.Value);
            }
        }

        // Copy request properties/options
        foreach (var option in request.Options)
        {
            clone.Options.TryAdd(option.Key, option.Value);
        }

        clone.Version = request.Version;

        return clone;
    }
}
