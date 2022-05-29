using maERP.Client.Contracts.Services;
using maERP.Client.Views;

namespace maERP.Client;

public partial class App : Application
{
	public App(INavigationService navigationService)
	{
		InitializeComponent();

        MainPage = new AppShell();

        navigationService.NavigateToPage<LoginPage>();
    }
}