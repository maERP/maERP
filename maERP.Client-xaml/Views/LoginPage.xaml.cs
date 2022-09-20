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

        // loadSavedData();
    }

    private async void loadSavedData()
    {
        tbxServer.Text = await SecureStorage.GetAsync("server");
        tbxUsername.Text = await SecureStorage.GetAsync("email");
        pbxPassword.Text = await SecureStorage.GetAsync("password");
    }

    private async void btnLogin_Clicked(object sender, System.EventArgs e)
    {
        string server = tbxServer.Text;
        string username = tbxUsername.Text;
        string password = pbxPassword.Text;

        bool result = await _viewModel.Login(server, username, password);

        if (!result)
        {
            await DisplayAlert("Login nicht möglich", "E-Mail oder Passwort falsch", "OK");
            return;            
        }

        if (cbSaveLogin.IsChecked)
        {
            await SecureStorage.SetAsync("server", tbxServer.Text);
            await SecureStorage.SetAsync("email", tbxServer.Text);
            await SecureStorage.SetAsync("password", tbxServer.Text);
        }
        else
        {
            SecureStorage.Remove("server");
            SecureStorage.Remove("email");
            SecureStorage.Remove("password");
        }

        await Shell.Current.GoToAsync(nameof(maERP.Client.Views.DashboardPage), false);
    }

    private void btnExit_Clicked(System.Object sender, System.EventArgs e)
    {
        System.Environment.Exit(0);
    }
}