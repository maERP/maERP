using maERP.Server.Models.Identity;

namespace maERP.Server.Contracts;

public interface IAuthService
{
    Task<AuthResponse> Login(AuthRequest request);
    Task<RegistrationResponse> Register(RegistrationRequest request);
}