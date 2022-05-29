using maERP.Client.ViewModels;

namespace maERP.Client.Views;

public partial class OrdersPage : ContentPage
{
    public OrdersPage(OrdersViewModel viewModel)
    {
        BindingContext = viewModel;
        InitializeComponent();
    }
}