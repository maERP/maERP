using System;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using maERP.UI.Services;
using Microsoft.Extensions.DependencyInjection;

namespace maERP.UI.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    private readonly IAuthenticationService _authenticationService;
    private readonly IServiceProvider _serviceProvider;

    [ObservableProperty]
    private bool isAuthenticated;

    [ObservableProperty]
    private ViewModelBase? currentView;

    [ObservableProperty]
    private string selectedMenuItem = "Dashboard";

    public LoginViewModel LoginViewModel { get; }
    
    private DashboardViewModel? _dashboardViewModel;
    private OrderListViewModel? _orderListViewModel;

    public MainViewModel(IAuthenticationService authenticationService,
                        LoginViewModel loginViewModel,
                        IServiceProvider serviceProvider)
    {
        _authenticationService = authenticationService;
        _serviceProvider = serviceProvider;
        LoginViewModel = loginViewModel;

        LoginViewModel.OnLoginSuccessful += OnLoginSuccessful;

        _ = InitializeAsync();
    }

    private async Task InitializeAsync()
    {
        // Versuche Auto-Login wenn gespeicherte Credentials vorhanden sind
        var autoLoginSuccessful = await LoginViewModel.TryAutoLoginAsync();

        if (autoLoginSuccessful)
        {
            IsAuthenticated = true;
            CurrentView = await GetDashboardViewModelAsync();
            SelectedMenuItem = "Dashboard";
        }
        else
        {
            IsAuthenticated = _authenticationService.IsAuthenticated;
            CurrentView = IsAuthenticated ? await GetDashboardViewModelAsync() : LoginViewModel;
        }
    }

    private async void OnLoginSuccessful()
    {
        IsAuthenticated = true;
        CurrentView = await GetDashboardViewModelAsync();
        SelectedMenuItem = "Dashboard";
    }

    [RelayCommand]
    private async Task LogoutAsync()
    {
        await _authenticationService.LogoutAsync();
        IsAuthenticated = false;
        CurrentView = LoginViewModel;
        SelectedMenuItem = "";
    }

    [RelayCommand]
    private async Task NavigateToMenuItem(string menuItem)
    {
        if (!IsAuthenticated) return;

        SelectedMenuItem = menuItem;

        CurrentView = menuItem switch
        {
            "Dashboard" => await GetDashboardViewModelAsync(),
            "Orders" => await GetOrderListViewModelAsync(),
            _ => await GetDashboardViewModelAsync()
        };
    }

    private Task<DashboardViewModel> GetDashboardViewModelAsync()
    {
        if (_dashboardViewModel == null)
        {
            _dashboardViewModel = _serviceProvider.GetRequiredService<DashboardViewModel>();
        }
        return Task.FromResult(_dashboardViewModel);
    }

    private async Task<OrderListViewModel> GetOrderListViewModelAsync()
    {
        if (_orderListViewModel == null)
        {
            _orderListViewModel = _serviceProvider.GetRequiredService<OrderListViewModel>();
            await _orderListViewModel.InitializeAsync();
        }
        return _orderListViewModel;
    }
}
