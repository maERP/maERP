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

namespace maERP.UI.Shared.Services;

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
            _debugService.LogInfo($"✅ X-Tenant-Id header set to: {_currentTenantId.Value}");
            
            // Verify the header was actually set
            var headerExists = _httpClient.DefaultRequestHeaders.Contains("X-Tenant-Id");
            var headerValue = headerExists ? string.Join(",", _httpClient.DefaultRequestHeaders.GetValues("X-Tenant-Id")) : "NOT FOUND";
            _debugService.LogInfo($"🔍 Header verification - Exists: {headerExists}, Value: {headerValue}");
        }
        else
        {
            _debugService.LogInfo("❌ X-Tenant-Id header cleared (no tenant set)");
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

                _debugService.LogInfo($"🔐 Login successful! Processing tenant data...");

                // Debug: Parse and display all JWT claims and roles
                JwtTokenParser.DebugTokenClaims(_token, msg => _debugService.LogInfo(msg));

                _debugService.LogInfo($"📊 Login Response - AvailableTenants Count: {authResponse.AvailableTenants?.Count ?? 0}");
                _debugService.LogInfo($"🏢 Login Response - CurrentTenantId: {authResponse.CurrentTenantId}");
                
                // Log available tenants details
                if (authResponse.AvailableTenants != null && authResponse.AvailableTenants.Count > 0)
                {
                    foreach (var tenant in authResponse.AvailableTenants)
                    {
                        _debugService.LogInfo($"   📋 Tenant: {tenant.Name} (ID: {tenant.Id})");
                    }
                }
                else
                {
                    _debugService.LogWarning($"⚠️ No tenants in login response! Trying JWT extraction...");
                }

                // Falls AvailableTenants in der Response leer sind, versuche sie aus dem JWT Token zu extrahieren
                if (authResponse.AvailableTenants == null || authResponse.AvailableTenants.Count == 0)
                {
                    authResponse.AvailableTenants = JwtTokenParser.ExtractAvailableTenants(_token);
                    _debugService.LogInfo($"🔍 Extracted from JWT - AvailableTenants Count: {authResponse.AvailableTenants.Count}");
                    
                    if (authResponse.AvailableTenants.Count > 0)
                    {
                        foreach (var tenant in authResponse.AvailableTenants)
                        {
                            _debugService.LogInfo($"   📋 JWT Tenant: {tenant.Name} (ID: {tenant.Id})");
                        }
                    }
                }

                // Falls CurrentTenantId nicht gesetzt ist, versuche es aus dem JWT Token zu extrahieren
                if (authResponse.CurrentTenantId == null)
                {
                    authResponse.CurrentTenantId = JwtTokenParser.ExtractCurrentTenantId(_token);
                    _debugService.LogInfo($"🔍 Extracted from JWT - CurrentTenantId: {authResponse.CurrentTenantId}");
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

    public async Task<RegistrationResponseDto> RegisterAsync(string firstName, string lastName, string email, string password, string serverUrl)
    {
        try
        {
            _serverUrl = serverUrl.TrimEnd('/');

            var registrationRequest = new
            {
                Firstname = firstName,
                Lastname = lastName,
                Email = email,
                Username = email,
                Password = password
            };

            var json = JsonSerializer.Serialize(registrationRequest, _jsonOptions);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var registerUrl = $"{_serverUrl}/api/v1/auth/register";
            var response = await _httpClient.PostAsync(registerUrl, content);

            if (response.IsSuccessStatusCode)
            {
                var resultContent = await response.Content.ReadAsStringAsync();

                try
                {
                    var jsonDoc = JsonDocument.Parse(resultContent);
                    var root = jsonDoc.RootElement;

                    var succeeded = (root.TryGetProperty("succeeded", out var succeededProp) || root.TryGetProperty("Succeeded", out succeededProp))
                                    && succeededProp.GetBoolean();

                    // Try to read messages array first (try both PascalCase and camelCase), then fall back to message
                    string? message = null;
                    JsonElement messagesProp;
                    bool hasMessages = root.TryGetProperty("messages", out messagesProp) || root.TryGetProperty("Messages", out messagesProp);

                    if (hasMessages && messagesProp.ValueKind == JsonValueKind.Array)
                    {
                        var messages = new List<string>();
                        foreach (var msg in messagesProp.EnumerateArray())
                        {
                            if (msg.ValueKind == JsonValueKind.String)
                            {
                                messages.Add(msg.GetString() ?? "");
                            }
                        }
                        message = string.Join("\n", messages);
                    }
                    else if (root.TryGetProperty("message", out var messageProp) || root.TryGetProperty("Message", out messageProp))
                    {
                        message = messageProp.GetString();
                    }

                    var userId = "";

                    if (root.TryGetProperty("data", out var dataProp) && dataProp.TryGetProperty("userId", out var userIdProp))
                    {
                        userId = userIdProp.GetString();
                    }

                    return new RegistrationResponseDto
                    {
                        Succeeded = succeeded,
                        Message = message ?? "Registrierung erfolgreich",
                        UserId = userId
                    };
                }
                catch
                {
                    return new RegistrationResponseDto
                    {
                        Succeeded = true,
                        Message = "Registrierung erfolgreich",
                        UserId = null
                    };
                }
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();

                _debugService.LogInfo($"❌ Registration failed with status: {response.StatusCode}");
                _debugService.LogInfo($"📄 Response content: {errorContent}");

                try
                {
                    var jsonDoc = JsonDocument.Parse(errorContent);
                    var root = jsonDoc.RootElement;

                    // Try to read messages array first (try both PascalCase and camelCase), then fall back to message
                    string? message = null;
                    JsonElement messagesProp;
                    bool hasMessages = root.TryGetProperty("messages", out messagesProp) || root.TryGetProperty("Messages", out messagesProp);

                    if (hasMessages && messagesProp.ValueKind == JsonValueKind.Array)
                    {
                        var messages = new List<string>();
                        foreach (var msg in messagesProp.EnumerateArray())
                        {
                            if (msg.ValueKind == JsonValueKind.String)
                            {
                                messages.Add(msg.GetString() ?? "");
                            }
                        }
                        message = string.Join("\n", messages);
                        _debugService.LogInfo($"✅ Extracted messages from array: {message}");
                    }
                    else if (root.TryGetProperty("message", out var messageProp) || root.TryGetProperty("Message", out messageProp))
                    {
                        message = messageProp.GetString();
                        _debugService.LogInfo($"✅ Extracted message: {message}");
                    }
                    else
                    {
                        _debugService.LogInfo($"⚠️ No messages or message property found in response");
                    }

                    var finalMessage = message ?? $"Registrierung fehlgeschlagen: {response.StatusCode}";
                    _debugService.LogInfo($"🔔 Final message to display: {finalMessage}");

                    return new RegistrationResponseDto
                    {
                        Succeeded = false,
                        Message = finalMessage
                    };
                }
                catch (Exception ex)
                {
                    _debugService.LogInfo($"⚠️ Error parsing error response: {ex.Message}");
                    return new RegistrationResponseDto
                    {
                        Succeeded = false,
                        Message = $"Registrierung fehlgeschlagen: {response.StatusCode}"
                    };
                }
            }
        }
        catch (Exception ex)
        {
            return new RegistrationResponseDto
            {
                Succeeded = false,
                Message = $"Registrierungsfehler: {ex.Message}"
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

            // Debug: Log current headers before request
            var hasTenantHeader = _httpClient.DefaultRequestHeaders.Contains("X-Tenant-Id");
            var tenantHeaderValue = hasTenantHeader ? string.Join(",", _httpClient.DefaultRequestHeaders.GetValues("X-Tenant-Id")) : "MISSING";
            var hasAuthHeader = _httpClient.DefaultRequestHeaders.Authorization != null;
            var authHeaderValue = hasAuthHeader ? $"Bearer {_httpClient.DefaultRequestHeaders.Authorization!.Parameter?.Substring(0, Math.Min(20, _httpClient.DefaultRequestHeaders.Authorization.Parameter?.Length ?? 0))}..." : "MISSING";

            _debugService.LogInfo($"🚀 Making GET request to: {url}");
            _debugService.LogInfo($"📋 Current X-Tenant-Id header: {(hasTenantHeader ? tenantHeaderValue : "MISSING")}");
            _debugService.LogInfo($"🔐 Current Authorization header: {authHeaderValue}");
            _debugService.LogInfo($"✅ IsAuthenticated: {IsAuthenticated}, Token exists: {!string.IsNullOrEmpty(_token)}");

            var response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<PaginatedResult<T>>(_jsonOptions);
                return result;
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                _debugService.LogWarning($"❌ GET {url} failed: {response.StatusCode} - {errorContent}");
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
                var errorContent = await response.Content.ReadAsStringAsync();
                _debugService.LogWarning($"GET {url} failed: {response.StatusCode} - {errorContent}");
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
                var errorContent = await response.Content.ReadAsStringAsync();
                _debugService.LogWarning($"POST {url} failed: {response.StatusCode} - {errorContent}");
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
                var errorContent = await response.Content.ReadAsStringAsync();
                _debugService.LogWarning($"PUT {url} failed: {response.StatusCode} - {errorContent}");
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
                var errorContent = await response.Content.ReadAsStringAsync();
                _debugService.LogWarning($"DELETE {url} failed: {response.StatusCode} - {errorContent}");
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
