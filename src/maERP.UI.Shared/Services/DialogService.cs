using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using maERP.Domain.Dtos.Warehouse;
using maERP.UI.Shared.Features.Warehouses.ViewModels;
using maERP.UI.Shared.Features.Warehouses.Views;
using maERP.UI.Shared.Shared.ViewModels;
using maERP.UI.Shared.Shared.Views;

namespace maERP.UI.Shared.Services;

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

            var dialogContent = new ConfirmationDialog
            {
                DataContext = dialogViewModel
            };

            var contentDialog = new ContentDialog
            {
                Title = title,
                Content = dialogContent,
                PrimaryButtonText = confirmText,
                SecondaryButtonText = cancelText,
                DefaultButton = ContentDialogButton.Primary,
                XamlRoot = GetXamlRoot()
            };

            var result = await contentDialog.ShowAsync();
            return result == ContentDialogResult.Primary;
        }
        catch (Exception ex)
        {
            _debugService.LogError(ex, "Error showing confirmation dialog");
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

            var dialogContent = new WarehouseSelectionDialog
            {
                DataContext = dialogViewModel
            };

            var contentDialog = new ContentDialog
            {
                Title = title,
                Content = dialogContent,
                PrimaryButtonText = "Auswählen",
                SecondaryButtonText = "Abbrechen",
                DefaultButton = ContentDialogButton.Primary,
                XamlRoot = GetXamlRoot()
            };

            var result = await contentDialog.ShowAsync();

            if (result == ContentDialogResult.Primary)
            {
                return dialogViewModel.SelectedWarehouseId;
            }

            return null;
        }
        catch (Exception ex)
        {
            _debugService.LogError(ex, "Error showing warehouse selection dialog");
            return null;
        }
    }

    private XamlRoot? GetXamlRoot()
    {
        try
        {
            // Try to get the XamlRoot from the current window
#if HAS_UNO || NETFX_CORE
            var window = Microsoft.UI.Xaml.Window.Current;
#else
            var window = App.Services?.GetService(typeof(Window)) as Window;
#endif
            return window?.Content?.XamlRoot;
        }
        catch (Exception ex)
        {
            _debugService.LogError(ex, "Could not get XamlRoot for dialog");
            return null;
        }
    }
}
