#nullable disable

using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using System.Text.Json;
using maERP.Shared.Dtos.User;
using maERP.Shared.Models;

namespace maERP.Shared.Services;

public class AuthStateProvider : AuthenticationStateProvider
{
    private readonly IClientTokenService _tokenService;
    private readonly IDataService<ApiUser> _dataService;

    public AuthStateProvider(IClientTokenService tokenService, IDataService<ApiUser> dataService)
    {
        _tokenService = tokenService;
        _dataService = dataService;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        try
        {
            var tokenDto = await _tokenService.GetToken();

            var identity = string.IsNullOrEmpty(tokenDto?.AccessToken) || tokenDto?.AccessTokenExpiration < DateTime.Now
            ? new ClaimsIdentity()
            : new ClaimsIdentity(ParseClaimsFromJwt(tokenDto.AccessToken), "jwt");

            return new AuthenticationState(new ClaimsPrincipal(identity));
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw new Exception() ;
        }
    }

    public async Task<LoginResponseDto> LoginUser(LoginDto loginDto)
    {
        try
        {
            var result = await _dataService.Login(loginDto.Server, loginDto.Email, loginDto.Password);
            await _tokenService.SetToken(result.AccessToken);
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