using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using maERP.UI.Features.GoodsReceipts.ViewModels;

namespace maERP.UI.Features.GoodsReceipts.Views;

public sealed partial class GoodsReceiptListView : UserControl
{
    public GoodsReceiptListView()
    {
        this.InitializeComponent();
    }

    private void DataGrid_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
    {
        if (DataContext is GoodsReceiptListViewModel viewModel && viewModel.SelectedGoodsReceipt != null)
        {
            // Add appropriate command execution here when the command exists
            // viewModel.ViewGoodsReceiptDetailsCommand.Execute(viewModel.SelectedGoodsReceipt);
        }
    }
}
