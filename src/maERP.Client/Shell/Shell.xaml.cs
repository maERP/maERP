namespace maERP.Client.Shell;

public sealed partial class Shell : UserControl, IContentControlProvider
{
    private INavigator? _navigator;

    public Shell()
    {
        this.InitializeComponent();
    }

    public ContentControl ContentControl => Splash;

    private void NavView_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
    {
        if (_navigator is null)
        {
            // Get navigator from service provider
            _navigator = (this.DataContext as ShellModel)?.GetNavigator();
        }

        if (args.IsSettingsSelected)
        {
            // TODO: Navigate to settings page when implemented
            return;
        }

        if (args.SelectedItem is NavigationViewItem item && item.Tag is string tag)
        {
            // Navigate based on tag
            NavigateToPage(tag);
        }
    }

    private async void NavigateToPage(string tag)
    {
        if (_navigator is null)
        {
            return;
        }

        try
        {
            switch (tag)
            {
                case "Main":
                    await _navigator.NavigateRouteAsync(this, "Main");
                    break;
                case "Customers":
                    await _navigator.NavigateRouteAsync(this, "Customers");
                    break;
                case "Products":
                    await _navigator.NavigateRouteAsync(this, "Products");
                    break;
                case "Orders":
                    await _navigator.NavigateRouteAsync(this, "Orders");
                    break;
                case "Invoices":
                    await _navigator.NavigateRouteAsync(this, "Invoices");
                    break;
                case "Warehouse":
                    await _navigator.NavigateRouteAsync(this, "Warehouses");
                    break;
                case "Manufacturers":
                    await _navigator.NavigateRouteAsync(this, "Manufacturers");
                    break;
                case "SalesChannels":
                    await _navigator.NavigateRouteAsync(this, "SalesChannels");
                    break;
                case "Statistics":
                    await _navigator.NavigateRouteAsync(this, "Statistics");
                    break;
            }
        }
        catch (Exception ex)
        {
            // Log navigation error
            System.Diagnostics.Debug.WriteLine($"Navigation error: {ex.Message}");
        }
    }
}
