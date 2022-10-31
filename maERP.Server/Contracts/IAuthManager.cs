#nullable disable

using maERP.Shared.Dtos.User;
using Microsoft.AspNetCore.Identity;

namespace maERP.Server.Contracts
{
	public interface IAuthManager
	{
		Task<IEnumerable<IdentityError>> Register(ApiUserDto userDto);

        Task<ApiUserDto> FindByIdAsync(string userId);

        Task<List<maERP.Shared.Models.ApiUser>> GetAllAsync();

        Task<IEnumerable<IdentityError>> UpdateAsync(ApiUserDto userDto);

		Task<IEnumerable<IdentityError>> DeleteAsync(ApiUserDto userDto);

		Task<IEnumerable<IdentityError>> DeleteByIdAsync(string userId);

		Task<LoginResponseDto> Login(LoginDto userDto);

		Task<string> CreateRefreshToken();

		Task<LoginResponseDto> VerifyRefreshToken(LoginResponseDto request);
	}
}