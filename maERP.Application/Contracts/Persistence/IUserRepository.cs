using maERP.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace maERP.Application.Contracts.Persistence;

public interface IUserRepository
{
    Task<List<ApplicationUser>> GetAllAsync();
    Task<ApplicationUser?> GetByIdAsync(string userId);
    Task<IEnumerable<IdentityError>> CreateAsync(ApplicationUser userToCreate, string password);
    Task<ApplicationUser> UpdateWithDetailsAsync(ApplicationUser userUpdateDto);
    Task<bool> Exists(string id);
}