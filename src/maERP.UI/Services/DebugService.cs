using System;
using System.Diagnostics;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using maERP.UI.Shared.ViewModels;
using maERP.UI.Shared.Views;
using Trace = System.Diagnostics.Trace;

namespace maERP.UI.Services;

public class DebugService : IDebugService
{
    private DebugWindow? _debugWindow;
    private DebugWindowViewModel? _debugWindowViewModel;

    public event Action<DebugLevel, string>? DebugLogAdded;

    public bool IsDebugWindowVisible => _debugWindow?.IsVisible == true;

    public void LogDebug(string message)
    {
        LogMessage(DebugLevel.Debug, message);
        WriteLog("DEBUG", message);
    }

    public void LogInfo(string message)
    {
        LogMessage(DebugLevel.Info, message);
        WriteLog("INFO", message);
    }

    public void LogWarning(string message)
    {
        LogMessage(DebugLevel.Warning, message);
        WriteLog("WARNING", message);
    }

    public void LogError(string message)
    {
        LogMessage(DebugLevel.Error, message);
        WriteLog("ERROR", message);
    }

    public void LogError(Exception exception, string? message = null)
    {
        var errorMessage = message != null
            ? $"{message}: {exception.Message}"
            : exception.Message;

        LogMessage(DebugLevel.Error, errorMessage);
        WriteLog("ERROR", errorMessage);
        WriteLog("ERROR", $"Stack trace: {exception.StackTrace}");
    }

    private void WriteLog(string level, string message)
    {
        var logMessage = $"[{level}] {message}";

        // Write to Debug output (visible in IDE when debugger is attached)
        Debug.WriteLine(logMessage);

        // Write to Console output (visible in terminal/console)
        Console.WriteLine(logMessage);

        #if DEBUG
        // In Debug builds, also write to System.Diagnostics.Trace
        Trace.WriteLine(logMessage);
        #endif
    }

    private void LogMessage(DebugLevel level, string message)
    {
        try
        {
            var timestampedMessage = $"[{DateTime.Now:HH:mm:ss.fff}] {message}";

            _debugWindowViewModel?.AddDebugLog(level, message);
            DebugLogAdded?.Invoke(level, timestampedMessage);
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error logging debug message: {ex.Message}");
        }
    }

    private void InitializeDebugWindow()
    {
        if (_debugWindow != null) return;

        try
        {
            _debugWindowViewModel = new DebugWindowViewModel();
            _debugWindow = new DebugWindow
            {
                DataContext = _debugWindowViewModel
            };

            _debugWindow.Closed += (_, _) =>
            {
                _debugWindow = null;
                _debugWindowViewModel = null;
            };

            LogInfo("Debug window initialized");
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Failed to initialize debug window: {ex.Message}");
        }
    }

    public void ShowDebugWindow()
    {
        try
        {
            if (_debugWindow == null)
            {
                InitializeDebugWindow();
            }

            if (_debugWindow != null)
            {
                _debugWindow.Show();
                _debugWindow.Activate();

                if (!_debugWindow.IsVisible)
                {
                    _debugWindow.WindowState = WindowState.Normal;
                }
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Failed to show debug window: {ex.Message}");
        }
    }

    public void HideDebugWindow()
    {
        try
        {
            _debugWindow?.Hide();
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Failed to hide debug window: {ex.Message}");
        }
    }

    public void ToggleDebugWindow()
    {
        if (IsDebugWindowVisible)
        {
            HideDebugWindow();
        }
        else
        {
            ShowDebugWindow();
        }
    }
}