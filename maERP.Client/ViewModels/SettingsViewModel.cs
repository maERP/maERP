#nullable disable

using maERP.Client.Contracts;

namespace maERP.Client.ViewModels
{    
    public class SettingsViewModel : BaseViewModel
    {
        readonly INavigationService _navigationService;

        public SettingsViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }
    }
}