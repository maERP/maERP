using maERP.Client.Features.Superadmin.Models;
using maERP.Domain.Dtos.Superadmin;

namespace maERP.Client.Features.Superadmin.Views;

public sealed partial class SuperadminTenantEditPage : Page
{
    public SuperadminTenantEditPage()
    {
        this.InitializeComponent();
    }

    private async void CancelButton_Click(object sender, RoutedEventArgs e)
    {
        if (DataContext is SuperadminTenantEditModel model)
        {
            await model.CancelAsync();
        }
    }

    private async void SaveButton_Click(object sender, RoutedEventArgs e)
    {
        if (DataContext is SuperadminTenantEditModel model)
        {
            await model.SaveAsync();
        }
    }

    private async void RemoveUserButton_Click(object sender, RoutedEventArgs e)
    {
        if (sender is Button button &&
            button.Tag is SuperadminTenantUserDto user &&
            DataContext is SuperadminTenantEditModel model)
        {
            await model.RemoveUserAsync(user);
        }
    }

    private async void AddUserButton_Click(object sender, RoutedEventArgs e)
    {
        if (DataContext is SuperadminTenantEditModel model)
        {
            await model.LoadAvailableUsersAsync();
        }
    }

    private void CancelAddUserFlyout_Click(object sender, RoutedEventArgs e)
    {
        AddUserFlyout.Hide();
    }

    private async void ConfirmAddUser_Click(object sender, RoutedEventArgs e)
    {
        if (DataContext is SuperadminTenantEditModel model)
        {
            await model.AddSelectedUserAsync();
            AddUserFlyout.Hide();
        }
    }
}
