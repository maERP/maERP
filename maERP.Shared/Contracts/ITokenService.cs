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