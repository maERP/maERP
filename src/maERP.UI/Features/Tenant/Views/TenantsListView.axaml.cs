using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using maERP.Domain.Dtos.Tenant;
using maERP.UI.Features.Tenant.ViewModels;

namespace maERP.UI.Features.Tenant.Views;

public partial class TenantsListView : UserControl
{
    public TenantsListView()
    {
        InitializeComponent();
    }

    private async void DataGrid_DoubleTapped(object? sender, TappedEventArgs e)
    {
        if (DataContext is TenantListViewModel viewModel && viewModel.SelectedTenant != null)
        {
            await viewModel.OnTenantDoubleClickedAsync(viewModel.SelectedTenant);
        }
    }
}
