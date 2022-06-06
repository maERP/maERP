using maERP.Client.Views;

namespace maERP.Client;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

		Routing.RegisterRoute(nameof(DashboardPage), typeof(DashboardPage));
		Routing.RegisterRoute(nameof(ProductsDetailPage), typeof(ProductsDetailPage));
		Routing.RegisterRoute(nameof(OrdersPage), typeof(OrdersPage));
		Routing.RegisterRoute(nameof(SettingsPage), typeof(SettingsPage));
	}
}