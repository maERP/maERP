using maERP.Client.Features.Orders.Models;
using Microsoft.UI.Xaml.Controls;

namespace maERP.Client.Features.Orders.Views;

public sealed partial class OrderEditPage : Page
{
    public OrderEditPage()
    {
        this.InitializeComponent();
    }

    private async void BackButton_Click(object sender, RoutedEventArgs e)
    {
        if (DataContext is OrderEditModel model)
        {
            await model.CancelAsync();
        }
    }

    private async void CancelButton_Click(object sender, RoutedEventArgs e)
    {
        if (DataContext is OrderEditModel model)
        {
            await model.CancelAsync();
        }
    }

    private async void SaveButton_Click(object sender, RoutedEventArgs e)
    {
        if (DataContext is OrderEditModel model)
        {
            await model.SaveAsync();
        }
    }

    private void CopyToInvoiceButton_Click(object sender, RoutedEventArgs e)
    {
        if (DataContext is OrderEditModel model)
        {
            model.CopyDeliveryToInvoiceAddress();
        }
    }

    private void CopyToDeliveryButton_Click(object sender, RoutedEventArgs e)
    {
        if (DataContext is OrderEditModel model)
        {
            model.CopyInvoiceToDeliveryAddress();
        }
    }
}
