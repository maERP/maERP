using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using maERP.UI.Features.Customers.ViewModels;

namespace maERP.UI.Features.Customers.Views;

public partial class CustomerListView : UserControl
{
    public CustomerListView()
    {
        InitializeComponent();
    }

    private void DataGrid_DoubleTapped(object? sender, TappedEventArgs e)
    {
        if (DataContext is CustomerListViewModel viewModel && viewModel.SelectedCustomer != null)
        {
            viewModel.ViewCustomerDetailsCommand.Execute(viewModel.SelectedCustomer);
        }
    }
}