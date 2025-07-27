using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using maERP.Domain.Dtos.Product;
using maERP.Domain.Dtos.TaxClass;
using maERP.Domain.Dtos.SalesChannel;
using maERP.Domain.Dtos.Manufacturer;
using maERP.UI.Services;
using maERP.UI.Shared.ViewModels;

namespace maERP.UI.Features.Products.ViewModels;

public partial class ProductInputViewModel : ViewModelBase
{
    private readonly IHttpService _httpService;
    private readonly IDebugService _debugService;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsEditMode))]
    [NotifyPropertyChangedFor(nameof(PageTitle))]
    private int productId;

    [ObservableProperty]
    [Required(ErrorMessage = "SKU ist erforderlich")]
    [StringLength(255, ErrorMessage = "SKU darf maximal 255 Zeichen haben")]
    [NotifyDataErrorInfo]
    private string sku = string.Empty;

    [ObservableProperty]
    [Required(ErrorMessage = "Produktname ist erforderlich")]
    [StringLength(255, ErrorMessage = "Produktname darf maximal 255 Zeichen haben")]
    [NotifyDataErrorInfo]
    private string name = string.Empty;

    [ObservableProperty]
    [StringLength(255, ErrorMessage = "Optimierter Name darf maximal 255 Zeichen haben")]
    private string nameOptimized = string.Empty;

    [ObservableProperty]
    [StringLength(32, ErrorMessage = "EAN darf maximal 32 Zeichen haben")]
    private string ean = string.Empty;

    [ObservableProperty]
    [StringLength(32, ErrorMessage = "ASIN darf maximal 32 Zeichen haben")]
    private string asin = string.Empty;

    [ObservableProperty]
    [StringLength(64000, ErrorMessage = "Beschreibung darf maximal 64000 Zeichen haben")]
    private string description = string.Empty;

    [ObservableProperty]
    [StringLength(64000, ErrorMessage = "Optimierte Beschreibung darf maximal 64000 Zeichen haben")]
    private string descriptionOptimized = string.Empty;

    [ObservableProperty]
    private bool useOptimized;

    [ObservableProperty]
    [Required(ErrorMessage = "Preis ist erforderlich")]
    [Range(0, double.MaxValue, ErrorMessage = "Preis muss größer oder gleich 0 sein")]
    [NotifyDataErrorInfo]
    private decimal price;

    [ObservableProperty]
    [Range(0, double.MaxValue, ErrorMessage = "UVP muss größer oder gleich 0 sein")]
    private decimal msrp;

    [ObservableProperty]
    [Range(0, double.MaxValue, ErrorMessage = "Gewicht muss größer oder gleich 0 sein")]
    private decimal weight;

    [ObservableProperty]
    [Range(0, double.MaxValue, ErrorMessage = "Breite muss größer oder gleich 0 sein")]
    private decimal width;

    [ObservableProperty]
    [Range(0, double.MaxValue, ErrorMessage = "Höhe muss größer oder gleich 0 sein")]
    private decimal height;

    [ObservableProperty]
    [Range(0, double.MaxValue, ErrorMessage = "Tiefe muss größer oder gleich 0 sein")]
    private decimal depth;

    [ObservableProperty]
    private int taxClassId;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(TaxClassId))]
    private TaxClassListDto? selectedTaxClass;

    [ObservableProperty]
    private int? manufacturerId;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ManufacturerId))]
    private ManufacturerListDto? selectedManufacturer;

    [ObservableProperty]
    private ObservableCollection<int> productSalesChannelIds = new();

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ShouldShowContent))]
    private bool isLoading;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ShouldShowContent))]
    private string errorMessage = string.Empty;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ShouldShowContent))]
    private bool isSaving;

    // Available options for dropdowns
    [ObservableProperty]
    private ObservableCollection<TaxClassListDto> availableTaxClasses = new();

    [ObservableProperty]
    private ObservableCollection<ManufacturerListDto> availableManufacturers = new();

    [ObservableProperty]
    private ObservableCollection<SalesChannelSelectionViewModel> availableSalesChannels = new();

    public bool IsEditMode => ProductId > 0;
    public string PageTitle => IsEditMode ? $"Produkt #{ProductId} bearbeiten" : "Neues Produkt erstellen";
    public bool ShouldShowContent => !IsLoading && !IsSaving && string.IsNullOrEmpty(ErrorMessage);

    public Action? GoBackAction { get; set; }
    public Func<int, Task>? NavigateToProductDetail { get; set; }

    public ProductInputViewModel(IHttpService httpService, IDebugService debugService)
    {
        _httpService = httpService;
        _debugService = debugService;
    }

    public async Task InitializeAsync(int productId = 0)
    {
        ProductId = productId;
        
        await LoadDropdownDataAsync();
        
        if (IsEditMode)
        {
            await LoadAsync();
        }
        else
        {
            ClearForm();
        }
    }

    [RelayCommand]
    private async Task LoadAsync()
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
                _debugService.LogWarning("GetAsync returned null - not authenticated or no server URL");
            }
            else if (result.Succeeded && result.Data != null)
            {
                var product = result.Data;
                
                // Map product data to form fields
                Sku = product.Sku;
                Name = product.Name;
                NameOptimized = product.NameOptimized;
                Ean = product.Ean;
                Asin = product.Asin;
                Description = product.Description;
                DescriptionOptimized = product.DescriptionOptimized;
                UseOptimized = product.UseOptimized;
                Price = product.Price;
                Msrp = product.Msrp;
                Weight = product.Weight;
                Width = product.Width;
                Height = product.Height;
                Depth = product.Depth;
                TaxClassId = product.TaxClassId;
                ManufacturerId = product.Manufacturer?.Id;
                
                // Set selected tax class
                SelectedTaxClass = AvailableTaxClasses.FirstOrDefault(tc => tc.Id == product.TaxClassId);
                
                // Set selected manufacturer
                if (product.Manufacturer != null)
                {
                    SelectedManufacturer = AvailableManufacturers.FirstOrDefault(m => m.Id == product.Manufacturer.Id);
                }
                
                // Load sales channels
                ProductSalesChannelIds.Clear();
                if (product.ProductSalesChannel != null)
                {
                    foreach (var id in product.ProductSalesChannel)
                    {
                        ProductSalesChannelIds.Add(id);
                    }
                }
                
                // Update sales channel selections
                foreach (var salesChannelVm in AvailableSalesChannels)
                {
                    salesChannelVm.IsSelected = ProductSalesChannelIds.Contains(salesChannelVm.SalesChannel.Id);
                }

                _debugService.LogInfo($"Loaded product {ProductId} for editing successfully");
            }
            else
            {
                ErrorMessage = result.Messages?.FirstOrDefault() ?? $"Fehler beim Laden des Produkts {ProductId}";
                _debugService.LogError($"Failed to load product {ProductId}: {ErrorMessage}");
            }
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Fehler beim Laden des Produkts: {ex.Message}";
            _debugService.LogError(ex, $"Exception loading product {ProductId}");
        }
        finally
        {
            IsLoading = false;
        }
    }

    [RelayCommand]
    private async Task SaveAsync()
    {
        if (!ValidateForm()) return;

        IsSaving = true;
        ErrorMessage = string.Empty;

        try
        {
            var productDto = new ProductInputDto
            {
                Id = ProductId,
                Sku = Sku,
                Name = Name,
                NameOptimized = NameOptimized,
                Ean = Ean,
                Asin = Asin,
                Description = Description,
                DescriptionOptimized = DescriptionOptimized,
                UseOptimized = UseOptimized,
                Price = Price,
                Msrp = Msrp,
                Weight = Weight,
                Width = Width,
                Height = Height,
                Depth = Depth,
                TaxClassId = TaxClassId,
                ManufacturerId = ManufacturerId,
                ProductSalesChannel = ProductSalesChannelIds.ToList()
            };

            var result = IsEditMode
                ? await _httpService.PutAsync<ProductInputDto, int>($"products/{ProductId}", productDto)
                : await _httpService.PostAsync<ProductInputDto, int>("products", productDto);

            if (result == null)
            {
                ErrorMessage = "Nicht authentifiziert oder Server-URL fehlt";
                _debugService.LogWarning("SaveAsync returned null - not authenticated or no server URL");
            }
            else if (result.Succeeded)
            {
                _debugService.LogInfo($"Product {(IsEditMode ? "updated" : "created")} successfully");
                
                if (IsEditMode && NavigateToProductDetail != null)
                {
                    await NavigateToProductDetail(ProductId);
                }
                else if (!IsEditMode && result.Data > 0 && NavigateToProductDetail != null)
                {
                    await NavigateToProductDetail(result.Data);
                }
                else
                {
                    GoBack();
                }
            }
            else
            {
                ErrorMessage = result.Messages?.FirstOrDefault() ?? $"Fehler beim {(IsEditMode ? "Speichern" : "Erstellen")} des Produkts";
                _debugService.LogError($"Failed to save product: {ErrorMessage}");
            }
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Fehler beim Speichern: {ex.Message}";
            _debugService.LogError(ex, "Exception saving product");
        }
        finally
        {
            IsSaving = false;
        }
    }

    [RelayCommand]
    private void Cancel()
    {
        GoBack();
    }

    [RelayCommand]
    private void GoBack()
    {
        GoBackAction?.Invoke();
    }

    private async Task LoadDropdownDataAsync()
    {
        try
        {
            // Load tax classes
            var taxClassResult = await _httpService.GetAsync<List<TaxClassListDto>>("taxclasses");
            if (taxClassResult?.Succeeded == true && taxClassResult.Data != null)
            {
                AvailableTaxClasses.Clear();
                foreach (var taxClass in taxClassResult.Data)
                {
                    AvailableTaxClasses.Add(taxClass);
                }
                
                // Set default tax class if available
                if (AvailableTaxClasses.Any() && TaxClassId == 0)
                {
                    TaxClassId = AvailableTaxClasses.First().Id;
                    SelectedTaxClass = AvailableTaxClasses.First();
                }
            }

            // Load manufacturers
            var manufacturerResult = await _httpService.GetAsync<List<ManufacturerListDto>>("manufacturers");
            if (manufacturerResult?.Succeeded == true && manufacturerResult.Data != null)
            {
                AvailableManufacturers.Clear();
                // Add empty option for no manufacturer
                AvailableManufacturers.Add(new ManufacturerListDto { Id = 0, Name = "--- Kein Hersteller ---", City = "", Country = "" });
                
                foreach (var manufacturer in manufacturerResult.Data)
                {
                    AvailableManufacturers.Add(manufacturer);
                }
                
                // Set default to no manufacturer if none selected
                if (ManufacturerId == null || ManufacturerId == 0)
                {
                    SelectedManufacturer = AvailableManufacturers.First();
                }
            }

            // Load sales channels
            var salesChannelResult = await _httpService.GetAsync<List<SalesChannelListDto>>("saleschannels");
            if (salesChannelResult?.Succeeded == true && salesChannelResult.Data != null)
            {
                AvailableSalesChannels.Clear();
                foreach (var salesChannel in salesChannelResult.Data)
                {
                    var isSelected = ProductSalesChannelIds.Contains(salesChannel.Id);
                    AvailableSalesChannels.Add(new SalesChannelSelectionViewModel(salesChannel, isSelected));
                }
            }
        }
        catch (Exception ex)
        {
            _debugService.LogError(ex, "Exception loading dropdown data");
        }
    }

    private void ClearForm()
    {
        Sku = string.Empty;
        Name = string.Empty;
        NameOptimized = string.Empty;
        Ean = string.Empty;
        Asin = string.Empty;
        Description = string.Empty;
        DescriptionOptimized = string.Empty;
        UseOptimized = false;
        Price = 0;
        Msrp = 0;
        Weight = 0;
        Width = 0;
        Height = 0;
        Depth = 0;
        TaxClassId = AvailableTaxClasses.Any() ? AvailableTaxClasses.First().Id : 0;
        SelectedTaxClass = AvailableTaxClasses.FirstOrDefault();
        ManufacturerId = null;
        SelectedManufacturer = AvailableManufacturers.FirstOrDefault();
        ProductSalesChannelIds.Clear();
        ErrorMessage = string.Empty;
        
        ClearErrors();
    }

    private bool ValidateForm()
    {
        ValidateAllProperties();
        
        if (HasErrors)
        {
            ErrorMessage = "Bitte korrigieren Sie die Eingabefehler";
            return false;
        }
        return true;
    }

    partial void OnSelectedTaxClassChanged(TaxClassListDto? value)
    {
        if (value != null)
        {
            TaxClassId = value.Id;
        }
    }

    partial void OnSelectedManufacturerChanged(ManufacturerListDto? value)
    {
        if (value != null && value.Id > 0)
        {
            ManufacturerId = value.Id;
        }
        else
        {
            ManufacturerId = null;
        }
    }

    [RelayCommand]
    private void ToggleSalesChannel(SalesChannelSelectionViewModel salesChannelVm)
    {
        salesChannelVm.IsSelected = !salesChannelVm.IsSelected;
        
        if (salesChannelVm.IsSelected)
        {
            if (!ProductSalesChannelIds.Contains(salesChannelVm.SalesChannel.Id))
            {
                ProductSalesChannelIds.Add(salesChannelVm.SalesChannel.Id);
            }
        }
        else
        {
            ProductSalesChannelIds.Remove(salesChannelVm.SalesChannel.Id);
        }
    }

    public bool IsSalesChannelSelected(int salesChannelId)
    {
        return ProductSalesChannelIds.Contains(salesChannelId);
    }
}

public partial class SalesChannelSelectionViewModel : ViewModelBase
{
    public SalesChannelListDto SalesChannel { get; }

    [ObservableProperty]
    private bool isSelected;

    public SalesChannelSelectionViewModel(SalesChannelListDto salesChannel, bool isSelected = false)
    {
        SalesChannel = salesChannel;
        IsSelected = isSelected;
    }
}