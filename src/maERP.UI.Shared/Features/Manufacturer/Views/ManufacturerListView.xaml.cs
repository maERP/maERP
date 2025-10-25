using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using maERP.UI.Features.Manufacturer.ViewModels;

namespace maERP.UI.Features.Manufacturer.Views;

public sealed partial class ManufacturerListView : UserControl
{
    public ManufacturerListView()
    {
        this.InitializeComponent();
    }

    private async void DataGrid_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
    {
        if (DataContext is ManufacturerListViewModel viewModel && viewModel.SelectedManufacturer != null)
        {
            await viewModel.ViewManufacturerDetailsCommand.ExecuteAsync(viewModel.SelectedManufacturer);
        }
    }
}
