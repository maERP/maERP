using Avalonia.Controls;
using Avalonia.Interactivity;
using maERP.UI.Features.Warehouses.ViewModels;

namespace maERP.UI.Features.Warehouses.Views;

public partial class WarehouseSelectionDialog : Window
{
    public WarehouseSelectionDialogViewModel? ViewModel => DataContext as WarehouseSelectionDialogViewModel;
    
    public bool DialogResult { get; private set; }
    public int? SelectedWarehouseId => ViewModel?.SelectedWarehouseId;

    public WarehouseSelectionDialog()
    {
        InitializeComponent();
    }

    private void OkButton_Click(object? sender, RoutedEventArgs e)
    {
        if (ViewModel?.IsValidSelection == true)
        {
            DialogResult = true;
            Close();
        }
    }

    private void CancelButton_Click(object? sender, RoutedEventArgs e)
    {
        DialogResult = false;
        Close();
    }
}