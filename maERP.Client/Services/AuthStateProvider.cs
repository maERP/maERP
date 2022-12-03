using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

using maERP.Client.Contracts;
using maERP.Client.Services;
using maERP.Shared.Models;

namespace maERP.Client.Services;

public class AuthStateProvider : AuthenticationStateProvider
{
    IDataService<ApiUser> _dataService;

    public AuthStateProvider(IDataService<ApiUser> dataService)
    {
        _dataService = dataService;
    }

    public async Task Login(string token, string refreshToken)
    {
        try
        {
            // await SecureStorage.Default.SetAsync("oauth_token", token);
            Preferences.Default.Set("oauth_token", token);
            Preferences.Default.Set("oauth_refresh_token", refreshToken);
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    public async Task Logout()
    {
        // SecureStorage.Default.Remove("oauth_token");
        Preferences.Default.Remove("oauth_token");
        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        Console.WriteLine("Call GetAuthenticationStateAsync");
        try
        {
            // var userInfo = await SecureStorage.Default.GetAsync("oauth_token");
            var userInfo = Preferences.Default.Get<string>("oauth_token", null);

            if (userInfo != null)
            {
                var claims = new[] { new Claim(ClaimTypes.Name, "Sample User") };
                var identity = new ClaimsIdentity(claims, "Custom authentication");
                return new AuthenticationState(new ClaimsPrincipal(identity));
            }
        }
        catch (Exception ex)
        {
            // This should be more properly handled
            Console.WriteLine("Request failed:" + ex.ToString());
        }

        return new AuthenticationState(new ClaimsPrincipal());
    }
}