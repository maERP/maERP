using maERP.Client.ViewModels;
using maERP.Data.Dtos.Product;

namespace maERP.Client.Views;

public partial class ProductsPage : ContentPage
{
    private readonly ProductsViewModel _viewModel;
    public ICollection<GetProductDto> Products;
    
    public ProductsPage(ProductsViewModel viewModel)
    {
        _viewModel = viewModel;
        BindingContext = viewModel;

        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        GetProducts();
    }

    public async void GetProducts()
    {
        Products = await _viewModel.GetProductList();
        productsListView.ItemsSource = Products;
    }

    void productsListView_ItemSelected(System.Object sender, Microsoft.Maui.Controls.SelectedItemChangedEventArgs e)
    {
        var selectedProduct = productsListView.SelectedItem as GetProductDto;

        if(selectedProduct != null)
        {
            Navigation.PushAsync(new ProductsDetailPage(selectedProduct.Id));
        }
    }
}