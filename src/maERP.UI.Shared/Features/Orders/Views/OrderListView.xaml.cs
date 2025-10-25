using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using maERP.UI.Features.Orders.ViewModels;

namespace maERP.UI.Features.Orders.Views;

public sealed partial class OrderListView : UserControl
{
    public OrderListView()
    {
        this.InitializeComponent();
    }

    private void DataGrid_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
    {
        if (DataContext is OrderListViewModel viewModel && viewModel.SelectedOrder != null)
        {
            viewModel.ViewOrderDetailsCommand.Execute(viewModel.SelectedOrder);
        }
    }
}
