using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using maERP.Client.Core.Abstractions;
using maERP.Client.Core.Exceptions;
using maERP.Client.Core.Models;
using maERP.Client.Features.Manufacturers.Services;
using maERP.Client.Features.Products.Services;
using maERP.Client.Features.TaxClasses.Services;
using maERP.Domain.Dtos.Manufacturer;
using maERP.Domain.Dtos.Product;
using maERP.Domain.Dtos.TaxClass;
using Microsoft.Extensions.Logging;

namespace maERP.Client.Features.Products.Models;

/// <summary>
/// Model for product edit/create page.
/// Inherits from AsyncInitializableModel for safe async initialization.
/// </summary>
public class ProductEditModel : AsyncInitializableModel
{
    private readonly IProductService _productService;
    private readonly ITaxClassService _taxClassService;
    private readonly IManufacturerService _manufacturerService;
    private readonly INavigator _navigator;
    private readonly IStringLocalizer _localizer;
    private readonly Guid? _productId;

    // Product Data - Basic Information
    private string _sku = string.Empty;
    private string _name = string.Empty;
    private string? _nameOptimized;
    private string? _ean;
    private string? _asin;
    private string? _description;
    private string? _descriptionOptimized;
    private bool _useOptimized;

    // Product Data - Pricing
    private decimal _price;
    private decimal _msrp;

    // Product Data - Dimensions
    private decimal _weight;
    private decimal _width;
    private decimal _height;
    private decimal _depth;

    // Product Data - Relationships
    private Guid _taxClassId;
    private Guid? _manufacturerId;

    // Dropdown Data
    private ObservableCollection<TaxClassListDto> _taxClasses = new();
    private ObservableCollection<ManufacturerListDto> _manufacturers = new();

    // UI State
    private bool _isSaving;
    private string _errorMessage = string.Empty;

    public ProductEditModel(
        IProductService productService,
        ITaxClassService taxClassService,
        IManufacturerService manufacturerService,
        INavigator navigator,
        IStringLocalizer localizer,
        ILogger<ProductEditModel> logger,
        ProductEditData? data = null)
        : base(logger)
    {
        _productService = productService;
        _taxClassService = taxClassService;
        _manufacturerService = manufacturerService;
        _navigator = navigator;
        _localizer = localizer;
        _productId = data?.ProductId;

        // Start async initialization with proper error handling
        StartInitialization();
    }

    /// <inheritdoc />
    protected override async Task InitializeCoreAsync(CancellationToken ct)
    {
        await Task.WhenAll(
            LoadTaxClassesAsync(ct),
            LoadManufacturersAsync(ct)
        );

        if (_productId.HasValue)
        {
            await LoadProductAsync(ct);
        }
    }

    private async Task LoadTaxClassesAsync(CancellationToken ct)
    {
        var parameters = new QueryParameters { PageSize = 1000 };
        var response = await _taxClassService.GetTaxClassesAsync(parameters, ct);
        TaxClasses.Clear();
        foreach (var taxClass in response.Data)
        {
            TaxClasses.Add(taxClass);
        }
    }

    private async Task LoadManufacturersAsync(CancellationToken ct)
    {
        var parameters = new QueryParameters { PageSize = 1000 };
        var response = await _manufacturerService.GetManufacturersAsync(parameters, ct);
        Manufacturers.Clear();
        foreach (var manufacturer in response.Data)
        {
            Manufacturers.Add(manufacturer);
        }
    }

    public bool IsEditMode => _productId.HasValue;

    public string Title => IsEditMode
        ? _localizer["ProductEditPage.TitleEdit"]
        : _localizer["ProductEditPage.TitleNew"];

    #region Product Data - Basic Information

    public string Sku
    {
        get => _sku;
        set
        {
            if (SetProperty(ref _sku, value))
            {
                OnPropertyChanged(nameof(CanSave));
            }
        }
    }

    public string Name
    {
        get => _name;
        set
        {
            if (SetProperty(ref _name, value))
            {
                OnPropertyChanged(nameof(CanSave));
            }
        }
    }

    public string? NameOptimized
    {
        get => _nameOptimized;
        set => SetProperty(ref _nameOptimized, value);
    }

    public string? Ean
    {
        get => _ean;
        set => SetProperty(ref _ean, value);
    }

    public string? Asin
    {
        get => _asin;
        set => SetProperty(ref _asin, value);
    }

    public string? Description
    {
        get => _description;
        set => SetProperty(ref _description, value);
    }

    public string? DescriptionOptimized
    {
        get => _descriptionOptimized;
        set => SetProperty(ref _descriptionOptimized, value);
    }

    public bool UseOptimized
    {
        get => _useOptimized;
        set => SetProperty(ref _useOptimized, value);
    }

    #endregion

    #region Product Data - Pricing

    public decimal Price
    {
        get => _price;
        set => SetProperty(ref _price, value);
    }

    public decimal Msrp
    {
        get => _msrp;
        set => SetProperty(ref _msrp, value);
    }

    #endregion

    #region Product Data - Dimensions

    public decimal Weight
    {
        get => _weight;
        set => SetProperty(ref _weight, value);
    }

    public decimal Width
    {
        get => _width;
        set => SetProperty(ref _width, value);
    }

    public decimal Height
    {
        get => _height;
        set => SetProperty(ref _height, value);
    }

    public decimal Depth
    {
        get => _depth;
        set => SetProperty(ref _depth, value);
    }

    #endregion

    #region Product Data - Relationships

    public Guid TaxClassId
    {
        get => _taxClassId;
        set
        {
            if (SetProperty(ref _taxClassId, value))
            {
                OnPropertyChanged(nameof(CanSave));
            }
        }
    }

    public Guid? ManufacturerId
    {
        get => _manufacturerId;
        set => SetProperty(ref _manufacturerId, value);
    }

    #endregion

    #region Dropdown Data

    public ObservableCollection<TaxClassListDto> TaxClasses
    {
        get => _taxClasses;
        set => SetProperty(ref _taxClasses, value);
    }

    public ObservableCollection<ManufacturerListDto> Manufacturers
    {
        get => _manufacturers;
        set => SetProperty(ref _manufacturers, value);
    }

    #endregion

    #region UI State

    /// <summary>
    /// Indicates whether a save operation is in progress.
    /// </summary>
    public bool IsSaving
    {
        get => _isSaving;
        private set
        {
            if (SetProperty(ref _isSaving, value))
            {
                OnPropertyChanged(nameof(IsLoading));
                OnPropertyChanged(nameof(IsNotLoading));
                OnPropertyChanged(nameof(CanSave));
            }
        }
    }

    /// <summary>
    /// Combined loading state (initializing or saving).
    /// </summary>
    public bool IsLoading => IsInitializing || IsSaving;

    /// <summary>
    /// Inverse of IsLoading for binding convenience.
    /// </summary>
    public bool IsNotLoading => !IsLoading;

    public string ErrorMessage
    {
        get => _errorMessage;
        set => SetProperty(ref _errorMessage, value);
    }

    public bool CanSave =>
        !string.IsNullOrWhiteSpace(Sku) &&
        !string.IsNullOrWhiteSpace(Name) &&
        TaxClassId != Guid.Empty &&
        !IsLoading;

    #endregion

    private async Task LoadProductAsync(CancellationToken ct)
    {
        if (!_productId.HasValue) return;

        var product = await _productService.GetProductAsync(_productId.Value, ct);
        if (product != null)
        {
            // Basic Information
            Sku = product.Sku;
            Name = product.Name;
            NameOptimized = product.NameOptimized;
            Ean = product.Ean;
            Asin = product.Asin;
            Description = product.Description;
            DescriptionOptimized = product.DescriptionOptimized;
            UseOptimized = product.UseOptimized;

            // Pricing
            Price = product.Price;
            Msrp = product.Msrp;

            // Dimensions
            Weight = product.Weight;
            Width = product.Width;
            Height = product.Height;
            Depth = product.Depth;

            // Relationships
            TaxClassId = product.TaxClassId;
            ManufacturerId = product.Manufacturer?.Id;
        }
    }

    public async Task SaveAsync(CancellationToken ct = default)
    {
        if (!CanSave) return;

        IsSaving = true;
        ErrorMessage = string.Empty;

        try
        {
            var input = new ProductInputDto
            {
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
                ManufacturerId = ManufacturerId
            };

            if (_productId.HasValue)
            {
                input.Id = _productId.Value;
                await _productService.UpdateProductAsync(_productId.Value, input, ct);
            }
            else
            {
                await _productService.CreateProductAsync(input, ct);
            }

            await _navigator.NavigateBackAsync(this);
        }
        catch (ApiException ex)
        {
            // Display detailed error messages from the API
            ErrorMessage = ex.CombinedMessage;
        }
        catch (Exception ex)
        {
            ErrorMessage = string.Format(_localizer["ProductEditPage.Error.SaveFailed"], ex.Message);
        }
        finally
        {
            IsSaving = false;
        }
    }

    public async Task CancelAsync()
    {
        await _navigator.NavigateBackAsync(this);
    }

    /// <inheritdoc />
    protected override void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        base.OnPropertyChanged(propertyName);

        // Handle IsInitializing changes from base class
        if (propertyName is nameof(IsInitializing))
        {
            base.OnPropertyChanged(nameof(IsLoading));
            base.OnPropertyChanged(nameof(IsNotLoading));
            base.OnPropertyChanged(nameof(CanSave));
        }
    }
}

/// <summary>
/// Navigation data for product edit page.
/// </summary>
public record ProductEditData(Guid? ProductId);
