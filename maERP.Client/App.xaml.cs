using maERP.Client.Contracts;
using maERP.Client.Views;
using maERP.Client.ViewModels;

namespace maERP.Client;

public partial class App : Application
{
	public App(INavigationService navigationService, LoginViewModel viewModel)
	{
		InitializeComponent();

        MainPage = new LoginPage(viewModel);
    }
}