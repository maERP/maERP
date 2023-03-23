using maERP.Shared.Dtos.User;

namespace maERP.Shared.Contracts;

public interface ITokenService
{
    Task SetToken(TokenDto tokenDTO);
    Task<TokenDto> GetToken();
    Task RemoveToken();
}