using maERP.Client.Features.Account.Models;
using Microsoft.UI.Xaml.Controls;

namespace maERP.Client.Features.Account.Views;

public sealed partial class AccountPage : Page
{
    public AccountPage()
    {
        this.InitializeComponent();
    }

    private async void SaveProfileButton_Click(object sender, RoutedEventArgs e)
    {
        if (DataContext is AccountModel model)
        {
            await model.SaveProfileAsync();
        }
    }

    private async void ChangePasswordButton_Click(object sender, RoutedEventArgs e)
    {
        if (DataContext is AccountModel model)
        {
            await model.ChangePasswordAsync();
        }
    }
}
