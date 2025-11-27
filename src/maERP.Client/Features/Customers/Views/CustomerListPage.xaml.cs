using CommunityToolkit.WinUI.UI.Controls;
using maERP.Client.Features.Customers.Models;
using maERP.Domain.Dtos.Customer;

namespace maERP.Client.Features.Customers.Views;

public sealed partial class CustomerListPage : Page
{
    private bool _isInitializing = true;

    public CustomerListPage()
    {
        this.InitializeComponent();
        this.Loaded += OnLoaded;
    }

    private void OnLoaded(object sender, RoutedEventArgs e)
    {
        _isInitializing = false;
    }

    private async void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (sender is TextBox textBox && DataContext is CustomerListModel model)
        {
            // Reset to first page when search changes
            await model.CurrentPage.UpdateAsync(_ => 0);
            await model.SearchQuery.UpdateAsync(_ => textBox.Text);
        }
    }

    private async void SortComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (_isInitializing) return;

        if (sender is ComboBox comboBox &&
            comboBox.SelectedItem is ComboBoxItem selectedItem &&
            selectedItem.Tag is string orderBy &&
            DataContext is CustomerListModel model)
        {
            await model.SetSortOrder(orderBy);
        }
    }

    private async void CreateCustomer_Click(object sender, RoutedEventArgs e)
    {
        if (DataContext is CustomerListModel model)
        {
            await model.CreateCustomer();
        }
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

    private async void PreviousPage_Click(object sender, RoutedEventArgs e)
    {
        if (DataContext is CustomerListModel model)
        {
            await model.GoToPreviousPage();
        }
    }

    private async void NextPage_Click(object sender, RoutedEventArgs e)
    {
        if (DataContext is CustomerListModel model)
        {
            await model.GoToNextPage();
        }
    }

    private async void PageSizeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (_isInitializing) return;

        if (sender is ComboBox comboBox &&
            comboBox.SelectedItem is ComboBoxItem selectedItem &&
            selectedItem.Tag is string pageSizeStr &&
            int.TryParse(pageSizeStr, out var pageSize) &&
            DataContext is CustomerListModel model)
        {
            await model.SetPageSize(pageSize);
        }
    }
}
