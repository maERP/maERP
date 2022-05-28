namespace maERP.Client;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

		var login = new Views.Login();
	}
}