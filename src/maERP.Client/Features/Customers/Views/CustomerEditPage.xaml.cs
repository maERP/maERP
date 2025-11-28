using maERP.Client.Features.Customers.Models;

namespace maERP.Client.Features.Customers.Views;

public sealed partial class CustomerEditPage : Page
{
    public CustomerEditPage()
    {
        this.InitializeComponent();
    }

    private async void CancelButton_Click(object sender, RoutedEventArgs e)
    {
        if (DataContext is CustomerEditModel model)
        {
            await model.CancelAsync();
        }
    }

    private async void SaveButton_Click(object sender, RoutedEventArgs e)
    {
        if (DataContext is CustomerEditModel model)
        {
            await model.SaveAsync();
        }
    }
}
