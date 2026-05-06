using maERP.Client.Features.Saless.Models;
using maERP.Domain.Dtos.Sales;

namespace maERP.Client.Features.Saless.Views;

public sealed partial class SalesListPage : Page
{
    private bool _isInitializing = true;
    private string _currentSortField = "DateSalesed";
    private bool _sortAscending = false;

    // Sort icon references - will be found after template is applied
    private readonly Dictionary<string, FontIcon> _sortIcons = new();

    public SalesListPage()
    {
        this.InitializeComponent();
        this.Loaded += OnLoaded;
    }

    private void OnLoaded(object sender, RoutedEventArgs e)
    {
        _isInitializing = false;

        // Cache sort icons for later updates
        CacheSortIcons();
    }

    private void CacheSortIcons()
    {
        // Find sort icons in the visual tree
        if (FindName("SortIconSalesId") is FontIcon iconSalesId)
            _sortIcons["SalesId"] = iconSalesId;
        if (FindName("SortIconCustomer") is FontIcon iconCustomer)
            _sortIcons["InvoiceAddressLastName"] = iconCustomer;
        if (FindName("SortIconTotal") is FontIcon iconTotal)
            _sortIcons["Total"] = iconTotal;
        if (FindName("SortIconStatus") is FontIcon iconStatus)
            _sortIcons["Status"] = iconStatus;
        if (FindName("SortIconDateSalesed") is FontIcon iconDateSalesed)
            _sortIcons["DateSalesed"] = iconDateSalesed;
    }

    private async void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (sender is TextBox textBox && DataContext is SalesListModel model)
        {
            // Reset to first page when search changes
            await model.CurrentPage.UpdateAsync(_ => 0);
            await model.SearchQuery.UpdateAsync(_ => textBox.Text);
        }
    }

    private async void SortHeader_Click(object sender, RoutedEventArgs e)
    {
        if (sender is not Button button || DataContext is not SalesListModel model)
            return;

        var sortField = button.Tag?.ToString() ?? string.Empty;
        if (string.IsNullOrEmpty(sortField))
            return;

        // Toggle direction if same field, otherwise default to ascending
        if (_currentSortField == sortField)
        {
            _sortAscending = !_sortAscending;
        }
        else
        {
            _currentSortField = sortField;
            _sortAscending = true;
        }

        // Update sort icons
        UpdateSortIcons();

        // Build sort parameter
        var sortDirection = _sortAscending ? "Ascending" : "Descending";
        var salesBy = $"{sortField} {sortDirection}";

        await model.SetSortSales(salesBy);
    }

    private void UpdateSortIcons()
    {
        // Try to re-cache if icons dictionary is empty (template might have been re-applied)
        if (_sortIcons.Count == 0)
        {
            CacheSortIcons();
        }

        foreach (var kvp in _sortIcons)
        {
            var icon = kvp.Value;
            var field = kvp.Key;

            if (field == _currentSortField)
            {
                icon.Visibility = Visibility.Visible;
                // E70D = ChevronUp (ascending), E70E = ChevronDown (descending)
                icon.Glyph = _sortAscending ? "\uE70D" : "\uE70E";
                icon.Foreground = (Microsoft.UI.Xaml.Media.Brush)Application.Current.Resources["PrimaryBrush"];
            }
            else
            {
                icon.Visibility = Visibility.Collapsed;
            }
        }
    }

    private async void NewSalesButton_Click(object sender, RoutedEventArgs e)
    {
        if (DataContext is SalesListModel model)
        {
            await model.CreateSales();
        }
    }

    private async void SalesRow_Click(object sender, RoutedEventArgs e)
    {
        if (sender is Button button &&
            button.DataContext is SalesListDto sales &&
            DataContext is SalesListModel model)
        {
            await model.ViewSales(sales);
        }
    }

    private async void PreviousPage_Click(object sender, RoutedEventArgs e)
    {
        if (DataContext is SalesListModel model)
        {
            await model.GoToPreviousPage();
        }
    }

    private async void NextPage_Click(object sender, RoutedEventArgs e)
    {
        if (DataContext is SalesListModel model)
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
            DataContext is SalesListModel model)
        {
            await model.SetPageSize(pageSize);
        }
    }
}
