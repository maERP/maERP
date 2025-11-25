namespace maERP.Client.Presentation;

public sealed partial class Shell : UserControl, IContentControlProvider
{
    public Shell()
    {
        this.InitializeComponent();
        NavView.SelectionChanged += OnNavigationViewSelectionChanged;
        TabBarNav.SelectionChanged += OnTabBarSelectionChanged;
        this.Loaded += OnShellLoaded;
    }

    public ContentControl ContentControl => Splash;

    private async void OnShellLoaded(object sender, RoutedEventArgs e)
    {
        System.Diagnostics.Debug.WriteLine("[Shell] Loaded event fired");

        // Get ShellModel from the service provider and set as DataContext
        try
        {
            var app = Application.Current as App;
            if (app?.Host?.Services != null)
            {
                var shellModel = app.Host.Services.GetRequiredService<ShellModel>();
                System.Diagnostics.Debug.WriteLine($"[Shell] Got ShellModel from DI. IsAuthenticated: {shellModel.IsAuthenticated}");

                // IMPORTANT: Set DataContext ONLY on NavView and TabBarNav, NOT on the Shell itself
                // This allows child pages to have their own DataContext set by navigation
                NavView.DataContext = shellModel;
                TabBarNav.DataContext = shellModel;

                System.Diagnostics.Debug.WriteLine($"[Shell] DataContext set. AuthenticatedVisibility: {shellModel.AuthenticatedVisibility}, NotAuthenticatedVisibility: {shellModel.NotAuthenticatedVisibility}");

                // Subscribe to property changes
                shellModel.PropertyChanged += OnShellModelPropertyChanged;

                await shellModel.InitializeAuthenticationState();

                // Force update visibility
                UpdateNavigationVisibility(shellModel);

                System.Diagnostics.Debug.WriteLine($"[Shell] After InitializeAuthenticationState. IsAuthenticated: {shellModel.IsAuthenticated}");
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("[Shell] FAILED to get Host or Services");
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"[Shell] Exception getting ShellModel: {ex.Message}");
        }
    }

    private void OnShellModelPropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(ShellModel.IsAuthenticated) ||
            e.PropertyName == nameof(ShellModel.AuthenticatedVisibility) ||
            e.PropertyName == nameof(ShellModel.NotAuthenticatedVisibility))
        {
            System.Diagnostics.Debug.WriteLine($"[Shell] Property changed: {e.PropertyName}");
            if (DataContext is ShellModel model)
            {
                UpdateNavigationVisibility(model);
            }
        }
    }

    private void UpdateNavigationVisibility(ShellModel model)
    {
        System.Diagnostics.Debug.WriteLine($"[Shell] UpdateNavigationVisibility - IsAuthenticated: {model.IsAuthenticated}");

        var authenticatedVisibility = model.AuthenticatedVisibility;
        var notAuthenticatedVisibility = model.NotAuthenticatedVisibility;

        System.Diagnostics.Debug.WriteLine($"[Shell] Authenticated: {authenticatedVisibility}, NotAuthenticated: {notAuthenticatedVisibility}");

        // Update NavigationView items
        foreach (var item in NavView.MenuItems)
        {
            if (item is NavigationViewItem navItem && navItem.Tag is string tag)
            {
                if (tag == "Login")
                {
                    navItem.Visibility = notAuthenticatedVisibility;
                    System.Diagnostics.Debug.WriteLine($"[Shell] Login item visibility: {navItem.Visibility}");
                }
                else
                {
                    navItem.Visibility = authenticatedVisibility;
                    System.Diagnostics.Debug.WriteLine($"[Shell] {tag} item visibility: {navItem.Visibility}");
                }
            }
            else if (item is NavigationViewItemSeparator separator)
            {
                separator.Visibility = authenticatedVisibility;
            }
            else if (item is NavigationViewItemHeader header)
            {
                header.Visibility = authenticatedVisibility;
            }
        }

        // Update footer items
        foreach (var item in NavView.FooterMenuItems)
        {
            if (item is NavigationViewItem navItem)
            {
                navItem.Visibility = authenticatedVisibility;
            }
        }

        // Update TabBar items
        foreach (var item in TabBarNav.Items)
        {
            if (item is Uno.Toolkit.UI.TabBarItem tabItem && tabItem.Tag is string tag)
            {
                if (tag == "Login")
                {
                    tabItem.Visibility = notAuthenticatedVisibility;
                }
                else
                {
                    tabItem.Visibility = authenticatedVisibility;
                }
            }
        }
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
            if (DataContext is ShellModel model)
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
            if (DataContext is ShellModel model)
            {
                await model.NavigateToPage(tag);
                await RefreshAuthenticationState(model);
            }
        }
    }
}
