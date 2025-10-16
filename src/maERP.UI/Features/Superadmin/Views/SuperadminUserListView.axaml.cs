using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using maERP.UI.Features.Superadmin.ViewModels;

namespace maERP.UI.Features.Superadmin.Views;

public partial class SuperadminUserListView : UserControl
{
    public SuperadminUserListView()
    {
        InitializeComponent();
    }

    private void DataGrid_DoubleTapped(object? sender, TappedEventArgs e)
    {
        if (DataContext is SuperadminUserListViewModel viewModel && viewModel.SelectedUser != null)
        {
            viewModel.ViewUserDetailsCommand.Execute(viewModel.SelectedUser);
        }
    }
}