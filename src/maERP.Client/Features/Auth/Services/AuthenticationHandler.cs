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
        var token = await _tokenStorage.GetTokenAsync();

        if (!string.IsNullOrEmpty(token))
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            _logger.LogDebug("Added Bearer token to request: {Uri}", request.RequestUri);
        }

        var tenantId = await _tokenStorage.GetCurrentTenantIdAsync();
        if (tenantId.HasValue)
        {
            request.Headers.Add("X-Tenant-Id", tenantId.Value.ToString());
            _logger.LogDebug("Added Tenant-Id header: {TenantId}", tenantId.Value);
        }

        return await base.SendAsync(request, cancellationToken);
    }
}
