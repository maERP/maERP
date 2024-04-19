﻿using Microsoft.AspNetCore.Components;
using MudBlazor;
using maERP.SharedUI;
using maERP.SharedUI.Contracts;
using Blazored.LocalStorage;
using System.Text.Json;
using maERP.SharedUI.Models;

namespace maERP.SharedUI.Pages.Auth;

public partial class Login
{
    [Inject]
    private NavigationManager? _navigationManager { get; set; }

    [Inject]
    private ILocalStorageService? _localStorage { get; set; }

    [Inject]
    private IAuthenticationService? _authenticationService { get; set; }

    [Inject]
    public ISnackbar? _snackbar { get; set; }

    private bool _showServerOverlay;
    private string newServer = string.Empty;

    MudForm? _form;
    bool _success;
    bool _loading;

    private List<LoginServer> _serverList = new();
    private readonly LoginFormModel _model = new();

    private string _spinnerClass = string.Empty;
    private string _errorMessage = string.Empty;

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();

        if (await _localStorage!.ContainKeyAsync("serverList"))
        {
            try
            {
                string serverJson = await _localStorage.GetItemAsStringAsync("serverList");
                _serverList = JsonSerializer.Deserialize<List<LoginServer>>(serverJson)!;
            }
            catch (JsonException)
            {
                await _localStorage.RemoveItemAsync("serverList");
            }
        }

        SelectFirstServerFromList();

        if (await _localStorage.ContainKeyAsync("email"))
        {
            _model.Email = await _localStorage.GetItemAsStringAsync("email");
        }
    }

    public void OpenServerOverlay()
    {
        if (_showServerOverlay)
        {
            _showServerOverlay = false;
            SelectFirstServerFromList();
        }
        else
        {
            _showServerOverlay = true;
        }

        StateHasChanged();
    }

    void RemoveFromServerList(string s)
    {
        _serverList = _serverList.Where(u => u.Url != s.ToString()).ToList();
    }

    void AddToServerList()
    {
        var newServerItem = new LoginServer
        {
            Url = newServer,
            LastUsed = DateTime.MinValue,
            Version = string.Empty
        };

        _serverList.Add(newServerItem);
        newServer = string.Empty;
    }

    void SelectFirstServerFromList()
    {
        if (_serverList.Count > 0)
        {
            _model.Server = _serverList.FirstOrDefault()!.Url;
        }
    }

    private async void OnSubmit()
    {
        _spinnerClass = "spinner-border spinner-border-sm";

        await _localStorage!.SetItemAsStringAsync("server", _model.Server);

        var loginResponse = await _authenticationService!.LoginAsync(_model.Email, _model.Password); // TODO add _model.RememberMe

        if (loginResponse == true)
        {
            if (_model.RememberMe == true)
            {
                await _localStorage.SetItemAsStringAsync("email", _model.Email);
                await _localStorage.SetItemAsStringAsync("password", _model.Password);
            }

            string serverJson = JsonSerializer.Serialize(_serverList);
            await _localStorage.SetItemAsStringAsync("serverList", serverJson);

            _navigationManager!.NavigateTo("/");
            return;
        }

        _errorMessage = "Login fehlgeschlagen";
        _spinnerClass = "";

        _snackbar!.Add(_errorMessage, Severity.Error);

        await _localStorage.RemoveItemAsync("server");

        this.StateHasChanged();
    }
}