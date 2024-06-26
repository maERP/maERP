﻿using AutoMapper;
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

    public async Task<List<UserVM>> GetUsers()
    {
        await AddBearerToken();
        var users = await _client.UsersAllAsync();
        return _mapper.Map<List<UserVM>>(users);
    }

    public async Task<UserVM> GetUserDetails(string id)
    {
        await AddBearerToken();
        var user = await _client.UsersGETAsync(id);
        return _mapper.Map<UserVM>(user);
    }

    public async Task<Response<Guid>> CreateUser(UserVM user)
    {
        try
        {
            await AddBearerToken();
            var userCreateCommand = _mapper.Map<UserCreateCommand>(user);
            await _client.UsersPOSTAsync(userCreateCommand);
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

    public async Task<Response<Guid>> UpdateUser(string id, UserVM user)
    {
        try
        {
            await AddBearerToken();
            var userUpdateCommand = _mapper.Map<UserUpdateCommand>(user);
            await _client.UsersPUTAsync(Convert.ToInt32(id), userUpdateCommand);
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