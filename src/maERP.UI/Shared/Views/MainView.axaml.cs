using Avalonia.Controls;
using Avalonia.Input;
using maERP.UI.Shared.ViewModels;

namespace maERP.UI.Shared.Views;

public partial class MainView : UserControl
{
    public MainView()
    {
        InitializeComponent();
        KeyDown += OnKeyDown;
    }

    private void OnKeyDown(object? sender, KeyEventArgs e)
    {
        if (e.Key == Key.F12)
        {
            if (DataContext is MainViewModel mainViewModel)
            {
                mainViewModel.ToggleDebugWindow();
            }
            e.Handled = true;
        }
    }
}
