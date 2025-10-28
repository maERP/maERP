using maERP.Application.Models.Identity;
using maERP.Domain.Dtos.Auth;

namespace maERP.Client.Services.Api.Clients;

/// <summary>
/// Implementation of authentication API client
/// </summary>
public class AuthApiClient : ApiClientBase, IAuthApiClient
{
    private const string BaseEndpoint = "api/v1/Auth";

    public AuthApiClient(HttpClient httpClient, ILogger<AuthApiClient> logger)
        : base(httpClient, logger)
    {
    }

    public async Task<LoginResponseDto?> LoginAsync(
        LoginRequestDto request,
        CancellationToken cancellationToken = default)
    {
        return await PostAsync<LoginRequestDto, LoginResponseDto>(
            $"{BaseEndpoint}/login",
            request,
            cancellationToken);
    }

    public async Task<RegistrationResponse?> RegisterAsync(
        RegistrationRequest request,
        CancellationToken cancellationToken = default)
    {
        return await PostAsync<RegistrationRequest, RegistrationResponse>(
            $"{BaseEndpoint}/register",
            request,
            cancellationToken);
    }

    public async Task<ForgotPasswordResponseDto?> ForgotPasswordAsync(
        string email,
        CancellationToken cancellationToken = default)
    {
        var request = new ForgotPasswordRequestDto { Email = email };

        return await PostAsync<ForgotPasswordRequestDto, ForgotPasswordResponseDto>(
            $"{BaseEndpoint}/forgot-password",
            request,
            cancellationToken);
    }

    public async Task<ResetPasswordResponseDto?> ResetPasswordAsync(
        string email,
        string token,
        string newPassword,
        CancellationToken cancellationToken = default)
    {
        var request = new ResetPasswordRequestDto
        {
            Email = email,
            Token = token,
            NewPassword = newPassword
        };

        return await PostAsync<ResetPasswordRequestDto, ResetPasswordResponseDto>(
            $"{BaseEndpoint}/reset-password",
            request,
            cancellationToken);
    }
}
