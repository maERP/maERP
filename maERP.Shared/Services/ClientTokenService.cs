using Blazored.LocalStorage;
using maERP.Shared.Dtos.User;

namespace maERP.Shared.Services;

public interface IClientTokenService
{
    Task SetToken(string token);
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

    public async Task SetToken(string token)
    {
        await _localStorageService.SetItemAsync("AccessToken", token);
    }

    public async Task<TokenDto> GetToken()
    {
        return await _localStorageService.GetItemAsync<TokenDto>("AccessToken");
    }

    public async Task RemoveToken()
    {
        await _localStorageService.RemoveItemAsync("AccessToken");
    }
}