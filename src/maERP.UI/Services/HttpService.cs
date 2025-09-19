using System;
using System.Collections.Generic;
using System.IO;
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
    private readonly IDebugService _debugService;
    private string? _token;
    private string? _serverUrl;
    private Guid? _currentTenantId;
    private readonly JsonSerializerOptions _jsonOptions = new JsonSerializerOptions
    {
        PropertyNameCaseInsensitive = true,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    public HttpService(HttpClient httpClient, IDebugService debugService)
    {
        _httpClient = httpClient;
        _debugService = debugService;
    }

    public string? ServerUrl => _serverUrl;
    public string? Token => _token;
    public bool IsAuthenticated => !string.IsNullOrEmpty(_token);

    public void SetCurrentTenant(Guid? tenantId)
    {
        _currentTenantId = tenantId;
        UpdateTenantHeader();
    }

    private void UpdateTenantHeader()
    {
        _httpClient.DefaultRequestHeaders.Remove("X-Tenant-Id");
        if (_currentTenantId.HasValue)
        {
            _httpClient.DefaultRequestHeaders.Add("X-Tenant-Id", _currentTenantId.Value.ToString());
        }
    }

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
            var httpResult = await response.Content.ReadFromJsonAsync<Result<LoginResponseDto>>(_jsonOptions);
            var authResponse = httpResult?.Data;

            if (authResponse?.Token != null)
            {
                _token = authResponse.Token;
                _httpClient.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _token);

                _debugService.LogInfo($"Login Response - AvailableTenants Count: {authResponse.AvailableTenants?.Count ?? 0}");
                _debugService.LogInfo($"Login Response - CurrentTenantId: {authResponse.CurrentTenantId}");

                // Falls AvailableTenants in der Response leer sind, versuche sie aus dem JWT Token zu extrahieren
                if (authResponse.AvailableTenants == null || authResponse.AvailableTenants.Count == 0)
                {
                    authResponse.AvailableTenants = JwtTokenParser.ExtractAvailableTenants(_token);
                    _debugService.LogInfo($"Extracted from JWT - AvailableTenants Count: {authResponse.AvailableTenants.Count}");
                }

                // Falls CurrentTenantId nicht gesetzt ist, versuche es aus dem JWT Token zu extrahieren
                if (authResponse.CurrentTenantId == null)
                {
                    authResponse.CurrentTenantId = JwtTokenParser.ExtractCurrentTenantId(_token);
                    _debugService.LogInfo($"Extracted from JWT - CurrentTenantId: {authResponse.CurrentTenantId}");
                }

                return authResponse;
            }

            return new LoginResponseDto
            {
                Succeeded = false,
                Message = $"Login failed with status: {response.StatusCode}"
            };
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
        _currentTenantId = null;
        _httpClient.DefaultRequestHeaders.Authorization = null;
        _httpClient.DefaultRequestHeaders.Remove("X-Tenant-Id");
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

    public async Task<FileDownloadResult> DownloadFileAsync(string endpoint, string suggestedFileName)
    {
        if (!IsAuthenticated || string.IsNullOrEmpty(_serverUrl))
        {
            return new FileDownloadResult
            {
                Success = false,
                ErrorMessage = "Not authenticated or no server URL"
            };
        }

        try
        {
            var url = $"{_serverUrl}/api/v1/{endpoint.TrimStart('/')}";
            _debugService.LogDebug($"Downloading from URL: {url}");
            var response = await _httpClient.GetAsync(url);
            _debugService.LogDebug($"Response status: {response.StatusCode}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsByteArrayAsync();

                // Get the Downloads folder path
                var downloadsPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
                downloadsPath = Path.Combine(downloadsPath, "Downloads");

                // Ensure the Downloads directory exists
                Directory.CreateDirectory(downloadsPath);

                // Generate unique filename if file already exists
                var fileName = suggestedFileName;
                var fullPath = Path.Combine(downloadsPath, fileName);
                var counter = 1;

                while (File.Exists(fullPath))
                {
                    var nameWithoutExtension = Path.GetFileNameWithoutExtension(suggestedFileName);
                    var extension = Path.GetExtension(suggestedFileName);
                    fileName = $"{nameWithoutExtension}_{counter}{extension}";
                    fullPath = Path.Combine(downloadsPath, fileName);
                    counter++;
                }

                // Write the file
                await File.WriteAllBytesAsync(fullPath, content);

                return new FileDownloadResult
                {
                    Success = true,
                    FilePath = fullPath
                };
            }
            else
            {
                return new FileDownloadResult
                {
                    Success = false,
                    ErrorMessage = $"Download failed with status: {response.StatusCode}"
                };
            }
        }
        catch (Exception ex)
        {
            return new FileDownloadResult
            {
                Success = false,
                ErrorMessage = $"Error downloading file: {ex.Message}"
            };
        }
    }
}