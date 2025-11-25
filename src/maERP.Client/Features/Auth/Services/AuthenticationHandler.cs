using System.Net.Http.Headers;

namespace maERP.Client.Features.Auth.Services;

public class AuthenticationHandler : DelegatingHandler
{
    private readonly ITokenStorageService _tokenStorage;
    private readonly ILogger<AuthenticationHandler> _logger;

    public AuthenticationHandler(
        ITokenStorageService tokenStorage,
        ILogger<AuthenticationHandler> logger)
    {
        _tokenStorage = tokenStorage;
        _logger = logger;
    }

    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        _logger.LogDebug("Processing authentication for request: {Method} {Uri}",
            request.Method, request.RequestUri);

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
            request.Headers.Add("X-Tenant-Id", tenantId.Value.ToString());
            _logger.LogDebug("Added X-Tenant-Id header: {TenantId} for {Uri}",
                tenantId.Value, request.RequestUri);
        }
        else
        {
            _logger.LogDebug("No tenant ID available for request: {Uri}", request.RequestUri);
        }

        var response = await base.SendAsync(request, cancellationToken);

        // Log authentication-related response issues
        if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
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
}
