using maERP.Client.Features.Customers.Models;

namespace maERP.Client.Features.Customers.Views;

public sealed partial class CustomerDetailPage : Page
{
    public CustomerDetailPage()
    {
        this.InitializeComponent();
    }

    private async void BackButton_Click(object sender, RoutedEventArgs e)
    {
        if (DataContext is CustomerDetailModel model)
        {
            await model.GoBack();
        }
    }

    private async void EditButton_Click(object sender, RoutedEventArgs e)
    {
        if (DataContext is CustomerDetailModel model)
        {
            await model.EditCustomer();
        }
    }

    private async void DeleteButton_Click(object sender, RoutedEventArgs e)
    {
        if (DataContext is CustomerDetailModel model)
        {
            await model.DeleteCustomer(CancellationToken.None);
        }
    }
}
