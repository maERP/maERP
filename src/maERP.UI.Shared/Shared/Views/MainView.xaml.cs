using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using Windows.System;
using maERP.UI.Shared.ViewModels;

namespace maERP.UI.Shared.Views;

public sealed partial class MainView : UserControl
{
    public MainView()
    {
        this.InitializeComponent();
        this.KeyDown += OnKeyDown;
    }

    private void OnKeyDown(object sender, KeyRoutedEventArgs e)
    {
        if (e.Key == VirtualKey.F12)
        {
            if (DataContext is MainViewModel mainViewModel)
            {
                mainViewModel.ToggleDebugWindow();
            }
            e.Handled = true;
        }
    }
}
