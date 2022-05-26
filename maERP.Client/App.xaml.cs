using maERP.Client.Contracts.Services;

namespace maERP.Client;

public partial class App : Application
{
	public App(INavigationService navigationService)
	{
		InitializeComponent();

        MainPage = new AppShell();
        navigationService.NavigateToMainPage();
    }
}