using maERP.SharedUI.Models.User;
using maERP.SharedUI.Services.Base;

namespace maERP.SharedUI.Contracts;

public interface IUserService
{
    Task<List<UserVm>> GetUsers();
    Task<UserVm> GetUserDetails(string id);
    Task<Response<Guid>> CreateUser(UserVm user);
    Task<Response<Guid>> UpdateUser(string id, UserVm user);
    Task<Response<Guid>> DeleteUser(string id);
}
