using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using Avalonia.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace maERP.UI.Shared.ViewModels;

public partial class DebugWindowViewModel : ViewModelBase
{
    [ObservableProperty]
    private ObservableCollection<DebugLogEntry> debugLogs = new();

    [ObservableProperty]
    private bool autoScroll = true;

    [ObservableProperty]
    private string statusText = "Debug console ready";

    [ObservableProperty]
    private int logCount;

    public string AutoScrollText => AutoScroll ? "📌 Auto-Scroll" : "📌 Manual";

    public event Action? ScrollToBottomRequested;

    public DebugWindowViewModel()
    {
        DebugLogs.CollectionChanged += (_, _) =>
        {
            LogCount = DebugLogs.Count;
            StatusText = $"Last update: {DateTime.Now:HH:mm:ss}";

            if (AutoScroll)
            {
                ScrollToBottomRequested?.Invoke();
            }
        };
    }

    [RelayCommand]
    private void ClearLogs()
    {
        DebugLogs.Clear();
        StatusText = "Logs cleared";
    }

    partial void OnAutoScrollChanged(bool value)
    {
        OnPropertyChanged(nameof(AutoScrollText));
        if (value)
        {
            ScrollToBottomRequested?.Invoke();
        }
    }

    public void AddDebugLog(DebugLevel level, string message)
    {
        var entry = new DebugLogEntry(level, message, DateTime.Now);

        if (DebugLogs.Count > 1000)
        {
            DebugLogs.RemoveAt(0);
        }

        DebugLogs.Add(entry);
    }

    public void AddInfoLog(string message) => AddDebugLog(DebugLevel.Info, message);
    public void AddWarningLog(string message) => AddDebugLog(DebugLevel.Warning, message);
    public void AddErrorLog(string message) => AddDebugLog(DebugLevel.Error, message);
    public void AddDebugLogEntry(string message) => AddDebugLog(DebugLevel.Debug, message);
}

public enum DebugLevel
{
    Debug,
    Info,
    Warning,
    Error
}

public class DebugLogEntry
{
    public DebugLogEntry(DebugLevel level, string message, DateTime timestamp)
    {
        Level = level.ToString().ToUpper();
        Message = message;
        Timestamp = timestamp;

        LevelBrush = level switch
        {
            DebugLevel.Debug => new SolidColorBrush(Color.FromArgb(40, 128, 128, 128)),
            DebugLevel.Info => new SolidColorBrush(Color.FromArgb(40, 0, 150, 255)),
            DebugLevel.Warning => new SolidColorBrush(Color.FromArgb(40, 255, 193, 7)),
            DebugLevel.Error => new SolidColorBrush(Color.FromArgb(40, 220, 53, 69)),
            _ => new SolidColorBrush(Color.FromArgb(40, 128, 128, 128))
        };

        LevelForeground = level switch
        {
            DebugLevel.Debug => new SolidColorBrush(Color.FromRgb(128, 128, 128)),
            DebugLevel.Info => new SolidColorBrush(Color.FromRgb(0, 123, 255)),
            DebugLevel.Warning => new SolidColorBrush(Color.FromRgb(255, 193, 7)),
            DebugLevel.Error => new SolidColorBrush(Color.FromRgb(220, 53, 69)),
            _ => new SolidColorBrush(Color.FromRgb(128, 128, 128))
        };
    }

    public string Level { get; }
    public string Message { get; }
    public DateTime Timestamp { get; }
    public IBrush LevelBrush { get; }
    public IBrush LevelForeground { get; }
}