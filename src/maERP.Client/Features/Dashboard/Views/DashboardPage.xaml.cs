using maERP.Client.Features.Dashboard.Models;

namespace maERP.Client.Features.Dashboard.Views;

public sealed partial class DashboardPage : Page
{
    public DashboardPage()
    {
        this.InitializeComponent();
    }

    private async void SalesRow_Click(object sender, RoutedEventArgs e)
    {
        if (sender is Button button &&
            button.DataContext is RecentSalesItem sales &&
            DataContext is DashboardModel model)
        {
            await model.ViewSales(sales);
        }
    }

    private async void ViewAllSaless_Click(object sender, RoutedEventArgs e)
    {
        if (DataContext is DashboardModel model)
        {
            await model.NavigateToSalesList();
        }
    }

    private async void ViewAllProducts_Click(object sender, RoutedEventArgs e)
    {
        if (DataContext is DashboardModel model)
        {
            await model.NavigateToProductList();
        }
    }
}
