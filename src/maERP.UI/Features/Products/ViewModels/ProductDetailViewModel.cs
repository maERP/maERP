using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using maERP.Domain.Dtos.Product;
using maERP.UI.Services;
using maERP.UI.Shared.ViewModels;

namespace maERP.UI.Features.Products.ViewModels;

public partial class ProductDetailViewModel : ViewModelBase
{
    private readonly IHttpService _httpService;
    private readonly IDebugService _debugService;

    [ObservableProperty]
    private ProductDetailDto? product;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ShouldShowContent))]
    private bool isLoading;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ShouldShowContent))]
    private string errorMessage = string.Empty;

    [ObservableProperty]
    private int productId;

    public bool ShouldShowContent => !IsLoading && string.IsNullOrEmpty(ErrorMessage) && Product != null;

    public Action? GoBackAction { get; set; }
    public Func<int, Task>? NavigateToProductInput { get; set; }

    // Computed properties for better display
    public string DisplayName => Product.UseOptimized && !string.IsNullOrEmpty(Product.NameOptimized)
        ? Product.NameOptimized
        : Product?.Name ?? string.Empty;

    public string DisplayDescription => Product.UseOptimized && !string.IsNullOrEmpty(Product.DescriptionOptimized)
        ? Product.DescriptionOptimized
        : Product?.Description ?? string.Empty;

    public bool HasDescription => !string.IsNullOrEmpty(DisplayDescription);
    public bool HasEan => Product != null && !string.IsNullOrEmpty(Product.Ean);
    public bool HasAsin => Product != null && !string.IsNullOrEmpty(Product.Asin);
    public bool HasMsrp => Product?.Msrp > 0;
    public bool HasDimensions => Product != null && (Product.Weight > 0 || Product.Width > 0 || Product.Height > 0 || Product.Depth > 0);
    public bool HasSalesChannels => Product?.ProductSalesChannel?.Any() == true;
    public bool HasStocks => Product?.ProductStocks?.Any() == true;

    // Pricing calculations
    public decimal DiscountPercentage => Product?.Msrp > 0 && Product?.Price > 0 && Product.Msrp > Product.Price
        ? Math.Round(((Product.Msrp - Product.Price) / Product.Msrp) * 100, 2)
        : 0;

    public bool HasDiscount => DiscountPercentage > 0;

    // Physical dimensions formatting
    public string DimensionsText
    {
        get
        {
            if (Product == null) return string.Empty;

            var dimensions = new List<string>();

            if (Product.Width > 0) dimensions.Add($"B: {Product.Width:F1} cm");
            if (Product.Height > 0) dimensions.Add($"H: {Product.Height:F1} cm");
            if (Product.Depth > 0) dimensions.Add($"T: {Product.Depth:F1} cm");

            return dimensions.Any() ? string.Join(" × ", dimensions) : "Keine Angaben";
        }
    }

    public string WeightText => Product?.Weight > 0 ? $"{Product.Weight:F2} kg" : "Keine Angabe";

    // Volume calculation
    public decimal Volume => Product != null && Product.Width > 0 && Product.Height > 0 && Product.Depth > 0
        ? Product.Width * Product.Height * Product.Depth
        : 0;

    public bool HasVolume => Volume > 0;
    public string VolumeText => HasVolume ? $"{Volume:F2} cm³" : "Nicht berechenbar";

    public ProductDetailViewModel(IHttpService httpService, IDebugService debugService)
    {
        _httpService = httpService;
        _debugService = debugService;
    }

    public async Task InitializeAsync(int productId)
    {
        ProductId = productId;
        await LoadProductAsync();
    }

    [RelayCommand]
    private async Task LoadProductAsync()
    {
        if (ProductId <= 0) return;

        IsLoading = true;
        ErrorMessage = string.Empty;

        try
        {
            var result = await _httpService.GetAsync<ProductDetailDto>($"products/{ProductId}");

            if (result == null)
            {
                ErrorMessage = "Nicht authentifiziert oder Server-URL fehlt";
                Product = null;
                _debugService.LogWarning("GetAsync returned null - not authenticated or no server URL");
            }
            else if (result.Succeeded && result.Data != null)
            {
                Product = result.Data;
                OnPropertyChanged(nameof(DisplayName));
                OnPropertyChanged(nameof(DisplayDescription));
                OnPropertyChanged(nameof(HasDescription));
                OnPropertyChanged(nameof(HasEan));
                OnPropertyChanged(nameof(HasAsin));
                OnPropertyChanged(nameof(HasMsrp));
                OnPropertyChanged(nameof(HasDimensions));
                OnPropertyChanged(nameof(HasSalesChannels));
                OnPropertyChanged(nameof(HasStocks));
                OnPropertyChanged(nameof(DiscountPercentage));
                OnPropertyChanged(nameof(HasDiscount));
                OnPropertyChanged(nameof(DimensionsText));
                OnPropertyChanged(nameof(WeightText));
                OnPropertyChanged(nameof(Volume));
                OnPropertyChanged(nameof(HasVolume));
                OnPropertyChanged(nameof(VolumeText));
                _debugService.LogInfo($"Loaded product {ProductId} successfully");
            }
            else
            {
                ErrorMessage = result.Messages?.FirstOrDefault() ?? $"Fehler beim Laden des Produkts {ProductId}";
                Product = null;
                _debugService.LogError($"Failed to load product {ProductId}: {ErrorMessage}");
            }
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Fehler beim Laden des Produkts: {ex.Message}";
            Product = null;
            _debugService.LogError(ex, $"Exception loading product {ProductId}");
        }
        finally
        {
            IsLoading = false;
        }
    }

    [RelayCommand]
    private async Task RefreshAsync()
    {
        await LoadProductAsync();
    }

    [RelayCommand]
    private void GoBack()
    {
        GoBackAction?.Invoke();
    }

    [RelayCommand]
    private async Task EditProductAsync()
    {
        if (ProductId > 0 && NavigateToProductInput != null)
        {
            await NavigateToProductInput(ProductId);
        }
    }
}