using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using maERP.UI.Features.Administration.ViewModels;

namespace maERP.UI.Features.Administration.Views;

public partial class TaxClassListView : UserControl
{
    public TaxClassListView()
    {
        InitializeComponent();
    }

    private void DataGrid_DoubleTapped(object? sender, TappedEventArgs e)
    {
        if (DataContext is TaxClassListViewModel viewModel && viewModel.SelectedTaxClass != null)
        {
            viewModel.ViewTaxClassDetailsCommand.Execute(viewModel.SelectedTaxClass);
        }
    }
}