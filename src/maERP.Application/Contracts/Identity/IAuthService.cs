using maERP.Application.Models.Identity;
using maERP.Domain.Dtos.Auth;
using maERP.Domain.Wrapper;

namespace maERP.Application.Contracts.Identity;

public interface IAuthService
{
    Task<Result<LoginResponseDto>> Login(AuthRequest request);
    Task<Result<LoginResponseDto>> Register(RegistrationRequest request);
    Task<Result<ForgotPasswordResponse>> ForgotPassword(ForgotPasswordRequest request);
    Task<Result<ResetPasswordResponse>> ResetPassword(ResetPasswordRequest request);

    /// <summary>
    /// Validates the supplied refresh token, rotates it, and returns a fresh JWT + refresh-token pair.
    /// On replay (presenting an already-revoked token) the entire token family is revoked.
    /// </summary>
    Task<Result<LoginResponseDto>> RefreshToken(string refreshToken);

    /// <summary>Revokes the supplied refresh token (and its successors). No-op if not found.</summary>
    Task Logout(string refreshToken);
}
