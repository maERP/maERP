using System.Net;

namespace maERP.Client.Services.Api.Handlers;

/// <summary>
/// HTTP message handler for centralized error handling and logging
/// </summary>
public class ErrorHandler : DelegatingHandler
{
    private readonly ILogger<ErrorHandler> _logger;

    public ErrorHandler(ILogger<ErrorHandler> logger)
    {
        _logger = logger;
    }

    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        HttpResponseMessage? response = null;

        try
        {
            response = await base.SendAsync(request, cancellationToken);

            // Log unsuccessful responses
            if (!response.IsSuccessStatusCode)
            {
                await LogErrorResponseAsync(request, response);
            }

            return response;
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError(ex, "HTTP request failed: {Method} {Uri} - {Message}",
                request.Method, request.RequestUri, ex.Message);
            throw;
        }
        catch (TaskCanceledException ex) when (ex.InnerException is TimeoutException)
        {
            _logger.LogError(ex, "HTTP request timeout: {Method} {Uri}",
                request.Method, request.RequestUri);
            throw new TimeoutException($"Request to {request.RequestUri} timed out", ex);
        }
        catch (TaskCanceledException ex)
        {
            _logger.LogWarning("HTTP request cancelled: {Method} {Uri}",
                request.Method, request.RequestUri);
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error during HTTP request: {Method} {Uri}",
                request.Method, request.RequestUri);
            throw;
        }
    }

    private async Task LogErrorResponseAsync(HttpRequestMessage request, HttpResponseMessage response)
    {
        var statusCode = (int)response.StatusCode;
        var reasonPhrase = response.ReasonPhrase ?? "Unknown";

        // Read response content for detailed error information
        string? content = null;
        try
        {
            content = await response.Content.ReadAsStringAsync();
        }
        catch
        {
            // Ignore errors reading content
        }

        switch (response.StatusCode)
        {
            case HttpStatusCode.BadRequest:
                _logger.LogWarning("Bad Request ({StatusCode}): {Method} {Uri} - {Content}",
                    statusCode, request.Method, request.RequestUri, content);
                break;

            case HttpStatusCode.Unauthorized:
                _logger.LogWarning("Unauthorized ({StatusCode}): {Method} {Uri} - Authentication required",
                    statusCode, request.Method, request.RequestUri);
                break;

            case HttpStatusCode.Forbidden:
                _logger.LogWarning("Forbidden ({StatusCode}): {Method} {Uri} - Insufficient permissions",
                    statusCode, request.Method, request.RequestUri);
                break;

            case HttpStatusCode.NotFound:
                _logger.LogWarning("Not Found ({StatusCode}): {Method} {Uri}",
                    statusCode, request.Method, request.RequestUri);
                break;

            case HttpStatusCode.Conflict:
                _logger.LogWarning("Conflict ({StatusCode}): {Method} {Uri} - {Content}",
                    statusCode, request.Method, request.RequestUri, content);
                break;

            case HttpStatusCode.InternalServerError:
            case HttpStatusCode.BadGateway:
            case HttpStatusCode.ServiceUnavailable:
            case HttpStatusCode.GatewayTimeout:
                _logger.LogError("Server Error ({StatusCode} {ReasonPhrase}): {Method} {Uri} - {Content}",
                    statusCode, reasonPhrase, request.Method, request.RequestUri, content);
                break;

            default:
                _logger.LogWarning("HTTP Error ({StatusCode} {ReasonPhrase}): {Method} {Uri} - {Content}",
                    statusCode, reasonPhrase, request.Method, request.RequestUri, content);
                break;
        }
    }
}
