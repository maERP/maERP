using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using maERP.UI.Features.Tenants.ViewModels;

namespace maERP.UI.Features.Tenants.Views;

public partial class TenantListView : UserControl
{
    public TenantListView()
    {
        InitializeComponent();
    }

    private void DataGrid_DoubleTapped(object? sender, TappedEventArgs e)
    {
        if (DataContext is TenantListViewModel viewModel && viewModel.SelectedTenant != null)
        {
            viewModel.ViewTenantDetailsCommand.Execute(viewModel.SelectedTenant);
        }
    }
}
