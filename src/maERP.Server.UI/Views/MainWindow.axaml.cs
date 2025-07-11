using Avalonia.Controls;
using System.ComponentModel;

namespace maERP.Server.UI.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        Closing += OnClosing;
    }

    private void OnClosing(object? sender, CancelEventArgs e)
    {
        e.Cancel = true;
        Hide();
        ShowInTaskbar = false;
    }
}