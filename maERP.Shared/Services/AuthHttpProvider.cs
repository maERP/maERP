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

    public AuthHttpProvider(IDataService<ApiUser> dataService, ITokenService tokenService)
	{
        _dataService = dataService;
        _tokenService = tokenService;
    }

    public async Task<LoginResponseDto> LoginUser(LoginDto loginDto)
    {
        try
        {
            var result = await _dataService.Login(loginDto.Server, loginDto.Email, loginDto.Password);
            await _tokenService.SetToken(result.Token);
            return result;
        }
        catch (Exception)
        {
            return new LoginResponseDto
            {
                Succeeded = false,
                Message = "Login fehlgeschlagen."
            };
        }
    }

    public async Task<LoginResponseDto> RefreshToken()
    {
        var token = await _tokenService.GetToken();
        var result = await _dataService.RefreshToken(token.RefreshToken);
        await _tokenService.SetToken(result.Token);
        return result;
    }

    public async Task LogoutUser()
    {
        await _tokenService.RemoveToken();
    }
}