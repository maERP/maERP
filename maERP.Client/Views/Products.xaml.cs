using maERP.Client.ViewModels;

namespace maERP.Client.Views;

public partial class ProductsPage : ContentPage
{
    public ProductsPage(ProductsViewModel viewModel)
    {
        BindingContext = viewModel;

        InitializeComponent();
    }
}