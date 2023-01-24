#nullable disable

using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Logging;
using maERP.Shared.Contracts;
using maERP.Shared.Services;
using maERP.Shared.Models;
using maERP.Shared.Dtos.User;
using System.Text.Json;
// using static System.Net.WebRequestMethods;

namespace maERP.Shared.Services;

public class AuthStateProvider : AuthenticationStateProvider
{
    private readonly ITokenService _tokenService;
    private readonly AuthHttpProvider _authHttpProvider;

    public AuthStateProvider(ITokenService tokenService, AuthHttpProvider authHttpProvider)
    {
        _tokenService = tokenService;
        _authHttpProvider = authHttpProvider;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        Console.WriteLine("Call GetAuthenticationStateAsync");
        var tokenDto = await _tokenService.GetToken();

        ClaimsIdentity identity = new();

        // chef if token is expired
        if (!string.IsNullOrEmpty(tokenDto?.AccessToken) && tokenDto?.AccessTokenExpiration >= DateTime.Now)
        {
            var loginResponseDto = await _authHttpProvider.RefreshToken();
            new ClaimsIdentity(ParseClaimsFromJwt(loginResponseDto.Token.AccessToken), "jwt");
            StateChanged();
        }

        /* old code
        var identity = string.IsNullOrEmpty(tokenDto?.AccessToken) || tokenDto?.AccessTokenExpiration < DateTime.Now
            ? new ClaimsIdentity()
            : new ClaimsIdentity(ParseClaimsFromJwt(tokenDto.AccessToken), "jwt");
        */

        return new AuthenticationState(new ClaimsPrincipal(identity));
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