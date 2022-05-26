using maERP.Client.ViewModels;

namespace maERP.Client.Views;

public partial class SecondPage : ContentPage
{
    public SecondPage(OrdersViewModel viewModel)
    {
        BindingContext = viewModel;
        InitializeComponent();
    }
}