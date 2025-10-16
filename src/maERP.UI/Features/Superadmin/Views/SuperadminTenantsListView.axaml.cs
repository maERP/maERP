using Avalonia.Controls;
using Avalonia.Input;
using maERP.UI.Features.Superadmin.ViewModels;

namespace maERP.UI.Features.Superadmin.Views;

public partial class SuperadminTenantsListView : UserControl
{
    public SuperadminTenantsListView()
    {
        InitializeComponent();
    }

    private void DataGrid_DoubleTapped(object? sender, TappedEventArgs e)
    {
        if (DataContext is SuperadminTenantsListViewModel viewModel && viewModel.SelectedTenant != null)
        {
            viewModel.ViewTenantDetailsCommand.Execute(viewModel.SelectedTenant);
        }
    }
}
