using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using maERP.UI.Shared.Features.Superadmin.ViewModels;

namespace maERP.UI.Shared.Features.Superadmin.Views;

public sealed partial class SuperadminUserListView : UserControl
{
    public SuperadminUserListView()
    {
        this.InitializeComponent();
    }

    private void DataGrid_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
    {
        if (DataContext is SuperadminUserListViewModel viewModel && viewModel.SelectedUser != null)
        {
            viewModel.ViewUserDetailsCommand.Execute(viewModel.SelectedUser);
        }
    }
}
