using maERP.Application.Models.Identity;
using maERP.Domain.Dtos.Auth;
using maERP.Domain.Wrapper;

namespace maERP.Application.Contracts.Identity;

public interface IAuthService
{
    Task<Result<LoginResponseDto>> Login(AuthRequest request);
    Task<Result<RegistrationResponse>> Register(RegistrationRequest request);
}