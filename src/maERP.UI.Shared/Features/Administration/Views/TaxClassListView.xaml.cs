using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using maERP.UI.Shared.Features.Administration.ViewModels;

namespace maERP.UI.Shared.Features.Administration.Views;

public sealed partial class TaxClassListView : UserControl
{
    public TaxClassListView()
    {
        this.InitializeComponent();
    }

    private void DataGrid_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
    {
        if (DataContext is TaxClassListViewModel viewModel && viewModel.SelectedTaxClass != null)
        {
            viewModel.ViewTaxClassDetailsCommand.Execute(viewModel.SelectedTaxClass);
        }
    }
}
