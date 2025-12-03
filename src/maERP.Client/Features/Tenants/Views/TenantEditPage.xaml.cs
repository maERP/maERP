using maERP.Client.Features.Tenants.Models;

namespace maERP.Client.Features.Tenants.Views;

public sealed partial class TenantEditPage : Page
{
    public TenantEditPage()
    {
        this.InitializeComponent();
    }

    private async void CancelButton_Click(object sender, RoutedEventArgs e)
    {
        if (DataContext is TenantEditModel model)
        {
            await model.CancelAsync();
        }
    }

    private async void SaveButton_Click(object sender, RoutedEventArgs e)
    {
        if (DataContext is TenantEditModel model)
        {
            await model.SaveAsync();
        }
    }

    private void AddUserButton_Click(object sender, RoutedEventArgs e)
    {
        if (DataContext is TenantEditModel model)
        {
            model.OpenAddUserOverlay();
        }
    }

    private void CloseOverlayButton_Click(object sender, RoutedEventArgs e)
    {
        if (DataContext is TenantEditModel model)
        {
            model.CloseAddUserOverlay();
        }
    }

    private async void SearchUserButton_Click(object sender, RoutedEventArgs e)
    {
        if (DataContext is TenantEditModel model)
        {
            await model.SearchUserAsync();
        }
    }

    private async void AddUserToTenantButton_Click(object sender, RoutedEventArgs e)
    {
        if (DataContext is TenantEditModel model)
        {
            await model.AddUserToTenantAsync();
        }
    }
}
