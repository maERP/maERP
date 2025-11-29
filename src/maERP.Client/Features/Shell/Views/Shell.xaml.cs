using maERP.Client.Features.Shell.Models;
using maERP.Client.Features.Auth.Models;
using maERP.Client.Features.Dashboard.Models;
using maERP.Client.Features.Customers.Models;
using maERP.Client.Features.Invoices.Models;
using maERP.Client.Features.Manufacturers.Models;
using maERP.Client.Features.Orders.Models;
using maERP.Client.Features.Products.Models;
using maERP.Client.Features.SalesChannels.Models;
using maERP.Client.Features.TaxClasses.Models;
using maERP.Client.Features.Warehouses.Models;
using maERP.Client.Features.AiModels.Models;
using maERP.Client.Features.AiPrompts.Models;
using Uno.Toolkit.UI;

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

        // Note: NavView.SelectionChanged is now wired in XAML
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
        NavItemCustomers.Visibility = Visibility.Visible;
        NavItemOrders.Visibility = Visibility.Visible;
        NavItemInvoices.Visibility = Visibility.Visible;
        NavItemProducts.Visibility = Visibility.Visible;
        NavItemManufacturers.Visibility = Visibility.Visible;
        NavItemWarehouses.Visibility = Visibility.Visible;
        NavItemSalesChannels.Visibility = Visibility.Visible;
        NavSeparator2.Visibility = Visibility.Visible;
        NavItemAiModels.Visibility = Visibility.Visible;
        NavItemAiPrompts.Visibility = Visibility.Visible;
        NavSeparator3.Visibility = Visibility.Visible;
        NavItemTaxClasses.Visibility = Visibility.Visible;

        // Header User Menu - visible when authenticated
        UserMenuPanel.Visibility = Visibility.Visible;

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
        NavItemCustomers.Visibility = Visibility.Collapsed;
        NavItemOrders.Visibility = Visibility.Collapsed;
        NavItemInvoices.Visibility = Visibility.Collapsed;
        NavItemProducts.Visibility = Visibility.Collapsed;
        NavItemManufacturers.Visibility = Visibility.Collapsed;
        NavItemWarehouses.Visibility = Visibility.Collapsed;
        NavItemSalesChannels.Visibility = Visibility.Collapsed;
        NavSeparator2.Visibility = Visibility.Collapsed;
        NavItemAiModels.Visibility = Visibility.Collapsed;
        NavItemAiPrompts.Visibility = Visibility.Collapsed;
        NavSeparator3.Visibility = Visibility.Collapsed;
        NavItemTaxClasses.Visibility = Visibility.Collapsed;

        // Header User Menu - hidden when not authenticated
        UserMenuPanel.Visibility = Visibility.Collapsed;

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
        // Retry mechanism because Host may not be available immediately
        const int maxRetries = 10;
        const int retryDelayMs = 100;

        for (int retry = 0; retry < maxRetries; retry++)
        {
            try
            {
                var app = Application.Current as App;
                if (app?.Host?.Services != null)
                {
                    var shellModel = app.Host.Services.GetRequiredService<ShellModel>();
                    Console.WriteLine($"[Shell] Got ShellModel from DI (attempt {retry + 1}). IsAuthenticated: {shellModel.IsAuthenticated}");

                    // IMPORTANT: Set DataContext ONLY on NavView and TabBarNav, NOT on the Shell itself
                    // This allows child pages to have their own DataContext set by navigation
                    NavView.DataContext = shellModel;
                    TabBarNav.DataContext = shellModel;

                    // Subscribe to property changes
                    shellModel.PropertyChanged += OnShellModelPropertyChanged;

                    // Now check authentication state and update visibility
                    await shellModel.InitializeAuthenticationState();
                    UpdateNavigationVisibility(shellModel);

                    // Initialize dark mode toggle state
                    InitializeDarkModeToggle();

                    Console.WriteLine($"[Shell] After InitializeAuthenticationState. IsAuthenticated: {shellModel.IsAuthenticated}");
                    return; // Success, exit the retry loop
                }
                else
                {
                    Console.WriteLine($"[Shell] Host or Services not available yet (attempt {retry + 1}/{maxRetries})");
                    await Task.Delay(retryDelayMs);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Shell] Exception getting ShellModel (attempt {retry + 1}): {ex.Message}");
                await Task.Delay(retryDelayMs);
            }
        }

        Console.WriteLine("[Shell] FAILED to get Host or Services after all retries");
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
        NavItemCustomers.Visibility = authenticatedVisibility;
        NavItemOrders.Visibility = authenticatedVisibility;
        NavItemInvoices.Visibility = authenticatedVisibility;
        NavItemProducts.Visibility = authenticatedVisibility;
        NavItemManufacturers.Visibility = authenticatedVisibility;
        NavItemWarehouses.Visibility = authenticatedVisibility;
        NavItemSalesChannels.Visibility = authenticatedVisibility;
        NavSeparator2.Visibility = authenticatedVisibility;
        NavItemAiModels.Visibility = authenticatedVisibility;
        NavItemAiPrompts.Visibility = authenticatedVisibility;
        NavSeparator3.Visibility = authenticatedVisibility;
        NavItemTaxClasses.Visibility = authenticatedVisibility;

        // Update Header User Menu
        UserMenuPanel.Visibility = authenticatedVisibility;

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
        Console.WriteLine($"[Shell] OnNavigationViewSelectionChanged fired");
        Console.WriteLine($"[Shell] SelectedItem type: {args.SelectedItem?.GetType().Name ?? "null"}");

        if (args.SelectedItem is NavigationViewItem item)
        {
            Console.WriteLine($"[Shell] NavigationViewItem found, Tag: {item.Tag}, Tag type: {item.Tag?.GetType().Name ?? "null"}");

            if (item.Tag is string tag)
            {
                Console.WriteLine($"[Shell] Tag is string: '{tag}'");

                // Get navigator from Splash (the ContentControl where navigation content is rendered)
                var navigator = Splash.Navigator();
                if (navigator != null)
                {
                    Console.WriteLine($"[Shell] Got navigator from Splash, calling NavigateToPageFromShell with tag: '{tag}'");
                    await NavigateToPageFromShell(navigator, tag);
                }
                else
                {
                    Console.WriteLine($"[Shell] Failed to get navigator from Splash, trying service provider...");
                    // Fallback: try getting navigator from service provider
                    var app = Application.Current as App;
                    if (app?.Host?.Services != null)
                    {
                        navigator = app.Host.Services.GetService<INavigator>();
                        if (navigator != null)
                        {
                            Console.WriteLine($"[Shell] Got navigator from service provider");
                            await NavigateToPageFromShell(navigator, tag);
                        }
                        else
                        {
                            Console.WriteLine($"[Shell] Failed to get navigator from service provider");
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine($"[Shell] Tag is NOT a string");
            }
        }
        else
        {
            Console.WriteLine($"[Shell] SelectedItem is NOT NavigationViewItem");
        }
    }

    private async Task NavigateToPageFromShell(INavigator navigator, string tag)
    {
        Console.WriteLine($"[Shell] NavigateToPageFromShell called with tag: '{tag}'");

        try
        {
            switch (tag)
            {
                case "Login":
                    Console.WriteLine("[Shell] Navigating to Login");
                    await navigator.NavigateViewModelAsync<LoginModel>(this);
                    break;
                case "Main":
                case "Dashboard":
                    Console.WriteLine("[Shell] Navigating to Dashboard");
                    await navigator.NavigateViewModelAsync<DashboardModel>(this);
                    break;
                case "Customers":
                    Console.WriteLine("[Shell] Navigating to CustomerList");
                    await navigator.NavigateViewModelAsync<CustomerListModel>(this);
                    break;
                case "Orders":
                    Console.WriteLine("[Shell] Navigating to OrderList");
                    await navigator.NavigateViewModelAsync<OrderListModel>(this);
                    break;
                case "Products":
                    Console.WriteLine("[Shell] Navigating to ProductList");
                    await navigator.NavigateViewModelAsync<ProductListModel>(this);
                    break;
                case "Manufacturers":
                    Console.WriteLine("[Shell] Navigating to ManufacturerList");
                    await navigator.NavigateViewModelAsync<ManufacturerListModel>(this);
                    break;
                case "Invoices":
                    Console.WriteLine("[Shell] Navigating to InvoiceList");
                    await navigator.NavigateViewModelAsync<InvoiceListModel>(this);
                    break;
                case "Warehouses":
                    Console.WriteLine("[Shell] Navigating to WarehouseList");
                    await navigator.NavigateViewModelAsync<WarehouseListModel>(this);
                    break;
                case "SalesChannels":
                    Console.WriteLine("[Shell] Navigating to SalesChannelList");
                    await navigator.NavigateViewModelAsync<SalesChannelListModel>(this);
                    break;
                case "TaxClasses":
                    Console.WriteLine("[Shell] Navigating to TaxClassList");
                    await navigator.NavigateViewModelAsync<TaxClassListModel>(this);
                    break;
                case "AiModels":
                    Console.WriteLine("[Shell] Navigating to AiModelList");
                    await navigator.NavigateViewModelAsync<AiModelListModel>(this);
                    break;
                case "AiPrompts":
                    Console.WriteLine("[Shell] Navigating to AiPromptList");
                    await navigator.NavigateViewModelAsync<AiPromptListModel>(this);
                    break;
                case "Settings":
                    Console.WriteLine("[Shell] Settings navigation - not yet implemented");
                    // TODO: Implement settings page navigation
                    break;
                case "Logout":
                    Console.WriteLine("[Shell] Logging out");
                    var app = Application.Current as App;
                    if (app?.Host?.Services != null)
                    {
                        var auth = app.Host.Services.GetRequiredService<IAuthenticationService>();
                        await auth.LogoutAsync(CancellationToken.None);
                    }
                    break;
                default:
                    Console.WriteLine($"[Shell] Tag '{tag}' not handled yet");
                    break;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[Shell] Navigation failed: {ex.Message}");
        }
    }

    private async void OnTabBarSelectionChanged(object? sender, TabBarSelectionChangedEventArgs args)
    {
        if (args.NewItem is TabBarItem item && item.Tag is string tag)
        {
            Console.WriteLine($"[Shell] TabBar selection changed to: '{tag}'");
            var navigator = Splash.Navigator();
            if (navigator != null)
            {
                await NavigateToPageFromShell(navigator, tag);
            }
            else
            {
                // Fallback: try getting navigator from service provider
                var app = Application.Current as App;
                navigator = app?.Host?.Services?.GetService<INavigator>();
                if (navigator != null)
                {
                    await NavigateToPageFromShell(navigator, tag);
                }
            }
        }
    }

    private async void OnSettingsClick(object sender, RoutedEventArgs e)
    {
        Console.WriteLine("[Shell] Settings menu item clicked");

        var navigator = Splash.Navigator();
        if (navigator == null)
        {
            var app = Application.Current as App;
            navigator = app?.Host?.Services?.GetService<INavigator>();
        }

        if (navigator != null)
        {
            await NavigateToPageFromShell(navigator, "Settings");
        }
    }

    private async void OnLogoutClick(object sender, RoutedEventArgs e)
    {
        Console.WriteLine("[Shell] Logout menu item clicked");

        var navigator = Splash.Navigator();
        if (navigator == null)
        {
            var app = Application.Current as App;
            navigator = app?.Host?.Services?.GetService<INavigator>();
        }

        if (navigator != null)
        {
            await NavigateToPageFromShell(navigator, "Logout");
        }
    }

    private void OnPaneToggleClick(object sender, RoutedEventArgs e)
    {
        NavView.IsPaneOpen = !NavView.IsPaneOpen;
    }

    private void OnDarkModeToggle(object sender, RoutedEventArgs e)
    {
        Console.WriteLine("[Shell] Dark mode toggle clicked");

        try
        {
            // ToggleMenuFlyoutItem already toggled IsChecked before this event fires
            // Use the new IsChecked state to determine the theme
            var isDarkMode = MenuItemDarkMode.IsChecked;
            Console.WriteLine($"[Shell] IsChecked (isDarkMode): {isDarkMode}");

            var xamlRoot = this.XamlRoot;
            if (xamlRoot != null)
            {
                var newTheme = isDarkMode ? ElementTheme.Dark : ElementTheme.Light;
                Console.WriteLine($"[Shell] Switching theme to: {newTheme}");

                SystemThemeHelper.SetApplicationTheme(xamlRoot, newTheme);
                Console.WriteLine("[Shell] Theme switched successfully");
            }
            else
            {
                Console.WriteLine("[Shell] XamlRoot not available");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[Shell] Failed to toggle theme: {ex.Message}");
            Console.WriteLine($"[Shell] Stack trace: {ex.StackTrace}");
        }
    }

    private void InitializeDarkModeToggle()
    {
        try
        {
            // Get the OS theme setting
            var osTheme = SystemThemeHelper.GetCurrentOsTheme();
            var isDarkMode = osTheme == Microsoft.UI.Xaml.ApplicationTheme.Dark;
            Console.WriteLine($"[Shell] InitializeDarkModeToggle: OS Theme={osTheme}, isDarkMode={isDarkMode}");

            // Set the toggle state to match OS theme
            MenuItemDarkMode.IsChecked = isDarkMode;

            // Apply the OS theme to the app
            var xamlRoot = this.XamlRoot;
            if (xamlRoot != null)
            {
                var appTheme = isDarkMode ? ElementTheme.Dark : ElementTheme.Light;
                SystemThemeHelper.SetApplicationTheme(xamlRoot, appTheme);
                Console.WriteLine($"[Shell] Applied OS theme to app: {appTheme}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[Shell] Failed to initialize dark mode toggle: {ex.Message}");
        }
    }
}
