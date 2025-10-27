using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using maERP.UI.Shared.Features.Customers.ViewModels;

namespace maERP.UI.Shared.Features.Customers.Views;

public sealed partial class CustomerListView : UserControl
{
    public CustomerListView()
    {
        this.InitializeComponent();
    }

    private void DataGrid_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
    {
        if (DataContext is CustomerListViewModel viewModel && viewModel.SelectedCustomer != null)
        {
            viewModel.ViewCustomerDetailsCommand.Execute(viewModel.SelectedCustomer);
        }
    }
}
