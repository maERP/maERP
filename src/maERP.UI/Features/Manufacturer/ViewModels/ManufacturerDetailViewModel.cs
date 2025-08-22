using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using maERP.Domain.Dtos.Manufacturer;
using maERP.UI.Services;
using maERP.UI.Shared.ViewModels;

namespace maERP.UI.Features.Manufacturer.ViewModels;

public partial class ManufacturerDetailViewModel : ViewModelBase
{
    private readonly IHttpService _httpService;
    private readonly IDialogService _dialogService;
    private readonly IDebugService _debugService;

    [ObservableProperty]
    private ManufacturerDetailDto? manufacturer;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ShouldShowContent))]
    private bool isLoading;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ShouldShowContent))]
    private string errorMessage = string.Empty;

    [ObservableProperty]
    private int manufacturerId;

    [ObservableProperty]
    private bool isDeleting;

    public bool ShouldShowContent => !IsLoading && string.IsNullOrEmpty(ErrorMessage) && Manufacturer != null;

    public Action? GoBackAction { get; set; }
    public Func<int, Task>? NavigateToEditManufacturer { get; set; }

    // Computed properties for better display
    public bool HasName => Manufacturer != null && !string.IsNullOrEmpty(Manufacturer.Name);
    public bool HasAddress => Manufacturer != null &&
        (!string.IsNullOrEmpty(Manufacturer.Street) ||
         !string.IsNullOrEmpty(Manufacturer.City) ||
         !string.IsNullOrEmpty(Manufacturer.Country));
    public bool HasContactInfo => Manufacturer != null &&
        (!string.IsNullOrEmpty(Manufacturer.Phone) ||
         !string.IsNullOrEmpty(Manufacturer.Email) ||
         !string.IsNullOrEmpty(Manufacturer.Website));

    public ManufacturerDetailViewModel(IHttpService httpService, IDialogService dialogService, IDebugService debugService)
    {
        _httpService = httpService;
        _dialogService = dialogService;
        _debugService = debugService;
    }

    public async Task InitializeAsync(int manufacturerId)
    {
        ManufacturerId = manufacturerId;
        await LoadManufacturerAsync();
    }

    [RelayCommand]
    private async Task LoadManufacturerAsync()
    {
        if (ManufacturerId <= 0) return;

        IsLoading = true;
        ErrorMessage = string.Empty;

        try
        {
            var result = await _httpService.GetAsync<ManufacturerDetailDto>($"manufacturers/{ManufacturerId}");

            if (result == null)
            {
                ErrorMessage = "Nicht authentifiziert oder Server-URL fehlt";
                Manufacturer = null;
                _debugService.LogWarning("GetAsync returned null - not authenticated or no server URL");
            }
            else if (result.Succeeded && result.Data != null)
            {
                Manufacturer = result.Data;
                OnPropertyChanged(nameof(HasName));
                OnPropertyChanged(nameof(HasAddress));
                OnPropertyChanged(nameof(HasContactInfo));
                _debugService.LogInfo($"Loaded manufacturer {ManufacturerId} successfully");
            }
            else
            {
                ErrorMessage = result.Messages?.FirstOrDefault() ?? $"Fehler beim Laden des Herstellers {ManufacturerId}";
                Manufacturer = null;
                _debugService.LogError($"Failed to load manufacturer {ManufacturerId}: {ErrorMessage}");
            }
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Fehler beim Laden des Herstellers: {ex.Message}";
            Manufacturer = null;
            _debugService.LogError(ex, $"Exception loading manufacturer {ManufacturerId}");
        }
        finally
        {
            IsLoading = false;
        }
    }

    [RelayCommand]
    private async Task RefreshAsync()
    {
        await LoadManufacturerAsync();
    }

    [RelayCommand]
    private void GoBack()
    {
        GoBackAction?.Invoke();
    }

    [RelayCommand]
    private async Task EditManufacturer()
    {
        if (Manufacturer == null || NavigateToEditManufacturer == null) return;

        await NavigateToEditManufacturer(Manufacturer.Id);
    }

    [RelayCommand]
    private async Task DeleteManufacturer()
    {
        if (Manufacturer == null || IsDeleting) return;

        // Show confirmation dialog
        var confirmed = await ShowDeleteConfirmationAsync();
        if (!confirmed) return;

        IsDeleting = true;
        ErrorMessage = string.Empty;

        try
        {
            var result = await _httpService.DeleteAsync($"manufacturers/{Manufacturer.Id}");

            if (result == null)
            {
                ErrorMessage = "Nicht authentifiziert oder Server-URL fehlt";
                _debugService.LogWarning("DeleteAsync returned null - not authenticated or no server URL");
            }
            else if (result.Succeeded)
            {
                _debugService.LogInfo($"Successfully deleted manufacturer {Manufacturer.Id}");
                GoBackAction?.Invoke();
            }
            else
            {
                ErrorMessage = result.Messages?.FirstOrDefault() ?? "Fehler beim L\u00f6schen des Herstellers";
                _debugService.LogError($"Failed to delete manufacturer {Manufacturer.Id}: {ErrorMessage}");
            }
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Fehler beim L\u00f6schen des Herstellers: {ex.Message}";
            _debugService.LogError(ex, $"Exception deleting manufacturer {Manufacturer.Id}");
        }
        finally
        {
            IsDeleting = false;
        }
    }

    [RelayCommand]
    private void OpenEmail(string? email)
    {
        if (string.IsNullOrEmpty(email)) return;

        try
        {
            var emailUri = $"mailto:{email}";
            Process.Start(new ProcessStartInfo(emailUri) { UseShellExecute = true });
            _debugService.LogInfo($"Opened email client for {email}");
        }
        catch (Exception ex)
        {
            _debugService.LogError(ex, $"Failed to open email client for {email}");
            ErrorMessage = "Fehler beim \u00d6ffnen des E-Mail-Clients";
        }
    }

    [RelayCommand]
    private void OpenWebsite(string? website)
    {
        if (string.IsNullOrEmpty(website)) return;

        try
        {
            var url = website.StartsWith("http://") || website.StartsWith("https://")
                ? website
                : $"https://{website}";

            Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });
            _debugService.LogInfo($"Opened website {url}");
        }
        catch (Exception ex)
        {
            _debugService.LogError(ex, $"Failed to open website {website}");
            ErrorMessage = "Fehler beim \u00d6ffnen der Website";
        }
    }

    private async Task<bool> ShowDeleteConfirmationAsync()
    {
        try
        {
            if (Manufacturer == null) return false;

            var message = $"M\u00f6chten Sie den Hersteller '{Manufacturer.Name}' wirklich l\u00f6schen?\n\nDieser Vorgang kann nicht r\u00fcckg\u00e4ngig gemacht werden.";

            return await _dialogService.ShowConfirmationDialogAsync(
                "Hersteller l\u00f6schen",
                message,
                "L\u00f6schen",
                "Abbrechen",
                "\ud83d\uddd1\ufe0f");
        }
        catch
        {
            return false;
        }
    }
}