using maERP.Domain;

namespace maERP.Application.Contracts.Persistence;

public interface IUserService
{
    Task<List<User>> GetUsers();
    Task<User> GetUser(string userId);
}