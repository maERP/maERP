using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using maERP.Domain.Dtos.Warehouse;
using maERP.UI.Services;
using maERP.UI.Shared.ViewModels;

namespace maERP.UI.Features.Warehouses.ViewModels;

public partial class WarehouseDetailViewModel : ViewModelBase
{
    private readonly IHttpService _httpService;
    private readonly IDialogService _dialogService;

    [ObservableProperty]
    private WarehouseDetailDto? warehouse;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ShouldShowContent))]
    private bool isLoading;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ShouldShowContent))]
    private string errorMessage = string.Empty;

    [ObservableProperty]
    private int warehouseId;

    [ObservableProperty]
    private bool isDeleting;

    public bool ShouldShowContent => !IsLoading && string.IsNullOrEmpty(ErrorMessage) && Warehouse != null;

    public Action? GoBackAction { get; set; }
    public Func<int, Task>? NavigateToEditWarehouse { get; set; }

    // Computed properties for better display
    public bool HasName => Warehouse != null && !string.IsNullOrEmpty(Warehouse.Name);

    // Future enhancement properties (currently placeholders)
    public bool HasSalesChannels => false; // TODO: Implement when sales channel data is available
    public bool HasProductStocks => Warehouse?.ProductCount > 0;
    public int TotalProducts => Warehouse?.ProductCount ?? 0;
    public decimal TotalStockValue => 0; // TODO: Calculate from product stocks and prices

    public WarehouseDetailViewModel(IHttpService httpService, IDialogService dialogService)
    {
        _httpService = httpService;
        _dialogService = dialogService;
    }

    public async Task InitializeAsync(int warehouseId)
    {
        WarehouseId = warehouseId;
        await LoadWarehouseAsync();
    }

    [RelayCommand]
    private async Task LoadWarehouseAsync()
    {
        if (WarehouseId <= 0) return;

        IsLoading = true;
        ErrorMessage = string.Empty;

        try
        {
            var result = await _httpService.GetAsync<WarehouseDetailDto>($"warehouses/{WarehouseId}");

            if (result == null)
            {
                ErrorMessage = "Nicht authentifiziert oder Server-URL fehlt";
                Warehouse = null;
                System.Diagnostics.Debug.WriteLine("GetAsync returned null - not authenticated or no server URL");
            }
            else if (result.Succeeded && result.Data != null)
            {
                Warehouse = result.Data;
                OnPropertyChanged(nameof(HasName));
                OnPropertyChanged(nameof(HasSalesChannels));
                OnPropertyChanged(nameof(HasProductStocks));
                OnPropertyChanged(nameof(TotalProducts));
                OnPropertyChanged(nameof(TotalStockValue));
                System.Diagnostics.Debug.WriteLine($"Loaded warehouse {WarehouseId} successfully");
            }
            else
            {
                ErrorMessage = result.Messages?.FirstOrDefault() ?? $"Fehler beim Laden des Lagers {WarehouseId}";
                Warehouse = null;
                System.Diagnostics.Debug.WriteLine($"Failed to load warehouse {WarehouseId}: {ErrorMessage}");
            }
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Fehler beim Laden des Lagers: {ex.Message}";
            Warehouse = null;
            System.Diagnostics.Debug.WriteLine($"Exception loading warehouse {WarehouseId}: {ex}");
        }
        finally
        {
            IsLoading = false;
        }
    }

    [RelayCommand]
    private async Task RefreshAsync()
    {
        await LoadWarehouseAsync();
    }

    [RelayCommand]
    private void GoBack()
    {
        GoBackAction?.Invoke();
    }

    [RelayCommand]
    private async Task EditWarehouse()
    {
        if (Warehouse == null || NavigateToEditWarehouse == null) return;
        
        await NavigateToEditWarehouse(Warehouse.Id);
    }

    [RelayCommand]
    private void ManageStock()
    {
        // TODO: Implement stock management navigation
        System.Diagnostics.Debug.WriteLine($"Managing stock for warehouse {WarehouseId}");
    }

    [RelayCommand]
    private void ManageSalesChannels()
    {
        // TODO: Implement sales channel management navigation
        System.Diagnostics.Debug.WriteLine($"Managing sales channels for warehouse {WarehouseId}");
    }

    [RelayCommand]
    private void ViewReports()
    {
        // TODO: Implement warehouse reports navigation
        System.Diagnostics.Debug.WriteLine($"Viewing reports for warehouse {WarehouseId}");
    }

    [RelayCommand]
    private async Task DeleteWarehouse()
    {
        if (Warehouse == null || IsDeleting) return;

        // Show confirmation dialog
        var confirmed = await ShowDeleteConfirmationAsync();
        if (!confirmed) return;

        int? targetWarehouseId = null;

        // If warehouse has products, ask user where to move them
        if (HasProductStocks)
        {
            targetWarehouseId = await ShowWarehouseSelectionDialogAsync();
            if (targetWarehouseId == null) return; // User cancelled
        }

        IsDeleting = true;
        ErrorMessage = string.Empty;

        try
        {
            var endpoint = $"warehouses/{Warehouse.Id}";
            if (targetWarehouseId.HasValue)
            {
                endpoint += $"?newWarehouseId={targetWarehouseId.Value}";
            }

            var result = await _httpService.DeleteAsync(endpoint);

            if (result == null)
            {
                ErrorMessage = "Nicht authentifiziert oder Server-URL fehlt";
                System.Diagnostics.Debug.WriteLine("DeleteAsync returned null - not authenticated or no server URL");
            }
            else if (result.Succeeded)
            {
                System.Diagnostics.Debug.WriteLine($"Successfully deleted warehouse {Warehouse.Id}");
                GoBackAction?.Invoke();
            }
            else
            {
                ErrorMessage = result.Messages?.FirstOrDefault() ?? "Fehler beim Löschen des Lagers";
                System.Diagnostics.Debug.WriteLine($"Failed to delete warehouse {Warehouse.Id}: {ErrorMessage}");
            }
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Fehler beim Löschen des Lagers: {ex.Message}";
            System.Diagnostics.Debug.WriteLine($"Exception deleting warehouse {Warehouse.Id}: {ex}");
        }
        finally
        {
            IsDeleting = false;
        }
    }

    private async Task<bool> ShowDeleteConfirmationAsync()
    {
        try
        {
            if (Warehouse == null) return false;
            
            var message = HasProductStocks 
                ? $"Möchten Sie das Lager '{Warehouse.Name}' wirklich löschen?\n\nEs enthält {TotalProducts} Produkte, die umverteilt werden."
                : $"Möchten Sie das Lager '{Warehouse.Name}' wirklich löschen?";
            
            return await _dialogService.ShowConfirmationDialogAsync(
                "Lager löschen",
                message,
                "Löschen",
                "Abbrechen",
                "🗑️");
        }
        catch
        {
            return false;
        }
    }

    private async Task<int?> ShowWarehouseSelectionDialogAsync()
    {
        try
        {
            // Load available warehouses (excluding current one)
            var warehousesResult = await _httpService.GetPaginatedAsync<WarehouseListDto>(
                "warehouses", 0, 100, "", "");

            if (warehousesResult?.Data == null || !warehousesResult.Succeeded)
            {
                System.Diagnostics.Debug.WriteLine("Failed to load warehouses for selection");
                return null;
            }

            var availableWarehouses = warehousesResult.Data
                .Where(w => w.Id != WarehouseId)
                .ToList();

            if (!availableWarehouses.Any())
            {
                System.Diagnostics.Debug.WriteLine("No other warehouses available for product redistribution");
                ErrorMessage = "Keine anderen Lager verfügbar für die Produktumverteilung";
                return null;
            }

            return await _dialogService.ShowWarehouseSelectionDialogAsync(
                "Produktumverteilung",
                $"Das Lager '{Warehouse?.Name}' enthält {TotalProducts} Produkte. Wählen Sie ein Ziellager für die Umverteilung:",
                availableWarehouses);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Exception during warehouse selection: {ex}");
            return null;
        }
    }
}