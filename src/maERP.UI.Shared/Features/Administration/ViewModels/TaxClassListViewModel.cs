using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using maERP.Domain.Dtos.TaxClass;
using maERP.UI.Services;
using maERP.UI.Shared.ViewModels;
namespace maERP.UI.Features.Administration.ViewModels;

public partial class TaxClassListViewModel : ViewModelBase
{
    private readonly IHttpService _httpService;
    private readonly IDebugService _debugService;

    [ObservableProperty]
    private ObservableCollection<TaxClassListDto> taxClasses = new();

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
    private TaxClassListDto? selectedTaxClass;

    public bool ShouldShowDataGrid => !IsLoading && string.IsNullOrEmpty(ErrorMessage);

    public Func<Task>? NavigateToTaxClassInput { get; set; }
    public Func<Guid, Task>? NavigateToTaxClassDetail { get; set; }

    public TaxClassListViewModel(IHttpService httpService, IDebugService debugService)
    {
        _httpService = httpService;
        _debugService = debugService;
    }

    public async Task InitializeAsync()
    {
        await LoadTaxClassesAsync();
    }

    [RelayCommand]
    private async Task LoadTaxClassesAsync()
    {
        IsLoading = true;
        ErrorMessage = string.Empty;

        try
        {
            var result = await _httpService.GetPaginatedAsync<TaxClassListDto>("taxclasses", CurrentPage, PageSize, SearchText, "Id Ascending");

            if (result == null)
            {
                ErrorMessage = "Nicht authentifiziert oder Server-URL fehlt";
                TaxClasses.Clear();
                _debugService.LogWarning("GetPaginatedAsync returned null - not authenticated or no server URL");
            }
            else if (result.Succeeded && result.Data != null)
            {
                TaxClasses.Clear();
                foreach (var taxClass in result.Data)
                {
                    TaxClasses.Add(taxClass);
                }
                TotalCount = result.TotalCount;
                _debugService.LogInfo($"Loaded {TaxClasses.Count} tax classes successfully");
            }
            else
            {
                ErrorMessage = result.Messages?.FirstOrDefault() ?? "Fehler beim Laden der Steuerklassen";
                TaxClasses.Clear();
                _debugService.LogError($"Failed to load tax classes: {ErrorMessage}");
            }
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Fehler beim Laden der Steuerklassen: {ex.Message}";
            TaxClasses.Clear();
            _debugService.LogError($"Exception loading tax classes: {ex}");
        }
        finally
        {
            IsLoading = false;
            _debugService.LogDebug("LoadTaxClassesAsync completed");
        }
    }

    [RelayCommand]
    private async Task SearchTaxClassesAsync()
    {
        CurrentPage = 0;
        await LoadTaxClassesAsync();
    }

    [RelayCommand]
    public async Task RefreshAsync()
    {
        await LoadTaxClassesAsync();
    }

    [RelayCommand]
    private async Task NextPageAsync()
    {
        if ((CurrentPage + 1) * PageSize < TotalCount)
        {
            CurrentPage++;
            await LoadTaxClassesAsync();
        }
    }

    [RelayCommand]
    private async Task PreviousPageAsync()
    {
        if (CurrentPage > 0)
        {
            CurrentPage--;
            await LoadTaxClassesAsync();
        }
    }

    [RelayCommand]
    private void SelectTaxClass(TaxClassListDto? taxClass)
    {
        SelectedTaxClass = taxClass;
    }

    [RelayCommand]
    private async Task AddTaxClass()
    {
        if (NavigateToTaxClassInput != null)
            await NavigateToTaxClassInput();
    }

    [RelayCommand]
    private async Task ViewTaxClassDetails(TaxClassListDto? taxClass)
    {
        if (taxClass == null || NavigateToTaxClassDetail == null) return;

        SelectedTaxClass = taxClass;
        await NavigateToTaxClassDetail(taxClass.Id);
    }

    public bool CanGoNext => (CurrentPage + 1) * PageSize < TotalCount;
    public bool CanGoPrevious => CurrentPage > 0;
    public int DisplayPageNumber => CurrentPage + 1;
    public int TotalPages => TotalCount > 0 ? (int)Math.Ceiling((double)TotalCount / PageSize) : 1;
}