using maERP.Client.ViewModels;
using maERP.Data.Dtos.Product;

namespace maERP.Client.Views;

public partial class ProductsPage : ContentPage
{   
    public ProductsPage(ProductsViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}