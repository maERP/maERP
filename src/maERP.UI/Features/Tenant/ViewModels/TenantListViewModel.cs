using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using maERP.Domain.Dtos.Tenant;
using maERP.UI.Services;
using maERP.UI.Shared.ViewModels;

namespace maERP.UI.Features.Tenant.ViewModels;

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
    [NotifyPropertyChangedFor(nameof(DisplayPageNumber))]
    private int currentPage;

    [ObservableProperty]
    private int pageSize = 50;

    [ObservableProperty]
    private int totalPages;

    [ObservableProperty]
    private TenantListDto? selectedTenant;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(CanGoPrevious))]
    private bool hasPreviousPage;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(CanGoNext))]
    private bool hasNextPage;

    public bool ShouldShowDataGrid => !IsLoading && string.IsNullOrEmpty(ErrorMessage);
    public bool CanGoPrevious => HasPreviousPage && !IsLoading;
    public bool CanGoNext => HasNextPage && !IsLoading;
    public int DisplayPageNumber => CurrentPage + 1;

    public Func<Guid, Task>? NavigateToEditTenant { get; set; }
    public Func<Task>? NavigateToCreateTenant { get; set; }

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
            var result = await _httpService.GetPaginatedAsync<TenantListDto>(
                "tenants",
                CurrentPage,
                PageSize,
                SearchText,
                "Name Ascending");

            if (result == null)
            {
                ErrorMessage = "Nicht authentifiziert oder Server-URL fehlt";
                Tenants.Clear();
                _debugService.LogWarning("GetPaginatedAsync returned null - not authenticated or no server URL");
            }
            else if (result.Succeeded && result.Data != null)
            {
                Tenants.Clear();
                foreach (var tenant in result.Data)
                {
                    Tenants.Add(tenant);
                }
                TotalCount = result.TotalCount;
                TotalPages = result.TotalPages;
                HasPreviousPage = result.HasPreviousPage;
                HasNextPage = result.HasNextPage;
                CurrentPage = result.CurrentPage;

                _debugService.LogInfo($"Loaded {Tenants.Count} tenants successfully");
            }
            else
            {
                ErrorMessage = result.Messages?.FirstOrDefault() ?? "Fehler beim Laden der Mandanten";
                Tenants.Clear();
                _debugService.LogError($"Failed to load tenants: {ErrorMessage}");
            }
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Fehler beim Laden der Mandanten: {ex.Message}";
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
        if (HasNextPage)
        {
            CurrentPage++;
            await LoadTenantsAsync();
        }
    }

    [RelayCommand]
    private async Task PreviousPageAsync()
    {
        if (HasPreviousPage)
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
    private async Task EditTenantAsync(TenantListDto? tenant)
    {
        if (tenant == null || NavigateToEditTenant == null) return;

        SelectedTenant = tenant;
        await NavigateToEditTenant(tenant.Id);
    }

    [RelayCommand]
    private async Task CreateNewTenantAsync()
    {
        if (NavigateToCreateTenant != null)
        {
            await NavigateToCreateTenant();
        }
    }

    public async Task OnTenantDoubleClickedAsync(TenantListDto? tenant)
    {
        await EditTenantAsync(tenant);
    }
}
