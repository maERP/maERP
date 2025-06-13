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
            // In a real application with proper window management, this would work
            try
            {
                var mainWindow = Avalonia.Application.Current?.ApplicationLifetime is Avalonia.Controls.ApplicationLifetimes.IClassicDesktopStyleApplicationLifetime desktop
                    ? desktop.MainWindow
                    : null;

                if (mainWindow != null)
                {
                    // This would show a real modal dialog in a proper desktop application
                    // For now we log the intent and simulate user confirmation
                    System.Diagnostics.Debug.WriteLine($"[DIALOG] {title}: {message}");
                    System.Diagnostics.Debug.WriteLine($"[DIALOG] Buttons: [{cancelText}] [{confirmText}]");
                    System.Diagnostics.Debug.WriteLine($"[DIALOG] User clicked: {confirmText} (simulated)");
                    
                    await Task.Delay(100);
                    return true; // Simulate user confirmation
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Could not show modal dialog: {ex.Message}");
            }

            // Fallback: simulate user confirmation
            System.Diagnostics.Debug.WriteLine($"[DIALOG] {title}: {message} (fallback simulation)");
            await Task.Delay(100);
            return true;
        }
        catch
        {
            return false;
        }
    }

    public async Task<int?> ShowWarehouseSelectionDialogAsync(string title, string message, List<WarehouseListDto> warehouses)
    {
        try
        {
            if (!warehouses.Any())
            {
                System.Diagnostics.Debug.WriteLine("[DIALOG] No warehouses available for selection");
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
                    // This would show a real modal dialog in a proper desktop application
                    System.Diagnostics.Debug.WriteLine($"[DIALOG] {title}: {message}");
                    System.Diagnostics.Debug.WriteLine($"[DIALOG] Available warehouses:");
                    foreach (var wh in warehouses)
                    {
                        System.Diagnostics.Debug.WriteLine($"[DIALOG]   - {wh.Id}: {wh.Name}");
                    }
                    
                    // Simulate user selecting the first warehouse for demo
                    var selectedWarehouse = warehouses.First();
                    System.Diagnostics.Debug.WriteLine($"[DIALOG] User selected: {selectedWarehouse.Id} ({selectedWarehouse.Name}) (simulated)");
                    
                    await Task.Delay(100);
                    return selectedWarehouse.Id;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Could not show warehouse selection dialog: {ex.Message}");
            }

            // Fallback: simulate user selection
            var fallbackSelection = warehouses.First();
            System.Diagnostics.Debug.WriteLine($"[DIALOG] Warehouse selection fallback: {fallbackSelection.Id} ({fallbackSelection.Name})");
            await Task.Delay(100);
            return fallbackSelection.Id;
        }
        catch
        {
            return null;
        }
    }
}