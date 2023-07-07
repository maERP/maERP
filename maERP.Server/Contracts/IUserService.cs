using maERP.Server.Models;

namespace maERP.Server.Contracts;

public interface IUserService
{
    Task<List<User>> GetUsers();
    Task<User> GetUser(string userId);
}