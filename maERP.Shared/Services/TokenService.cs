using Blazored.LocalStorage;
using maERP.Shared.Contracts;
using maERP.Shared.Dtos.User;

namespace maERP.Shared.Services;

public class TokenService : ITokenService
{
    private readonly ILocalStorageService _localStorageService;

    public TokenService(ILocalStorageService localStorageService)
    {
        _localStorageService = localStorageService;
    }

    public async Task SetToken(TokenDto tokenDTO)
    {
        await _localStorageService.SetItemAsync("token", tokenDTO);
    }

    public async Task<TokenDto> GetToken()
    {
        return await _localStorageService.GetItemAsync<TokenDto>("token");
    }

    public async Task RemoveToken()
    {
        await _localStorageService.RemoveItemAsync("token");
    }
}