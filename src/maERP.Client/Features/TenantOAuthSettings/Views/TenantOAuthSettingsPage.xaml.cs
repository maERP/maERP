using maERP.Client.Features.TenantOAuthSettings.Models;
using Microsoft.UI.Xaml.Controls;

namespace maERP.Client.Features.TenantOAuthSettings.Views;

public sealed partial class TenantOAuthSettingsPage : Page
{
    public TenantOAuthSettingsPage()
    {
        this.InitializeComponent();
    }

    private async void BackButton_Click(object sender, RoutedEventArgs e)
    {
        if (DataContext is TenantOAuthSettingsModel model) await model.GoBackAsync();
    }

    private async void SaveButton_Click(object sender, RoutedEventArgs e)
    {
        if (DataContext is TenantOAuthSettingsModel model) await model.SaveAsync();
    }

    private async void DeleteButton_Click(object sender, RoutedEventArgs e)
    {
        if (DataContext is TenantOAuthSettingsModel model) await model.DeleteAsync();
    }
}
