using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using maERP.Domain.Dtos.Product;
using maERP.UI.Services;
using maERP.UI.Shared.ViewModels;
namespace maERP.UI.Features.Products.ViewModels;

public partial class ProductListViewModel : ViewModelBase
{
    private readonly IHttpService _httpService;
    private readonly IDebugService _debugService;

    [ObservableProperty]
    private ObservableCollection<ProductListDto> products = new();

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
    private ProductListDto? selectedProduct;

    public bool ShouldShowDataGrid => !IsLoading && string.IsNullOrEmpty(ErrorMessage);

    public Func<Guid, Task>? NavigateToProductDetail { get; set; }
    public Func<Task>? NavigateToProductInput { get; set; }

    public ProductListViewModel(IHttpService httpService, IDebugService debugService)
    {
        _httpService = httpService;
        _debugService = debugService;
    }

    public async Task InitializeAsync()
    {
        await LoadProductsAsync();
    }

    [RelayCommand]
    private async Task LoadProductsAsync()
    {
        IsLoading = true;
        ErrorMessage = string.Empty;

        try
        {
            var result = await _httpService.GetPaginatedAsync<ProductListDto>("products", CurrentPage, PageSize, SearchText, "Name Ascending");

            if (result == null)
            {
                ErrorMessage = "Nicht authentifiziert oder Server-URL fehlt";
                Products.Clear();
                _debugService.LogWarning("GetPaginatedAsync returned null - not authenticated or no server URL");
            }
            else if (result.Succeeded && result.Data != null)
            {
                Products.Clear();
                foreach (var product in result.Data)
                {
                    Products.Add(product);
                }
                TotalCount = result.TotalCount;
                _debugService.LogInfo($"Loaded {Products.Count} products successfully");
            }
            else
            {
                ErrorMessage = result.Messages?.FirstOrDefault() ?? "Fehler beim Laden der Produkte";
                Products.Clear();
                _debugService.LogError($"Failed to load products: {ErrorMessage}");
            }
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Fehler beim Laden der Produkte: {ex.Message}";
            Products.Clear();
            _debugService.LogError(ex, "Exception loading products");
        }
        finally
        {
            IsLoading = false;
            _debugService.LogDebug("finally");
        }
    }

    [RelayCommand]
    private async Task SearchProductsAsync()
    {
        CurrentPage = 0;
        await LoadProductsAsync();
    }

    [RelayCommand]
    public async Task RefreshAsync()
    {
        await LoadProductsAsync();
    }

    [RelayCommand]
    private async Task NextPageAsync()
    {
        if ((CurrentPage + 1) * PageSize < TotalCount)
        {
            CurrentPage++;
            await LoadProductsAsync();
        }
    }

    [RelayCommand]
    private async Task PreviousPageAsync()
    {
        if (CurrentPage > 0)
        {
            CurrentPage--;
            await LoadProductsAsync();
        }
    }

    [RelayCommand]
    private void SelectProduct(ProductListDto? product)
    {
        SelectedProduct = product;
    }

    [RelayCommand]
    private async Task ViewProductDetails(ProductListDto? product)
    {
        if (product == null || NavigateToProductDetail == null) return;

        SelectedProduct = product;
        await NavigateToProductDetail(product.Id);
    }

    [RelayCommand]
    private async Task CreateProductAsync()
    {
        if (NavigateToProductInput != null)
        {
            await NavigateToProductInput();
        }
    }

    public bool CanGoNext => (CurrentPage + 1) * PageSize < TotalCount;
    public bool CanGoPrevious => CurrentPage > 0;
    public int DisplayPageNumber => CurrentPage + 1;
    public int TotalPages => TotalCount > 0 ? (int)Math.Ceiling((double)TotalCount / PageSize) : 1;
}