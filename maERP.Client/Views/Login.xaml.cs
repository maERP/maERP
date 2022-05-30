using maERP.Client.Services;
using maERP.Data.Dtos.User;

namespace maERP.Client.Views;

public partial class LoginPage : ContentPage
{
	public LoginPage()
	{
		InitializeComponent();
	}

    private async void btnLogin_Clicked(object sender, System.EventArgs e)
    {
        string server = tbxServer.Text;
        string username = tbxUsername.Text;
        string password = pbxPassword.Text;

        var ds = new DataService<LoginDto>();

        //var product = await ops.Request("GET", "/Products/1");

        var user = await ds.Login(server, username, password);

        if (user == null || user.Token.Length <= 0)
        {
            await DisplayAlert("Login nicht möglich", "E-Mail oder Passwort falsch", "OK");
            return;            
        }

        await Navigation.PushModalAsync(new AppShell());
    }

    private void btnExit_Clicked(System.Object sender, System.EventArgs e)
    {
        System.Environment.Exit(0);
    }
}