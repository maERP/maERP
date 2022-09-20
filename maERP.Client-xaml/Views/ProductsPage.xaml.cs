using maERP.Client.ViewModels;
using maERP.Data.Dtos.Product;

namespace maERP.Client.Views;

public partial class ProductsPage : ContentPage
{
    private readonly ProductsViewModel _viewModel;

    public ProductsPage(ProductsViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
        this._viewModel = viewModel;
        Title = "Artikelübersicht";        
    }

    protected async override void OnAppearing()
    {
        base.OnAppearing();

        await _viewModel.GetProductsAsync();
    }
}