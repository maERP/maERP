using CommunityToolkit.WinUI.UI.Controls;
using maERP.Client.Features.Customers.Models;
using maERP.Domain.Dtos.Customer;

namespace maERP.Client.Features.Customers.Views;

public sealed partial class CustomerListPage : Page
{
    public CustomerListPage()
    {
        this.InitializeComponent();
    }

    private async void CustomerDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (sender is DataGrid dataGrid &&
            dataGrid.SelectedItem is CustomerListDto customer &&
            DataContext is CustomerListModel model)
        {
            // Clear selection to allow re-selecting the same item
            dataGrid.SelectedItem = null;
            await model.ViewCustomer(customer);
        }
    }
}
