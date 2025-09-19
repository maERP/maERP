using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using maERP.Domain.Dtos.Customer;
using maERP.UI.Services;
using maERP.UI.Shared.ViewModels;

namespace maERP.UI.Features.Customers.ViewModels;

public partial class CustomerListViewModel : ViewModelBase
{
    private readonly IHttpService _httpService;
    private readonly IDebugService _debugService;

    [ObservableProperty]
    private ObservableCollection<CustomerListDto> customers = new();

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
    private CustomerListDto? selectedCustomer;

    public bool ShouldShowDataGrid => !IsLoading && string.IsNullOrEmpty(ErrorMessage);

    public Func<Guid, Task>? NavigateToCustomerDetail { get; set; }
    public Func<Task>? NavigateToCreateCustomer { get; set; }

    public CustomerListViewModel(IHttpService httpService, IDebugService debugService)
    {
        _httpService = httpService;
        _debugService = debugService;
    }

    public async Task InitializeAsync()
    {
        await LoadCustomersAsync();
    }

    [RelayCommand]
    private async Task LoadCustomersAsync()
    {
        IsLoading = true;
        ErrorMessage = string.Empty;

        try
        {
            var result = await _httpService.GetPaginatedAsync<CustomerListDto>("customers", CurrentPage, PageSize, SearchText, "FirstName Ascending");

            if (result == null)
            {
                ErrorMessage = "Nicht authentifiziert oder Server-URL fehlt";
                Customers.Clear();
                _debugService.LogWarning("GetPaginatedAsync returned null - not authenticated or no server URL");
            }
            else if (result.Succeeded && result.Data != null)
            {
                Customers.Clear();
                foreach (var customer in result.Data)
                {
                    Customers.Add(customer);
                }
                TotalCount = result.TotalCount;
                _debugService.LogInfo($"Loaded {Customers.Count} customers successfully");
            }
            else
            {
                ErrorMessage = result.Messages?.FirstOrDefault() ?? "Fehler beim Laden der Kunden";
                Customers.Clear();
                _debugService.LogError($"Failed to load customers: {ErrorMessage}");
            }
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Fehler beim Laden der Kunden: {ex.Message}";
            Customers.Clear();
            _debugService.LogError(ex, "Exception loading customers");
        }
        finally
        {
            IsLoading = false;
            _debugService.LogDebug("finally");
        }
    }

    [RelayCommand]
    private async Task SearchCustomersAsync()
    {
        CurrentPage = 0;
        await LoadCustomersAsync();
    }

    [RelayCommand]
    public async Task RefreshAsync()
    {
        await LoadCustomersAsync();
    }

    [RelayCommand]
    private async Task NextPageAsync()
    {
        if ((CurrentPage + 1) * PageSize < TotalCount)
        {
            CurrentPage++;
            await LoadCustomersAsync();
        }
    }

    [RelayCommand]
    private async Task PreviousPageAsync()
    {
        if (CurrentPage > 0)
        {
            CurrentPage--;
            await LoadCustomersAsync();
        }
    }

    [RelayCommand]
    private void SelectCustomer(CustomerListDto? customer)
    {
        SelectedCustomer = customer;
    }

    [RelayCommand]
    private async Task ViewCustomerDetails(CustomerListDto? customer)
    {
        if (customer == null || NavigateToCustomerDetail == null) return;

        SelectedCustomer = customer;
        await NavigateToCustomerDetail(customer.Id);
    }

    [RelayCommand]
    private async Task CreateNewCustomer()
    {
        if (NavigateToCreateCustomer == null) return;

        await NavigateToCreateCustomer();
    }

    public bool CanGoNext => (CurrentPage + 1) * PageSize < TotalCount;
    public bool CanGoPrevious => CurrentPage > 0;
    public int DisplayPageNumber => CurrentPage + 1;
    public int TotalPages => TotalCount > 0 ? (int)Math.Ceiling((double)TotalCount / PageSize) : 1;
}