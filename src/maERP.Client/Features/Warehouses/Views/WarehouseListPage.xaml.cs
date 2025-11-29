using maERP.Client.Features.Warehouses.Models;
using maERP.Domain.Dtos.Warehouse;

namespace maERP.Client.Features.Warehouses.Views;

public sealed partial class WarehouseListPage : Page
{
    private bool _isInitializing = true;
    private string _currentSortField = "Name";
    private bool _sortAscending = true;

    // Sort icon references - will be found after template is applied
    private readonly Dictionary<string, FontIcon> _sortIcons = new();

    public WarehouseListPage()
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
        if (FindName("SortIconName") is FontIcon iconName)
            _sortIcons["Name"] = iconName;
    }

    private async void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (sender is TextBox textBox && DataContext is WarehouseListModel model)
        {
            // Reset to first page when search changes
            await model.CurrentPage.UpdateAsync(_ => 0);
            await model.SearchQuery.UpdateAsync(_ => textBox.Text);
        }
    }

    private async void SortHeader_Click(object sender, RoutedEventArgs e)
    {
        if (sender is not Button button || DataContext is not WarehouseListModel model)
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
        var orderBy = $"{sortField} {sortDirection}";

        await model.SetSortOrder(orderBy);
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

    private async void CreateWarehouse_Click(object sender, RoutedEventArgs e)
    {
        if (DataContext is WarehouseListModel model)
        {
            await model.CreateWarehouse();
        }
    }

    private async void WarehouseRow_Click(object sender, RoutedEventArgs e)
    {
        if (sender is Button button &&
            button.DataContext is WarehouseListDto warehouse &&
            DataContext is WarehouseListModel model)
        {
            await model.ViewWarehouse(warehouse);
        }
    }

    private async void PreviousPage_Click(object sender, RoutedEventArgs e)
    {
        if (DataContext is WarehouseListModel model)
        {
            await model.GoToPreviousPage();
        }
    }

    private async void NextPage_Click(object sender, RoutedEventArgs e)
    {
        if (DataContext is WarehouseListModel model)
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
            DataContext is WarehouseListModel model)
        {
            await model.SetPageSize(pageSize);
        }
    }
}
