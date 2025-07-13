using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core.Plugins;
using System.Linq;
using Avalonia.Markup.Xaml;
using maERP.Server.UI.ViewModels;
using maERP.Server.UI.Views;
using Avalonia.Controls;
using Avalonia.Platform;
using System;
using System.Threading.Tasks;

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
            
            // Create system tray icon first
            CreateTrayIcon();
            
            // Hide window on startup and show in tray
            _mainWindow.WindowState = WindowState.Minimized;
            _mainWindow.ShowInTaskbar = false;
            _mainWindow.IsVisible = false;
            
            // Start maERP.Server automatically after tray icon is created
            _ = Task.Run(async () => await StartServerAsync());
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
        
        var serverStatusItem = new NativeMenuItem("Server Status");
        serverStatusItem.Click += (sender, args) => ShowServerStatus();
        
        var restartServerItem = new NativeMenuItem("Restart Server");
        restartServerItem.Click += (sender, args) => _ = Task.Run(RestartServerAsync);
        
        var exitItem = new NativeMenuItem("Exit");
        exitItem.Click += (sender, args) => ExitApplication();

        menu.Add(showItem);
        menu.Add(hideItem);
        menu.Add(separatorItem);
        menu.Add(serverStatusItem);
        menu.Add(restartServerItem);
        // fmenu.Add(separatorItem);
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

    private async Task StartServerAsync()
    {
        try
        {
            var success = await ServerManager.StartServerAsync(5000);
            if (success)
            {
                UpdateTrayIcon("maERP Server UI - Server Running");
            }
            else
            {
                UpdateTrayIcon("maERP Server UI - Server Failed to Start");
            }
        }
        catch (Exception ex)
        {
            UpdateTrayIcon($"maERP Server UI - Server Error: {ex.Message}");
        }
    }
    
    private async Task RestartServerAsync()
    {
        UpdateTrayIcon("maERP Server UI - Restarting Server...");
        ServerManager.StopServer();
        await Task.Delay(2000); // Wait for clean shutdown
        await StartServerAsync();
    }
    
    private void ShowServerStatus()
    {
        var isRunning = ServerManager.IsServerRunning();
        var url = ServerManager.GetServerUrl();
        var status = isRunning ? "Running" : "Stopped";
        
        // You could show a dialog here or update the main window
        // For now, just update the tray tooltip
        UpdateTrayIcon($"maERP Server UI - Server {status} ({url})");
    }
    
    private void UpdateTrayIcon(string toolTip)
    {
        if (_trayIcon != null)
        {
            Avalonia.Threading.Dispatcher.UIThread.InvokeAsync(() =>
            {
                if (_trayIcon.IsVisible)
                {
                    _trayIcon.ToolTipText = toolTip;
                }
            });
        }
    }

    private void ExitApplication()
    {
        // Stop server before exiting
        ServerManager.StopServer();
        
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