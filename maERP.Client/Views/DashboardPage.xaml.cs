using maERP.Client.ViewModels;

namespace maERP.Client.Views;

public partial class MainPage : ContentPage
{	
	public MainPage(DashboardModel viewModel)
	{
        BindingContext = viewModel;

        InitializeComponent();
	}
}