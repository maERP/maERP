using maERP.Client.ViewModels;


namespace maERP.Client.Views;

public partial class LoginPage : ContentPage
{
    public LoginViewModel _viewModel { get; }

    public LoginPage(LoginViewModel viewModel)
	{
        BindingContext = viewModel;
        _viewModel = viewModel;

        InitializeComponent();
    }

    private async void btnLogin_Clicked(object sender, System.EventArgs e)
    {
        string server = tbxServer.Text;
        string username = tbxUsername.Text;
        string password = pbxPassword.Text;

        var result = await _viewModel.Login(server, username, password);

        if (!result)
        {
            await DisplayAlert("Login nicht möglich", "E-Mail oder Passwort falsch", "OK");
            return;            
        }

        await Shell.Current.GoToAsync(nameof(maERP.Client.Views.DashboardPage), false);
    }

    private void btnExit_Clicked(System.Object sender, System.EventArgs e)
    {
        System.Environment.Exit(0);
    }
}