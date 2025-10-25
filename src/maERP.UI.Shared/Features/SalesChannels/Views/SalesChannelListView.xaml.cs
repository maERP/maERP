using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using maERP.UI.Features.SalesChannels.ViewModels;

namespace maERP.UI.Features.SalesChannels.Views;

public sealed partial class SalesChannelListView : UserControl
{
    public SalesChannelListView()
    {
        this.InitializeComponent();
    }

    private void DataGrid_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
    {
        if (DataContext is SalesChannelListViewModel viewModel && viewModel.SelectedSalesChannel != null)
        {
            viewModel.ViewSalesChannelDetailsCommand.Execute(viewModel.SelectedSalesChannel);
        }
    }
}
