using System;
using maERP.Shared.Contracts;
using maERP.Shared.Dtos.User;
using maERP.Shared.Models;
using Microsoft.AspNetCore.Components.Authorization;

namespace maERP.Shared.Services;

public class AuthHttpProvider
{
    IDataService<ApiUser> _dataService;
    ITokenService _tokenService;
    AuthStateProvider _authStateProvider;

    public AuthHttpProvider(IDataService<ApiUser> dataService, ITokenService tokenService, AuthStateProvider authStateProvider)
	{
        _dataService = dataService;
        _tokenService = tokenService;
        _authStateProvider = authStateProvider;

    }

    public async Task<LoginResponseDto> LoginUser(LoginDto loginDto)
    {
        try
        {
            var result = await _dataService.Login(loginDto.Server, loginDto.Email, loginDto.Password);
            await _tokenService.SetToken(result.Token);
            _authStateProvider.StateChanged();
            return result;
        }
        catch (Exception)
        {
            return new LoginResponseDto
            {
                Succeeded = false,
                Message = "Sorry, we were unable to log you in at this time. Please try again shortly."
            };
        }
    }

    public async Task LogoutUser()
    {
        await _tokenService.RemoveToken();
        _authStateProvider.StateChanged();
    }
}