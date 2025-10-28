namespace maERP.Client.Services.Api.Handlers;

/// <summary>
/// HTTP message handler that dynamically sets the base URL from stored server configuration
/// This allows the server URL to be changed at runtime (e.g., during login)
/// </summary>
public class DynamicBaseUrlHandler : DelegatingHandler
{
    private readonly Services.Authentication.IAuthenticationStateService _authStateService;
    private readonly ILogger<DynamicBaseUrlHandler> _logger;

    public DynamicBaseUrlHandler(
        Services.Authentication.IAuthenticationStateService authStateService,
        ILogger<DynamicBaseUrlHandler> logger)
    {
        _authStateService = authStateService;
        _logger = logger;
    }

    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        // Get the dynamically stored server URL
        var serverUrl = await _authStateService.GetServerUrlAsync();

        if (!string.IsNullOrEmpty(serverUrl))
        {
            // If we have a stored server URL, use it to construct the full URL
            if (request.RequestUri != null)
            {
                if (!request.RequestUri.IsAbsoluteUri)
                {
                    var baseUri = new Uri(serverUrl);
                    var relativeUri = request.RequestUri.ToString().TrimStart('/');
                    request.RequestUri = new Uri(baseUri, relativeUri);
                    _logger.LogDebug("Request URL set to: {Url}", request.RequestUri);
                }
                else
                {
                    // Request already has an absolute URI, likely from HttpClient.BaseAddress
                    // Replace the base URL with our stored server URL
                    var baseUri = new Uri(serverUrl);
                    var relativePath = request.RequestUri.PathAndQuery;
                    request.RequestUri = new Uri(baseUri, relativePath);
                    _logger.LogDebug("Request URL updated to: {Url}", request.RequestUri);
                }
            }
        }
        else
        {
            // No server URL stored yet (e.g., before login)
            // The request will use the HttpClient's default BaseAddress from configuration
            _logger.LogDebug("Using default BaseAddress (no server URL configured yet)");
        }

        return await base.SendAsync(request, cancellationToken);
    }
}
