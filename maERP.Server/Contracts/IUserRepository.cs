using Microsoft.AspNetCore.Identity;
using maERP.Shared.Dtos.User;

namespace maERP.Server.Contracts;

public interface IUserRepository
{
    Task<List<UserListDto>> GetAllAsync();
    Task<UserDetailDto> GetByIdAsync(string userId);
    Task<IEnumerable<IdentityError>> AddAsync(UserCreateDto userDto);
    // Task<ApplicationUser> UpdateAsync(ApplicationUser userDto);
    Task<bool> Exists(string id);
    Task UpdateWithDetailsAsync(string id, UserUpdateDto userUpdateDto);
}