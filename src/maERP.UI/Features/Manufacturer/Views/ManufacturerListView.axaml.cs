using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using maERP.UI.Features.Manufacturer.ViewModels;

namespace maERP.UI.Features.Manufacturer.Views;

public partial class ManufacturerListView : UserControl
{
    public ManufacturerListView()
    {
        InitializeComponent();
    }
    
    private void DataGrid_DoubleTapped(object? sender, TappedEventArgs e)
    {
        if (DataContext is ManufacturerListViewModel viewModel && viewModel.SelectedManufacturer != null)
        {
            viewModel.ViewManufacturerDetailsCommand.Execute(viewModel.SelectedManufacturer);
        }
    }
}