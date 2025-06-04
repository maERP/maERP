using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using maERP.UI.Services;

namespace maERP.UI.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    private readonly IAuthenticationService _authenticationService;

    [ObservableProperty]
    private bool isAuthenticated;

    [ObservableProperty]
    private ViewModelBase? currentView;

    [ObservableProperty]
    private string selectedMenuItem = "Dashboard";

    public LoginViewModel LoginViewModel { get; }
    public DashboardViewModel DashboardViewModel { get; }
    public OrderListViewModel OrderListViewModel { get; }

    public MainViewModel(IAuthenticationService authenticationService,
                        LoginViewModel loginViewModel,
                        DashboardViewModel dashboardViewModel,
                        OrderListViewModel orderListViewModel)
    {
        _authenticationService = authenticationService;
        LoginViewModel = loginViewModel;
        DashboardViewModel = dashboardViewModel;
        OrderListViewModel = orderListViewModel;

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
            CurrentView = DashboardViewModel;
            SelectedMenuItem = "Dashboard";
        }
        else
        {
            IsAuthenticated = _authenticationService.IsAuthenticated;
            CurrentView = IsAuthenticated ? DashboardViewModel : LoginViewModel;
        }
    }

    private void OnLoginSuccessful()
    {
        IsAuthenticated = true;
        CurrentView = DashboardViewModel;
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
    private void NavigateToMenuItem(string menuItem)
    {
        if (!IsAuthenticated) return;

        SelectedMenuItem = menuItem;

        CurrentView = menuItem switch
        {
            "Dashboard" => DashboardViewModel,
            "Orders" => OrderListViewModel,
            _ => DashboardViewModel
        };
    }
}
