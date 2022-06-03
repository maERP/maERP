using maERP.Client.Contracts;
using maERP.Data.Dtos.Product;

namespace maERP.Client.ViewModels
{    
    public class ProductsViewModel : ViewModelBase
    {
        readonly INavigationService _navigationService;
        readonly IDataService<ICollection<GetProductDto>> _dataService;

        public ProductsViewModel(INavigationService navigationService, IDataService<ICollection<GetProductDto>> dataService)
        {
            _navigationService = navigationService;
            _dataService = dataService;

            Console.WriteLine("loaded");
        }

        public async Task<ICollection<GetProductDto>> GetProductList()
        {
            var productList =  await _dataService.Request("GET", "/Products/getAll");

            return productList;
        }
    }
}