#nullable disable

using maERP.Client.Contracts;

namespace maERP.Client.ViewModels
{
    public class DashboardModel : BaseViewModel
    {
        readonly INavigationService _navigationService;

        public DashboardModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }
    }
}