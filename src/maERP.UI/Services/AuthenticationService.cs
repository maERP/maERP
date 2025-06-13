using System.Threading.Tasks;
using maERP.Domain.Dtos.Auth;

namespace maERP.UI.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly IHttpService _httpService;

    public AuthenticationService(IHttpService httpService)
    {
        _httpService = httpService;
    }

    public bool IsAuthenticated => _httpService.IsAuthenticated;
    public string? Token => _httpService.Token;
    public string? ServerUrl => _httpService.ServerUrl;

    public async Task<LoginResponseDto> LoginAsync(string email, string password, string serverUrl)
    {
        return await _httpService.LoginAsync(email, password, serverUrl);
    }

    public async Task LogoutAsync()
    {
        await _httpService.LogoutAsync();
    }
}