using System;
using maERP.UI.Shared.ViewModels;

namespace maERP.UI.Services;

public interface IDebugService
{
    event Action<DebugLevel, string>? DebugLogAdded;

    void LogDebug(string message);
    void LogInfo(string message);
    void LogWarning(string message);
    void LogError(string message);
    void LogError(Exception exception, string? message = null);

    void ShowDebugWindow();
    void HideDebugWindow();
    void ToggleDebugWindow();

    bool IsDebugWindowVisible { get; }
}