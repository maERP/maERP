#nullable disable

using maERP.Data.Dtos.User;
using Microsoft.AspNetCore.Identity;

namespace maERP.Server.Contracts
{
	public interface IAuthManager
	{
		Task<IEnumerable<IdentityError>> Register(ApiUserDto userDto);

		Task<AuthResponseDto> Login(LoginDto userDto);

		Task<string> CreateRefreshToken();

		Task<AuthResponseDto> VerifyRefreshToken(AuthResponseDto request);
	}
}