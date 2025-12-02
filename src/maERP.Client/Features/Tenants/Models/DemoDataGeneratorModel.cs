using maERP.Client.Core.Abstractions;
using maERP.Client.Core.Services.NameGeneration;
using Microsoft.Extensions.Logging;

namespace maERP.Client.Features.Tenants.Models;

/// <summary>
/// Model for the demo data generator page.
/// Allows generating demo products, customers, orders, and AI-generated data.
/// </summary>
public class DemoDataGeneratorModel : AsyncInitializableModel
{
    private readonly INavigator _navigator;
    private readonly IStringLocalizer _localizer;
    private readonly INameGeneratorFactory _nameGeneratorFactory;
    private readonly Guid _tenantId;
    private readonly string _tenantName;

    // Counts
    private double _productsCount = 50;
    private double _customersCount = 25;
    private double _ordersCount = 50;
    private double _aiDataCount = 10;

    // Generation states
    private bool _isGeneratingProducts;
    private bool _isGeneratingCustomers;
    private bool _isGeneratingOrders;
    private bool _isGeneratingAiData;

    public DemoDataGeneratorModel(
        INavigator navigator,
        IStringLocalizer localizer,
        INameGeneratorFactory nameGeneratorFactory,
        ILogger<DemoDataGeneratorModel> logger,
        DemoDataGeneratorData? data = null)
        : base(logger)
    {
        _navigator = navigator;
        _localizer = localizer;
        _nameGeneratorFactory = nameGeneratorFactory;
        _tenantId = data?.TenantId ?? Guid.Empty;
        _tenantName = data?.TenantName ?? string.Empty;

        // No async initialization needed for this page
        StartInitialization();
    }

    /// <inheritdoc />
    protected override Task InitializeCoreAsync(CancellationToken ct)
    {
        // No async initialization needed
        return Task.CompletedTask;
    }

    #region Properties

    /// <summary>
    /// Page title including tenant name.
    /// </summary>
    public string Title => string.IsNullOrEmpty(_tenantName)
        ? _localizer["DemoDataGeneratorPage.Title"]
        : string.Format(_localizer["DemoDataGeneratorPage.TitleWithTenant"], _tenantName);

    /// <summary>
    /// Number of products to generate.
    /// </summary>
    public double ProductsCount
    {
        get => _productsCount;
        set => SetProperty(ref _productsCount, value);
    }

    /// <summary>
    /// Number of customers to generate.
    /// </summary>
    public double CustomersCount
    {
        get => _customersCount;
        set => SetProperty(ref _customersCount, value);
    }

    /// <summary>
    /// Number of orders to generate.
    /// </summary>
    public double OrdersCount
    {
        get => _ordersCount;
        set => SetProperty(ref _ordersCount, value);
    }

    /// <summary>
    /// Number of AI-generated data entries to create.
    /// </summary>
    public double AiDataCount
    {
        get => _aiDataCount;
        set => SetProperty(ref _aiDataCount, value);
    }

    #endregion

    #region Generation States

    /// <summary>
    /// Indicates whether products are currently being generated.
    /// </summary>
    public bool IsGeneratingProducts
    {
        get => _isGeneratingProducts;
        private set
        {
            if (SetProperty(ref _isGeneratingProducts, value))
            {
                OnPropertyChanged(nameof(IsNotGeneratingProducts));
            }
        }
    }

    /// <summary>
    /// Inverse of IsGeneratingProducts for binding convenience.
    /// </summary>
    public bool IsNotGeneratingProducts => !IsGeneratingProducts;

    /// <summary>
    /// Indicates whether customers are currently being generated.
    /// </summary>
    public bool IsGeneratingCustomers
    {
        get => _isGeneratingCustomers;
        private set
        {
            if (SetProperty(ref _isGeneratingCustomers, value))
            {
                OnPropertyChanged(nameof(IsNotGeneratingCustomers));
            }
        }
    }

    /// <summary>
    /// Inverse of IsGeneratingCustomers for binding convenience.
    /// </summary>
    public bool IsNotGeneratingCustomers => !IsGeneratingCustomers;

    /// <summary>
    /// Indicates whether orders are currently being generated.
    /// </summary>
    public bool IsGeneratingOrders
    {
        get => _isGeneratingOrders;
        private set
        {
            if (SetProperty(ref _isGeneratingOrders, value))
            {
                OnPropertyChanged(nameof(IsNotGeneratingOrders));
            }
        }
    }

    /// <summary>
    /// Inverse of IsGeneratingOrders for binding convenience.
    /// </summary>
    public bool IsNotGeneratingOrders => !IsGeneratingOrders;

    /// <summary>
    /// Indicates whether AI data is currently being generated.
    /// </summary>
    public bool IsGeneratingAiData
    {
        get => _isGeneratingAiData;
        private set
        {
            if (SetProperty(ref _isGeneratingAiData, value))
            {
                OnPropertyChanged(nameof(IsNotGeneratingAiData));
            }
        }
    }

    /// <summary>
    /// Inverse of IsGeneratingAiData for binding convenience.
    /// </summary>
    public bool IsNotGeneratingAiData => !IsGeneratingAiData;

    #endregion

    #region Actions

    /// <summary>
    /// Navigate back to the previous page.
    /// </summary>
    public async Task NavigateBackAsync()
    {
        await _navigator.NavigateBackAsync(this);
    }

    /// <summary>
    /// Generate demo products.
    /// </summary>
    public async Task GenerateProductsAsync(CancellationToken ct = default)
    {
        if (IsGeneratingProducts) return;

        IsGeneratingProducts = true;

        try
        {
            var count = (int)ProductsCount;
            var generator = _nameGeneratorFactory.CreateProductGenerator();
            var productNames = generator.GenerateMany(count);

            // TODO: Create products via API with generated names
            // foreach (var name in productNames)
            // {
            //     await _productService.CreateProductAsync(new ProductInputDto { Name = name, ... }, ct);
            // }

            await Task.Delay(100, ct); // Placeholder until API is implemented
        }
        finally
        {
            IsGeneratingProducts = false;
        }
    }

    /// <summary>
    /// Generate demo customers.
    /// </summary>
    public async Task GenerateCustomersAsync(CancellationToken ct = default)
    {
        if (IsGeneratingCustomers) return;

        IsGeneratingCustomers = true;

        try
        {
            var count = (int)CustomersCount;
            var generator = _nameGeneratorFactory.CreateCustomerGenerator();
            var customerNames = generator.GenerateMany(count);

            // TODO: Create customers via API with generated names
            // foreach (var fullName in customerNames)
            // {
            //     var parts = fullName.Split(' ', 2);
            //     var firstName = parts[0];
            //     var lastName = parts.Length > 1 ? parts[1] : string.Empty;
            //
            //     await _customerService.CreateCustomerAsync(new CustomerInputDto
            //     {
            //         Firstname = firstName,
            //         Lastname = lastName,
            //         ...
            //     }, ct);
            // }

            await Task.Delay(100, ct); // Placeholder until API is implemented
        }
        finally
        {
            IsGeneratingCustomers = false;
        }
    }

    /// <summary>
    /// Generate demo orders.
    /// </summary>
    public async Task GenerateOrdersAsync(CancellationToken ct = default)
    {
        if (IsGeneratingOrders) return;

        IsGeneratingOrders = true;

        try
        {
            // TODO: Implement orders generation via API
            await Task.Delay(1000, ct); // Placeholder
        }
        finally
        {
            IsGeneratingOrders = false;
        }
    }

    /// <summary>
    /// Generate AI-powered demo data.
    /// </summary>
    public async Task GenerateAiDataAsync(CancellationToken ct = default)
    {
        if (IsGeneratingAiData) return;

        IsGeneratingAiData = true;

        try
        {
            // TODO: Implement AI data generation via API
            await Task.Delay(1000, ct); // Placeholder
        }
        finally
        {
            IsGeneratingAiData = false;
        }
    }

    #endregion
}

/// <summary>
/// Navigation data for the demo data generator page.
/// </summary>
public record DemoDataGeneratorData(Guid TenantId, string TenantName);
