using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using maERP.Domain.Dtos.Order;
using maERP.UI.Services;

namespace maERP.UI.ViewModels;

public partial class OrderListViewModel : ViewModelBase
{
    private readonly IHttpService _httpService;

    [ObservableProperty]
    private ObservableCollection<OrderListDto> orders = new();

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
    private OrderListDto? selectedOrder;

    public bool ShouldShowDataGrid => !IsLoading && string.IsNullOrEmpty(ErrorMessage);

    public Func<int, Task>? NavigateToOrderDetail { get; set; }

    public OrderListViewModel(IHttpService httpService)
    {
        _httpService = httpService;
    }

    public async Task InitializeAsync()
    {
        await LoadOrdersAsync();
    }

    [RelayCommand]
    private async Task LoadOrdersAsync()
    {
        IsLoading = true;
        ErrorMessage = string.Empty;

        try
        {
            var result = await _httpService.GetPaginatedAsync<OrderListDto>("orders", CurrentPage, PageSize, SearchText, "DateOrdered Descending");

            if (result == null)
            {
                ErrorMessage = "Nicht authentifiziert oder Server-URL fehlt";
                Orders.Clear();
                System.Diagnostics.Debug.WriteLine("GetPaginatedAsync returned null - not authenticated or no server URL");
            }
            else if (result.Succeeded && result.Data != null)
            {
                Orders.Clear();
                foreach (var order in result.Data)
                {
                    Orders.Add(order);
                }
                TotalCount = result.TotalCount;
                System.Diagnostics.Debug.WriteLine($"Loaded {Orders.Count} orders successfully");
            }
            else
            {
                ErrorMessage = result.Messages?.FirstOrDefault() ?? "Fehler beim Laden der Bestellungen";
                Orders.Clear();
                System.Diagnostics.Debug.WriteLine($"Failed to load orders: {ErrorMessage}");
            }
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Fehler beim Laden der Bestellungen: {ex.Message}";
            Orders.Clear();
            System.Diagnostics.Debug.WriteLine($"Exception loading orders: {ex}");
        }
        finally
        {
            IsLoading = false;
            System.Diagnostics.Debug.WriteLine($"finally");
        }
    }

    [RelayCommand]
    private async Task SearchOrdersAsync()
    {
        CurrentPage = 0;
        await LoadOrdersAsync();
    }

    [RelayCommand]
    public async Task RefreshAsync()
    {
        await LoadOrdersAsync();
    }

    [RelayCommand]
    private async Task NextPageAsync()
    {
        if ((CurrentPage + 1) * PageSize < TotalCount)
        {
            CurrentPage++;
            await LoadOrdersAsync();
        }
    }

    [RelayCommand]
    private async Task PreviousPageAsync()
    {
        if (CurrentPage > 0)
        {
            CurrentPage--;
            await LoadOrdersAsync();
        }
    }

    [RelayCommand]
    private void SelectOrder(OrderListDto? order)
    {
        SelectedOrder = order;
    }

    [RelayCommand]
    private async Task ViewOrderDetails(OrderListDto? order)
    {
        if (order == null || NavigateToOrderDetail == null) return;
        
        SelectedOrder = order;
        await NavigateToOrderDetail(order.Id);
    }

    public bool CanGoNext => (CurrentPage + 1) * PageSize < TotalCount;
    public bool CanGoPrevious => CurrentPage > 0;
    public int DisplayPageNumber => CurrentPage + 1;
    public int TotalPages => TotalCount > 0 ? (int)Math.Ceiling((double)TotalCount / PageSize) : 1;
}