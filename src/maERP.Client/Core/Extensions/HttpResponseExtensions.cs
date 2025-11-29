using System.Net.Http.Json;
using System.Text.Json;
using maERP.Client.Core.Exceptions;

namespace maERP.Client.Core.Extensions;

/// <summary>
/// Extension methods for HttpResponseMessage to handle API error responses.
/// </summary>
public static class HttpResponseExtensions
{
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    /// <summary>
    /// Ensures the response is successful or throws an ApiException with error messages from the server.
    /// This replaces EnsureSuccessStatusCode() to provide detailed error information.
    /// </summary>
    /// <param name="response">The HTTP response to check.</param>
    /// <param name="ct">Cancellation token.</param>
    /// <exception cref="ApiException">Thrown when the response indicates failure, containing server error messages.</exception>
    public static async Task EnsureSuccessOrThrowApiExceptionAsync(
        this HttpResponseMessage response,
        CancellationToken ct = default)
    {
        if (response.IsSuccessStatusCode)
        {
            return;
        }

        var messages = await ExtractErrorMessagesAsync(response, ct);
        throw new ApiException(response.StatusCode, messages);
    }

    /// <summary>
    /// Extracts error messages from an HTTP response.
    /// Tries to parse the response body as JSON and extract Messages array.
    /// </summary>
    private static async Task<List<string>> ExtractErrorMessagesAsync(
        HttpResponseMessage response,
        CancellationToken ct)
    {
        var messages = new List<string>();

        try
        {
            var content = await response.Content.ReadAsStringAsync(ct);
            if (string.IsNullOrWhiteSpace(content))
            {
                messages.Add($"Request failed with status code {(int)response.StatusCode} ({response.StatusCode})");
                return messages;
            }

            // Try to parse as standard API error response
            var errorResponse = JsonSerializer.Deserialize<ApiErrorResponse>(content, JsonOptions);

            if (errorResponse?.Messages is { Count: > 0 })
            {
                messages.AddRange(errorResponse.Messages);
            }
            else if (!string.IsNullOrWhiteSpace(errorResponse?.Title))
            {
                // RFC 7807 ProblemDetails format
                messages.Add(errorResponse.Title);
                if (errorResponse.Errors is { Count: > 0 })
                {
                    foreach (var error in errorResponse.Errors)
                    {
                        foreach (var errorMessage in error.Value)
                        {
                            messages.Add(errorMessage);
                        }
                    }
                }
            }
            else
            {
                // Fallback: use the raw content if it looks like a simple string
                messages.Add(content.Length > 500 ? content[..500] + "..." : content);
            }
        }
        catch (JsonException)
        {
            // If JSON parsing fails, provide a generic error message
            messages.Add($"Request failed with status code {(int)response.StatusCode} ({response.StatusCode})");
        }
        catch (Exception)
        {
            // For any other exception during error extraction, provide a generic error
            messages.Add($"Request failed with status code {(int)response.StatusCode} ({response.StatusCode})");
        }

        return messages.Count > 0
            ? messages
            : new List<string> { $"Request failed with status code {(int)response.StatusCode} ({response.StatusCode})" };
    }

    /// <summary>
    /// Internal class to deserialize API error responses.
    /// Supports both the standard ApiResponse format and RFC 7807 ProblemDetails.
    /// </summary>
    private class ApiErrorResponse
    {
        // Standard API response format
        public int StatusCode { get; set; }
        public List<string> Messages { get; set; } = new();
        public bool Succeeded { get; set; }

        // RFC 7807 ProblemDetails format
        public string? Title { get; set; }
        public string? Detail { get; set; }
        public Dictionary<string, string[]>? Errors { get; set; }
    }
}
