using System;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using maERP.UI.Features.Warehouses.ViewModels;

namespace maERP.UI.Features.Warehouses.Views;

// Note: This UserControl is designed to be hosted in a ContentDialog
// The DialogService will need to create and manage the ContentDialog wrapper
public sealed partial class WarehouseSelectionDialog : UserControl
{
    public WarehouseSelectionDialogViewModel? ViewModel => DataContext as WarehouseSelectionDialogViewModel;

    public bool DialogResult { get; private set; }
    public Guid? SelectedWarehouseId => ViewModel?.SelectedWarehouseId;

    // Event to notify when dialog should close
    public event EventHandler<bool>? DialogClosing;

    public WarehouseSelectionDialog()
    {
        this.InitializeComponent();
    }

    private void OkButton_Click(object sender, RoutedEventArgs e)
    {
        if (ViewModel?.IsValidSelection == true)
        {
            DialogResult = true;
            DialogClosing?.Invoke(this, true);
        }
    }

    private void CancelButton_Click(object sender, RoutedEventArgs e)
    {
        DialogResult = false;
        DialogClosing?.Invoke(this, false);
    }
}
