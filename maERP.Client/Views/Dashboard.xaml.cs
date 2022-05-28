using maERP.Client.ViewModels;
using maERP.Client.Operations;
using maERP.Client.Models;

namespace maERP.Client.Views;

public partial class MainPage : ContentPage
{	
	public MainPage(DashboardModel viewModel)
	{
        BindingContext = viewModel;

        InitializeComponent();
	}

    private async void btnLogin_Clicked(object sender, System.EventArgs e)
    {
        string username = tbxUsername.Text;
        string password = pbxPassword.Text;

        Console.WriteLine("debug 1");

        var ops = new ApiOperations<Product>();

        Console.WriteLine("debug 2");

        var product = await ops.Request("GET", "/Products/1");

        Console.WriteLine("debug 3");

        await DisplayAlert("Product", product.Name, "OK");

        // NavigationService.Navigate(new DetailsPage());
    }
}