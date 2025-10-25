using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using maERP.UI.Features.Superadmin.ViewModels;

namespace maERP.UI.Features.Superadmin.Views;

public sealed partial class SuperadminTenantsListView : UserControl
{
    public SuperadminTenantsListView()
    {
        this.InitializeComponent();
    }

    private void DataGrid_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
    {
        if (DataContext is SuperadminTenantsListViewModel viewModel && viewModel.SelectedTenant != null)
        {
            viewModel.ViewTenantDetailsCommand.Execute(viewModel.SelectedTenant);
        }
    }
}
