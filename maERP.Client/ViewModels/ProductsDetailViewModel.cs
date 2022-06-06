#nullable disable

using System.ComponentModel;
using System.Runtime.CompilerServices;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using maERP.Client.Contracts;
using maERP.Data.Dtos.Product;

namespace maERP.Client.ViewModels;

[QueryProperty(nameof(Product), "Product")]
public partial class ProductsDetailViewModel : BaseViewModel
{
    [ObservableProperty]
    GetProductDto product;

    private readonly IDataService<ProductDto> _dataService;

    public ProductsDetailViewModel(IDataService<ProductDto> dataService)
    {
        this._dataService = dataService;
    }

    [ICommand]
    async Task GetProductDetailAsync()
    {
        Console.WriteLine("DEBUG 100");

        if (IsBusy)
            return;

        try
        {
            IsBusy = true;
            // Product = await _dataService.Request("GET", "/Product/" + Product.Id);

            Product.SKU = "20002";
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
