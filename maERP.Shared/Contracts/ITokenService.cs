using System;
using Blazored.LocalStorage;
using maERP.Shared.Dtos.User;

namespace maERP.Shared.Contracts;

public interface ITokenService
{
    Task<TokenDto> GetToken();
    Task RemoveToken();
    Task SetToken(TokenDto tokenDTO);
}

public class TokenService : ITokenService
{
    private readonly ILocalStorageService localStorageService;

    public TokenService(ILocalStorageService localStorageService)
    {
        this.localStorageService = localStorageService;
    }

    public async Task SetToken(TokenDto tokenDTO)
    {
        await localStorageService.SetItemAsync("token", tokenDTO);
    }

    public async Task<TokenDto> GetToken()
    {
        return await localStorageService.GetItemAsync<TokenDto>("token");
    }

    public async Task RemoveToken()
    {
        await localStorageService.RemoveItemAsync("token");
    }
}