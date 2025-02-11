using System.Text.Json;
using Blazored.LocalStorage;
using maERP.SharedUI.Contracts;
using maERP.SharedUI.Models;
using maERP.SharedUI.Providers;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace maERP.SharedUI.Pages.Auth;

public partial class Login
{
    [Inject]
    private NavigationManager? NavigationManager { get; set; }

    [Inject]
    private ILocalStorageService? LocalStorage { get; set; }

    [Inject]
    public required IHttpService HttpService { get; set; }

    [Inject]
    public required ISnackbar Snackbar { get; set; }

    [Inject]
    public required ApiAuthenticationStateProvider ApiAuthenticationStateProvider { get; set; }

    private bool _showServerOverlay;
    private string _newServer = string.Empty;

    // ReSharper disable once NotAccessedField.Local
    MudForm? _form;
    // ReSharper disable once RedundantDefaultMemberInitializer
    bool _success = false;
    bool _loading = false;

    private List<LoginServer> _serverList = new();
    private readonly LoginFormModel _model = new();

    private string _errorMessage = string.Empty;

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();

        if (await LocalStorage!.ContainKeyAsync("serverList"))
        {
            try
            {
                string serverJson = await LocalStorage.GetItemAsStringAsync("serverList") ?? throw new Exception();
                _serverList = JsonSerializer.Deserialize<List<LoginServer>>(serverJson)!;
            }
            catch (JsonException)
            {
                await LocalStorage.RemoveItemAsync("serverList");
            }
        }

        SelectFirstServerFromList();

        if (await LocalStorage.ContainKeyAsync("email"))
        {
            _model.Email = await LocalStorage.GetItemAsStringAsync("email") ?? throw new Exception();
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
        _serverList = _serverList.Where(u => u.Url != s).ToList();
    }

    void AddToServerList()
    {
        var newServerItem = new LoginServer
        {
            Url = _newServer,
            LastUsed = DateTime.MinValue,
            Version = string.Empty
        };

        _serverList.Add(newServerItem);
        _newServer = string.Empty;
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
        await LocalStorage!.SetItemAsStringAsync("server", _model.Server);

        var loginResponse = await HttpService.LoginAsync(_model.Email, _model.Password, _model.RememberMe);

        if (loginResponse)
        {
            if (_model.RememberMe)
            {
                await LocalStorage.SetItemAsStringAsync("email", _model.Email);
                await LocalStorage.SetItemAsStringAsync("password", _model.Password);
            }

            string serverJson = JsonSerializer.Serialize(_serverList);
            await LocalStorage.SetItemAsStringAsync("serverList", serverJson);

            NavigationManager!.NavigateTo("/");
            return;
        }

        _errorMessage = "Login fehlgeschlagen";

        Snackbar!.Add(_errorMessage, Severity.Error);

        await LocalStorage.RemoveItemAsync("server");

        StateHasChanged();
    }
}