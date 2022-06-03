#nullable disable

using maERP.Client.Contracts;

namespace maERP.Client.ViewModels
{
    public class ProductsDetailViewModel : ViewModelBase
    {
        readonly INavigationService _navigationService;

        public ProductsDetailViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }
    }
}
