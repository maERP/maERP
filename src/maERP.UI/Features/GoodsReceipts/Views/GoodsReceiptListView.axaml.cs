using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using maERP.UI.Features.GoodsReceipts.ViewModels;

namespace maERP.UI.Features.GoodsReceipts.Views;

public partial class GoodsReceiptListView : UserControl
{
    public GoodsReceiptListView()
    {
        InitializeComponent();
    }

    private void DataGrid_DoubleTapped(object? sender, TappedEventArgs e)
    {
        if (DataContext is GoodsReceiptListViewModel viewModel && viewModel.SelectedGoodsReceipt != null)
        {
            // Add appropriate command execution here when the command exists
            // viewModel.ViewGoodsReceiptDetailsCommand.Execute(viewModel.SelectedGoodsReceipt);
        }
    }
}