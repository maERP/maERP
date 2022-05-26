using maERP.Client.ViewModels;

namespace maERP.Client.Views;

public partial class ThirdPage : ContentPage
{
    public ThirdPage(ProductsViewModel viewModel)
    {
        BindingContext = viewModel;
        InitializeComponent();
    }
}