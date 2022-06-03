using maERP.Client.Contracts;
using maERP.Data.Dtos.User;

namespace maERP.Client.ViewModels
{
	public class LoginViewModel : ViewModelBase
	{
        readonly IDataService<LoginResponseDto> _dataService;

        public LoginViewModel(IDataService<LoginResponseDto> dataService)
		{
            _dataService = dataService;
        }

        public async Task<bool> Login(string server, string username, string password)
        {
            return await _dataService.Login(server, username, password);
        }
    }
}