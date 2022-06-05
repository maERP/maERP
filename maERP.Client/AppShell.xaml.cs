using maERP.Client.Views;
using maERP.Client.ViewModels;

namespace maERP.Client;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

		Routing.RegisterRoute(nameof(ProductsDetailPage), typeof(ProductsDetailPage));
	}
}