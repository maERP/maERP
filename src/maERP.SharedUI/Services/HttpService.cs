using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using Blazored.LocalStorage;
using maERP.Domain.Dtos.Auth;
using maERP.SharedUI.Contracts;
using maERP.SharedUI.Providers;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Logging;

namespace maERP.SharedUI.Services;

/// <summary>
/// Generic HTTP service for making REST API calls with authentication support
/// </summary>
public class HttpService : IHttpService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<HttpService> _logger;
    private readonly JsonSerializerOptions _jsonOptions;
    private readonly AuthenticationStateProvider _authenticataionStateProvider;
    private readonly ILocalStorageService _localStorageService;

    public HttpService(HttpClient httpClient,
        ILogger<HttpService> logger,
        AuthenticationStateProvider authenticataionStateProvider,
        ILocalStorageService localStorageService)
    {
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
        
        _authenticataionStateProvider = authenticataionStateProvider;
        _localStorageService = localStorageService;

    }

    /// <summary>
    /// Authenticates the user with the API using credentials
    /// </summary>
    public async Task<bool> LoginAsync(string email, string password, bool rememberMe)
    {
        try
        {
            _logger.LogInformation("Attempting login for user {Email}", email);
            
            var loginRequest = new LoginDto
            {
                Email = email,
                Password = password
            };

            var response = await _httpClient.PostAsJsonAsync("/api/v1/Auth/login", loginRequest, _jsonOptions);
            response.EnsureSuccessStatusCode();
            
            var authResponse = await response.Content.ReadFromJsonAsync<LoginResponseDto>(_jsonOptions);
            if (authResponse?.Token != null)
            {
                _httpClient.DefaultRequestHeaders.Authorization = 
                    new AuthenticationHeaderValue("Bearer", authResponse.Token);
                _logger.LogInformation("Login successful for user {Email}", email);

                await _localStorageService.SetItemAsync("authToken", authResponse.Token);
                await ((ApiAuthenticationStateProvider)_authenticataionStateProvider).LoggedIn();
                return true;
            }

            _logger.LogWarning("Login failed for user {Email} - No token received", email);
            return false;
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError(ex, "Login failed for user {Email}", email);
            throw;
        }
    }
    

    /// <summary>
    /// Logs out the current user by clearing the authentication token
    /// </summary>
    public async void Logout()
    {
        await _localStorageService.RemoveItemAsync("authToken");
        await ((ApiAuthenticationStateProvider) _authenticataionStateProvider).LoggedOut();

        _httpClient.DefaultRequestHeaders.Authorization = null;
        _logger.LogInformation("User logged out");
        // TODO: implement killing session on server side
    }

    /// <summary>
    /// Ensures the user is authenticated before making a request
    /// </summary>
    private async void EnsureAuthenticated()
    {
        string authToken = await _localStorageService.GetItemAsStringAsync("authToken") ?? string.Empty;

        if (string.IsNullOrEmpty(authToken))
        {
            _logger.LogError("Attempted to make authenticated request without being logged in");
            throw new UnauthorizedAccessException("User is not authenticated. Please login first.");
        }
    }

    /// <summary>
    /// Sends a GET request to the specified URI and returns the deserialized response.
    /// </summary>
    public async Task<T?> GetAsync<T>(string uri, bool requiresAuth = true)
    {
        if (requiresAuth) EnsureAuthenticated();
        
        try
        {
            _logger.LogInformation("Making GET request to {Uri}", uri);
            var response = await _httpClient.GetAsync(uri);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<T>(_jsonOptions);
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError(ex, "HTTP request failed for GET {Uri}", uri);
            throw;
        }
        catch (JsonException ex)
        {
            _logger.LogError(ex, "JSON deserialization failed for GET {Uri}", uri);
            throw;
        }
    }

    /// <summary>
    /// Sends a POST request to the specified URI with the given content and returns the deserialized response.
    /// </summary>
    public async Task<TResponse?> PostAsync<TRequest, TResponse>(string uri, TRequest content, bool requiresAuth = true)
    {
        if (requiresAuth) EnsureAuthenticated();

        try
        {
            _logger.LogInformation("Making POST request to {Uri}", uri);
            var response = await _httpClient.PostAsJsonAsync(uri, content, _jsonOptions);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<TResponse>(_jsonOptions);
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError(ex, "HTTP request failed for POST {Uri}", uri);
            throw;
        }
        catch (JsonException ex)
        {
            _logger.LogError(ex, "JSON serialization/deserialization failed for POST {Uri}", uri);
            throw;
        }
    }

    /// <summary>
    /// Sends a POST request to the specified URI with the given content serialized as JSON
    /// </summary>
    public async Task<HttpResponseMessage> PostAsJsonAsync<TRequest>(string uri, TRequest content, bool requiresAuth = true)
    {
        try
        {
            if (requiresAuth)
            {
                EnsureAuthenticated();
            }

            _logger.LogInformation("Sending POST request to {Uri}", uri);
            var response = await _httpClient.PostAsJsonAsync(uri, content, _jsonOptions);
            _logger.LogInformation("POST request to {Uri} completed with status code {StatusCode}", uri, response.StatusCode);
            
            return response;
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError(ex, "POST request to {Uri} failed", uri);
            throw;
        }
    }

    /// <summary>
    /// Sends a PUT request to the specified URI with the given content and returns the deserialized response.
    /// </summary>
    public async Task<TResponse?> PutAsync<TRequest, TResponse>(string uri, TRequest content, bool requiresAuth = true)
    {
        if (requiresAuth) EnsureAuthenticated();

        try
        {
            _logger.LogInformation("Making PUT request to {Uri}", uri);
            var response = await _httpClient.PutAsJsonAsync(uri, content, _jsonOptions);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<TResponse>(_jsonOptions);
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError(ex, "HTTP request failed for PUT {Uri}", uri);
            throw;
        }
        catch (JsonException ex)
        {
            _logger.LogError(ex, "JSON serialization/deserialization failed for PUT {Uri}", uri);
            throw;
        }
    }

    /// <summary>
    /// Sends a PUT request to the specified URI with the given content serialized as JSON
    /// </summary>
    public async Task<HttpResponseMessage> PutAsJsonAsync<TRequest>(string uri, TRequest content, bool requiresAuth = true)
    {
        try
        {
            if (requiresAuth)
            {
                EnsureAuthenticated();
            }

            _logger.LogInformation("Sending PUT request to {Uri}", uri);
            var response = await _httpClient.PutAsJsonAsync(uri, content, _jsonOptions);
            _logger.LogInformation("PUT request to {Uri} completed with status code {StatusCode}", uri, response.StatusCode);
            
            return response;
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError(ex, "PUT request to {Uri} failed", uri);
            throw;
        }
    }

    /// <summary>
    /// Sends a DELETE request to the specified URI.
    /// </summary>
    public async Task DeleteAsync(string uri, bool requiresAuth = true)
    {
        if (requiresAuth) EnsureAuthenticated();

        try
        {
            _logger.LogInformation("Making DELETE request to {Uri}", uri);
            var response = await _httpClient.DeleteAsync(uri);
            response.EnsureSuccessStatusCode();
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError(ex, "HTTP request failed for DELETE {Uri}", uri);
            throw;
        }
    }

    /// <summary>
    /// Sends a PUT request to the specified URI with the given content and returns the deserialized response.
    /// </summary>
    public async Task<T?> UpdateAsync<T>(string url, T data)
    {
        var response = await _httpClient.PutAsJsonAsync(url, data);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<T>();
    }
}