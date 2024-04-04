using System.IdentityModel.Tokens.Jwt;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace maERP.Shared.Providers;

public class ApiAuthenticationStateProvider : AuthenticationStateProvider
{
    private readonly ILocalStorageService _localStorage;
    private readonly JwtSecurityTokenHandler _jwtSecurityTokenHandler;
    
    public ApiAuthenticationStateProvider(ILocalStorageService localStorage)
    {
        this._localStorage = localStorage;
        _jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
    }
    
    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var user = new ClaimsPrincipal(new ClaimsIdentity()); // create empty user
        var isTokenPresent = await _localStorage.ContainKeyAsync("token");
        if (isTokenPresent == false)
        {
            return new AuthenticationState(user);
        }
        
        var savedToken = await _localStorage.GetItemAsync<string>("token");
        var tokenContent = _jwtSecurityTokenHandler.ReadJwtToken(savedToken);
        
        if(tokenContent.ValidTo < DateTime.Now)
        {
            await _localStorage.RemoveItemAsync("token");
            return new AuthenticationState(user);
        }

        var claims = await GetClaims();
        
        user = new ClaimsPrincipal(new ClaimsIdentity(claims, "jwt"));

        return new AuthenticationState(user);
    }

    public async Task LoggedIn()
    {
        var claims = await GetClaims();
        var user = new ClaimsPrincipal(new ClaimsIdentity(claims, "jwt"));
        var authState = Task.FromResult(new AuthenticationState(user));
        NotifyAuthenticationStateChanged(authState);
    }
    
    public async Task LoggedOut()
    {
        await _localStorage.RemoveItemAsync("token");
        var nobody = new ClaimsPrincipal(new ClaimsIdentity());
        var authState = Task.FromResult(new AuthenticationState(nobody));
        NotifyAuthenticationStateChanged(authState);
    }

    private async Task<List<Claim>> GetClaims()
    {
        var savedToken = await _localStorage.GetItemAsync<string>("token");
        var tokenContent = _jwtSecurityTokenHandler.ReadJwtToken(savedToken);
        var claims = tokenContent.Claims.ToList();
        claims.Add(new Claim(ClaimTypes.Name, tokenContent.Subject));
        return claims;
    }
}