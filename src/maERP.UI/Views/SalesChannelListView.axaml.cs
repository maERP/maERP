using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using maERP.UI.ViewModels;

namespace maERP.UI.Views;

public partial class SalesChannelListView : UserControl
{
    public SalesChannelListView()
    {
        InitializeComponent();
    }
    
    private void DataGrid_DoubleTapped(object? sender, TappedEventArgs e)
    {
        if (DataContext is SalesChannelListViewModel viewModel && viewModel.SelectedSalesChannel != null)
        {
            viewModel.ViewSalesChannelDetailsCommand.Execute(viewModel.SelectedSalesChannel);
        }
    }
}