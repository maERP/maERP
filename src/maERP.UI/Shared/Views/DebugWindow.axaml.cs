using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using maERP.UI.Shared.ViewModels;

namespace maERP.UI.Shared.Views;

public partial class DebugWindow : Window
{
    private ScrollViewer? _logScrollViewer;

    public DebugWindow()
    {
        InitializeComponent();
        
        KeyDown += OnKeyDown;
        
        Loaded += OnLoaded;
    }

    private void OnLoaded(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        _logScrollViewer = this.FindControl<ScrollViewer>("LogScrollViewer");
        
        if (DataContext is DebugWindowViewModel viewModel)
        {
            viewModel.ScrollToBottomRequested += OnScrollToBottomRequested;
        }
    }

    private void OnScrollToBottomRequested()
    {
        _logScrollViewer?.ScrollToEnd();
    }

    private void OnKeyDown(object? sender, KeyEventArgs e)
    {
        if (e.Key == Key.Escape)
        {
            Hide();
            e.Handled = true;
        }
    }

    protected override void OnClosing(WindowClosingEventArgs e)
    {
        e.Cancel = true;
        Hide();
    }
}