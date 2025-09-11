using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using maERP.Domain.Dtos.Warehouse;
using maERP.UI.Features.Warehouses.ViewModels;
using maERP.UI.Features.Warehouses.Views;
using maERP.UI.Shared.ViewModels;
using maERP.UI.Shared.Views;

namespace maERP.UI.Services;

public class DialogService : IDialogService
{
    private readonly IDebugService _debugService;

    public DialogService(IDebugService debugService)
    {
        _debugService = debugService;
    }
    public async Task<bool> ShowConfirmationDialogAsync(string title, string message, string confirmText = "Ja", string cancelText = "Abbrechen", string icon = "❓")
    {
        try
        {
            var dialogViewModel = new ConfirmationDialogViewModel();
            dialogViewModel.Initialize(title, message, confirmText, cancelText, icon);

            var dialog = new ConfirmationDialog
            {
                DataContext = dialogViewModel
            };

            // Try to get the main window for proper modal dialog display
            try
            {
                var mainWindow = Avalonia.Application.Current?.ApplicationLifetime is Avalonia.Controls.ApplicationLifetimes.IClassicDesktopStyleApplicationLifetime desktop
                    ? desktop.MainWindow
                    : null;

                if (mainWindow != null)
                {
                    await dialog.ShowDialog(mainWindow);
                    return dialog.DialogResult;
                }
            }
            catch (Exception ex)
            {
                _debugService.LogError(ex, "Could not show modal dialog");
            }

            // Fallback: simulate user confirmation for non-desktop platforms
            _debugService.LogWarning($"[DIALOG] {title}: {message} (fallback simulation)");
            await Task.Delay(100);
            return true;
        }
        catch
        {
            return false;
        }
    }

    public async Task<Guid?> ShowWarehouseSelectionDialogAsync(string title, string message, List<WarehouseListDto> warehouses)
    {
        try
        {
            if (!warehouses.Any())
            {
                _debugService.LogWarning("[DIALOG] No warehouses available for selection");
                return null;
            }

            var dialogViewModel = new WarehouseSelectionDialogViewModel();
            dialogViewModel.Initialize(warehouses, title, message);

            var dialog = new WarehouseSelectionDialog
            {
                DataContext = dialogViewModel
            };

            // Try to get the main window for proper modal dialog display
            try
            {
                var mainWindow = Avalonia.Application.Current?.ApplicationLifetime is Avalonia.Controls.ApplicationLifetimes.IClassicDesktopStyleApplicationLifetime desktop
                    ? desktop.MainWindow
                    : null;

                if (mainWindow != null)
                {
                    await dialog.ShowDialog(mainWindow);
                    return dialog.DialogResult ? dialog.SelectedWarehouseId : null;
                }
            }
            catch (Exception ex)
            {
                _debugService.LogError(ex, "Could not show warehouse selection dialog");
            }

            // Fallback: simulate user selection for non-desktop platforms
            var fallbackSelection = warehouses.First();
            _debugService.LogWarning($"[DIALOG] Warehouse selection fallback: {fallbackSelection.Id} ({fallbackSelection.Name})");
            await Task.Delay(100);
            return fallbackSelection.Id;
        }
        catch
        {
            return null;
        }
    }
}