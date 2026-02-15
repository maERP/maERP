using maERP.Client.Features.Tenants.Models;
using Microsoft.UI.Xaml.Controls;
using Windows.ApplicationModel.Resources;

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

        var confirmed = await ShowDeleteConfirmationAsync();
        if (confirmed)
        {
            await model.DeleteTenant();
        }
    }

    private async Task<bool> ShowDeleteConfirmationAsync()
    {
        var resourceLoader = ResourceLoader.GetForViewIndependentUse();

        var dialog = new ContentDialog
        {
            Title = resourceLoader.GetString("TenantDetailPage.DeleteConfirmation.Title"),
            Content = resourceLoader.GetString("TenantDetailPage.DeleteConfirmation.Content"),
            PrimaryButtonText = resourceLoader.GetString("Common.Delete"),
            CloseButtonText = resourceLoader.GetString("Common.Cancel"),
            DefaultButton = ContentDialogButton.Close,
            XamlRoot = this.XamlRoot,
            Style = Application.Current.Resources["DefaultContentDialogStyle"] as Style
        };

        var result = await dialog.ShowAsync();
        return result == ContentDialogResult.Primary;
    }
}
