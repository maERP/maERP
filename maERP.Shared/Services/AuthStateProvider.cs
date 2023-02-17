#nullable disable

using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Logging;
using maERP.Shared.Contracts;
using maERP.Shared.Services;
using maERP.Shared.Models;
using maERP.Shared.Dtos.User;
using System.Text.Json;
using System.Security.Principal;

namespace maERP.Shared.Services;

public class AuthStateProvider : AuthenticationStateProvider
{
    private readonly ITokenService _tokenService;
    private readonly IDataService<ApiUser> _dataService;

    public AuthStateProvider(ITokenService tokenService, IDataService<ApiUser> dataService)
    {
        _tokenService = tokenService;
        _dataService = dataService;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        try
        {
            var tokenDto = await _tokenService.GetToken();

            // empty token            
            if(tokenDto is null ||
               string.IsNullOrEmpty(tokenDto.AccessToken) ||
               string.IsNullOrEmpty(tokenDto.RefreshToken) ||
               string.IsNullOrEmpty(tokenDto.BaseUrl))
            {
                await _tokenService.RemoveToken();
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
            }
            // token is expired
            else if (!string.IsNullOrEmpty(tokenDto?.AccessToken) && tokenDto?.AccessTokenExpiration <= DateTime.Now)
            {
                var loginResponseDto = await RefreshToken();

                if(loginResponseDto == null)
                {
                    await _tokenService.RemoveToken();
                    StateChanged();
                    return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
                }

                var identity = new ClaimsIdentity(ParseClaimsFromJwt(loginResponseDto.Token.AccessToken), "jwt");
                StateChanged();
                return new AuthenticationState(new ClaimsPrincipal(identity));
            }
            // check if token is valid
            else if (!string.IsNullOrEmpty(tokenDto?.AccessToken))
            {
                bool result = await _dataService.CheckAccessToken(tokenDto.AccessToken);

                if (result)
                {
                    var identity = new ClaimsIdentity(ParseClaimsFromJwt(tokenDto.AccessToken), "jwt");
                    StateChanged();
                    return new AuthenticationState(new ClaimsPrincipal(identity));
                }
            }

            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw new Exception() ;
        }

        /* old code
        var identity = string.IsNullOrEmpty(tokenDto?.AccessToken) || tokenDto?.AccessTokenExpiration < DateTime.Now
            ? new ClaimsIdentity()
            : new ClaimsIdentity(ParseClaimsFromJwt(tokenDto.AccessToken), "jwt");
        */
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

    public async Task<bool> CheckAccessToken()
    {
        var token = await _tokenService.GetToken();
        var result = await _dataService.CheckAccessToken(token.AccessToken);

        return result;
    }

    public async Task<LoginResponseDto> RefreshToken()
    {
        var token = await _tokenService.GetToken();
        var result = await _dataService.RefreshToken(token.RefreshToken);

        if(result != null)
        {
            await _tokenService.SetToken(result.Token);
        }
                
        return result;
    }

    public async Task LogoutUser()
    {
        await _tokenService.RemoveToken();
    }

    private static IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
    {
        var payload = jwt.Split('.')[1];
        var jsonBytes = ParseBase64WithoutPadding(payload);
        var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);
        return keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString()));
    }

    private static byte[] ParseBase64WithoutPadding(string base64)
    {
        switch (base64.Length % 4)
        {
            case 2: base64 += "=="; break;
            case 3: base64 += "="; break;
        }
        return Convert.FromBase64String(base64);
    }

    public void StateChanged()
    {
        Console.WriteLine("Call StateChanged");
        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }
}