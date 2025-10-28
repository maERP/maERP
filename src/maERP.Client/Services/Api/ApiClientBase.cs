using System.Net.Http.Json;
using System.Text.Json;
using maERP.Domain.Wrapper;

namespace maERP.Client.Services.Api;

/// <summary>
/// Base class for all API clients with generic HTTP methods
/// </summary>
public abstract class ApiClientBase
{
    protected readonly HttpClient HttpClient;
    protected readonly ILogger Logger;
    protected readonly JsonSerializerOptions JsonOptions;

    protected ApiClientBase(HttpClient httpClient, ILogger logger)
    {
        HttpClient = httpClient;
        Logger = logger;

        // Configure JSON serialization options to match server configuration
        JsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
    }

    /// <summary>
    /// Sends a GET request and returns the deserialized response
    /// </summary>
    protected async Task<T?> GetAsync<T>(string endpoint, CancellationToken cancellationToken = default)
    {
        try
        {
            Logger.LogDebug("GET {Endpoint}", endpoint);
            return await HttpClient.GetFromJsonAsync<T>(endpoint, JsonOptions, cancellationToken);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "GET request failed: {Endpoint}", endpoint);
            throw;
        }
    }

    /// <summary>
    /// Sends a GET request and returns the HttpResponseMessage (for custom handling)
    /// </summary>
    protected async Task<HttpResponseMessage> GetResponseAsync(string endpoint, CancellationToken cancellationToken = default)
    {
        try
        {
            Logger.LogDebug("GET {Endpoint}", endpoint);
            return await HttpClient.GetAsync(endpoint, cancellationToken);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "GET request failed: {Endpoint}", endpoint);
            throw;
        }
    }

    /// <summary>
    /// Sends a POST request and returns the deserialized response
    /// </summary>
    protected async Task<TResponse?> PostAsync<TRequest, TResponse>(
        string endpoint,
        TRequest data,
        CancellationToken cancellationToken = default)
    {
        try
        {
            Logger.LogDebug("POST {Endpoint}", endpoint);
            var response = await HttpClient.PostAsJsonAsync(endpoint, data, JsonOptions, cancellationToken);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<TResponse>(JsonOptions, cancellationToken);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "POST request failed: {Endpoint}", endpoint);
            throw;
        }
    }

    /// <summary>
    /// Sends a POST request without expecting a response body (e.g., for 204 No Content)
    /// </summary>
    protected async Task<HttpResponseMessage> PostAsync<TRequest>(
        string endpoint,
        TRequest data,
        CancellationToken cancellationToken = default)
    {
        try
        {
            Logger.LogDebug("POST {Endpoint}", endpoint);
            return await HttpClient.PostAsJsonAsync(endpoint, data, JsonOptions, cancellationToken);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "POST request failed: {Endpoint}", endpoint);
            throw;
        }
    }

    /// <summary>
    /// Sends a PUT request and returns the deserialized response
    /// </summary>
    protected async Task<TResponse?> PutAsync<TRequest, TResponse>(
        string endpoint,
        TRequest data,
        CancellationToken cancellationToken = default)
    {
        try
        {
            Logger.LogDebug("PUT {Endpoint}", endpoint);
            var response = await HttpClient.PutAsJsonAsync(endpoint, data, JsonOptions, cancellationToken);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<TResponse>(JsonOptions, cancellationToken);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "PUT request failed: {Endpoint}", endpoint);
            throw;
        }
    }

    /// <summary>
    /// Sends a PUT request without expecting a response body
    /// </summary>
    protected async Task<HttpResponseMessage> PutAsync<TRequest>(
        string endpoint,
        TRequest data,
        CancellationToken cancellationToken = default)
    {
        try
        {
            Logger.LogDebug("PUT {Endpoint}", endpoint);
            return await HttpClient.PutAsJsonAsync(endpoint, data, JsonOptions, cancellationToken);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "PUT request failed: {Endpoint}", endpoint);
            throw;
        }
    }

    /// <summary>
    /// Sends a DELETE request
    /// </summary>
    protected async Task<HttpResponseMessage> DeleteAsync(
        string endpoint,
        CancellationToken cancellationToken = default)
    {
        try
        {
            Logger.LogDebug("DELETE {Endpoint}", endpoint);
            return await HttpClient.DeleteAsync(endpoint, cancellationToken);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "DELETE request failed: {Endpoint}", endpoint);
            throw;
        }
    }

    /// <summary>
    /// Builds a URL with query parameters
    /// </summary>
    protected static string BuildUrl(string baseUrl, Dictionary<string, string?>? queryParams = null)
    {
        if (queryParams == null || queryParams.Count == 0)
        {
            return baseUrl;
        }

        var queryString = string.Join("&",
            queryParams
                .Where(kvp => !string.IsNullOrEmpty(kvp.Value))
                .Select(kvp => $"{Uri.EscapeDataString(kvp.Key)}={Uri.EscapeDataString(kvp.Value!)}"));

        return string.IsNullOrEmpty(queryString) ? baseUrl : $"{baseUrl}?{queryString}";
    }

    /// <summary>
    /// Ensures the response was successful and returns the deserialized content
    /// </summary>
    protected async Task<T?> EnsureSuccessAndReadAsync<T>(
        HttpResponseMessage response,
        CancellationToken cancellationToken = default)
    {
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<T>(JsonOptions, cancellationToken);
    }

    /// <summary>
    /// Checks if the response is successful (2xx status code)
    /// </summary>
    protected static bool IsSuccess(HttpResponseMessage response)
    {
        return response.IsSuccessStatusCode;
    }
}
