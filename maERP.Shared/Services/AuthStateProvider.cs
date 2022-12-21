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
    private readonly ITokenService tokenService;

    public AuthStateProvider()
    {

    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var tokenDTO = await tokenService.GetToken();
        var identity = string.IsNullOrEmpty(tokenDTO?.Token) || tokenDTO?.Expiration < DateTime.Now
            ? new ClaimsIdentity()
            : new ClaimsIdentity(ParseClaimsFromJwt(tokenDTO.Token), "jwt");
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
        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }
}