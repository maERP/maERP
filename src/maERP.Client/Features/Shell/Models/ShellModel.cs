using System.ComponentModel;
using maERP.Client.Core.Constants;
using maERP.Client.Features.Auth.Models;
using maERP.Client.Features.Customers.Models;
using maERP.Client.Features.Dashboard.Models;
using Microsoft.UI.Xaml;

namespace maERP.Client.Features.Shell.Models;

public partial class ShellModel : INotifyPropertyChanged
{
    private readonly INavigator _navigator;
    private readonly IAuthenticationService _authentication;
    private bool _isAuthenticated = false; // Default to false

    public event PropertyChangedEventHandler? PropertyChanged;

    // Static event for authentication state changes - allows Shell to subscribe without DI
    public static event EventHandler<bool>? AuthenticationStateChanged;

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
            return _isAuthenticated;
        }
        private set
        {
            if (_isAuthenticated != value)
            {
                Console.WriteLine($"[ShellModel] IsAuthenticated changing from {_isAuthenticated} to {value}");
                _isAuthenticated = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsAuthenticated)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsNotAuthenticated)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(AuthenticatedVisibility)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(NotAuthenticatedVisibility)));

                // Raise static event for Shell to update navigation
                Console.WriteLine($"[ShellModel] Raising AuthenticationStateChanged event with value: {value}");
                AuthenticationStateChanged?.Invoke(this, value);
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
        await _navigator.NavigateRouteAsync(this, Routes.Login, qualifier: Qualifiers.ClearBackStack);
    }

    public async ValueTask NavigateToPage(string tag)
    {
        Console.WriteLine($"[ShellModel] NavigateToPage called with tag: '{tag}'");

        switch (tag)
        {
            case "Login":
                Console.WriteLine("[ShellModel] Navigating to LoginModel");
                await _navigator.NavigateViewModelAsync<LoginModel>(this);
                break;
            case "Main":
            case "Dashboard":
                Console.WriteLine("[ShellModel] Navigating to DashboardModel");
                await _navigator.NavigateViewModelAsync<DashboardModel>(this);
                break;
            case "Customers":
                Console.WriteLine("[ShellModel] Navigating to CustomerListModel");
                try
                {
                    var result = await _navigator.NavigateViewModelAsync<CustomerListModel>(this);
                    Console.WriteLine($"[ShellModel] Navigation to CustomerListModel result: {result?.Success}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[ShellModel] Navigation to CustomerListModel FAILED: {ex.Message}");
                }
                break;
            case "Orders":
                // TODO: Add Orders page when created
                // await _navigator.NavigateViewModelAsync<OrderListModel>(this);
                break;
            case "Products":
                // TODO: Add Products page when created
                // await _navigator.NavigateViewModelAsync<ProductListModel>(this);
                break;
            case "Inventory":
                // TODO: Add Inventory page when created
                // await _navigator.NavigateViewModelAsync<WarehouseListModel>(this);
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
