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
}
