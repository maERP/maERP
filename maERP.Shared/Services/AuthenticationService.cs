using Blazored.LocalStorage;
using maERP.Shared.Contracts;
using maERP.Shared.Models.Identity;
using maERP.Shared.Providers;
using Microsoft.AspNetCore.Components.Authorization;

namespace maERP.Shared.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly AuthenticationStateProvider _authenticationStateProvider;
    private readonly ILocalStorageService _localStorage;
    private readonly IApiDataProvider<object> _dataService;
    
    public AuthenticationService(AuthenticationStateProvider authenticationStateProvider, ILocalStorageService localStorage, IApiDataProvider<object> dataService) 
    {
        this._authenticationStateProvider = authenticationStateProvider;
        this._localStorage = localStorage;
        this._dataService = dataService;
    }

    public async Task<bool> AuthenticateAsync(string email, string password)
    {
        try
        {
            AuthRequest authenticationRequest = new AuthRequest()
            {
                Email = email,
                Password = password
            };

            var authenticationResponse = await _dataService.Login(authenticationRequest);
        
            if(authenticationResponse.Token != string.Empty)
            {
                await _localStorage.SetItemAsync("token", authenticationResponse.Token);
                
                await ((ApiAuthenticationStateProvider) _authenticationStateProvider).LoggedIn();

                return true;
            }

            return false;
        }
        catch (Exception)
        {
            return false;
        }
    }
    
    public async Task LogoutAsync()
    {
        await _localStorage.RemoveItemAsync("token");
        await ((ApiAuthenticationStateProvider) _authenticationStateProvider).LoggedOut();
    }
    
    public async Task<bool> RegisterAsync(string firstName, string lastName, string email, string password)
    {
        RegistrationRequest registrationRequest = new RegistrationRequest()
        {
            FirstName = firstName,
            LastName = lastName,
            Email = email,
            Password = password
        };

        var response = await _dataService.RegisterAsync(registrationRequest);

        if (!string.IsNullOrEmpty(response.UserId))
        {
            return true;
        }

        return false;
    }
}