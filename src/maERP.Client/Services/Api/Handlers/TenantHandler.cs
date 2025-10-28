using maERP.Client.Services.Tenant;

namespace maERP.Client.Services.Api.Handlers;

/// <summary>
/// HTTP message handler that automatically adds X-Tenant-Id header to requests
/// </summary>
public class TenantHandler : DelegatingHandler
{
    private const string TenantHeaderName = "X-Tenant-Id";
    private readonly ITenantService _tenantService;
    private readonly ILogger<TenantHandler> _logger;

    public TenantHandler(
        ITenantService tenantService,
        ILogger<TenantHandler> logger)
    {
        _tenantService = tenantService;
        _logger = logger;
    }

    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        var tenantId = _tenantService.GetCurrentTenantId();

        if (tenantId.HasValue)
        {
            // Add or update X-Tenant-Id header
            if (request.Headers.Contains(TenantHeaderName))
            {
                request.Headers.Remove(TenantHeaderName);
            }

            request.Headers.Add(TenantHeaderName, tenantId.Value.ToString());
            _logger.LogDebug("Added X-Tenant-Id header: {TenantId} for {Method} {Uri}",
                tenantId.Value, request.Method, request.RequestUri);
        }
        else
        {
            // Some endpoints don't require tenant context (e.g., tenant list, user profile)
            _logger.LogDebug("No tenant context set for request: {Method} {Uri}",
                request.Method, request.RequestUri);
        }

        return await base.SendAsync(request, cancellationToken);
    }
}
