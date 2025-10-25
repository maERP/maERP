using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using maERP.UI.Features.Tenant.ViewModels;

namespace maERP.UI.Features.Tenant.Views;

public sealed partial class TenantsListView : UserControl
{
    public TenantsListView()
    {
        this.InitializeComponent();
    }

    private async void DataGrid_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
    {
        if (DataContext is TenantListViewModel viewModel && viewModel.SelectedTenant != null)
        {
            await viewModel.OnTenantDoubleClickedAsync(viewModel.SelectedTenant);
        }
    }
}
