using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using maERP.UI.Shared.ViewModels;

namespace maERP.UI.Shared.Views;

public partial class DebugWindow : Window
{
    private TextBox? _logTextBox;

    public DebugWindow()
    {
        InitializeComponent();

        KeyDown += OnKeyDown;

        Loaded += OnLoaded;
    }

    private void OnLoaded(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        _logTextBox = this.FindControl<TextBox>("LogTextBox");

        if (DataContext is DebugWindowViewModel viewModel)
        {
            viewModel.ScrollToBottomRequested += OnScrollToBottomRequested;
        }
    }

    private void OnScrollToBottomRequested()
    {
        if (_logTextBox != null)
        {
            _logTextBox.CaretIndex = _logTextBox.Text?.Length ?? 0;
        }
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