using System.Diagnostics;

namespace maERP.Client.Services.Endpoints;

internal class DebugHttpHandler : DelegatingHandler
{
    private readonly ILogger<DebugHttpHandler> _logger;

    public DebugHttpHandler(ILogger<DebugHttpHandler> logger, HttpMessageHandler? innerHandler = null)
        : base(innerHandler ?? new HttpClientHandler())
    {
        _logger = logger;
    }

    protected async override Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        var requestId = Guid.NewGuid().ToString("N")[..8];
        var stopwatch = Stopwatch.StartNew();

        _logger.LogDebug(
            "[{RequestId}] HTTP {Method} {Uri}",
            requestId,
            request.Method,
            request.RequestUri);

#if DEBUG
        // Log request headers in debug mode
        foreach (var header in request.Headers)
        {
            // Don't log sensitive headers in full
            var value = header.Key.Equals("Authorization", StringComparison.OrdinalIgnoreCase)
                ? "[REDACTED]"
                : string.Join(", ", header.Value);
            _logger.LogDebug(
                "[{RequestId}] Request Header: {Key}: {Value}",
                requestId,
                header.Key,
                value);
        }

        // Log request body if present (be careful with sensitive data)
        if (request.Content is not null)
        {
            var requestBody = await request.Content.ReadAsStringAsync(cancellationToken);
            if (!string.IsNullOrEmpty(requestBody))
            {
                // Truncate very long bodies
                var truncatedBody = requestBody.Length > 1000
                    ? requestBody[..1000] + "...[truncated]"
                    : requestBody;
                _logger.LogDebug(
                    "[{RequestId}] Request Body: {Body}",
                    requestId,
                    truncatedBody);
            }
        }
#endif

        HttpResponseMessage response;
        try
        {
            response = await base.SendAsync(request, cancellationToken);
            stopwatch.Stop();

            if (response.IsSuccessStatusCode)
            {
                _logger.LogDebug(
                    "[{RequestId}] HTTP {StatusCode} {ReasonPhrase} in {ElapsedMs}ms",
                    requestId,
                    (int)response.StatusCode,
                    response.ReasonPhrase,
                    stopwatch.ElapsedMilliseconds);
            }
            else
            {
                _logger.LogWarning(
                    "[{RequestId}] HTTP {StatusCode} {ReasonPhrase} for {Method} {Uri} in {ElapsedMs}ms",
                    requestId,
                    (int)response.StatusCode,
                    response.ReasonPhrase,
                    request.Method,
                    request.RequestUri,
                    stopwatch.ElapsedMilliseconds);

#if DEBUG
                // Log response body for failed requests in debug mode
                var responseBody = await response.Content.ReadAsStringAsync(cancellationToken);
                if (!string.IsNullOrEmpty(responseBody))
                {
                    var truncatedBody = responseBody.Length > 2000
                        ? responseBody[..2000] + "...[truncated]"
                        : responseBody;
                    _logger.LogWarning(
                        "[{RequestId}] Response Body: {Body}",
                        requestId,
                        truncatedBody);
                }
#endif
            }

            return response;
        }
        catch (Exception ex)
        {
            stopwatch.Stop();
            _logger.LogError(
                ex,
                "[{RequestId}] HTTP request failed for {Method} {Uri} after {ElapsedMs}ms",
                requestId,
                request.Method,
                request.RequestUri,
                stopwatch.ElapsedMilliseconds);
            throw;
        }
    }
}
