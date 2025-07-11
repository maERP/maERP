using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core;
using Avalonia.Data.Core.Plugins;
using System.Linq;
using Avalonia.Markup.Xaml;
using maERP.Server.UI.ViewModels;
using maERP.Server.UI.Views;
using Avalonia.Controls;
using Avalonia.Platform;
using System;

namespace maERP.Server.UI;

public partial class App : Application
{
    private TrayIcon? _trayIcon;
    private MainWindow? _mainWindow;

    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            // Avoid duplicate validations from both Avalonia and the CommunityToolkit. 
            // More info: https://docs.avaloniaui.net/docs/guides/development-guides/data-validation#manage-validationplugins
            DisableAvaloniaDataAnnotationValidation();
            
            _mainWindow = new MainWindow
            {
                DataContext = new MainWindowViewModel(),
            };
            
            desktop.MainWindow = _mainWindow;
            
            // Create system tray icon
            CreateTrayIcon();
            
            // Hide window on startup and show in tray
            _mainWindow.WindowState = WindowState.Minimized;
            _mainWindow.ShowInTaskbar = false;
            _mainWindow.IsVisible = false;
        }

        base.OnFrameworkInitializationCompleted();
    }

    private void CreateTrayIcon()
    {
        _trayIcon = new TrayIcon
        {
            Icon = new WindowIcon(AssetLoader.Open(new Uri("avares://maERP.Server.UI/Assets/avalonia-logo.ico"))),
            ToolTipText = "maERP Server UI",
            IsVisible = true
        };

        var menu = new NativeMenu();
        
        var showItem = new NativeMenuItem("Show Window");
        showItem.Click += (sender, args) => ShowMainWindow();
        
        var hideItem = new NativeMenuItem("Hide Window");
        hideItem.Click += (sender, args) => HideMainWindow();
        
        var separatorItem = new NativeMenuItemSeparator();
        
        var exitItem = new NativeMenuItem("Exit");
        exitItem.Click += (sender, args) => ExitApplication();

        menu.Add(showItem);
        menu.Add(hideItem);
        menu.Add(separatorItem);
        menu.Add(exitItem);

        _trayIcon.Menu = menu;
        
        // Double-click to show/hide window
        _trayIcon.Clicked += (sender, args) => ToggleMainWindow();
    }

    private void ShowMainWindow()
    {
        if (_mainWindow != null)
        {
            _mainWindow.Show();
            _mainWindow.WindowState = WindowState.Normal;
            _mainWindow.ShowInTaskbar = true;
            _mainWindow.Activate();
        }
    }

    private void HideMainWindow()
    {
        if (_mainWindow != null)
        {
            _mainWindow.Hide();
            _mainWindow.ShowInTaskbar = false;
        }
    }

    private void ToggleMainWindow()
    {
        if (_mainWindow != null)
        {
            if (_mainWindow.IsVisible)
            {
                HideMainWindow();
            }
            else
            {
                ShowMainWindow();
            }
        }
    }

    private void ExitApplication()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.Shutdown();
        }
    }

    private void DisableAvaloniaDataAnnotationValidation()
    {
        // Get an array of plugins to remove
        var dataValidationPluginsToRemove =
            BindingPlugins.DataValidators.OfType<DataAnnotationsValidationPlugin>().ToArray();

        // remove each entry found
        foreach (var plugin in dataValidationPluginsToRemove)
        {
            BindingPlugins.DataValidators.Remove(plugin);
        }
    }
}