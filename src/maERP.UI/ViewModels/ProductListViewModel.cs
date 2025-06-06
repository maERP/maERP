using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using maERP.Domain.Dtos.Product;
using maERP.UI.Services;

namespace maERP.UI.ViewModels;

public partial class ProductListViewModel : ViewModelBase
{
    private readonly IHttpService _httpService;

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

    public Func<int, Task>? NavigateToProductDetail { get; set; }

    public ProductListViewModel(IHttpService httpService)
    {
        _httpService = httpService;
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
                System.Diagnostics.Debug.WriteLine("GetPaginatedAsync returned null - not authenticated or no server URL");
            }
            else if (result.Succeeded && result.Data != null)
            {
                Products.Clear();
                foreach (var product in result.Data)
                {
                    Products.Add(product);
                }
                TotalCount = result.TotalCount;
                System.Diagnostics.Debug.WriteLine($"Loaded {Products.Count} products successfully");
            }
            else
            {
                ErrorMessage = result.Messages?.FirstOrDefault() ?? "Fehler beim Laden der Produkte";
                Products.Clear();
                System.Diagnostics.Debug.WriteLine($"Failed to load products: {ErrorMessage}");
            }
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Fehler beim Laden der Produkte: {ex.Message}";
            Products.Clear();
            System.Diagnostics.Debug.WriteLine($"Exception loading products: {ex}");
        }
        finally
        {
            IsLoading = false;
            System.Diagnostics.Debug.WriteLine($"finally");
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

    public bool CanGoNext => (CurrentPage + 1) * PageSize < TotalCount;
    public bool CanGoPrevious => CurrentPage > 0;
    public int DisplayPageNumber => CurrentPage + 1;
    public int TotalPages => TotalCount > 0 ? (int)Math.Ceiling((double)TotalCount / PageSize) : 1;
}