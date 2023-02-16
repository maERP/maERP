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
// using static System.Net.WebRequestMethods;

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
        Console.WriteLine("Call GetAuthenticationStateAsync");
        var tokenDto = await _tokenService.GetToken();

        try
        {
            Console.WriteLine("TOKEN: {0}", tokenDto?.AccessToken);          
            Console.WriteLine("EXPIRE: {0}", tokenDto?.AccessTokenExpiration);
            Console.WriteLine("BASEURL: {0}", tokenDto?.BaseUrl);

            if(string.IsNullOrEmpty(tokenDto.BaseUrl))
            {
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
            }
            // check if token is expired
            if (!string.IsNullOrEmpty(tokenDto?.AccessToken) && tokenDto?.AccessTokenExpiration <= DateTime.Now)
            {
                Console.WriteLine("refresh token");
                var loginResponseDto = await RefreshToken();
                var identity = new ClaimsIdentity(ParseClaimsFromJwt(loginResponseDto.Token.AccessToken), "jwt");
                StateChanged();
                return new AuthenticationState(new ClaimsPrincipal(identity));
            }
            // check if token is valid
            else if (!string.IsNullOrEmpty(tokenDto?.AccessToken))
            {
                Console.WriteLine("CheckToken");
                bool result = await _dataService.CheckAccessToken(tokenDto.AccessToken);

                if (result)
                {
                    Console.WriteLine("CheckToken true");
                    var identity = new ClaimsIdentity(ParseClaimsFromJwt(tokenDto.AccessToken), "jwt");
                    StateChanged();
                    return new AuthenticationState(new ClaimsPrincipal(identity));
                }

                Console.WriteLine("CheckToken false");
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
            Console.WriteLine("Debug 1");
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
        await _tokenService.SetToken(result.Token);
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