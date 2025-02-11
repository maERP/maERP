using maERP.Domain.Entities;

namespace maERP.Application.Contracts.Persistence;

public interface IUserService
{
    Task<List<ApplicationUser>> GetUsers();
    Task<ApplicationUser> GetUser(string userId);
}