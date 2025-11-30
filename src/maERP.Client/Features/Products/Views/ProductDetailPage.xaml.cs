using maERP.Client.Features.Products.Models;
using Microsoft.UI.Xaml.Controls;

namespace maERP.Client.Features.Products.Views;

public sealed partial class ProductDetailPage : Page
{
    public ProductDetailPage()
    {
        this.InitializeComponent();
    }

    private async void BackButton_Click(object sender, RoutedEventArgs e)
    {
        if (DataContext is ProductDetailModel model)
        {
            await model.GoBack();
        }
    }

    private async void EditButton_Click(object sender, RoutedEventArgs e)
    {
        if (DataContext is ProductDetailModel model)
        {
            await model.EditProduct();
        }
    }
}
