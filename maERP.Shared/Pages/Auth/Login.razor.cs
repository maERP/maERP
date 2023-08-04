using maERP.Shared.Models;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using maERP.Shared.Contracts;
using Blazored.LocalStorage;
using System.Text.Json;

namespace maERP.Shared.Pages.Auth;

public partial class Login
{
    [Inject]
    private NavigationManager? NavManager { get; set; }

    [Inject]
    private ILocalStorageService? LocalStorage { get; set; }

    [Inject]
    private IAuthenticationService? AuthenticationService { get; set; }

    [Inject]
    public ISnackbar? Snackbar { get; set; }

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

        if(await LocalStorage!.ContainKeyAsync("serverList"))
        {
            try
            {
                string serverJson = await LocalStorage.GetItemAsStringAsync("serverList");
                _serverList = JsonSerializer.Deserialize<List<LoginServer>>(serverJson)!;
            }
            catch(JsonException)
            {
                await LocalStorage.RemoveItemAsync("serverList");
            }            
        }

        SelectFirstServerFromList();

        if(await LocalStorage.ContainKeyAsync("email"))
        {
            _model.Email = await LocalStorage.GetItemAsStringAsync("email");
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

        await LocalStorage!.SetItemAsStringAsync("server", _model.Server);

        var loginResponse = await AuthenticationService!.AuthenticateAsync(_model.Email, _model.Password, _model.RememberMe);

        if (loginResponse == true)
        {
            if(_model.RememberMe == true)
            {
                await LocalStorage.SetItemAsStringAsync("email", _model.Email);
                await LocalStorage.SetItemAsStringAsync("password", _model.Password);
            }

            string serverJson = JsonSerializer.Serialize(_serverList);
            await LocalStorage.SetItemAsStringAsync("serverList", serverJson);

            NavManager!.NavigateTo("/");
            return;
        }

        _errorMessage = "Login fehlgeschlagen";
        _spinnerClass = "";

        Snackbar!.Add(_errorMessage, Severity.Error);

        await LocalStorage.RemoveItemAsync("server");

        this.StateHasChanged();
    }
}