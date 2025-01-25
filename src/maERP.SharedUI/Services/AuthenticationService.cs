using Blazored.LocalStorage;
using maERP.SharedUI.Contracts;
using maERP.SharedUI.Providers;
using maERP.SharedUI.Services.Base;
using Microsoft.AspNetCore.Components.Authorization;

namespace maERP.SharedUI.Services;

public class AuthenticationService : BaseHttpService, IAuthenticationService
{
    private readonly AuthenticationStateProvider _authenticataionStateProvider;

    public AuthenticationService(
        IClient client,
        ILocalStorageService localStorage,
        AuthenticationStateProvider authenticationStateProvider) : base(client, localStorage)
    {
        _authenticataionStateProvider = authenticationStateProvider;
    }

    public async Task<bool> LoginAsync(string email, string password)
    {
        try
        { 
            AuthRequest authenticationRequest = new AuthRequest
            {
                Email = email,
                Password = password
            };

            var authenticationResponse = await Client.LoginAsync(authenticationRequest);

            if(authenticationResponse.Token != string.Empty)
            {
                await Localstorage.SetItemAsync("authToken", authenticationResponse.Token);
                
                await ((ApiAuthenticationStateProvider) _authenticataionStateProvider).LoggedIn();

                return true;
            }

            return false;
        }
        catch(ApiException)
        {
            return false;
        }
    }

    public async Task LogoutAsync()
    {
        await ((ApiAuthenticationStateProvider) _authenticataionStateProvider).LoggedOut();
    }

    public async Task<bool> RegisterAsync(string firstName, string lastName, string username, string email, string password)
    {
        RegistrationRequest registrationRequest = new RegistrationRequest
        {
            Firstname = firstName,
            Lastname = lastName,
            Username = username,
            Email = email,
            Password = password
        };

        var response = await Client.RegisterAsync(registrationRequest);

        if(!string.IsNullOrEmpty(response.UserId))
        {
            return true;
        }

        return false;
    }
}