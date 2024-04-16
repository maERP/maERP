using maERP.SharedUI.Models.User;
using maERP.SharedUI.Services.Base;

namespace maERP.SharedUI.Contracts;

public interface IUserService
{
    Task<List<UserVM>> GetUsers();
    Task<UserVM> GetUserDetails(string id);
    Task<Response<Guid>> CreateUser(UserVM user);
    Task<Response<Guid>> UpdateUser(string id, UserVM user);
    Task<Response<Guid>> DeleteUser(string id);
}
