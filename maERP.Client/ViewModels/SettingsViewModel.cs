#nullable disable

using maERP.Client.Contracts.Services;

namespace maERP.Client.ViewModels
{    
    public class SettingsViewModel : ViewModelBase
    {
        readonly INavigationService _navigationService;

        public SettingsViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }
    }
}
