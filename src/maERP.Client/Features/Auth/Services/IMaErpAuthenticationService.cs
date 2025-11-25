using maERP.Domain.Dtos.Auth;

namespace maERP.Client.Features.Auth.Services;

public interface IMaErpAuthenticationService
{
    Task<LoginResponseDto?> LoginAsync(LoginRequestDto request, CancellationToken cancellationToken = default);
    Task<bool> ValidateTokenAsync(string token, CancellationToken cancellationToken = default);
}
