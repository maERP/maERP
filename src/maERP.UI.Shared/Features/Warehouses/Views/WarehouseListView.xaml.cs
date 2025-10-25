using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using maERP.UI.Features.Warehouses.ViewModels;

namespace maERP.UI.Features.Warehouses.Views;

public sealed partial class WarehouseListView : UserControl
{
    public WarehouseListView()
    {
        this.InitializeComponent();
    }

    private void DataGrid_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
    {
        if (DataContext is WarehouseListViewModel viewModel && viewModel.SelectedWarehouse != null)
        {
            viewModel.ViewWarehouseDetailsCommand.Execute(viewModel.SelectedWarehouse);
        }
    }
}
