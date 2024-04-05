using maERP.Application.Dtos.User;
using maERP.Domain;
using Microsoft.AspNetCore.Identity;

namespace maERP.Application.Contracts.Persistence;

public interface IUserRepository
{
    Task<List<ApplicationUser>> GetAllAsync();
    Task<ApplicationUser> GetByIdAsync(string userId);
    // Task<IEnumerable<IdentityError>> AddAsync(ApplicationUser userDto);
    // Task<ApplicationUser> UpdateAsync(ApplicationUser userDto);
    Task<bool> Exists(string id);
    Task UpdateWithDetailsAsync(string id, UserUpdateDto userUpdateDto);
}