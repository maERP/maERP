using maERP.Client.Core.Exceptions;
using Windows.ApplicationModel.Resources;
using maERP.Client.Features.Account.Models;
using maERP.Client.Features.Shell.Models;
using maERP.Client.Features.Auth.Services;
using maERP.Client.Features.Tenants.Services;
using maERP.Domain.Dtos.Auth;
using maERP.Domain.Dtos.Tenant;
using maERP.Client.Features.Dashboard.Models;
using maERP.Client.Features.Customers.Models;
using maERP.Client.Features.Invoices.Models;
using maERP.Client.Features.Manufacturers.Models;
using maERP.Client.Features.Orders.Models;
using maERP.Client.Features.Products.Models;
using maERP.Client.Features.SalesChannels.Models;
using maERP.Client.Features.SalesChannels.Services;
using maERP.Client.Features.SalesChannelDashboards.Models;
using maERP.Client.Features.Superadmin.Models;
using maERP.Domain.Enums;
using maERP.Client.Features.TaxClasses.Models;
using maERP.Client.Features.Tenants.Models;
using maERP.Client.Features.Warehouses.Models;
using maERP.Client.Features.AiModels.Models;
using maERP.Client.Features.AiPrompts.Models;
using Uno.Toolkit.UI;

namespace maERP.Client.Features.Shell.Views;

public sealed partial class Shell : UserControl, IContentControlProvider
{
    // Mapping of navigation tags to their corresponding sidebar Button
    private Dictionary<string, Button>? _sidebarTagMap;

    // Currently highlighted sidebar button (active state)
    private Button? _activeNavButton;

    // Cached reference to avoid service lookup on every pointer move
    private ISessionManager? _sessionManager;

    // Dynamic SalesChannel sidebar items (buttons inside SalesChannelSubItemsContainer)
    private readonly List<Button> _dynamicSalesChannelItems = new();
    private readonly SemaphoreSlim _salesChannelRefreshLock = new(1, 1);

    public Shell()
    {
        this.InitializeComponent();

        SetUnauthenticatedVisibility();

        ShellModel.AuthenticationStateChanged += OnAuthenticationStateChanged;
        ShellModel.TenantStateChanged += OnTenantStateChanged;
        ShellModel.NoTenantsStateChanged += OnNoTenantsStateChanged;
        ShellModel.SalesChannelsChanged += OnSalesChannelsChanged;

        this.PointerMoved += OnUserActivity;
        this.KeyDown += OnUserActivity;

        TabBarNav.SelectionChanged += OnTabBarSelectionChanged;
        this.Loaded += OnShellLoaded;
    }

    private void OnUserActivity(object sender, RoutedEventArgs e)
    {
        _sessionManager ??= (Application.Current as App)?.Host?.Services?.GetService<ISessionManager>();
        _sessionManager?.RecordUserActivity();
    }

    private void InitializeSidebarTagMap()
    {
        _sidebarTagMap = new Dictionary<string, Button>
        {
            { "Main", NavItemDashboard },
            { "Dashboard", NavItemDashboard },
            { "Customers", NavItemCustomers },
            { "Products", NavItemProducts },
            { "Manufacturers", NavItemManufacturers },
            { "Orders", NavItemOrders },
            { "Invoices", NavItemInvoices },
            { "SalesChannels", NavItemSalesChannels },
            { "TaxClasses", NavItemTaxClasses },
            { "Warehouses", NavItemWarehouses },
            { "AiModels", NavItemAiModels },
            { "AiPrompts", NavItemAiPrompts },
            { "TenantOAuthSettings", NavItemTenantOAuthSettings },
            { "SuperadminTenants", NavItemSuperadminTenants },
            { "SystemOAuthSettings", NavItemSystemOAuthSettings }
        };
    }

    /// <summary>
    /// Highlights the sidebar button that corresponds to the given tag.
    /// </summary>
    private void UpdateSidebarSelection(string? tag)
    {
        if (_sidebarTagMap == null)
        {
            InitializeSidebarTagMap();
        }

        if (_activeNavButton != null)
        {
            _activeNavButton.Background = new SolidColorBrush(Microsoft.UI.Colors.Transparent);
            _activeNavButton.ClearValue(Button.FontWeightProperty);
        }

        _activeNavButton = null;

        if (!string.IsNullOrEmpty(tag) && _sidebarTagMap!.TryGetValue(tag, out var btn))
        {
            if (Application.Current.Resources["PrimaryContainerBrush"] is Brush highlight)
            {
                btn.Background = highlight;
            }
            _activeNavButton = btn;
        }
    }

    private async void OnAuthenticationStateChanged(object? sender, bool isAuthenticated)
    {
        Console.WriteLine($"[Shell] OnAuthenticationStateChanged received: {isAuthenticated}");

        if (isAuthenticated)
        {
            SetAuthenticatedVisibility();
            await UpdateSuperadminMenuVisibilityAsync();
            UpdateTenantDisplay();
            await RefreshSalesChannelSidebar();
        }
        else
        {
            SetUnauthenticatedVisibility();
            GroupSuperadminPanel.Visibility = Visibility.Collapsed;
        }
    }

    private async void OnTenantStateChanged(object? sender, TenantListDto? tenant)
    {
        Console.WriteLine($"[Shell] OnTenantStateChanged received: {tenant?.Name ?? "null"}");
        UpdateTenantDisplay();
        await RefreshSalesChannelSidebar();
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
                TenantSwitcher.Visibility = Visibility.Collapsed;
                TenantName.Visibility = Visibility.Visible;
                TenantName.Text = "maERP";
                return;
            }

            var tenants = tenantContext.AvailableTenants;

            if (tenants.Count > 1)
            {
                TenantSwitcherText.Text = tenantContext.CurrentTenant?.Name ?? "Tenant";
                PopulateTenantMenu(tenants, tenantContext.CurrentTenantId);
                TenantSwitcher.Visibility = Visibility.Visible;
                TenantName.Visibility = Visibility.Collapsed;
            }
            else
            {
                TenantSwitcher.Visibility = Visibility.Collapsed;
                TenantName.Visibility = Visibility.Visible;
                TenantName.Text = tenants.Count == 1 ? tenants[0].Name : "maERP";
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

            if (tenant.Id == currentTenantId)
            {
                menuItem.Icon = new FontIcon { Glyph = "\uE73E" };
            }

            menuItem.Click += OnTenantMenuItemClick;
            TenantMenuFlyout.Items.Add(menuItem);
        }
    }

    private async void OnTenantMenuItemClick(object sender, RoutedEventArgs e)
    {
        if (sender is not MenuFlyoutItem menuItem) return;
        if (menuItem.Tag is not Guid selectedTenantId) return;

        TenantMenuFlyout.Hide();

        try
        {
            var app = Application.Current as App;
            var tenantContext = app?.Host?.Services?.GetService<ITenantContextService>();

            if (tenantContext?.CurrentTenantId == selectedTenantId) return;

            if (tenantContext != null)
            {
                await tenantContext.SetCurrentTenantAsync(selectedTenantId);
            }

            var navigator = Splash.Navigator() ?? app?.Host?.Services?.GetService<INavigator>();

            if (navigator != null)
            {
                await navigator.NavigateViewModelAsync<DashboardModel>(this, qualifier: Qualifiers.ClearBackStack);
                UpdateSidebarSelection("Dashboard");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[Shell] OnTenantMenuItemClick error: {ex.Message}");
        }
    }

    private void SetAuthenticatedVisibility()
    {
        LoginOverlay.Visibility = Visibility.Collapsed;
        RegistrationOverlay.Visibility = Visibility.Collapsed;
        FirstTenantOverlay.Visibility = Visibility.Collapsed;

        Sidebar.Visibility = Visibility.Visible;
        ContentHeader.Visibility = Visibility.Visible;

        NavItemDashboard.Visibility = Visibility.Visible;
        NavItemCustomers.Visibility = Visibility.Visible;
        NavItemProducts.Visibility = Visibility.Visible;
        NavItemManufacturers.Visibility = Visibility.Visible;
        NavItemOrders.Visibility = Visibility.Visible;
        NavItemInvoices.Visibility = Visibility.Visible;
        NavItemSalesChannels.Visibility = Visibility.Visible;
        NavItemTaxClasses.Visibility = Visibility.Visible;
        NavItemWarehouses.Visibility = Visibility.Visible;
        NavItemAiModels.Visibility = Visibility.Visible;
        NavItemAiPrompts.Visibility = Visibility.Visible;
        NavItemTenantOAuthSettings.Visibility = Visibility.Visible;

        TabItemDashboard.Visibility = Visibility.Visible;
        TabItemCustomers.Visibility = Visibility.Visible;
        TabItemOrders.Visibility = Visibility.Visible;
        TabItemSettings.Visibility = Visibility.Visible;
        TabItemLogout.Visibility = Visibility.Visible;
    }

    private void SetUnauthenticatedVisibility()
    {
        InitializeLoginOverlay();
        LoginOverlay.Visibility = Visibility.Visible;
        RegistrationOverlay.Visibility = Visibility.Collapsed;
        FirstTenantOverlay.Visibility = Visibility.Collapsed;

        FirstTenantName.Text = string.Empty;
        FirstTenantDescription.Text = string.Empty;
        FirstTenantSaveButton.IsEnabled = false;
        FirstTenantErrorBanner.Visibility = Visibility.Collapsed;
        FirstTenantErrorText.Text = string.Empty;
        FirstTenantProgress.Visibility = Visibility.Collapsed;
        FirstTenantProgress.IsActive = false;

        Sidebar.Visibility = Visibility.Collapsed;
        ContentHeader.Visibility = Visibility.Collapsed;

        TenantSwitcher.Visibility = Visibility.Collapsed;
        TenantName.Visibility = Visibility.Visible;
        TenantName.Text = "maERP";
        TenantMenuFlyout.Items.Clear();

        TabItemDashboard.Visibility = Visibility.Collapsed;
        TabItemCustomers.Visibility = Visibility.Collapsed;
        TabItemOrders.Visibility = Visibility.Collapsed;
        TabItemSettings.Visibility = Visibility.Collapsed;
        TabItemLogout.Visibility = Visibility.Collapsed;

        ClearDynamicSalesChannelItems();
        UpdateSidebarSelection(null);
    }

    private void SetNoTenantsVisibility()
    {
        LoginOverlay.Visibility = Visibility.Collapsed;
        RegistrationOverlay.Visibility = Visibility.Collapsed;
        FirstTenantOverlay.Visibility = Visibility.Visible;

        FirstTenantName.Text = string.Empty;
        FirstTenantDescription.Text = string.Empty;
        FirstTenantSaveButton.IsEnabled = false;
        FirstTenantErrorBanner.Visibility = Visibility.Collapsed;
        FirstTenantErrorText.Text = string.Empty;
        FirstTenantProgress.Visibility = Visibility.Collapsed;
        FirstTenantProgress.IsActive = false;

        Sidebar.Visibility = Visibility.Collapsed;
        ContentHeader.Visibility = Visibility.Collapsed;

        TenantSwitcher.Visibility = Visibility.Collapsed;
        TenantName.Visibility = Visibility.Collapsed;

        TabItemDashboard.Visibility = Visibility.Collapsed;
        TabItemCustomers.Visibility = Visibility.Collapsed;
        TabItemOrders.Visibility = Visibility.Collapsed;
        TabItemSettings.Visibility = Visibility.Collapsed;
        TabItemLogout.Visibility = Visibility.Collapsed;
    }

    public ContentControl ContentControl => Splash;

    private async void OnShellLoaded(object sender, RoutedEventArgs e)
    {
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
                    TabBarNav.DataContext = shellModel;
                    shellModel.PropertyChanged += OnShellModelPropertyChanged;

                    await shellModel.InitializeAuthenticationState();
                    UpdateNavigationVisibility(shellModel);

                    await UpdateSuperadminMenuVisibilityAsync();

                    if (shellModel.IsAuthenticated)
                    {
                        UpdateTenantDisplay();
                    }

                    InitializeDarkModeToggle();
                    return;
                }
                else
                {
                    await Task.Delay(retryDelayMs);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Shell] Exception getting ShellModel (attempt {retry + 1}): {ex.Message}");
                await Task.Delay(retryDelayMs);
            }
        }
    }

    private void OnShellModelPropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(ShellModel.IsAuthenticated))
        {
            if (sender is ShellModel model)
            {
                UpdateNavigationVisibility(model);
            }
        }
    }

    private void UpdateNavigationVisibility(ShellModel model)
    {
        if (model.IsAuthenticated)
        {
            SetAuthenticatedVisibility();
        }
        else
        {
            SetUnauthenticatedVisibility();
        }
    }

    /// <summary>
    /// Unified click handler for all sidebar nav buttons. Uses the button's Tag to route.
    /// </summary>
    private async void OnNavItemClick(object sender, RoutedEventArgs e)
    {
        if (sender is not FrameworkElement el || el.Tag is not string tag) return;

        var navigator = Splash.Navigator();
        if (navigator == null)
        {
            var app = Application.Current as App;
            navigator = app?.Host?.Services?.GetService<INavigator>();
        }

        if (navigator != null)
        {
            await NavigateToPageFromShell(navigator, tag);
        }
    }

    private async Task NavigateToPageFromShell(INavigator navigator, string tag)
    {
        try
        {
            switch (tag)
            {
                case "Main":
                case "Dashboard":
                    await navigator.NavigateViewModelAsync<DashboardModel>(this);
                    break;
                case "Customers":
                    await navigator.NavigateViewModelAsync<CustomerListModel>(this);
                    break;
                case "Orders":
                    await navigator.NavigateViewModelAsync<OrderListModel>(this);
                    break;
                case "Products":
                    await navigator.NavigateViewModelAsync<ProductListModel>(this);
                    break;
                case "Manufacturers":
                    await navigator.NavigateViewModelAsync<ManufacturerListModel>(this);
                    break;
                case "Invoices":
                    await navigator.NavigateViewModelAsync<InvoiceListModel>(this);
                    break;
                case "Warehouses":
                    await navigator.NavigateViewModelAsync<WarehouseListModel>(this);
                    break;
                case "SalesChannels":
                    await navigator.NavigateViewModelAsync<SalesChannelListModel>(this);
                    break;
                case "TaxClasses":
                    await navigator.NavigateViewModelAsync<TaxClassListModel>(this);
                    break;
                case "AiModels":
                    await navigator.NavigateViewModelAsync<AiModelListModel>(this);
                    break;
                case "AiPrompts":
                    await navigator.NavigateViewModelAsync<AiPromptListModel>(this);
                    break;
                case "Tenants":
                    await navigator.NavigateViewModelAsync<TenantListModel>(this);
                    break;
                case "SuperadminTenants":
                    await navigator.NavigateViewModelAsync<SuperadminTenantListModel>(this);
                    break;
                case "SystemOAuthSettings":
                    await navigator.NavigateViewModelAsync<maERP.Client.Features.SystemOAuthSettings.Models.SystemOAuthSettingsModel>(this);
                    break;
                case "TenantOAuthSettings":
                    await navigator.NavigateViewModelAsync<maERP.Client.Features.TenantOAuthSettings.Models.TenantOAuthSettingsModel>(this);
                    break;
                case "UserProfile":
                    await navigator.NavigateViewModelAsync<AccountModel>(this);
                    break;
                case "Settings":
                    break;
                case "Logout":
                    var app = Application.Current as App;
                    if (app?.Host?.Services != null)
                    {
                        var auth = app.Host.Services.GetRequiredService<IAuthenticationService>();
                        await auth.LogoutAsync(CancellationToken.None);
                    }
                    break;
                default:
                    if (tag.StartsWith("SalesChannel_"))
                    {
                        var parts = tag.Split('_', 4);
                        if (parts.Length >= 4 && Guid.TryParse(parts[1], out var scId) && int.TryParse(parts[2], out var typeInt))
                        {
                            var scType = (SalesChannelType)typeInt;
                            var scName = parts[3];
                            var data = new SalesChannelDashboardData(scId, scName, scType);

                            switch (scType)
                            {
                                case SalesChannelType.PointOfSale:
                                    await navigator.NavigateViewModelAsync<PosDashboardModel>(this, data: data);
                                    break;
                                case SalesChannelType.Shopware5:
                                    await navigator.NavigateViewModelAsync<Shopware5DashboardModel>(this, data: data);
                                    break;
                                default:
                                    await navigator.NavigateDataAsync(this, new SalesChannelDetailData(scId));
                                    break;
                            }
                        }
                    }
                    break;
            }

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
            var navigator = Splash.Navigator();
            if (navigator == null)
            {
                var app = Application.Current as App;
                navigator = app?.Host?.Services?.GetService<INavigator>();
            }

            if (navigator != null)
            {
                await NavigateToPageFromShell(navigator, tag);
            }
        }
    }

    private async Task UpdateSuperadminMenuVisibilityAsync()
    {
        try
        {
            var app = Application.Current as App;
            if (app?.Host?.Services == null) return;

            var tokenStorage = app.Host.Services.GetService<ITokenStorageService>();
            if (tokenStorage == null) return;

            var isSuperadmin = await tokenStorage.IsInRoleAsync("Superadmin");
            GroupSuperadminPanel.Visibility = isSuperadmin ? Visibility.Visible : Visibility.Collapsed;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[Shell] Error checking superadmin role: {ex.Message}");
            GroupSuperadminPanel.Visibility = Visibility.Collapsed;
        }
    }

    private async void OnLogoutClick(object sender, RoutedEventArgs e)
    {
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

    private void OnDarkModeToggle(object sender, RoutedEventArgs e)
    {
        try
        {
            var xamlRoot = this.XamlRoot;
            if (xamlRoot == null) return;

            // Determine current theme via the root element's actual theme
            var currentTheme = (this.XamlRoot?.Content as FrameworkElement)?.ActualTheme
                               ?? Microsoft.UI.Xaml.ApplicationTheme.Light.ToElementTheme();

            var newTheme = currentTheme == ElementTheme.Dark ? ElementTheme.Light : ElementTheme.Dark;
            SystemThemeHelper.SetApplicationTheme(xamlRoot, newTheme);
            UpdateDarkModeIcon(newTheme == ElementTheme.Dark);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[Shell] Failed to toggle theme: {ex.Message}");
        }
    }

    private void InitializeDarkModeToggle()
    {
        try
        {
            var osTheme = SystemThemeHelper.GetCurrentOsTheme();
            var isDarkMode = osTheme == Microsoft.UI.Xaml.ApplicationTheme.Dark;

            var xamlRoot = this.XamlRoot;
            if (xamlRoot != null)
            {
                var appTheme = isDarkMode ? ElementTheme.Dark : ElementTheme.Light;
                SystemThemeHelper.SetApplicationTheme(xamlRoot, appTheme);
            }

            UpdateDarkModeIcon(isDarkMode);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[Shell] Failed to initialize dark mode toggle: {ex.Message}");
        }
    }

    private void UpdateDarkModeIcon(bool isDarkMode)
    {
        // Moon (&#xE708;) when dark, Sun-like (&#xE793;) when light
        DarkModeIcon.Glyph = isDarkMode ? "\uE708" : "\uE793";
    }

    private void OnGroupHeaderClick(object sender, RoutedEventArgs e)
    {
        if (sender is not Button btn || btn.Tag is not string contentName) return;

        if (this.FindName(contentName) is not FrameworkElement content) return;

        var isExpanding = content.Visibility == Visibility.Collapsed;
        content.Visibility = isExpanding ? Visibility.Visible : Visibility.Collapsed;

        // Chevron sibling: replace "Content" suffix with "Chevron"
        var chevronName = contentName.EndsWith("Content")
            ? contentName.Substring(0, contentName.Length - "Content".Length) + "Chevron"
            : contentName + "Chevron";

        if (this.FindName(chevronName) is FontIcon chevron)
        {
            // Down (E70D) when expanded, Right (E76C) when collapsed
            chevron.Glyph = isExpanding ? "\uE70D" : "\uE76C";
        }
    }

    #region Dynamic SalesChannel Sidebar

    private async void OnSalesChannelsChanged(object? sender, EventArgs e)
    {
        await RefreshSalesChannelSidebar();
    }

    private async Task RefreshSalesChannelSidebar()
    {
        if (!await _salesChannelRefreshLock.WaitAsync(0)) return;

        try
        {
            ClearDynamicSalesChannelItems();

            var app = Application.Current as App;
            var salesChannelService = app?.Host?.Services?.GetService<ISalesChannelService>();
            if (salesChannelService == null) return;

            var parameters = new Core.Models.QueryParameters { PageSize = 100 };
            var response = await salesChannelService.GetSalesChannelsAsync(parameters);

            ClearDynamicSalesChannelItems();

            if (response.Data.Count == 0) return;

            if (_sidebarTagMap == null) InitializeSidebarTagMap();

            var items = new List<Button>();

            foreach (var sc in response.Data)
            {
                var tag = $"SalesChannel_{sc.Id}_{(int)sc.SalesChannelType}_{sc.Name}";
                var glyph = sc.SalesChannelType switch
                {
                    SalesChannelType.PointOfSale => "\uE7BF",
                    SalesChannelType.Shopware5 => "\uE774",
                    SalesChannelType.Shopware6 => "\uE774",
                    SalesChannelType.WooCommerce => "\uE774",
                    SalesChannelType.eBay => "\uE774",
                    SalesChannelType.Amazon => "\uE774",
                    _ => "\uE774"
                };

                var btn = new Button
                {
                    Tag = tag,
                    Style = (Style)this.Resources["SidebarNavItemStyle"],
                    Margin = new Thickness(24, 1, 8, 1),
                    Height = 30,
                    Content = new StackPanel
                    {
                        Orientation = Orientation.Horizontal,
                        Spacing = 10,
                        Children =
                        {
                            new FontIcon { Glyph = glyph, FontSize = 14 },
                            new TextBlock
                            {
                                Text = sc.Name,
                                Style = (Style)Application.Current.Resources["BodySmall"],
                                VerticalAlignment = VerticalAlignment.Center,
                                TextTrimming = TextTrimming.CharacterEllipsis
                            }
                        }
                    }
                };
                btn.Click += OnNavItemClick;

                items.Add(btn);
                _sidebarTagMap![tag] = btn;
            }

            SalesChannelSubItemsContainer.ItemsSource = items;
            _dynamicSalesChannelItems.AddRange(items);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[Shell] RefreshSalesChannelSidebar error: {ex.Message}");
        }
        finally
        {
            _salesChannelRefreshLock.Release();
        }
    }

    private void ClearDynamicSalesChannelItems()
    {
        SalesChannelSubItemsContainer.ItemsSource = null;
        _dynamicSalesChannelItems.Clear();

        if (_sidebarTagMap != null)
        {
            var dynamicKeys = _sidebarTagMap.Keys.Where(k => k.StartsWith("SalesChannel_")).ToList();
            foreach (var key in dynamicKeys)
            {
                _sidebarTagMap.Remove(key);
            }
        }
    }

    #endregion

    #region Login Overlay

    private void InitializeLoginOverlay()
    {
        LoginServerUrl.Text = "https://";
        LoginEmail.Text = string.Empty;
        LoginPassword.Password = string.Empty;
        LoginErrorBanner.Visibility = Visibility.Collapsed;
        LoginErrorText.Text = string.Empty;
        LoginProgress.Visibility = Visibility.Collapsed;
        LoginProgress.IsActive = false;
        LoginButton.IsEnabled = true;
        LoginServerUrl.Visibility = Visibility.Visible;
        RegisterLink.Visibility = Visibility.Collapsed;

        // Runtime config (WASM: /config.json from nginx env var) may pin the
        // server URL — hide the input and use the configured value.
        if (maERP.Client.Core.Configuration.RuntimeConfig.IsServerUrlRestricted)
        {
            LoginServerUrl.Text = maERP.Client.Core.Configuration.RuntimeConfig.RestrictServerUrl!;
            LoginServerUrl.Visibility = Visibility.Collapsed;
        }

        try
        {
            var app = Application.Current as App;
            var hostEnvironment = app?.Host?.Services?.GetService<IHostEnvironment>();
            if (hostEnvironment?.IsDevelopment() == true)
            {
                if (!maERP.Client.Core.Configuration.RuntimeConfig.IsServerUrlRestricted)
                {
                    LoginServerUrl.Text = "https://localhost:8443";
                }
                LoginEmail.Text = "admin@localhost.com";
                LoginPassword.Password = "P@ssword1";
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[Shell] InitializeLoginOverlay error: {ex.Message}");
        }

        // Fetch /api/v1/server-info for the current URL (pinned, dev default,
        // or empty https:// placeholder) to decide whether the registration
        // link should appear. In the free-form case the link is also refreshed
        // on LostFocus and right before login.
        _ = RefreshRegistrationLinkAsync(LoginServerUrl.Text);
    }

    private void LoginServerUrl_LostFocus(object sender, RoutedEventArgs e)
    {
        var serverUrl = LoginServerUrl.Text?.Trim();
        if (string.IsNullOrWhiteSpace(serverUrl))
        {
            RegisterLink.Visibility = Visibility.Collapsed;
            return;
        }

        if (!serverUrl.StartsWith("http://", StringComparison.OrdinalIgnoreCase) &&
            !serverUrl.StartsWith("https://", StringComparison.OrdinalIgnoreCase))
        {
            serverUrl = "https://" + serverUrl;
        }

        _ = RefreshRegistrationLinkAsync(serverUrl);
    }

    private async Task RefreshRegistrationLinkAsync(string serverUrl)
    {
        try
        {
            // Skip half-typed URLs like "https://" — Uri.TryCreate would
            // accept them but the request is guaranteed to fail.
            if (!Uri.TryCreate(serverUrl, UriKind.Absolute, out var uri) ||
                string.IsNullOrWhiteSpace(uri.Host))
            {
                RegisterLink.Visibility = Visibility.Collapsed;
                return;
            }

            var app = Application.Current as App;
            var serverInfoService = app?.Host?.Services?.GetService<IServerInfoService>();
            if (serverInfoService == null) return;

            var info = await serverInfoService.GetServerInfoAsync(serverUrl);
            RegisterLink.Visibility = info?.RegistrationEnabled == true
                ? Visibility.Visible
                : Visibility.Collapsed;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[Shell] RefreshRegistrationLinkAsync error: {ex.Message}");
            RegisterLink.Visibility = Visibility.Collapsed;
        }
    }

    private void LoginInput_KeyDown(object sender, Microsoft.UI.Xaml.Input.KeyRoutedEventArgs e)
    {
        if (e.Key == Windows.System.VirtualKey.Enter)
        {
            e.Handled = true;
            LoginButton_Click(LoginButton, new RoutedEventArgs());
        }
    }

    private async void LoginButton_Click(object sender, RoutedEventArgs e)
    {
        var serverUrl = LoginServerUrl.Text?.Trim();
        var email = LoginEmail.Text?.Trim();
        var password = LoginPassword.Password;

        if (string.IsNullOrWhiteSpace(serverUrl) || string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
        {
            LoginErrorText.Text = "Please fill in all fields";
            LoginErrorBanner.Visibility = Visibility.Visible;
            return;
        }

        if (!serverUrl.StartsWith("http://", StringComparison.OrdinalIgnoreCase) &&
            !serverUrl.StartsWith("https://", StringComparison.OrdinalIgnoreCase))
        {
            serverUrl = "https://" + serverUrl;
        }

        serverUrl = serverUrl.TrimEnd('/');

        // If the user typed the URL and clicked Login without leaving the
        // field first, LostFocus may not have fired — refresh the registration
        // link visibility now (fire-and-forget; do not block login).
        _ = RefreshRegistrationLinkAsync(serverUrl);

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
            var tokenStorage = app.Host.Services.GetRequiredService<ITokenStorageService>();

            var credentials = new Dictionary<string, string>
            {
                ["Email"] = email,
                ["Password"] = password,
                ["ServerUrl"] = serverUrl
            };

            var success = await auth.LoginAsync(dispatcher: null, credentials);

            if (success)
            {
                await tokenStorage.SetRememberMeAsync(LoginRememberMe.IsChecked == true);
                shellModel.UpdateAuthenticationState(true);

                if (tenantContext.AvailableTenants.Count == 0)
                {
                    shellModel.UpdateNoTenantsState(true);
                }
                else
                {
                    var navigator = Splash.Navigator() ?? app.Host.Services.GetService<INavigator>();

                    if (navigator != null)
                    {
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
            LoginErrorText.Text = ex.CombinedMessage;
            LoginErrorBanner.Visibility = Visibility.Visible;
        }
        catch (Exception ex)
        {
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

    #region Registration Overlay

    private void RegisterLink_Click(Microsoft.UI.Xaml.Documents.Hyperlink sender, Microsoft.UI.Xaml.Documents.HyperlinkClickEventArgs args)
    {
        ResetRegistrationOverlay();
        LoginOverlay.Visibility = Visibility.Collapsed;
        RegistrationOverlay.Visibility = Visibility.Visible;
    }

    private void ResetRegistrationOverlay()
    {
        RegisterFirstname.Text = string.Empty;
        RegisterLastname.Text = string.Empty;
        RegisterEmail.Text = string.Empty;
        RegisterPassword.Password = string.Empty;
        RegisterPasswordConfirm.Password = string.Empty;
        RegisterErrorBanner.Visibility = Visibility.Collapsed;
        RegisterErrorText.Text = string.Empty;
        RegisterProgress.Visibility = Visibility.Collapsed;
        RegisterProgress.IsActive = false;
        RegisterSubmitButton.IsEnabled = true;
        RegisterCancelButton.IsEnabled = true;
    }

    private void RegisterCancel_Click(object sender, RoutedEventArgs e)
    {
        RegistrationOverlay.Visibility = Visibility.Collapsed;
        LoginOverlay.Visibility = Visibility.Visible;
    }

    private async void RegisterSubmit_Click(object sender, RoutedEventArgs e)
    {
        var firstname = RegisterFirstname.Text?.Trim();
        var lastname = RegisterLastname.Text?.Trim();
        var email = RegisterEmail.Text?.Trim();
        var password = RegisterPassword.Password;
        var passwordConfirm = RegisterPasswordConfirm.Password;

        if (string.IsNullOrWhiteSpace(firstname) || string.IsNullOrWhiteSpace(lastname) ||
            string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
        {
            RegisterErrorText.Text = "Bitte füllen Sie alle Felder aus.";
            RegisterErrorBanner.Visibility = Visibility.Visible;
            return;
        }

        if (password != passwordConfirm)
        {
            RegisterErrorText.Text = "Die Passwörter stimmen nicht überein.";
            RegisterErrorBanner.Visibility = Visibility.Visible;
            return;
        }

        var serverUrl = maERP.Client.Core.Configuration.RuntimeConfig.IsServerUrlRestricted
            ? maERP.Client.Core.Configuration.RuntimeConfig.RestrictServerUrl!
            : LoginServerUrl.Text?.Trim();

        if (string.IsNullOrWhiteSpace(serverUrl))
        {
            RegisterErrorText.Text = "Server-URL fehlt.";
            RegisterErrorBanner.Visibility = Visibility.Visible;
            return;
        }

        if (!serverUrl.StartsWith("http://", StringComparison.OrdinalIgnoreCase) &&
            !serverUrl.StartsWith("https://", StringComparison.OrdinalIgnoreCase))
        {
            serverUrl = "https://" + serverUrl;
        }
        serverUrl = serverUrl.TrimEnd('/');

        RegisterSubmitButton.IsEnabled = false;
        RegisterCancelButton.IsEnabled = false;
        RegisterProgress.Visibility = Visibility.Visible;
        RegisterProgress.IsActive = true;
        RegisterErrorBanner.Visibility = Visibility.Collapsed;

        try
        {
            var app = Application.Current as App;
            if (app?.Host?.Services == null)
            {
                throw new InvalidOperationException("Services not available");
            }

            var maErpAuth = app.Host.Services.GetRequiredService<IMaErpAuthenticationService>();
            var tenantContext = app.Host.Services.GetRequiredService<ITenantContextService>();
            var shellModel = app.Host.Services.GetRequiredService<ShellModel>();

            var request = new RegisterRequestDto
            {
                Firstname = firstname,
                Lastname = lastname,
                Email = email,
                Username = email,
                Password = password
            };

            var response = await maErpAuth.RegisterAsync(serverUrl, request);

            if (response?.Succeeded == true && !string.IsNullOrEmpty(response.Token))
            {
                shellModel.UpdateAuthenticationState(true);

                if (tenantContext.AvailableTenants.Count == 0)
                {
                    shellModel.UpdateNoTenantsState(true);
                }
                else
                {
                    var navigator = Splash.Navigator() ?? app.Host.Services.GetService<INavigator>();
                    if (navigator != null)
                    {
                        await navigator.NavigateViewModelAsync<DashboardModel>(this, qualifier: Qualifiers.ClearBackStack);
                    }
                }
            }
            else
            {
                RegisterErrorText.Text = response?.Message ?? "Registrierung fehlgeschlagen.";
                RegisterErrorBanner.Visibility = Visibility.Visible;
            }
        }
        catch (ApiException ex)
        {
            RegisterErrorText.Text = ex.CombinedMessage;
            RegisterErrorBanner.Visibility = Visibility.Visible;
        }
        catch (Exception ex)
        {
            RegisterErrorText.Text = ex.Message;
            RegisterErrorBanner.Visibility = Visibility.Visible;
        }
        finally
        {
            RegisterProgress.Visibility = Visibility.Collapsed;
            RegisterProgress.IsActive = false;
            RegisterSubmitButton.IsEnabled = true;
            RegisterCancelButton.IsEnabled = true;
        }
    }

    #endregion

    #region First Tenant Creation

    private void FirstTenantName_TextChanged(object sender, TextChangedEventArgs e)
    {
        FirstTenantSaveButton.IsEnabled = !string.IsNullOrWhiteSpace(FirstTenantName.Text);
    }

    private async void FirstTenantCancel_Click(object sender, RoutedEventArgs e)
    {
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
        var tenantName = FirstTenantName.Text?.Trim();
        if (string.IsNullOrWhiteSpace(tenantName)) return;

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

            var input = new TenantInputDto
            {
                Name = tenantName,
                Description = FirstTenantDescription.Text?.Trim()
            };

            var newTenantId = await tenantService.CreateTenantAsync(input);

            await tenantContext.RefreshTokenAndTenantsAsync();

            if (newTenantId != Guid.Empty)
            {
                await tenantContext.SetCurrentTenantAsync(newTenantId);
            }

            shellModel.UpdateNoTenantsState(false);

            var navigator = Splash.Navigator() ?? app.Host.Services.GetService<INavigator>();

            if (navigator != null)
            {
                await navigator.NavigateViewModelAsync<DashboardModel>(this, qualifier: Qualifiers.ClearBackStack);
            }
        }
        catch (ApiException ex)
        {
            FirstTenantErrorText.Text = ex.CombinedMessage;
            FirstTenantErrorBanner.Visibility = Visibility.Visible;
        }
        catch (Exception ex)
        {
            FirstTenantErrorText.Text = ex.Message;
            FirstTenantErrorBanner.Visibility = Visibility.Visible;
        }
        finally
        {
            FirstTenantProgress.Visibility = Visibility.Collapsed;
            FirstTenantProgress.IsActive = false;
            FirstTenantSaveButton.IsEnabled = !string.IsNullOrWhiteSpace(FirstTenantName.Text);
            FirstTenantCancelButton.IsEnabled = true;
        }
    }

    #endregion
}

internal static class ApplicationThemeExtensions
{
    public static ElementTheme ToElementTheme(this Microsoft.UI.Xaml.ApplicationTheme theme)
        => theme == Microsoft.UI.Xaml.ApplicationTheme.Dark ? ElementTheme.Dark : ElementTheme.Light;
}
