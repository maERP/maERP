using maERP.Domain.Entities;

namespace maERP.Application.Contracts.Persistence;

public interface IUserService
{
    Task<List<User>> GetUsers();
    Task<User> GetUser(string userId);
}