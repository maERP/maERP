#nullable disable

using System.ComponentModel;
using System.Runtime.CompilerServices;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using maERP.Client.Contracts;
using maERP.Data.Dtos.Product;

namespace maERP.Client.ViewModels;

[QueryProperty(nameof(QueryProduct), "QueryProduct")]
[QueryProperty(nameof(Product), "Product")]
public partial class ProductsDetailViewModel : BaseViewModel
{
    [ObservableProperty]
    GetProductDto queryProduct;

    [ObservableProperty]
    ProductDto product;

    private readonly IDataService<ProductDto> _dataService;

    public ProductsDetailViewModel(IDataService<ProductDto> dataService)
    {
        this._dataService = dataService;
        /*
        fullproduct.Id = product.Id;
        fullproduct.SKU = product.SKU;
        fullproduct.Name = product.Name;
        fullproduct.Price = product.Price;
        */
    }

    [ICommand]
    public async Task GetProductDetailAsync()
    {
        if (IsBusy)
            return;

        try
        {
            IsBusy = true;
            Product = await _dataService.Request("GET", "/Products/" + QueryProduct.Id);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Unable to get Product Details: {ex.Message}");
            await Shell.Current.DisplayAlert("Error!", ex.Message, "OK");
        }
        finally
        {
            IsBusy = false;
        }
    }
}