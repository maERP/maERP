using AutoMapper;
using Blazored.LocalStorage;
using maERP.SharedUI.Contracts;
using maERP.SharedUI.Models.User;
using maERP.SharedUI.Services.Base;

namespace maERP.SharedUI.Services;

public class UserService : BaseHttpService, IUserService
{
    private readonly IMapper _mapper;

    public UserService(IClient client, IMapper mapper, ILocalStorageService localStorageService) : base(client, localStorageService)
    {
        _mapper = mapper;
    }

    public async Task<List<UserVm>> GetUsers()
    {
        await AddBearerToken();
        var users = await Client.UsersAllAsync();
        return _mapper.Map<List<UserVm>>(users);
    }

    public async Task<UserVm> GetUserDetails(string id)
    {
        await AddBearerToken();
        var user = await Client.UsersGETAsync(id);
        return _mapper.Map<UserVm>(user);
    }

    public async Task<Response<Guid>> CreateUser(UserVm user)
    {
        try
        {
            await AddBearerToken();
            var userCreateCommand = _mapper.Map<UserCreateCommand>(user);
            await Client.UsersPOSTAsync(userCreateCommand);
            return new Response<Guid>
            {
                Success = true
            };
        }
        catch (ApiException ex)
        {
            return ConvertApiExceptions<Guid>(ex);
        }
    }

    public async Task<Response<Guid>> UpdateUser(string id, UserVm user)
    {
        try
        {
            await AddBearerToken();
            var userUpdateCommand = _mapper.Map<UserUpdateCommand>(user);
            await Client.UsersPUTAsync(Convert.ToInt32(id), userUpdateCommand);
            return new Response<Guid>
            {
                Success = true
            };
        }
        catch (ApiException ex)
        {
            return ConvertApiExceptions<Guid>(ex);
        }
    }
    public async Task<Response<Guid>> DeleteUser(string id)
    {
        try
        {
            await AddBearerToken();
            // await _client.UsersDELETEAsync(id);
            return new Response<Guid>
            {
                Success = true
            };
        }
        catch (ApiException ex)
        {
            return ConvertApiExceptions<Guid>(ex);
        }
    }
}