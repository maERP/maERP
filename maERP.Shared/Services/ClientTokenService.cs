using Blazored.LocalStorage;
using maERP.Shared.Dtos.User;

namespace maERP.Shared.Services;

public interface IClientTokenService
{
    Task SetToken(TokenDto tokenDTO);
    Task<TokenDto> GetToken();
    Task RemoveToken();
}

public class ClientTokenService : IClientTokenService
{
    private readonly ILocalStorageService _localStorageService;

    public ClientTokenService(ILocalStorageService localStorageService)
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