using maERP.Client.Contracts;
using maERP.Client.Views;
using maERP.Client.ViewModels;

namespace maERP.Client;

public partial class App : Application
{
	public App(LoginViewModel viewModel)
	{
		InitializeComponent();

        // MainPage = new LoginPage(viewModel);
        MainPage = new AppShell();
    }
}