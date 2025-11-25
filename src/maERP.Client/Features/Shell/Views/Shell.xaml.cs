using maERP.Client.Features.Shell.Models;

namespace maERP.Client.Features.Shell.Views;

public sealed partial class Shell : UserControl, IContentControlProvider
{
    public Shell()
    {
        this.InitializeComponent();

        // Set initial visibility: only Login visible, all other items hidden
        // This ensures correct state before authentication is checked
        SetUnauthenticatedVisibility();

        // Subscribe to static authentication state changed event
        ShellModel.AuthenticationStateChanged += OnAuthenticationStateChanged;

        NavView.SelectionChanged += OnNavigationViewSelectionChanged;
        TabBarNav.SelectionChanged += OnTabBarSelectionChanged;
        this.Loaded += OnShellLoaded;
    }

    private void OnAuthenticationStateChanged(object? sender, bool isAuthenticated)
    {
        Console.WriteLine($"[Shell] OnAuthenticationStateChanged received: {isAuthenticated}");

        // Update visibility based on authentication state
        if (isAuthenticated)
        {
            SetAuthenticatedVisibility();
        }
        else
        {
            SetUnauthenticatedVisibility();
        }
    }

    private void SetAuthenticatedVisibility()
    {
        Console.WriteLine("[Shell] SetAuthenticatedVisibility called");

        // NavigationView menu items - Login hidden, all others visible
        NavItemLogin.Visibility = Visibility.Collapsed;
        NavItemDashboard.Visibility = Visibility.Visible;
        NavSeparator1.Visibility = Visibility.Visible;
        NavHeaderModules.Visibility = Visibility.Visible;
        NavItemCustomers.Visibility = Visibility.Visible;
        NavItemOrders.Visibility = Visibility.Visible;
        NavItemProducts.Visibility = Visibility.Visible;
        NavItemInventory.Visibility = Visibility.Visible;

        // Footer items - visible
        NavItemSettings.Visibility = Visibility.Visible;
        NavItemLogout.Visibility = Visibility.Visible;

        // TabBar items - Login hidden, all others visible
        TabItemLogin.Visibility = Visibility.Collapsed;
        TabItemDashboard.Visibility = Visibility.Visible;
        TabItemCustomers.Visibility = Visibility.Visible;
        TabItemOrders.Visibility = Visibility.Visible;
        TabItemSettings.Visibility = Visibility.Visible;
        TabItemLogout.Visibility = Visibility.Visible;
    }

    private void SetUnauthenticatedVisibility()
    {
        Console.WriteLine("[Shell] SetUnauthenticatedVisibility called");

        // NavigationView menu items - only Login visible
        NavItemLogin.Visibility = Visibility.Visible;
        NavItemDashboard.Visibility = Visibility.Collapsed;
        NavSeparator1.Visibility = Visibility.Collapsed;
        NavHeaderModules.Visibility = Visibility.Collapsed;
        NavItemCustomers.Visibility = Visibility.Collapsed;
        NavItemOrders.Visibility = Visibility.Collapsed;
        NavItemProducts.Visibility = Visibility.Collapsed;
        NavItemInventory.Visibility = Visibility.Collapsed;

        // Footer items - hidden
        NavItemSettings.Visibility = Visibility.Collapsed;
        NavItemLogout.Visibility = Visibility.Collapsed;

        // TabBar items - only Login visible
        TabItemLogin.Visibility = Visibility.Visible;
        TabItemDashboard.Visibility = Visibility.Collapsed;
        TabItemCustomers.Visibility = Visibility.Collapsed;
        TabItemOrders.Visibility = Visibility.Collapsed;
        TabItemSettings.Visibility = Visibility.Collapsed;
        TabItemLogout.Visibility = Visibility.Collapsed;
    }

    public ContentControl ContentControl => Splash;

    private async void OnShellLoaded(object sender, RoutedEventArgs e)
    {
        Console.WriteLine("[Shell] Loaded event fired");

        // Get ShellModel from the service provider and set as DataContext
        try
        {
            var app = Application.Current as App;
            if (app?.Host?.Services != null)
            {
                var shellModel = app.Host.Services.GetRequiredService<ShellModel>();
                Console.WriteLine($"[Shell] Got ShellModel from DI. IsAuthenticated: {shellModel.IsAuthenticated}");

                // IMPORTANT: Set DataContext ONLY on NavView and TabBarNav, NOT on the Shell itself
                // This allows child pages to have their own DataContext set by navigation
                NavView.DataContext = shellModel;
                TabBarNav.DataContext = shellModel;

                // Subscribe to property changes
                shellModel.PropertyChanged += OnShellModelPropertyChanged;

                // Now check authentication state and update visibility
                await shellModel.InitializeAuthenticationState();
                UpdateNavigationVisibility(shellModel);

                Console.WriteLine($"[Shell] After InitializeAuthenticationState. IsAuthenticated: {shellModel.IsAuthenticated}");
            }
            else
            {
                Console.WriteLine("[Shell] FAILED to get Host or Services");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[Shell] Exception getting ShellModel: {ex.Message}");
        }
    }

    private void OnShellModelPropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(ShellModel.IsAuthenticated))
        {
            Console.WriteLine($"[Shell] Property changed: {e.PropertyName}");
            if (sender is ShellModel model)
            {
                Console.WriteLine($"[Shell] IsAuthenticated changed to: {model.IsAuthenticated}");
                UpdateNavigationVisibility(model);
            }
        }
    }

    private void UpdateNavigationVisibility(ShellModel model)
    {
        Console.WriteLine($"[Shell] UpdateNavigationVisibility - IsAuthenticated: {model.IsAuthenticated}");

        var authenticatedVisibility = model.IsAuthenticated ? Visibility.Visible : Visibility.Collapsed;
        var notAuthenticatedVisibility = model.IsAuthenticated ? Visibility.Collapsed : Visibility.Visible;

        Console.WriteLine($"[Shell] Authenticated: {authenticatedVisibility}, NotAuthenticated: {notAuthenticatedVisibility}");

        // Update NavigationView menu items directly by name
        NavItemLogin.Visibility = notAuthenticatedVisibility;
        NavItemDashboard.Visibility = authenticatedVisibility;
        NavSeparator1.Visibility = authenticatedVisibility;
        NavHeaderModules.Visibility = authenticatedVisibility;
        NavItemCustomers.Visibility = authenticatedVisibility;
        NavItemOrders.Visibility = authenticatedVisibility;
        NavItemProducts.Visibility = authenticatedVisibility;
        NavItemInventory.Visibility = authenticatedVisibility;

        // Update footer items
        NavItemSettings.Visibility = authenticatedVisibility;
        NavItemLogout.Visibility = authenticatedVisibility;

        // Update TabBar items
        TabItemLogin.Visibility = notAuthenticatedVisibility;
        TabItemDashboard.Visibility = authenticatedVisibility;
        TabItemCustomers.Visibility = authenticatedVisibility;
        TabItemOrders.Visibility = authenticatedVisibility;
        TabItemSettings.Visibility = authenticatedVisibility;
        TabItemLogout.Visibility = authenticatedVisibility;

        Console.WriteLine($"[Shell] NavItemLogin.Visibility set to: {NavItemLogin.Visibility}");
        Console.WriteLine($"[Shell] NavItemDashboard.Visibility set to: {NavItemDashboard.Visibility}");
    }

    private async Task RefreshAuthenticationState(ShellModel model)
    {
        // Wait a bit for navigation to complete
        await Task.Delay(100);
        await model.InitializeAuthenticationState();
    }

    private async void OnNavigationViewSelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
    {
        if (args.SelectedItem is NavigationViewItem item && item.Tag is string tag)
        {
            if (NavView.DataContext is ShellModel model)
            {
                await model.NavigateToPage(tag);
                await RefreshAuthenticationState(model);
            }
        }
    }

    private async void OnTabBarSelectionChanged(object? sender, TabBarSelectionChangedEventArgs args)
    {
        if (args.NewItem is TabBarItem item && item.Tag is string tag)
        {
            if (NavView.DataContext is ShellModel model)
            {
                await model.NavigateToPage(tag);
                await RefreshAuthenticationState(model);
            }
        }
    }
}
