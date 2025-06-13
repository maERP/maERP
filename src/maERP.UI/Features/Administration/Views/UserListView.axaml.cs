using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using maERP.UI.Features.Administration.ViewModels;

namespace maERP.UI.Features.Administration.Views;

public partial class UserListView : UserControl
{
    public UserListView()
    {
        InitializeComponent();
    }
    
    private void DataGrid_DoubleTapped(object? sender, TappedEventArgs e)
    {
        if (DataContext is UserListViewModel viewModel && viewModel.SelectedUser != null)
        {
            viewModel.ViewUserDetailsCommand.Execute(viewModel.SelectedUser);
        }
    }
}