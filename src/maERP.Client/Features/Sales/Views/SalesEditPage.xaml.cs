using maERP.Client.Features.Saless.Models;
using Microsoft.UI.Xaml.Controls;

namespace maERP.Client.Features.Saless.Views;

public sealed partial class SalesEditPage : Page
{
    public SalesEditPage()
    {
        this.InitializeComponent();
    }

    private async void BackButton_Click(object sender, RoutedEventArgs e)
    {
        if (DataContext is SalesEditModel model)
        {
            await model.CancelAsync();
        }
    }

    private async void CancelButton_Click(object sender, RoutedEventArgs e)
    {
        if (DataContext is SalesEditModel model)
        {
            await model.CancelAsync();
        }
    }

    private async void SaveButton_Click(object sender, RoutedEventArgs e)
    {
        if (DataContext is SalesEditModel model)
        {
            await model.SaveAsync();
        }
    }

    private void CopyToInvoiceButton_Click(object sender, RoutedEventArgs e)
    {
        if (DataContext is SalesEditModel model)
        {
            model.CopyDeliveryToInvoiceAddress();
        }
    }

    private void CopyToDeliveryButton_Click(object sender, RoutedEventArgs e)
    {
        if (DataContext is SalesEditModel model)
        {
            model.CopyInvoiceToDeliveryAddress();
        }
    }
}
