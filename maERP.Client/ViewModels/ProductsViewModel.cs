using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Input;
using maERP.Client.Contracts;
using maERP.Data.Dtos.Product;
using maERP.Client.Views;
using CommunityToolkit.Mvvm.ComponentModel;

namespace maERP.Client.ViewModels
{
    public partial class ProductsViewModel : BaseViewModel
    {
        [ObservableProperty]
        bool isRefreshing;

        public ObservableCollection<GetProductDto> Products { get; } = new();
        IDataService<ICollection<GetProductDto>> _dataService;

        public ProductsViewModel(IDataService<ICollection<GetProductDto>> dataService)
        {
            Title = "Artikel";
            _dataService = dataService;
        }

        [ICommand]
        public async Task GetProductsAsync()
        {
            if (IsBusy)
                return;
            
            try
            {
                IsBusy = true;
                var products = await _dataService.Request("GET", "/Products/GetAll");

                if (Products.Count != 0)
                    Products.Clear();

                foreach (var product in products)
                    Products.Add(product);

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unable to get Products: {ex.Message}");
                await Shell.Current.DisplayAlert("Error!", ex.Message, "OK");
            }
            finally
            {
                IsBusy = false;
                IsRefreshing = false;
            }
        }

        [ICommand]
        async Task GoToDetails(GetProductDto product)
        {
            if (product == null)
                return;

            await Shell.Current.GoToAsync(nameof(maERP.Client.Views.ProductsDetailPage), true, new Dictionary<string, object>
            {
                {"QueryProduct", product },
                {"Fullproduct", new ProductDto() }
            });
        }
    }
}