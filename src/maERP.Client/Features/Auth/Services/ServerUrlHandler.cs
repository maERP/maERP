namespace maERP.Client.Features.Auth.Services;

/// <summary>
/// Handler that sets the request URI base address from stored server URL.
/// </summary>
public class ServerUrlHandler : DelegatingHandler
{
    private readonly ITokenStorageService _tokenStorage;
    private readonly ILogger<ServerUrlHandler> _logger;

    public ServerUrlHandler(
        ITokenStorageService tokenStorage,
        ILogger<ServerUrlHandler> logger)
    {
        _tokenStorage = tokenStorage;
        _logger = logger;
    }

    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        var serverUrl = await _tokenStorage.GetServerUrlAsync();

        if (string.IsNullOrEmpty(serverUrl))
        {
            _logger.LogError("No server URL configured. Cannot make API request to: {Path}",
                request.RequestUri?.PathAndQuery);
            throw new InvalidOperationException("Server URL is not configured. Please login first.");
        }

        // If the request URI is relative or doesn't have a host, prepend the server URL
        if (request.RequestUri != null && !request.RequestUri.IsAbsoluteUri)
        {
            var baseUri = new Uri(serverUrl.TrimEnd('/'));
            request.RequestUri = new Uri(baseUri, request.RequestUri.OriginalString);
            _logger.LogDebug("Set request URI to: {Uri}", request.RequestUri);
        }
        else if (request.RequestUri != null && string.IsNullOrEmpty(request.RequestUri.Host))
        {
            var baseUri = new Uri(serverUrl.TrimEnd('/'));
            request.RequestUri = new Uri(baseUri, request.RequestUri.PathAndQuery);
            _logger.LogDebug("Set request URI to: {Uri}", request.RequestUri);
        }

        return await base.SendAsync(request, cancellationToken);
    }
}
