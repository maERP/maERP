using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using maERP.Domain.Dtos.Manufacturer;
using maERP.UI.Services;
using maERP.UI.Shared.ViewModels;

namespace maERP.UI.Features.Manufacturer.ViewModels;

public partial class ManufacturerListViewModel : ViewModelBase
{
    private readonly IHttpService _httpService;
    private readonly IDebugService _debugService;

    [ObservableProperty]
    private ObservableCollection<ManufacturerListDto> manufacturers = new();

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
    private ManufacturerListDto? selectedManufacturer;

    public bool ShouldShowDataGrid => !IsLoading && string.IsNullOrEmpty(ErrorMessage);

    public Func<int, Task>? NavigateToManufacturerDetail { get; set; }
    public Func<Task>? NavigateToManufacturerCreate { get; set; }

    public ManufacturerListViewModel(IHttpService httpService, IDebugService debugService)
    {
        _httpService = httpService;
        _debugService = debugService;
    }

    public async Task InitializeAsync()
    {
        await LoadManufacturersAsync();
    }

    [RelayCommand]
    private async Task LoadManufacturersAsync()
    {
        IsLoading = true;
        ErrorMessage = string.Empty;

        try
        {
            var result = await _httpService.GetPaginatedAsync<ManufacturerListDto>("manufacturers", CurrentPage, PageSize, SearchText, "Name Ascending");

            if (result == null)
            {
                ErrorMessage = "Nicht authentifiziert oder Server-URL fehlt";
                Manufacturers.Clear();
                _debugService.LogWarning("GetPaginatedAsync returned null - not authenticated or no server URL");
            }
            else if (result.Succeeded && result.Data != null)
            {
                Manufacturers.Clear();
                foreach (var manufacturer in result.Data)
                {
                    Manufacturers.Add(manufacturer);
                }
                TotalCount = result.TotalCount;
                _debugService.LogInfo($"Loaded {Manufacturers.Count} manufacturers successfully");
            }
            else
            {
                ErrorMessage = result.Messages?.FirstOrDefault() ?? "Fehler beim Laden der Hersteller";
                Manufacturers.Clear();
                _debugService.LogError($"Failed to load manufacturers: {ErrorMessage}");
            }
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Fehler beim Laden der Hersteller: {ex.Message}";
            Manufacturers.Clear();
            _debugService.LogError(ex, "Exception loading manufacturers");
        }
        finally
        {
            IsLoading = false;
            _debugService.LogDebug("finally");
        }
    }

    [RelayCommand]
    private async Task SearchManufacturersAsync()
    {
        CurrentPage = 0;
        await LoadManufacturersAsync();
    }

    [RelayCommand]
    public async Task RefreshAsync()
    {
        await LoadManufacturersAsync();
    }

    [RelayCommand]
    private async Task NextPageAsync()
    {
        if ((CurrentPage + 1) * PageSize < TotalCount)
        {
            CurrentPage++;
            await LoadManufacturersAsync();
        }
    }

    [RelayCommand]
    private async Task PreviousPageAsync()
    {
        if (CurrentPage > 0)
        {
            CurrentPage--;
            await LoadManufacturersAsync();
        }
    }

    [RelayCommand]
    private void SelectManufacturer(ManufacturerListDto? manufacturer)
    {
        SelectedManufacturer = manufacturer;
    }

    [RelayCommand]
    private async Task ViewManufacturerDetails(ManufacturerListDto? manufacturer)
    {
        if (manufacturer == null || NavigateToManufacturerDetail == null) return;
        
        SelectedManufacturer = manufacturer;
        await NavigateToManufacturerDetail(manufacturer.Id);
    }

    [RelayCommand]
    private async Task CreateNewManufacturer()
    {
        if (NavigateToManufacturerCreate == null) return;
        
        await NavigateToManufacturerCreate();
    }

    public bool CanGoNext => (CurrentPage + 1) * PageSize < TotalCount;
    public bool CanGoPrevious => CurrentPage > 0;
    public int DisplayPageNumber => CurrentPage + 1;
    public int TotalPages => TotalCount > 0 ? (int)Math.Ceiling((double)TotalCount / PageSize) : 1;
}