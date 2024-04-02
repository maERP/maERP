using maERP.Shared.Models.Database;

namespace maERP.Server.Contracts;

public interface IUserService
{
    Task<List<User>> GetUsers();
    Task<User> GetUser(string userId);
}