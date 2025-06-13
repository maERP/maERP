using System.Threading.Tasks;
using maERP.Domain.Dtos.Auth;

namespace maERP.UI.Services;

public interface IAuthenticationService
{
    Task<LoginResponseDto> LoginAsync(string email, string password, string serverUrl);
    Task LogoutAsync();
    bool IsAuthenticated { get; }
    string? Token { get; }
    string? ServerUrl { get; }
}