using System.Net.Http.Json;
using maERP.Client.Core.Constants;
using maERP.Client.Core.Extensions;
using maERP.Client.Core.Json;
using maERP.Client.Core.Models;
using maERP.Client.Features.Auth.Services;
using maERP.Domain.Dtos.Account;
using Microsoft.Extensions.Logging;

namespace maERP.Client.Features.Account.Services;

public class AccountService : IAccountService
{
    private readonly HttpClient _httpClient;
    private readonly ITokenStorageService _tokenStorage;
    private readonly ILogger<AccountService> _logger;

    public AccountService(
        IHttpClientFactory httpClientFactory,
        ITokenStorageService tokenStorage,
        ILogger<AccountService> logger)
    {
        _httpClient = httpClientFactory.CreateClient("MaErpApi");
        _tokenStorage = tokenStorage;
        _logger = logger;
    }

    private async Task<string> GetBaseUrlAsync()
    {
        var serverUrl = await _tokenStorage.GetServerUrlAsync();
        if (string.IsNullOrEmpty(serverUrl))
        {
            throw new InvalidOperationException("Server URL is not configured. Please login first.");
        }
        return serverUrl.TrimEnd('/');
    }

    public async Task<CurrentUserProfileDto?> GetCurrentUserAsync(CancellationToken ct = default)
    {
        var baseUrl = await GetBaseUrlAsync();
        var url = $"{baseUrl}{ApiEndpoints.Account.Me}";

        _logger.LogInformation("Fetching current user profile from {Url}", url);

        var apiResponse = await _httpClient.GetFromJsonAsync(
            url, AppJsonSerializerContext.Default.ApiResponseCurrentUserProfileDto, ct);

        return apiResponse?.Data;
    }

    public async Task UpdateCurrentUserAsync(
        string email,
        string firstname,
        string lastname,
        string phoneNumber,
        CancellationToken ct = default)
    {
        var baseUrl = await GetBaseUrlAsync();
        var url = $"{baseUrl}{ApiEndpoints.Account.Me}";

        var payload = new UpdateCurrentUserPayload(email, firstname, lastname, phoneNumber);
        var response = await _httpClient.PutAsJsonAsync(
            url, payload, AppJsonSerializerContext.Default.UpdateCurrentUserPayload, ct);

        await response.EnsureSuccessOrThrowApiExceptionAsync(ct);
    }

    public async Task ChangePasswordAsync(
        string currentPassword,
        string newPassword,
        string newPasswordConfirm,
        CancellationToken ct = default)
    {
        var baseUrl = await GetBaseUrlAsync();
        var url = $"{baseUrl}{ApiEndpoints.Account.ChangePassword}";

        var payload = new ChangePasswordPayload(currentPassword, newPassword, newPasswordConfirm);
        var response = await _httpClient.PostAsJsonAsync(
            url, payload, AppJsonSerializerContext.Default.ChangePasswordPayload, ct);

        await response.EnsureSuccessOrThrowApiExceptionAsync(ct);
    }
}
