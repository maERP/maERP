#nullable disable

using System.ComponentModel;
using System.Runtime.CompilerServices;
using maERP.Client.Contracts;
using maERP.Data.Dtos.Product;

namespace maERP.Client.ViewModels
{
    public class ProductsDetailViewModel : BaseViewModel
    {
        readonly INavigationService _navigationService;
        readonly IDataService<ProductDto> _dataService;

        public ProductsDetailViewModel(INavigationService navigationService, IDataService<ProductDto> dataService)
        {
            this._navigationService = navigationService;
            this._dataService = dataService;
        }

        public async Task<ProductDto> GetProduct()
        {
            var product = await _dataService.Request("GET", "/Products/1");

            return product;
        }
    }
}
