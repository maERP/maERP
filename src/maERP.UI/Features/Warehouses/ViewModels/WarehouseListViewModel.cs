using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using maERP.Domain.Dtos.Warehouse;
using maERP.UI.Services;
using maERP.UI.Shared.ViewModels;
namespace maERP.UI.Features.Warehouses.ViewModels;

public partial class WarehouseListViewModel : ViewModelBase
{
    private readonly IHttpService _httpService;
    private readonly IDebugService _debugService;

    [ObservableProperty]
    private ObservableCollection<WarehouseListDto> warehouses = new();

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ShouldShowDataGrid))]
    private bool isLoading;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ShouldShowDataGrid))]
    private string errorMessage = string.Empty;

    [ObservableProperty]
    private string searchText = string.Empty;

    [ObservableProperty]
    private int totalCount;

    [ObservableProperty]
    private int currentPage;

    [ObservableProperty]
    private int pageSize = 50;

    [ObservableProperty]
    private WarehouseListDto? selectedWarehouse;

    public bool ShouldShowDataGrid => !IsLoading && string.IsNullOrEmpty(ErrorMessage);

    public Action<int>? NavigateToWarehouseDetail { get; set; }
    public Func<Task>? NavigateToWarehouseCreate { get; set; }

    public WarehouseListViewModel(IHttpService httpService, IDebugService debugService)
    {
        _httpService = httpService;
        _debugService = debugService;
    }

    public async Task InitializeAsync()
    {
        await LoadWarehousesAsync();
    }

    [RelayCommand]
    private async Task LoadWarehousesAsync()
    {
        IsLoading = true;
        ErrorMessage = string.Empty;

        try
        {
            var result = await _httpService.GetPaginatedAsync<WarehouseListDto>("warehouses", CurrentPage, PageSize, SearchText, "Name Ascending");

            if (result == null)
            {
                ErrorMessage = "Nicht authentifiziert oder Server-URL fehlt";
                Warehouses.Clear();
                _debugService.LogWarning("GetPaginatedAsync returned null - not authenticated or no server URL");
            }
            else if (result.Succeeded && result.Data != null)
            {
                Warehouses.Clear();
                foreach (var warehouse in result.Data)
                {
                    Warehouses.Add(warehouse);
                }
                TotalCount = result.TotalCount;
                _debugService.LogInfo($"Loaded {Warehouses.Count} warehouses successfully");
            }
            else
            {
                ErrorMessage = result.Messages?.FirstOrDefault() ?? "Fehler beim Laden der Lager";
                Warehouses.Clear();
                _debugService.LogError($"Failed to load warehouses: {ErrorMessage}");
            }
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Fehler beim Laden der Lager: {ex.Message}";
            Warehouses.Clear();
            _debugService.LogError(ex, "Exception loading warehouses");
        }
        finally
        {
            IsLoading = false;
            _debugService.LogDebug("finally");
        }
    }

    [RelayCommand]
    private async Task SearchWarehousesAsync()
    {
        CurrentPage = 0;
        await LoadWarehousesAsync();
    }

    [RelayCommand]
    public async Task RefreshAsync()
    {
        await LoadWarehousesAsync();
    }

    [RelayCommand]
    private async Task NextPageAsync()
    {
        if ((CurrentPage + 1) * PageSize < TotalCount)
        {
            CurrentPage++;
            await LoadWarehousesAsync();
        }
    }

    [RelayCommand]
    private async Task PreviousPageAsync()
    {
        if (CurrentPage > 0)
        {
            CurrentPage--;
            await LoadWarehousesAsync();
        }
    }

    [RelayCommand]
    private void SelectWarehouse(WarehouseListDto? warehouse)
    {
        SelectedWarehouse = warehouse;
    }

    [RelayCommand]
    private void ViewWarehouseDetails(WarehouseListDto? warehouse)
    {
        if (warehouse == null || NavigateToWarehouseDetail == null) return;
        
        SelectedWarehouse = warehouse;
        NavigateToWarehouseDetail(warehouse.Id);
    }

    [RelayCommand]
    private async Task CreateNewWarehouse()
    {
        if (NavigateToWarehouseCreate == null) return;
        
        await NavigateToWarehouseCreate();
    }

    public bool CanGoNext => (CurrentPage + 1) * PageSize < TotalCount;
    public bool CanGoPrevious => CurrentPage > 0;
    public int DisplayPageNumber => CurrentPage + 1;
    public int TotalPages => TotalCount > 0 ? (int)Math.Ceiling((double)TotalCount / PageSize) : 1;
}