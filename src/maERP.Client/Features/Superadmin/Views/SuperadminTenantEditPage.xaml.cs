using maERP.Client.Features.Superadmin.Models;

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
}
