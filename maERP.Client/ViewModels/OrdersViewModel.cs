#nullable disable

using maERP.Client.Contracts;

namespace maERP.Client.ViewModels
{
    public class OrdersViewModel : ViewModelBase
    {
        readonly INavigationService _navigationService;

        public OrdersViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }
    }
}
