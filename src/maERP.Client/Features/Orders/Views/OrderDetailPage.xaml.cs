using maERP.Client.Features.Orders.Models;
using Microsoft.UI.Xaml.Controls;

namespace maERP.Client.Features.Orders.Views;

public sealed partial class OrderDetailPage : Page
{
    public OrderDetailPage()
    {
        this.InitializeComponent();
    }

    private async void BackButton_Click(object sender, RoutedEventArgs e)
    {
        if (DataContext is OrderDetailModel model)
        {
            await model.GoBack();
        }
    }

    private async void EditButton_Click(object sender, RoutedEventArgs e)
    {
        if (DataContext is OrderDetailModel model)
        {
            await model.EditOrder();
        }
    }
}
