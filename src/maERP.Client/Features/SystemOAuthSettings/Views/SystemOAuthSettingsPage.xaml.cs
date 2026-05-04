using maERP.Client.Features.SystemOAuthSettings.Models;
using Microsoft.UI.Xaml.Controls;

namespace maERP.Client.Features.SystemOAuthSettings.Views;

public sealed partial class SystemOAuthSettingsPage : Page
{
    public SystemOAuthSettingsPage()
    {
        this.InitializeComponent();
    }

    private async void BackButton_Click(object sender, RoutedEventArgs e)
    {
        if (DataContext is SystemOAuthSettingsModel model) await model.GoBackAsync();
    }

    private async void SaveButton_Click(object sender, RoutedEventArgs e)
    {
        if (DataContext is SystemOAuthSettingsModel model) await model.SaveAsync();
    }
}
