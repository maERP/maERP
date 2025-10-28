using maERP.Application.Models.Identity;
using maERP.Domain.Dtos.Auth;

namespace maERP.Client.Services.Api.Clients;

/// <summary>
/// API client for authentication operations
/// </summary>
public interface IAuthApiClient
{
    /// <summary>
    /// Login with email and password
    /// </summary>
    Task<LoginResponseDto?> LoginAsync(string email, string password, CancellationToken cancellationToken = default);

    /// <summary>
    /// Register a new user account
    /// </summary>
    Task<RegistrationResponse?> RegisterAsync(RegistrationRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Request password reset
    /// </summary>
    Task<ForgotPasswordResponseDto?> ForgotPasswordAsync(string email, CancellationToken cancellationToken = default);

    /// <summary>
    /// Reset password with token
    /// </summary>
    Task<ResetPasswordResponseDto?> ResetPasswordAsync(string email, string token, string newPassword, CancellationToken cancellationToken = default);
}
