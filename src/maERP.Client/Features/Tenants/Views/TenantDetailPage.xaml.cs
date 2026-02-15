using maERP.Client.Features.Tenants.Models;
using Microsoft.UI.Xaml.Controls;

namespace maERP.Client.Features.Tenants.Views;

public sealed partial class TenantDetailPage : Page
{
    private TaskCompletionSource<bool>? _deleteConfirmationTcs;

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

    private Task<bool> ShowDeleteConfirmationAsync()
    {
        _deleteConfirmationTcs = new TaskCompletionSource<bool>();
        DeleteConfirmationOverlay.Visibility = Visibility.Visible;
        return _deleteConfirmationTcs.Task;
    }

    private void DeleteConfirmationCancel_Click(object sender, RoutedEventArgs e)
    {
        DeleteConfirmationOverlay.Visibility = Visibility.Collapsed;
        _deleteConfirmationTcs?.TrySetResult(false);
    }

    private void DeleteConfirmationConfirm_Click(object sender, RoutedEventArgs e)
    {
        DeleteConfirmationOverlay.Visibility = Visibility.Collapsed;
        _deleteConfirmationTcs?.TrySetResult(true);
    }
}
