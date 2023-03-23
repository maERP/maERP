#nullable disable

using Microsoft.AspNetCore.Identity;
using maERP.Shared.Dtos.User;
using maERP.Shared.Models;

namespace maERP.Server.Contracts;

public interface IAuthManager
{
    Task<LoginResponseDto> Login(LoginDto userDto);

	Task<string> CreateRefreshToken();

	Task<LoginResponseDto> VerifyRefreshToken(RefreshTokenDto request);

    Task<IEnumerable<IdentityError>> Register(ApiUserDto userDto);

    Task<ApiUser> UpdateAsync(ApiUserDto userDto);

    Task<IQueryable<ApiUser>> GetAllAsync();

    Task<ApiUserDto> GetByIdAsync(string userId);
}