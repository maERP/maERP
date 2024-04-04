using maERP.Application.Models.Identity;

namespace maERP.Application.Contracts.Persistence;

public interface IAuthService
{
    Task<AuthResponse> Login(AuthRequest request);
    Task<RegistrationResponse> Register(RegistrationRequest request);
}