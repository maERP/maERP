using System;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using maERP.UI.Shared.Shared.ViewModels;

namespace maERP.UI.Shared.Shared.Views;

public sealed partial class ConfirmationDialog : UserControl
{
    public ConfirmationDialogViewModel? ViewModel => DataContext as ConfirmationDialogViewModel;

    public bool DialogResult { get; private set; }

    public event EventHandler? DialogClosed;

    public ConfirmationDialog()
    {
        this.InitializeComponent();
    }

    private void ConfirmButton_Click(object sender, RoutedEventArgs e)
    {
        DialogResult = true;
        DialogClosed?.Invoke(this, EventArgs.Empty);
    }

    private void CancelButton_Click(object sender, RoutedEventArgs e)
    {
        DialogResult = false;
        DialogClosed?.Invoke(this, EventArgs.Empty);
    }
}
