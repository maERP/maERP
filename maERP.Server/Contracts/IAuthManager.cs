#nullable disable

using maERP.Server.Models.Users;
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