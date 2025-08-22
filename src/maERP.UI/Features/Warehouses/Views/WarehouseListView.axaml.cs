using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using maERP.UI.Features.Warehouses.ViewModels;

namespace maERP.UI.Features.Warehouses.Views;

public partial class WarehouseListView : UserControl
{
    public WarehouseListView()
    {
        InitializeComponent();
    }

    private void DataGrid_DoubleTapped(object? sender, TappedEventArgs e)
    {
        if (DataContext is WarehouseListViewModel viewModel && viewModel.SelectedWarehouse != null)
        {
            viewModel.ViewWarehouseDetailsCommand.Execute(viewModel.SelectedWarehouse);
        }
    }
}