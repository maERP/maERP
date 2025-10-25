using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using maERP.UI.Shared.ViewModels;

namespace maERP.UI.Shared.Views;

public sealed partial class DebugWindow : UserControl
{
    private TextBox? _logTextBox;

    public DebugWindow()
    {
        this.InitializeComponent();
        this.Loaded += OnLoaded;
    }

    private void OnLoaded(object sender, RoutedEventArgs e)
    {
        _logTextBox = LogTextBox;

        if (DataContext is DebugWindowViewModel viewModel)
        {
            viewModel.ScrollToBottomRequested += OnScrollToBottomRequested;
        }
    }

    private void OnScrollToBottomRequested()
    {
        if (_logTextBox != null)
        {
            _logTextBox.SelectionStart = _logTextBox.Text?.Length ?? 0;
        }
    }
}
