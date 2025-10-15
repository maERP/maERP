using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using maERP.Domain.Dtos.Tenant;
using maERP.UI.Services;
using maERP.UI.Shared.ViewModels;

namespace maERP.UI.Features.Tenants.ViewModels;

public partial class TenantListViewModel : ViewModelBase
{
    private readonly IHttpService _httpService;
    private readonly IDebugService _debugService;

    [ObservableProperty]
    private ObservableCollection<TenantListDto> tenants = new();

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
    private TenantListDto? selectedTenant;

    public bool ShouldShowDataGrid => !IsLoading && string.IsNullOrEmpty(ErrorMessage);

    public Func<Guid, Task>? NavigateToTenantDetail { get; set; }
    public Func<Task>? NavigateToTenantInput { get; set; }

    public TenantListViewModel(IHttpService httpService, IDebugService debugService)
    {
        _httpService = httpService;
        _debugService = debugService;
    }

    public async Task InitializeAsync()
    {
        await LoadTenantsAsync();
    }

    [RelayCommand]
    private async Task LoadTenantsAsync()
    {
        IsLoading = true;
        ErrorMessage = string.Empty;

        try
        {
            var result = await _httpService.GetPaginatedAsync<TenantListDto>("superadmin/tenants", CurrentPage, PageSize, SearchText, "Name Ascending");

            if (result == null)
            {
                ErrorMessage = "Nicht authentifiziert oder Server-URL fehlt";
                Tenants.Clear();
                _debugService.LogWarning("GetPaginatedAsync returned null for tenants - not authenticated or missing server URL");
            }
            else if (result.Succeeded && result.Data != null)
            {
                Tenants.Clear();
                foreach (var tenant in result.Data)
                {
                    Tenants.Add(tenant);
                }
                TotalCount = result.TotalCount;
                _debugService.LogInfo($"Loaded {Tenants.Count} tenants successfully");
            }
            else
            {
                ErrorMessage = result.Messages?.FirstOrDefault() ?? "Fehler beim Laden der Tenants";
                Tenants.Clear();
                _debugService.LogError($"Failed to load tenants: {ErrorMessage}");
            }
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Fehler beim Laden der Tenants: {ex.Message}";
            Tenants.Clear();
            _debugService.LogError(ex, "Exception loading tenants");
        }
        finally
        {
            IsLoading = false;
        }
    }

    [RelayCommand]
    private async Task SearchTenantsAsync()
    {
        CurrentPage = 0;
        await LoadTenantsAsync();
    }

    [RelayCommand]
    public async Task RefreshAsync()
    {
        await LoadTenantsAsync();
    }

    [RelayCommand]
    private async Task NextPageAsync()
    {
        if ((CurrentPage + 1) * PageSize < TotalCount)
        {
            CurrentPage++;
            await LoadTenantsAsync();
        }
    }

    [RelayCommand]
    private async Task PreviousPageAsync()
    {
        if (CurrentPage > 0)
        {
            CurrentPage--;
            await LoadTenantsAsync();
        }
    }

    [RelayCommand]
    private void SelectTenant(TenantListDto? tenant)
    {
        SelectedTenant = tenant;
    }

    [RelayCommand]
    private async Task ViewTenantDetails(TenantListDto? tenant)
    {
        if (tenant == null || NavigateToTenantDetail == null)
        {
            return;
        }

        SelectedTenant = tenant;
        await NavigateToTenantDetail(tenant.Id);
    }

    [RelayCommand]
    private async Task CreateTenantAsync()
    {
        if (NavigateToTenantInput != null)
        {
            await NavigateToTenantInput();
        }
    }

    public bool CanGoNext => (CurrentPage + 1) * PageSize < TotalCount;
    public bool CanGoPrevious => CurrentPage > 0;
    public int DisplayPageNumber => CurrentPage + 1;
    public int TotalPages => TotalCount > 0 ? (int)Math.Ceiling((double)TotalCount / PageSize) : 1;
}
