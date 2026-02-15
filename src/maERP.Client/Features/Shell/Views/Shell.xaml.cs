using maERP.Client.Core.Exceptions;
using maERP.Client.Features.Shell.Models;
using maERP.Client.Features.Auth.Services;
using maERP.Client.Features.Tenants.Services;
using maERP.Domain.Dtos.Tenant;
using maERP.Client.Features.Dashboard.Models;
using maERP.Client.Features.Customers.Models;
using maERP.Client.Features.Invoices.Models;
using maERP.Client.Features.Manufacturers.Models;
using maERP.Client.Features.Orders.Models;
using maERP.Client.Features.Products.Models;
using maERP.Client.Features.SalesChannels.Models;
using maERP.Client.Features.Superadmin.Models;
using maERP.Client.Features.TaxClasses.Models;
using maERP.Client.Features.Tenants.Models;
using maERP.Client.Features.Warehouses.Models;
using maERP.Client.Features.AiModels.Models;
using maERP.Client.Features.AiPrompts.Models;
using Uno.Toolkit.UI;

namespace maERP.Client.Features.Shell.Views;

public sealed partial class Shell : UserControl, IContentControlProvider
{
    // Mapping of navigation tags to their corresponding NavigationViewItems
    // Tags that exist in this dictionary will be highlighted in the sidebar
    private Dictionary<string, NavigationViewItem>? _sidebarTagMap;

    // Flag to prevent recursive selection changes
    private bool _isUpdatingSidebarSelection;

    public Shell()
    {
        this.InitializeComponent();

        // Set initial visibility: only Login visible, all other items hidden
        // This ensures correct state before authentication is checked
        SetUnauthenticatedVisibility();

        // Subscribe to static authentication state changed event
        ShellModel.AuthenticationStateChanged += OnAuthenticationStateChanged;

        // Subscribe to static tenant state changed event
        ShellModel.TenantStateChanged += OnTenantStateChanged;

        // Subscribe to static no-tenants state changed event
        ShellModel.NoTenantsStateChanged += OnNoTenantsStateChanged;

        // Note: NavView.SelectionChanged is now wired in XAML
        TabBarNav.SelectionChanged += OnTabBarSelectionChanged;
        this.Loaded += OnShellLoaded;
    }

    /// <summary>
    /// Initialize the mapping of sidebar tags to NavigationViewItems.
    /// Only tags that have sidebar entries are included.
    /// </summary>
    private void InitializeSidebarTagMap()
    {
        _sidebarTagMap = new Dictionary<string, NavigationViewItem>
        {
            { "Main", NavItemDashboard },
            { "Dashboard", NavItemDashboard },
            { "Customers", NavItemCustomers },
            { "Orders", NavItemOrders },
            { "Invoices", NavItemInvoices },
            { "Products", NavItemProducts },
            { "Manufacturers", NavItemManufacturers }
        };
    }

    /// <summary>
    /// Updates the sidebar selection to match the current page.
    /// If the tag doesn't correspond to a sidebar item, clears the selection.
    /// </summary>
    /// <param name="tag">The navigation tag of the current page</param>
    private void UpdateSidebarSelection(string? tag)
    {
        if (_isUpdatingSidebarSelection) return;

        try
        {
            _isUpdatingSidebarSelection = true;

            // Ensure the tag map is initialized
            if (_sidebarTagMap == null)
            {
                InitializeSidebarTagMap();
            }

            Console.WriteLine($"[Shell] UpdateSidebarSelection for tag: '{tag}'");

            if (!string.IsNullOrEmpty(tag) && _sidebarTagMap!.TryGetValue(tag, out var navItem))
            {
                // Tag has a corresponding sidebar item - select it
                if (NavView.SelectedItem != navItem)
                {
                    Console.WriteLine($"[Shell] Selecting sidebar item: {tag}");
                    NavView.SelectedItem = navItem;
                }
            }
            else
            {
                // Tag has no sidebar item (e.g., TaxClasses, Warehouses, etc.)
                // Clear the selection to avoid misleading highlighting
                Console.WriteLine($"[Shell] Clearing sidebar selection (tag '{tag}' has no sidebar item)");
                NavView.SelectedItem = null;
            }
        }
        finally
        {
            _isUpdatingSidebarSelection = false;
        }
    }

    private async void OnAuthenticationStateChanged(object? sender, bool isAuthenticated)
    {
        Console.WriteLine($"[Shell] OnAuthenticationStateChanged received: {isAuthenticated}");

        // Update visibility based on authentication state
        if (isAuthenticated)
        {
            SetAuthenticatedVisibility();
            await UpdateSuperadminMenuVisibilityAsync();
            UpdateTenantDisplay();
        }
        else
        {
            // Complete Shell reset - handles all UI state including FirstTenantOverlay,
            // tenant display, navigation items, and sidebar selection
            SetUnauthenticatedVisibility();
            MenuItemSuperadminTenants.Visibility = Visibility.Collapsed;
        }
    }

    private void OnTenantStateChanged(object? sender, TenantListDto? tenant)
    {
        Console.WriteLine($"[Shell] OnTenantStateChanged received: {tenant?.Name ?? "null"}");
        UpdateTenantDisplay();
    }

    private async void OnNoTenantsStateChanged(object? sender, bool hasNoTenants)
    {
        Console.WriteLine($"[Shell] OnNoTenantsStateChanged received: {hasNoTenants}");

        if (hasNoTenants)
        {
            SetNoTenantsVisibility();
        }
        else
        {
            // User now has tenants, show full authenticated UI
            SetAuthenticatedVisibility();
            await UpdateSuperadminMenuVisibilityAsync();
            UpdateTenantDisplay();
        }
    }

    private void UpdateTenantDisplay()
    {
        try
        {
            var app = Application.Current as App;
            var tenantContext = app?.Host?.Services?.GetService<ITenantContextService>();
            if (tenantContext == null)
            {
                Console.WriteLine("[Shell] UpdateTenantDisplay: TenantContextService not available");
                TenantSwitcher.Visibility = Visibility.Collapsed;
                TenantName.Visibility = Visibility.Visible;
                TenantName.Text = "maERP";
                return;
            }

            var tenants = tenantContext.AvailableTenants;
            Console.WriteLine($"[Shell] UpdateTenantDisplay: {tenants.Count} tenants available");

            if (tenants.Count > 1)
            {
                // Multiple tenants: Show dropdown button
                TenantSwitcherText.Text = tenantContext.CurrentTenant?.Name ?? "Tenant";
                PopulateTenantMenu(tenants, tenantContext.CurrentTenantId);
                TenantSwitcher.Visibility = Visibility.Visible;
                TenantName.Visibility = Visibility.Collapsed;
                Console.WriteLine($"[Shell] Showing tenant dropdown with {tenants.Count} items, current: {tenantContext.CurrentTenant?.Name}");
            }
            else
            {
                // One or no tenant: Show text
                TenantSwitcher.Visibility = Visibility.Collapsed;
                TenantName.Visibility = Visibility.Visible;
                TenantName.Text = tenants.Count == 1 ? tenants[0].Name : "maERP";
                Console.WriteLine($"[Shell] Showing tenant text: {TenantName.Text}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[Shell] UpdateTenantDisplay error: {ex.Message}");
            TenantSwitcher.Visibility = Visibility.Collapsed;
            TenantName.Visibility = Visibility.Visible;
            TenantName.Text = "maERP";
        }
    }

    private void PopulateTenantMenu(IReadOnlyList<TenantListDto> tenants, Guid? currentTenantId)
    {
        TenantMenuFlyout.Items.Clear();

        foreach (var tenant in tenants)
        {
            var menuItem = new MenuFlyoutItem
            {
                Text = tenant.Name,
                Tag = tenant.Id
            };

            // Add checkmark icon for current tenant
            if (tenant.Id == currentTenantId)
            {
                menuItem.Icon = new FontIcon { Glyph = "\uE73E" }; // Checkmark
            }

            menuItem.Click += OnTenantMenuItemClick;
            TenantMenuFlyout.Items.Add(menuItem);
        }
    }

    private async void OnTenantMenuItemClick(object sender, RoutedEventArgs e)
    {
        Console.WriteLine($"[Shell] OnTenantMenuItemClick called. Sender: {sender?.GetType().Name}");

        if (sender is not MenuFlyoutItem menuItem)
        {
            Console.WriteLine($"[Shell] Sender is not a MenuFlyoutItem");
            return;
        }

        Console.WriteLine($"[Shell] MenuFlyoutItem Tag: {menuItem.Tag} (Type: {menuItem.Tag?.GetType().Name})");

        if (menuItem.Tag is not Guid selectedTenantId)
        {
            Console.WriteLine($"[Shell] Tag is not a Guid - cannot switch tenant");
            return;
        }

        Console.WriteLine($"[Shell] Selected tenant ID: {selectedTenantId}");

        // Close the flyout immediately to avoid UI issues
        TenantMenuFlyout.Hide();

        try
        {
            var app = Application.Current as App;
            var tenantContext = app?.Host?.Services?.GetService<ITenantContextService>();

            // Check if this is a different tenant than the current one
            bool isSameTenant = tenantContext?.CurrentTenantId == selectedTenantId;
            Console.WriteLine($"[Shell] Current tenant: {tenantContext?.CurrentTenantId}, Selected: {selectedTenantId}, IsSame: {isSameTenant}");

            if (isSameTenant)
            {
                Console.WriteLine("[Shell] Same tenant selected, skipping switch");
                return;
            }

            // Switch tenant via TenantContextService
            if (tenantContext != null)
            {
                Console.WriteLine("[Shell] Calling SetCurrentTenantAsync...");
                await tenantContext.SetCurrentTenantAsync(selectedTenantId);
                Console.WriteLine("[Shell] SetCurrentTenantAsync completed");
            }

            // Navigate to Dashboard after tenant switch
            var navigator = Splash.Navigator();
            if (navigator == null)
            {
                navigator = app?.Host?.Services?.GetService<INavigator>();
            }

            if (navigator != null)
            {
                Console.WriteLine("[Shell] Navigating to Dashboard after tenant switch");
                await navigator.NavigateViewModelAsync<DashboardModel>(this, qualifier: Qualifiers.ClearBackStack);
                Console.WriteLine("[Shell] Navigation to Dashboard completed");

                // Update sidebar selection to Dashboard
                UpdateSidebarSelection("Dashboard");
            }
            else
            {
                Console.WriteLine("[Shell] Navigator not found");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[Shell] OnTenantMenuItemClick error: {ex.Message}");
            Console.WriteLine($"[Shell] Stack trace: {ex.StackTrace}");
        }
    }

    private void SetAuthenticatedVisibility()
    {
        Console.WriteLine("[Shell] SetAuthenticatedVisibility called");

        // Hide overlays
        LoginOverlay.Visibility = Visibility.Collapsed;
        FirstTenantOverlay.Visibility = Visibility.Collapsed;

        // Show AppHeader and NavigationView pane
        AppHeader.Visibility = Visibility.Visible;
        NavView.IsPaneVisible = true;

        // NavigationView menu items - all visible
        NavItemDashboard.Visibility = Visibility.Visible;
        NavSeparator1.Visibility = Visibility.Visible;
        NavItemCustomers.Visibility = Visibility.Visible;
        NavItemOrders.Visibility = Visibility.Visible;
        NavItemInvoices.Visibility = Visibility.Visible;
        NavItemProducts.Visibility = Visibility.Visible;
        NavItemManufacturers.Visibility = Visibility.Visible;

        // Header User Menu - visible when authenticated
        UserMenuPanel.Visibility = Visibility.Visible;

        // TabBar items - all visible
        TabItemDashboard.Visibility = Visibility.Visible;
        TabItemCustomers.Visibility = Visibility.Visible;
        TabItemOrders.Visibility = Visibility.Visible;
        TabItemSettings.Visibility = Visibility.Visible;
        TabItemLogout.Visibility = Visibility.Visible;
    }

    private void SetUnauthenticatedVisibility()
    {
        Console.WriteLine("[Shell] SetUnauthenticatedVisibility called - resetting complete Shell state");

        // Show LoginOverlay, hide FirstTenantOverlay
        InitializeLoginOverlay();
        LoginOverlay.Visibility = Visibility.Visible;
        FirstTenantOverlay.Visibility = Visibility.Collapsed;

        // Reset FirstTenantOverlay form state
        FirstTenantName.Text = string.Empty;
        FirstTenantDescription.Text = string.Empty;
        FirstTenantSaveButton.IsEnabled = false;
        FirstTenantErrorBanner.Visibility = Visibility.Collapsed;
        FirstTenantErrorText.Text = string.Empty;
        FirstTenantProgress.Visibility = Visibility.Collapsed;
        FirstTenantProgress.IsActive = false;

        // Hide AppHeader
        AppHeader.Visibility = Visibility.Collapsed;

        // Hide NavigationView pane
        NavView.IsPaneVisible = false;

        // Reset tenant display to default
        TenantSwitcher.Visibility = Visibility.Collapsed;
        TenantName.Visibility = Visibility.Visible;
        TenantName.Text = "maERP";
        TenantMenuFlyout.Items.Clear();

        // NavigationView menu items - all hidden
        NavItemDashboard.Visibility = Visibility.Collapsed;
        NavSeparator1.Visibility = Visibility.Collapsed;
        NavItemCustomers.Visibility = Visibility.Collapsed;
        NavItemOrders.Visibility = Visibility.Collapsed;
        NavItemInvoices.Visibility = Visibility.Collapsed;
        NavItemProducts.Visibility = Visibility.Collapsed;
        NavItemManufacturers.Visibility = Visibility.Collapsed;

        // Header User Menu - hidden when not authenticated
        UserMenuPanel.Visibility = Visibility.Collapsed;

        // TabBar items - all hidden
        TabItemDashboard.Visibility = Visibility.Collapsed;
        TabItemCustomers.Visibility = Visibility.Collapsed;
        TabItemOrders.Visibility = Visibility.Collapsed;
        TabItemSettings.Visibility = Visibility.Collapsed;
        TabItemLogout.Visibility = Visibility.Collapsed;

        // Clear sidebar selection
        UpdateSidebarSelection(null);

        Console.WriteLine("[Shell] SetUnauthenticatedVisibility completed - Shell fully reset");
    }

    private void SetNoTenantsVisibility()
    {
        Console.WriteLine("[Shell] SetNoTenantsVisibility called");

        // Hide LoginOverlay, show FirstTenantOverlay
        LoginOverlay.Visibility = Visibility.Collapsed;
        FirstTenantOverlay.Visibility = Visibility.Visible;

        // Reset form state
        FirstTenantName.Text = string.Empty;
        FirstTenantDescription.Text = string.Empty;
        FirstTenantSaveButton.IsEnabled = false;
        FirstTenantErrorBanner.Visibility = Visibility.Collapsed;
        FirstTenantErrorText.Text = string.Empty;
        FirstTenantProgress.Visibility = Visibility.Collapsed;
        FirstTenantProgress.IsActive = false;

        // NavigationView menu items - all hidden for users without tenants
        NavItemDashboard.Visibility = Visibility.Collapsed;
        NavSeparator1.Visibility = Visibility.Collapsed;
        NavItemCustomers.Visibility = Visibility.Collapsed;
        NavItemOrders.Visibility = Visibility.Collapsed;
        NavItemInvoices.Visibility = Visibility.Collapsed;
        NavItemProducts.Visibility = Visibility.Collapsed;
        NavItemManufacturers.Visibility = Visibility.Collapsed;

        // Hide NavigationView pane completely
        NavView.IsPaneVisible = false;

        // Header - hide completely for first tenant creation
        AppHeader.Visibility = Visibility.Collapsed;

        // Hide tenant display
        TenantSwitcher.Visibility = Visibility.Collapsed;
        TenantName.Visibility = Visibility.Collapsed;

        // TabBar items - all hidden
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

                    // Update superadmin menu visibility
                    await UpdateSuperadminMenuVisibilityAsync();

                    // Update tenant display if authenticated
                    if (shellModel.IsAuthenticated)
                    {
                        UpdateTenantDisplay();
                    }

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

        if (model.IsAuthenticated)
        {
            SetAuthenticatedVisibility();
        }
        else
        {
            SetUnauthenticatedVisibility();
        }

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
        // Skip if we're programmatically updating the sidebar selection
        if (_isUpdatingSidebarSelection)
        {
            Console.WriteLine("[Shell] OnNavigationViewSelectionChanged skipped (programmatic update)");
            return;
        }

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
                case "Tenants":
                    Console.WriteLine("[Shell] Navigating to TenantList");
                    await navigator.NavigateViewModelAsync<TenantListModel>(this);
                    break;
                case "SuperadminTenants":
                    Console.WriteLine("[Shell] Navigating to SuperadminTenantList");
                    await navigator.NavigateViewModelAsync<SuperadminTenantListModel>(this);
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

            // Update sidebar selection to match the navigated page
            // For pages without a sidebar entry, this will clear the selection
            UpdateSidebarSelection(tag);
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

    private async void OnTenantsClick(object sender, RoutedEventArgs e)
    {
        Console.WriteLine("[Shell] Tenants menu item clicked");

        var navigator = Splash.Navigator();
        if (navigator == null)
        {
            var app = Application.Current as App;
            navigator = app?.Host?.Services?.GetService<INavigator>();
        }

        if (navigator != null)
        {
            await NavigateToPageFromShell(navigator, "Tenants");
        }
    }

    private async void OnSuperadminTenantsClick(object sender, RoutedEventArgs e)
    {
        Console.WriteLine("[Shell] Superadmin Tenants menu item clicked");

        var navigator = Splash.Navigator();
        if (navigator == null)
        {
            var app = Application.Current as App;
            navigator = app?.Host?.Services?.GetService<INavigator>();
        }

        if (navigator != null)
        {
            await NavigateToPageFromShell(navigator, "SuperadminTenants");
        }
    }

    private async void OnSalesChannelsClick(object sender, RoutedEventArgs e)
    {
        Console.WriteLine("[Shell] Sales Channels menu item clicked");

        var navigator = Splash.Navigator();
        if (navigator == null)
        {
            var app = Application.Current as App;
            navigator = app?.Host?.Services?.GetService<INavigator>();
        }

        if (navigator != null)
        {
            await NavigateToPageFromShell(navigator, "SalesChannels");
        }
    }

    private async void OnTaxClassesClick(object sender, RoutedEventArgs e)
    {
        Console.WriteLine("[Shell] Tax Classes menu item clicked");

        var navigator = Splash.Navigator();
        if (navigator == null)
        {
            var app = Application.Current as App;
            navigator = app?.Host?.Services?.GetService<INavigator>();
        }

        if (navigator != null)
        {
            await NavigateToPageFromShell(navigator, "TaxClasses");
        }
    }

    private async void OnWarehousesClick(object sender, RoutedEventArgs e)
    {
        Console.WriteLine("[Shell] Warehouses menu item clicked");

        var navigator = Splash.Navigator();
        if (navigator == null)
        {
            var app = Application.Current as App;
            navigator = app?.Host?.Services?.GetService<INavigator>();
        }

        if (navigator != null)
        {
            await NavigateToPageFromShell(navigator, "Warehouses");
        }
    }

    private async void OnAiModelsClick(object sender, RoutedEventArgs e)
    {
        Console.WriteLine("[Shell] AI Models menu item clicked");

        var navigator = Splash.Navigator();
        if (navigator == null)
        {
            var app = Application.Current as App;
            navigator = app?.Host?.Services?.GetService<INavigator>();
        }

        if (navigator != null)
        {
            await NavigateToPageFromShell(navigator, "AiModels");
        }
    }

    private async void OnAiPromptsClick(object sender, RoutedEventArgs e)
    {
        Console.WriteLine("[Shell] AI Prompts menu item clicked");

        var navigator = Splash.Navigator();
        if (navigator == null)
        {
            var app = Application.Current as App;
            navigator = app?.Host?.Services?.GetService<INavigator>();
        }

        if (navigator != null)
        {
            await NavigateToPageFromShell(navigator, "AiPrompts");
        }
    }

    private async Task UpdateSuperadminMenuVisibilityAsync()
    {
        try
        {
            var app = Application.Current as App;
            if (app?.Host?.Services == null)
            {
                Console.WriteLine("[Shell] Cannot check superadmin role - services not available");
                return;
            }

            var tokenStorage = app.Host.Services.GetService<ITokenStorageService>();
            if (tokenStorage == null)
            {
                Console.WriteLine("[Shell] Cannot check superadmin role - token storage not available");
                return;
            }

            var isSuperadmin = await tokenStorage.IsInRoleAsync("Superadmin");
            Console.WriteLine($"[Shell] User is Superadmin: {isSuperadmin}");

            MenuItemSuperadminTenants.Visibility = isSuperadmin ? Visibility.Visible : Visibility.Collapsed;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[Shell] Error checking superadmin role: {ex.Message}");
            MenuItemSuperadminTenants.Visibility = Visibility.Collapsed;
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

    #region Login Overlay

    private void InitializeLoginOverlay()
    {
        // Reset form state
        LoginServerUrl.Text = "https://";
        LoginEmail.Text = string.Empty;
        LoginPassword.Password = string.Empty;
        LoginErrorBanner.Visibility = Visibility.Collapsed;
        LoginErrorText.Text = string.Empty;
        LoginProgress.Visibility = Visibility.Collapsed;
        LoginProgress.IsActive = false;
        LoginButton.IsEnabled = true;

        // Pre-fill dev credentials if in Development environment
        try
        {
            var app = Application.Current as App;
            var hostEnvironment = app?.Host?.Services?.GetService<IHostEnvironment>();
            if (hostEnvironment?.IsDevelopment() == true)
            {
                LoginServerUrl.Text = "https://localhost:8443";
                LoginEmail.Text = "admin@localhost.com";
                LoginPassword.Password = "P@ssword1";
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[Shell] InitializeLoginOverlay - could not check environment: {ex.Message}");
        }
    }

    private async void LoginButton_Click(object sender, RoutedEventArgs e)
    {
        Console.WriteLine("[Shell] LoginButton_Click - attempting login");

        var serverUrl = LoginServerUrl.Text?.Trim();
        var email = LoginEmail.Text?.Trim();
        var password = LoginPassword.Password;

        // Validate inputs
        if (string.IsNullOrWhiteSpace(serverUrl) || string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
        {
            LoginErrorText.Text = "Please fill in all fields";
            LoginErrorBanner.Visibility = Visibility.Visible;
            return;
        }

        // Normalize server URL
        if (!serverUrl.StartsWith("http://", StringComparison.OrdinalIgnoreCase) &&
            !serverUrl.StartsWith("https://", StringComparison.OrdinalIgnoreCase))
        {
            serverUrl = "https://" + serverUrl;
        }

        serverUrl = serverUrl.TrimEnd('/');

        // Show progress
        LoginButton.IsEnabled = false;
        LoginProgress.Visibility = Visibility.Visible;
        LoginProgress.IsActive = true;
        LoginErrorBanner.Visibility = Visibility.Collapsed;

        try
        {
            var app = Application.Current as App;
            if (app?.Host?.Services == null)
            {
                throw new InvalidOperationException("Services not available");
            }

            var auth = app.Host.Services.GetRequiredService<IAuthenticationService>();
            var tenantContext = app.Host.Services.GetRequiredService<ITenantContextService>();
            var shellModel = app.Host.Services.GetRequiredService<ShellModel>();

            var credentials = new Dictionary<string, string>
            {
                ["Email"] = email,
                ["Password"] = password,
                ["ServerUrl"] = serverUrl
            };

            // IDispatcher is nullable - passing null is safe since we're already on the UI thread
            var success = await auth.LoginAsync(dispatcher: null, credentials);

            if (success)
            {
                shellModel.UpdateAuthenticationState(true);

                // Check if user has any tenants
                if (tenantContext.AvailableTenants.Count == 0)
                {
                    // No tenants - show first tenant creation overlay
                    shellModel.UpdateNoTenantsState(true);
                }
                else
                {
                    // Has tenants - navigate to Dashboard
                    var navigator = Splash.Navigator();
                    if (navigator == null)
                    {
                        navigator = app.Host.Services.GetService<INavigator>();
                    }

                    if (navigator != null)
                    {
                        Console.WriteLine("[Shell] Login successful, navigating to Dashboard");
                        await navigator.NavigateViewModelAsync<DashboardModel>(this, qualifier: Qualifiers.ClearBackStack);
                    }
                }
            }
            else
            {
                LoginErrorText.Text = "Login failed. Please check your credentials and server URL.";
                LoginErrorBanner.Visibility = Visibility.Visible;
            }
        }
        catch (ApiException ex)
        {
            Console.WriteLine($"[Shell] LoginButton_Click API error: {ex.CombinedMessage}");
            LoginErrorText.Text = ex.CombinedMessage;
            LoginErrorBanner.Visibility = Visibility.Visible;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[Shell] LoginButton_Click error: {ex.Message}");
            LoginErrorText.Text = $"An error occurred: {ex.Message}";
            LoginErrorBanner.Visibility = Visibility.Visible;
        }
        finally
        {
            LoginProgress.Visibility = Visibility.Collapsed;
            LoginProgress.IsActive = false;
            LoginButton.IsEnabled = true;
        }
    }

    #endregion

    #region First Tenant Creation

    private void FirstTenantName_TextChanged(object sender, TextChangedEventArgs e)
    {
        // Enable save button only if name is not empty
        FirstTenantSaveButton.IsEnabled = !string.IsNullOrWhiteSpace(FirstTenantName.Text);
    }

    private async void FirstTenantCancel_Click(object sender, RoutedEventArgs e)
    {
        Console.WriteLine("[Shell] FirstTenantCancel_Click - logging out");

        try
        {
            var app = Application.Current as App;
            if (app?.Host?.Services != null)
            {
                var auth = app.Host.Services.GetRequiredService<IAuthenticationService>();
                await auth.LogoutAsync(CancellationToken.None);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[Shell] FirstTenantCancel_Click error: {ex.Message}");
        }
    }

    private async void FirstTenantSave_Click(object sender, RoutedEventArgs e)
    {
        Console.WriteLine("[Shell] FirstTenantSave_Click - creating first tenant");

        var tenantName = FirstTenantName.Text?.Trim();
        if (string.IsNullOrWhiteSpace(tenantName))
        {
            return;
        }

        // Show progress
        FirstTenantSaveButton.IsEnabled = false;
        FirstTenantCancelButton.IsEnabled = false;
        FirstTenantProgress.Visibility = Visibility.Visible;
        FirstTenantProgress.IsActive = true;
        FirstTenantErrorBanner.Visibility = Visibility.Collapsed;

        try
        {
            var app = Application.Current as App;
            if (app?.Host?.Services == null)
            {
                throw new InvalidOperationException("Services not available");
            }

            var tenantService = app.Host.Services.GetRequiredService<ITenantService>();
            var tenantContext = app.Host.Services.GetRequiredService<ITenantContextService>();
            var shellModel = app.Host.Services.GetRequiredService<ShellModel>();

            // Create the tenant
            var input = new TenantInputDto
            {
                Name = tenantName,
                Description = FirstTenantDescription.Text?.Trim()
            };

            var newTenantId = await tenantService.CreateTenantAsync(input);
            Console.WriteLine($"[Shell] First tenant created with ID: {newTenantId}");

            // Refresh JWT token to include the new tenant in claims, then refresh tenant list
            await tenantContext.RefreshTokenAndTenantsAsync();
            Console.WriteLine("[Shell] JWT token and tenant list refreshed");

            // Set the new tenant as current
            if (newTenantId != Guid.Empty)
            {
                await tenantContext.SetCurrentTenantAsync(newTenantId);
                Console.WriteLine("[Shell] New tenant set as current");
            }

            // Hide overlay and show full authenticated UI
            shellModel.UpdateNoTenantsState(false);

            // Navigate to Dashboard
            var navigator = Splash.Navigator();
            if (navigator == null)
            {
                navigator = app.Host.Services.GetService<INavigator>();
            }

            if (navigator != null)
            {
                Console.WriteLine("[Shell] Navigating to Dashboard after first tenant creation");
                await navigator.NavigateViewModelAsync<DashboardModel>(this, qualifier: Qualifiers.ClearBackStack);
            }
        }
        catch (ApiException ex)
        {
            Console.WriteLine($"[Shell] FirstTenantSave_Click API error: {ex.CombinedMessage}");
            FirstTenantErrorText.Text = ex.CombinedMessage;
            FirstTenantErrorBanner.Visibility = Visibility.Visible;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[Shell] FirstTenantSave_Click error: {ex.Message}");
            FirstTenantErrorText.Text = ex.Message;
            FirstTenantErrorBanner.Visibility = Visibility.Visible;
        }
        finally
        {
            // Hide progress and re-enable buttons
            FirstTenantProgress.Visibility = Visibility.Collapsed;
            FirstTenantProgress.IsActive = false;
            FirstTenantSaveButton.IsEnabled = !string.IsNullOrWhiteSpace(FirstTenantName.Text);
            FirstTenantCancelButton.IsEnabled = true;
        }
    }

    #endregion
}
