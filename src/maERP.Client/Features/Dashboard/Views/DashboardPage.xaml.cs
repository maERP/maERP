using maERP.Client.Features.Dashboard.Models;

namespace maERP.Client.Features.Dashboard.Views;

public sealed partial class DashboardPage : Page
{
    public DashboardPage()
    {
        this.InitializeComponent();
    }

    private async void OrderRow_Click(object sender, RoutedEventArgs e)
    {
        if (sender is Button button &&
            button.DataContext is RecentOrderItem order &&
            DataContext is DashboardModel model)
        {
            await model.ViewOrder(order);
        }
    }
}
