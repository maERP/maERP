using System;
using System.Diagnostics;
using maERP.UI.Shared.ViewModels;

namespace maERP.UI.Services;

public class DebugService : IDebugService
{
    private DebugWindowViewModel? _debugWindowViewModel;

    public event Action<DebugLevel, string>? DebugLogAdded;

    // TODO: Re-enable DebugWindow for Uno Platform
    public bool IsDebugWindowVisible => false;

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
        System.Diagnostics.Trace.WriteLine(logMessage);
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

    // TODO: Re-enable DebugWindow for Uno Platform
    public void ShowDebugWindow()
    {
        Debug.WriteLine("Debug window not yet implemented for Uno Platform");
    }

    public void HideDebugWindow()
    {
        Debug.WriteLine("Debug window not yet implemented for Uno Platform");
    }

    public void ToggleDebugWindow()
    {
        Debug.WriteLine("Debug window toggle not yet implemented for Uno Platform");
    }
}
