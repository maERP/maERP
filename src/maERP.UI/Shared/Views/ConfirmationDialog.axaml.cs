using Avalonia.Controls;
using Avalonia.Interactivity;
using maERP.UI.Shared.ViewModels;

namespace maERP.UI.Shared.Views;

public partial class ConfirmationDialog : Window
{
    public ConfirmationDialogViewModel? ViewModel => DataContext as ConfirmationDialogViewModel;

    public bool DialogResult { get; private set; }

    public ConfirmationDialog()
    {
        InitializeComponent();
    }

    private void ConfirmButton_Click(object? sender, RoutedEventArgs e)
    {
        DialogResult = true;
        Close();
    }

    private void CancelButton_Click(object? sender, RoutedEventArgs e)
    {
        DialogResult = false;
        Close();
    }
}