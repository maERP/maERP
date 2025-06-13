using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using maERP.Domain.Dtos.Auth;
using maERP.Domain.Wrapper;

namespace maERP.UI.Services;

public class HttpService : IHttpService
{
    private readonly HttpClient _httpClient;
    private string? _token;
    private string? _serverUrl;
    private readonly JsonSerializerOptions _jsonOptions = new JsonSerializerOptions
    {
        PropertyNameCaseInsensitive = true,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    public HttpService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public string? ServerUrl => _serverUrl;
    public string? Token => _token;
    public bool IsAuthenticated => !string.IsNullOrEmpty(_token);

    public async Task<LoginResponseDto> LoginAsync(string email, string password, string serverUrl)
    {
        try
        {
            _serverUrl = serverUrl.TrimEnd('/');

            var loginRequest = new LoginRequestDto
            {
                Email = email,
                Password = password,
                Server = serverUrl
            };

            var json = JsonSerializer.Serialize(loginRequest, _jsonOptions);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var loginUrl = $"{_serverUrl}/api/v1/auth/login";
            var response = await _httpClient.PostAsync(loginUrl, content);
            var authResponse = await response.Content.ReadFromJsonAsync<LoginResponseDto>(_jsonOptions);

            if (authResponse?.Token != null)
            {
                _token = authResponse.Token;
                _httpClient.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _token);

                return authResponse;
            }
            else
            {
                return new LoginResponseDto
                {
                    Succeeded = false,
                    Message = $"Login failed with status: {response.StatusCode}"
                };
            }
        }
        catch (Exception ex)
        {
            return new LoginResponseDto
            {
                Succeeded = false,
                Message = $"Login error: {ex.Message}"
            };
        }
    }

    public Task LogoutAsync()
    {
        _token = null;
        _serverUrl = null;
        _httpClient.DefaultRequestHeaders.Authorization = null;
        return Task.CompletedTask;
    }

    public async Task<PaginatedResult<T>?> GetPaginatedAsync<T>(string endpoint, int pageNumber = 0, int pageSize = 50, string searchString = "", string orderBy = "")
    {
        if (!IsAuthenticated || string.IsNullOrEmpty(_serverUrl))
        {
            return null;
        }

        try
        {
            var queryParams = $"?pageNumber={pageNumber}&pageSize={pageSize}&searchString={searchString}&orderBy={orderBy}";
            var url = $"{_serverUrl}/api/v1/{endpoint.TrimStart('/')}{queryParams}";

            var response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<PaginatedResult<T>>(_jsonOptions);
                return result;
            }
            else
            {
                return PaginatedResult<T>.Failure(new List<string> { $"Failed to load data: {response.StatusCode}" });
            }
        }
        catch (Exception ex)
        {
            return PaginatedResult<T>.Failure(new List<string> { $"Error loading data: {ex.Message}" });
        }
    }

    public async Task<Result<T>?> GetAsync<T>(string endpoint)
    {
        if (!IsAuthenticated || string.IsNullOrEmpty(_serverUrl))
        {
            return null;
        }

        try
        {
            var url = $"{_serverUrl}/api/v1/{endpoint.TrimStart('/')}";
            var response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<Result<T>>(_jsonOptions);
                return result;
            }
            else
            {
                return Result<T>.Fail($"Failed to load data: {response.StatusCode}");
            }
        }
        catch (Exception ex)
        {
            return Result<T>.Fail($"Error loading data: {ex.Message}");
        }
    }

    public async Task<Result<TResponse>?> PostAsync<TRequest, TResponse>(string endpoint, TRequest data)
    {
        if (!IsAuthenticated || string.IsNullOrEmpty(_serverUrl))
        {
            return null;
        }

        try
        {
            var url = $"{_serverUrl}/api/v1/{endpoint.TrimStart('/')}";
            var json = JsonSerializer.Serialize(data, _jsonOptions);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(url, content);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<Result<TResponse>>(_jsonOptions);
                return result;
            }
            else
            {
                return Result<TResponse>.Fail($"Failed to post data: {response.StatusCode}");
            }
        }
        catch (Exception ex)
        {
            return Result<TResponse>.Fail($"Error posting data: {ex.Message}");
        }
    }

    public async Task<Result<TResponse>?> PutAsync<TRequest, TResponse>(string endpoint, TRequest data)
    {
        if (!IsAuthenticated || string.IsNullOrEmpty(_serverUrl))
        {
            return null;
        }

        try
        {
            var url = $"{_serverUrl}/api/v1/{endpoint.TrimStart('/')}";
            var json = JsonSerializer.Serialize(data, _jsonOptions);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync(url, content);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<Result<TResponse>>(_jsonOptions);
                return result;
            }
            else
            {
                return Result<TResponse>.Fail($"Failed to update data: {response.StatusCode}");
            }
        }
        catch (Exception ex)
        {
            return Result<TResponse>.Fail($"Error updating data: {ex.Message}");
        }
    }

    public async Task<Result?> DeleteAsync(string endpoint)
    {
        if (!IsAuthenticated || string.IsNullOrEmpty(_serverUrl))
        {
            return null;
        }

        try
        {
            var url = $"{_serverUrl}/api/v1/{endpoint.TrimStart('/')}";
            var response = await _httpClient.DeleteAsync(url);

            if (response.IsSuccessStatusCode)
            {
                return (Result)Result.Success();
            }
            else
            {
                return (Result)Result.Fail($"Failed to delete data: {response.StatusCode}");
            }
        }
        catch (Exception ex)
        {
            return (Result)Result.Fail($"Error deleting data: {ex.Message}");
        }
    }
}