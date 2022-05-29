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

        var ops = new DataService<LoginDto>();

        // var product = await ops.Request("GET", "/Products/1");
        var login = await ops.Login(server, username, password);

        await DisplayAlert("Product", login.Email, "OK");

        // NavigationService.Navigate(new DetailsPage());
    }
}