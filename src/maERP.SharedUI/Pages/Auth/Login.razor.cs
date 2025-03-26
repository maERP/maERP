using System.Text.Json;
using System.Diagnostics;
using Blazored.LocalStorage;
using maERP.SharedUI.Contracts;
using maERP.SharedUI.Models;
using maERP.SharedUI.Providers;
using maERP.SharedUI.Services;
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
    
    [Inject]
    public required ServerUrlProvider ServerUrlProvider { get; set; }
    
    [Inject]
    public required IServerUrlService ServerUrlService { get; set; }

    private bool _showServerOverlay;
    private string _newServer = string.Empty;

    // ReSharper disable once NotAccessedField.Local
    private MudForm? _form;
    private bool _success;
    private readonly bool _isLoading = false;

    private List<LoginServer> _serverList = new();
    private readonly LoginFormModel _model = new();

    private string _errorMessage = string.Empty;

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();

        // Initialize the server URL service
        await ServerUrlService.InitializeAsync();

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

        // Check if debugger is attached (running from IDE)
        if (Debugger.IsAttached)
        {
            // Auto-populate admin login when in debug mode
            _model.Email = "admin@localhost.com";
            _model.Password = "P@ssword1";
            _model.Server = "https://localhost:8443";
            _model.RememberMe = true;
        }
        else if (await LocalStorage.ContainKeyAsync("email"))
        {
            _model.Email = await LocalStorage.GetItemAsStringAsync("email") ?? throw new Exception();
            _model.RememberMe = await LocalStorage.GetItemAsync<bool>("remember_me");
        }
        
        // If server URL is already loaded, use it in the form
        var serverUrl = ServerUrlProvider.ServerUrl;
        if (serverUrl != null && !string.IsNullOrEmpty(serverUrl.ToString()))
        {
            _model.Server = serverUrl.ToString();
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
        // Set the server URL directly in the provider first, which will immediately update HttpClient
        ServerUrlProvider.SetServerUrl(_model.Server);
        
        // Then update the service which handles persistence to local storage
        await ServerUrlService.SetServerUrlAsync(_model.Server);
        
        // Store in local storage (but the primary source will now be ServerUrlService)
        await LocalStorage!.SetItemAsStringAsync("server", _model.Server);

        var loginResponse = await HttpService.LoginAsync(_model.Email, _model.Password, _model.RememberMe);

        if (loginResponse)
        {
            if (_model.RememberMe)
            {
                await LocalStorage.SetItemAsStringAsync("email", _model.Email);
                await LocalStorage.SetItemAsStringAsync("password", _model.Password);
            }

            // Update the LastUsed property for the selected server
            var selectedServer = _serverList.FirstOrDefault(s => s.Url == _model.Server);
            if (selectedServer != null)
            {
                selectedServer.LastUsed = DateTime.Now;
            }
            else
            {
                // If the server is not in the list, add it
                _serverList.Add(new LoginServer 
                { 
                    Url = _model.Server, 
                    LastUsed = DateTime.Now,
                    Version = string.Empty
                });
            }

            string serverJson = JsonSerializer.Serialize(_serverList);
            await LocalStorage.SetItemAsStringAsync("serverList", serverJson);

            NavigationManager!.NavigateTo("/");
            return;
        }

        _errorMessage = "Login fehlgeschlagen";

        Snackbar.Add(_errorMessage, Severity.Error);

        await LocalStorage.RemoveItemAsync("server");

        StateHasChanged();
    }
}