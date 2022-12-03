#nullable disable

using maERP.Shared.Dtos.User;
using maERP.Shared.Models;
using Microsoft.AspNetCore.Identity;

namespace maERP.Server.Contracts
{
	public interface IAuthManager
	{
        Task<LoginResponseDto> Login(LoginDto userDto);

		Task<string> CreateRefreshToken();

		Task<LoginResponseDto> VerifyRefreshToken(LoginResponseDto request);

        Task<IEnumerable<IdentityError>> Register(ApiUserDto userDto);

        Task<ApiUser> UpdateAsync(ApiUserDto userDto);

        Task<IQueryable<ApiUser>> GetAllAsync();

        Task<ApiUserDto> GetByIdAsync(string userId);
    }
}