using maERP.Client.Features.Tenants.Models;
using Microsoft.UI.Xaml.Controls;

namespace maERP.Client.Features.Tenants.Views;

public sealed partial class TenantDetailPage : Page
{
    public TenantDetailPage()
    {
        this.InitializeComponent();
    }

    private async void BackButton_Click(object sender, RoutedEventArgs e)
    {
        if (DataContext is TenantDetailModel model)
        {
            await model.GoBack();
        }
    }

    private async void EditButton_Click(object sender, RoutedEventArgs e)
    {
        if (DataContext is TenantDetailModel model)
        {
            await model.EditTenant();
        }
    }

    private async void GenerateDemoDataButton_Click(object sender, RoutedEventArgs e)
    {
        if (sender is Button button &&
            button.Tag is string tenantName &&
            DataContext is TenantDetailModel model)
        {
            await model.NavigateToDemoDataGenerator(tenantName);
        }
    }

    private void ClearTenantButton_Click(object sender, RoutedEventArgs e)
    {
        // TODO: Implement clear tenant functionality
    }

    private async void DeleteTenantButton_Click(object sender, RoutedEventArgs e)
    {
        if (DataContext is not TenantDetailModel model)
        {
            return;
        }

        var dialog = new ContentDialog
        {
            Title = "Delete Tenant",
            Content = "Are you sure you want to delete this tenant? This action cannot be undone.",
            PrimaryButtonText = "Delete",
            CloseButtonText = "Cancel",
            DefaultButton = ContentDialogButton.Close,
            XamlRoot = this.XamlRoot
        };

        var result = await dialog.ShowAsync();

        if (result == ContentDialogResult.Primary)
        {
            await model.DeleteTenant();
        }
    }
}
