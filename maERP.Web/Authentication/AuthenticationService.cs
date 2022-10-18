#nullable disable

using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Components.Authorization;
using Blazored.LocalStorage;
using maERP.Web.Models;

namespace maERP.Web.Authentication;

public class AuthenticationService : IAuthenticationService
{
    private readonly HttpClient _client;
    private readonly AuthenticationStateProvider _authStateProvider;
    private readonly ILocalStorageService _localStorage;

    public AuthenticationService(HttpClient client,
                                 AuthenticationStateProvider authStateProvider,
                                 ILocalStorageService localStorage)
    {
        _client = client;
        _authStateProvider = authStateProvider;
        _localStorage = localStorage;
    }

    public async Task<AuthenticatedUserModel> Login(AuthenticationUserModel userForAuthentication)
    {
        var data = new FormUrlEncodedContent(new[]
        {
            new KeyValuePair<string, string>("grant_type", "password"),
            new KeyValuePair<string, string>("username", userForAuthentication.Email),
            new KeyValuePair<string, string>("password", userForAuthentication.Password)
        });

        var authResult = await _client.PostAsync("https://erp.martin-andrich.de/api/Login", data);
        var authContent = await authResult.Content.ReadAsStringAsync();

        if (!authResult.IsSuccessStatusCode)
        {
            return null;
        }

        var result = JsonSerializer.Deserialize<AuthenticatedUserModel>(
            authContent,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        await _localStorage.SetItemAsStringAsync("authToken", result.Access_Token);

        ((AuthStateProvider)_authStateProvider).NotifyUserAuthentication(result.Access_Token);

        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", result.Access_Token);

        return result;
    }

    public async Task Logout()
    {
        await _localStorage.RemoveItemAsync("authToken");
        ((AuthStateProvider)_authStateProvider).NotifyUserLogout();
        _client.DefaultRequestHeaders.Authorization = null;
    }
}