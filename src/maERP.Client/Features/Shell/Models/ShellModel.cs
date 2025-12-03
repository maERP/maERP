using System.ComponentModel;
using maERP.Client.Core.Constants;
using maERP.Client.Features.Auth.Models;
using maERP.Client.Features.Auth.Services;
using maERP.Client.Features.Customers.Models;
using maERP.Client.Features.Dashboard.Models;
using maERP.Domain.Dtos.Tenant;
using Microsoft.UI.Xaml;

namespace maERP.Client.Features.Shell.Models;

public partial class ShellModel : INotifyPropertyChanged
{
    private readonly INavigator _navigator;
    private readonly IAuthenticationService _authentication;
    private readonly ITenantContextService _tenantContext;
    private bool _isAuthenticated = false; // Default to false

    public event PropertyChangedEventHandler? PropertyChanged;

    // Static event for authentication state changes - allows Shell to subscribe without DI
    public static event EventHandler<bool>? AuthenticationStateChanged;

    // Static event for tenant state changes - allows Shell to subscribe without DI
    public static event EventHandler<TenantListDto?>? TenantStateChanged;

    public ShellModel(
        IAuthenticationService authentication,
        INavigator navigator,
        ITenantContextService tenantContext)
    {
        _navigator = navigator;
        _authentication = authentication;
        _tenantContext = tenantContext;
        _authentication.LoggedOut += LoggedOut;
        _tenantContext.CurrentTenantChanged += OnCurrentTenantChanged;

        // Initialize authentication state asynchronously
        _ = InitializeAuthenticationState();
    }

    private void OnCurrentTenantChanged(object? sender, TenantListDto? tenant)
    {
        Console.WriteLine($"[ShellModel] OnCurrentTenantChanged: {tenant?.Name ?? "null"}");
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(AvailableTenants)));
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CurrentTenant)));
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(HasMultipleTenants)));
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(HasTenants)));
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CurrentTenantDisplayName)));
        TenantStateChanged?.Invoke(this, tenant);
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

    // Tenant properties
    public IReadOnlyList<TenantListDto> AvailableTenants => _tenantContext.AvailableTenants;
    public TenantListDto? CurrentTenant => _tenantContext.CurrentTenant;
    public bool HasMultipleTenants => AvailableTenants.Count > 1;
    public bool HasTenants => AvailableTenants.Count > 0;
    public string CurrentTenantDisplayName => CurrentTenant?.Name ?? "maERP";

    public async Task SwitchTenantAsync(Guid tenantId)
    {
        Console.WriteLine($"[ShellModel] SwitchTenantAsync called with tenantId: {tenantId}");
        await _tenantContext.SetCurrentTenantAsync(tenantId);

        // Navigate to Dashboard to reload UI with new tenant context
        Console.WriteLine("[ShellModel] Navigating to Dashboard after tenant switch");
        await _navigator.NavigateViewModelAsync<DashboardModel>(this, qualifier: Qualifiers.ClearBackStack);
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
        await _tenantContext.ClearAsync();
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
