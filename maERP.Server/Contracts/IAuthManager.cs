#nullable disable

using maERP.Data.Dtos.User;
using Microsoft.AspNetCore.Identity;

namespace maERP.Server.Contracts
{
	public interface IAuthManager
	{
		Task<IEnumerable<IdentityError>> Register(ApiUserDto userDto);

		Task<LoginResponseDto> Login(LoginDto userDto);

		Task<string> CreateRefreshToken();

		Task<LoginResponseDto> VerifyRefreshToken(LoginResponseDto request);
	}
}