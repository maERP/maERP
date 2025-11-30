using maERP.Client.Features.Products.Models;
using Microsoft.UI.Xaml.Controls;

namespace maERP.Client.Features.Products.Views;

public sealed partial class ProductEditPage : Page
{
    public ProductEditPage()
    {
        this.InitializeComponent();
    }

    private async void CancelButton_Click(object sender, RoutedEventArgs e)
    {
        if (DataContext is ProductEditModel model)
        {
            await model.CancelAsync();
        }
    }

    private async void SaveButton_Click(object sender, RoutedEventArgs e)
    {
        if (DataContext is ProductEditModel model)
        {
            await model.SaveAsync();
        }
    }
}
