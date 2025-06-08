using System;
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

    public bool ShouldShowContent => !IsLoading && string.IsNullOrEmpty(ErrorMessage) && Warehouse != null;

    public Action? GoBackAction { get; set; }
    public Func<int, Task>? NavigateToEditWarehouse { get; set; }

    // Computed properties for better display
    public bool HasName => Warehouse != null && !string.IsNullOrEmpty(Warehouse.Name);

    // Future enhancement properties (currently placeholders)
    public bool HasSalesChannels => false; // TODO: Implement when sales channel data is available
    public bool HasProductStocks => false; // TODO: Implement when product stock data is available
    public int TotalProducts => 0; // TODO: Calculate from product stocks
    public decimal TotalStockValue => 0; // TODO: Calculate from product stocks and prices

    public WarehouseDetailViewModel(IHttpService httpService)
    {
        _httpService = httpService;
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
}