using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Input;
using maERP.Client.Contracts;
using maERP.Data.Dtos.Product;

namespace maERP.Client.ViewModels
{
    public class ProductsViewModel : BaseViewModel
    {
        public ObservableCollection<GetProductDto> Products { get; } = new();
        IDataService<ICollection<GetProductDto>> _dataService;

        public ProductsViewModel(IDataService<ICollection<GetProductDto>> dataService)
        {
            Title = "Artikel";
            _dataService = dataService;
        }

        [ICommand]
        async Task GetProductsAsync()
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
                Console.WriteLine($"Unable to get monkeys: {ex.Message}");
                await Shell.Current.DisplayAlert("Error!", ex.Message, "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}