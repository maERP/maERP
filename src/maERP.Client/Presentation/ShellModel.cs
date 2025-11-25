using System.ComponentModel;
using Microsoft.UI.Xaml;

namespace maERP.Client.Presentation;

public partial class ShellModel : INotifyPropertyChanged
{
    private readonly INavigator _navigator;
    private readonly IAuthenticationService _authentication;
    private bool _isAuthenticated = false; // Default to false

    public event PropertyChangedEventHandler? PropertyChanged;

    public ShellModel(
        IAuthenticationService authentication,
        INavigator navigator)
    {
        _navigator = navigator;
        _authentication = authentication;
        _authentication.LoggedOut += LoggedOut;

        // Initialize authentication state asynchronously
        _ = InitializeAuthenticationState();
    }

    public bool IsAuthenticated
    {
        get
        {
            System.Diagnostics.Debug.WriteLine($"[ShellModel] IsAuthenticated GET returning: {_isAuthenticated}");
            return _isAuthenticated;
        }
        private set
        {
            if (_isAuthenticated != value)
            {
                System.Diagnostics.Debug.WriteLine($"[ShellModel] IsAuthenticated changing from {_isAuthenticated} to {value}");
                _isAuthenticated = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsAuthenticated)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsNotAuthenticated)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(AuthenticatedVisibility)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(NotAuthenticatedVisibility)));
            }
        }
    }

    public bool IsNotAuthenticated
    {
        get
        {
            System.Diagnostics.Debug.WriteLine($"[ShellModel] IsNotAuthenticated GET returning: {!_isAuthenticated}");
            return !_isAuthenticated;
        }
    }

    public Visibility AuthenticatedVisibility
    {
        get
        {
            var visibility = IsAuthenticated ? Visibility.Visible : Visibility.Collapsed;
            System.Diagnostics.Debug.WriteLine($"[ShellModel] AuthenticatedVisibility GET returning: {visibility}");
            return visibility;
        }
    }

    public Visibility NotAuthenticatedVisibility
    {
        get
        {
            var visibility = IsNotAuthenticated ? Visibility.Visible : Visibility.Collapsed;
            System.Diagnostics.Debug.WriteLine($"[ShellModel] NotAuthenticatedVisibility GET returning: {visibility}");
            return visibility;
        }
    }

    public async Task InitializeAuthenticationState()
    {
        System.Diagnostics.Debug.WriteLine("[ShellModel] InitializeAuthenticationState called");
        var authenticated = await _authentication.RefreshAsync();
        System.Diagnostics.Debug.WriteLine($"[ShellModel] RefreshAsync returned: {authenticated}");
        IsAuthenticated = authenticated;
        System.Diagnostics.Debug.WriteLine($"[ShellModel] IsAuthenticated set to: {IsAuthenticated}");
    }

    private async void LoggedOut(object? sender, EventArgs e)
    {
        IsAuthenticated = false;
        await _navigator.NavigateViewModelAsync<LoginModel>(this, qualifier: Qualifiers.ClearBackStack);
    }

    public async ValueTask NavigateToPage(string tag)
    {
        switch (tag)
        {
            case "Login":
                await _navigator.NavigateViewModelAsync<LoginModel>(this);
                break;
            case "Main":
                await _navigator.NavigateViewModelAsync<MainModel>(this);
                break;
            case "Second":
                await _navigator.NavigateViewModelAsync<SecondModel>(this);
                break;
            case "Inventory":
                // TODO: Add Inventory page when created
                // await _navigator.NavigateViewModelAsync<InventoryModel>(this);
                break;
            case "Reports":
                // TODO: Add Reports page when created
                // await _navigator.NavigateViewModelAsync<ReportsModel>(this);
                break;
            case "Settings":
                // TODO: Add Settings page when created
                // await _navigator.NavigateViewModelAsync<SettingsModel>(this);
                break;
            case "Logout":
                await _authentication.LogoutAsync(CancellationToken.None);
                break;
        }
    }

    public void UpdateAuthenticationState(bool isAuthenticated)
    {
        IsAuthenticated = isAuthenticated;
    }
}
