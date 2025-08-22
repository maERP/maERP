using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using maERP.UI.Features.Orders.ViewModels;

namespace maERP.UI.Features.Orders.Views;

public partial class OrderListView : UserControl
{
    public OrderListView()
    {
        InitializeComponent();
    }

    private void DataGrid_DoubleTapped(object? sender, TappedEventArgs e)
    {
        if (DataContext is OrderListViewModel viewModel && viewModel.SelectedOrder != null)
        {
            viewModel.ViewOrderDetailsCommand.Execute(viewModel.SelectedOrder);
        }
    }
}