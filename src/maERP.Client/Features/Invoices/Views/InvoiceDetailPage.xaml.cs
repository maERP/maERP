using maERP.Client.Features.Invoices.Models;
using Microsoft.UI.Xaml.Controls;

namespace maERP.Client.Features.Invoices.Views;

public sealed partial class InvoiceDetailPage : Page
{
    public InvoiceDetailPage()
    {
        this.InitializeComponent();
    }

    private async void BackButton_Click(object sender, RoutedEventArgs e)
    {
        if (DataContext is InvoiceDetailModel model)
        {
            await model.GoBack();
        }
    }
}
