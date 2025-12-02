using maERP.Client.Core.Abstractions;
using maERP.Client.Core.Exceptions;
using maERP.Client.Core.Models;
using maERP.Client.Core.Services.NameGeneration;
using maERP.Client.Features.AiModels.Services;
using maERP.Client.Features.AiPrompts.Services;
using maERP.Client.Features.Countries.Services;
using maERP.Client.Features.Customers.Services;
using maERP.Client.Features.Orders.Services;
using maERP.Client.Features.Products.Services;
using maERP.Client.Features.SalesChannels.Services;
using maERP.Client.Features.TaxClasses.Services;
using maERP.Domain.Dtos.AiModel;
using maERP.Domain.Dtos.AiPrompt;
using maERP.Domain.Dtos.Customer;
using maERP.Domain.Dtos.CustomerAddress;
using maERP.Domain.Dtos.Order;
using maERP.Domain.Dtos.Product;
using maERP.Domain.Entities;
using maERP.Domain.Enums;
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
    private readonly IProductService _productService;
    private readonly ICustomerService _customerService;
    private readonly IOrderService _orderService;
    private readonly IAiModelService _aiModelService;
    private readonly IAiPromptService _aiPromptService;
    private readonly ITaxClassService _taxClassService;
    private readonly ICountryService _countryService;
    private readonly ISalesChannelService _salesChannelService;
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

    // Progress tracking - Products
    private int _productsProgress;
    private int _productsTotalCount;
    private CancellationTokenSource? _productsCts;
    private string? _productsErrorMessage;

    // Progress tracking - Customers
    private int _customersProgress;
    private int _customersTotalCount;
    private CancellationTokenSource? _customersCts;
    private string? _customersErrorMessage;

    // Progress tracking - Orders
    private int _ordersProgress;
    private int _ordersTotalCount;
    private CancellationTokenSource? _ordersCts;
    private string? _ordersErrorMessage;

    // Progress tracking - AI Data
    private int _aiDataProgress;
    private int _aiDataTotalCount;
    private CancellationTokenSource? _aiDataCts;
    private string? _aiDataErrorMessage;

    public DemoDataGeneratorModel(
        INavigator navigator,
        IStringLocalizer localizer,
        INameGeneratorFactory nameGeneratorFactory,
        IProductService productService,
        ICustomerService customerService,
        IOrderService orderService,
        IAiModelService aiModelService,
        IAiPromptService aiPromptService,
        ITaxClassService taxClassService,
        ICountryService countryService,
        ISalesChannelService salesChannelService,
        ILogger<DemoDataGeneratorModel> logger,
        DemoDataGeneratorData? data = null)
        : base(logger)
    {
        _navigator = navigator;
        _localizer = localizer;
        _nameGeneratorFactory = nameGeneratorFactory;
        _productService = productService;
        _customerService = customerService;
        _orderService = orderService;
        _aiModelService = aiModelService;
        _aiPromptService = aiPromptService;
        _taxClassService = taxClassService;
        _countryService = countryService;
        _salesChannelService = salesChannelService;
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

    #region Progress Tracking Properties - Products

    /// <summary>
    /// Current progress of product generation.
    /// </summary>
    public int ProductsProgress
    {
        get => _productsProgress;
        private set
        {
            if (SetProperty(ref _productsProgress, value))
            {
                OnPropertyChanged(nameof(ProductsProgressPercent));
            }
        }
    }

    /// <summary>
    /// Total count of products to generate.
    /// </summary>
    public int ProductsTotalCount
    {
        get => _productsTotalCount;
        private set => SetProperty(ref _productsTotalCount, value);
    }

    /// <summary>
    /// Progress percentage for products generation.
    /// </summary>
    public double ProductsProgressPercent => ProductsTotalCount > 0
        ? (double)ProductsProgress / ProductsTotalCount * 100
        : 0;

    /// <summary>
    /// Error message for products generation.
    /// </summary>
    public string? ProductsErrorMessage
    {
        get => _productsErrorMessage;
        private set => SetProperty(ref _productsErrorMessage, value);
    }

    #endregion

    #region Progress Tracking Properties - Customers

    /// <summary>
    /// Current progress of customer generation.
    /// </summary>
    public int CustomersProgress
    {
        get => _customersProgress;
        private set
        {
            if (SetProperty(ref _customersProgress, value))
            {
                OnPropertyChanged(nameof(CustomersProgressPercent));
            }
        }
    }

    /// <summary>
    /// Total count of customers to generate.
    /// </summary>
    public int CustomersTotalCount
    {
        get => _customersTotalCount;
        private set => SetProperty(ref _customersTotalCount, value);
    }

    /// <summary>
    /// Progress percentage for customers generation.
    /// </summary>
    public double CustomersProgressPercent => CustomersTotalCount > 0
        ? (double)CustomersProgress / CustomersTotalCount * 100
        : 0;

    /// <summary>
    /// Error message for customers generation.
    /// </summary>
    public string? CustomersErrorMessage
    {
        get => _customersErrorMessage;
        private set => SetProperty(ref _customersErrorMessage, value);
    }

    #endregion

    #region Progress Tracking Properties - Orders

    /// <summary>
    /// Current progress of order generation.
    /// </summary>
    public int OrdersProgress
    {
        get => _ordersProgress;
        private set
        {
            if (SetProperty(ref _ordersProgress, value))
            {
                OnPropertyChanged(nameof(OrdersProgressPercent));
            }
        }
    }

    /// <summary>
    /// Total count of orders to generate.
    /// </summary>
    public int OrdersTotalCount
    {
        get => _ordersTotalCount;
        private set => SetProperty(ref _ordersTotalCount, value);
    }

    /// <summary>
    /// Progress percentage for orders generation.
    /// </summary>
    public double OrdersProgressPercent => OrdersTotalCount > 0
        ? (double)OrdersProgress / OrdersTotalCount * 100
        : 0;

    /// <summary>
    /// Error message for orders generation.
    /// </summary>
    public string? OrdersErrorMessage
    {
        get => _ordersErrorMessage;
        private set => SetProperty(ref _ordersErrorMessage, value);
    }

    #endregion

    #region Progress Tracking Properties - AI Data

    /// <summary>
    /// Current progress of AI data generation.
    /// </summary>
    public int AiDataProgress
    {
        get => _aiDataProgress;
        private set
        {
            if (SetProperty(ref _aiDataProgress, value))
            {
                OnPropertyChanged(nameof(AiDataProgressPercent));
            }
        }
    }

    /// <summary>
    /// Total count of AI data items to generate.
    /// </summary>
    public int AiDataTotalCount
    {
        get => _aiDataTotalCount;
        private set => SetProperty(ref _aiDataTotalCount, value);
    }

    /// <summary>
    /// Progress percentage for AI data generation.
    /// </summary>
    public double AiDataProgressPercent => AiDataTotalCount > 0
        ? (double)AiDataProgress / AiDataTotalCount * 100
        : 0;

    /// <summary>
    /// Error message for AI data generation.
    /// </summary>
    public string? AiDataErrorMessage
    {
        get => _aiDataErrorMessage;
        private set => SetProperty(ref _aiDataErrorMessage, value);
    }

    #endregion

    #region Navigation

    /// <summary>
    /// Navigate back to the previous page.
    /// </summary>
    public async Task NavigateBackAsync()
    {
        await _navigator.NavigateBackAsync(this);
    }

    #endregion

    #region Cancel Methods

    /// <summary>
    /// Cancel products generation.
    /// </summary>
    public void CancelProductsGeneration() => _productsCts?.Cancel();

    /// <summary>
    /// Cancel customers generation.
    /// </summary>
    public void CancelCustomersGeneration() => _customersCts?.Cancel();

    /// <summary>
    /// Cancel orders generation.
    /// </summary>
    public void CancelOrdersGeneration() => _ordersCts?.Cancel();

    /// <summary>
    /// Cancel AI data generation.
    /// </summary>
    public void CancelAiDataGeneration() => _aiDataCts?.Cancel();

    #endregion

    #region Generation Actions

    /// <summary>
    /// Generate demo products.
    /// </summary>
    public async Task GenerateProductsAsync(CancellationToken ct = default)
    {
        if (IsGeneratingProducts) return;

        IsGeneratingProducts = true;
        ProductsErrorMessage = null;
        ProductsProgress = 0;
        ProductsTotalCount = (int)ProductsCount;

        _productsCts = CancellationTokenSource.CreateLinkedTokenSource(ct);
        var token = _productsCts.Token;

        try
        {
            // 1. Fetch first available TaxClass (required field)
            var taxClassResponse = await _taxClassService.GetTaxClassesAsync(
                new QueryParameters { PageNumber = 0, PageSize = 1 }, token);

            if (taxClassResponse.Data == null || taxClassResponse.Data.Count == 0)
            {
                throw new InvalidOperationException(_localizer["DemoDataGeneratorPage.Error.NoTaxClass"]);
            }
            var taxClassId = taxClassResponse.Data[0].Id;

            // 2. Generate product names using name generator
            var generator = _nameGeneratorFactory.CreateProductGenerator();
            var productNames = generator.GenerateMany(ProductsTotalCount);

            var random = new Random();

            // 3. Create products one by one with progress tracking
            for (int i = 0; i < productNames.Count; i++)
            {
                token.ThrowIfCancellationRequested();

                var name = productNames[i];
                var price = Math.Round((decimal)(random.NextDouble() * 490 + 9.99), 2);
                var msrpMultiplier = 1.1 + random.NextDouble() * 0.2; // 10-30% markup

                var input = new ProductInputDto
                {
                    Sku = $"DEMO-{i + 1:D4}",
                    Name = name,
                    Ean = GenerateEan13(random),
                    Asin = GenerateAsin(random),
                    Description = $"Demo product: {name}. This is a sample product generated for demonstration purposes.",
                    Price = price,
                    Msrp = Math.Round(price * (decimal)msrpMultiplier, 2),
                    Weight = Math.Round((decimal)(random.NextDouble() * 10 + 0.1), 2),
                    Width = Math.Round((decimal)(random.NextDouble() * 50 + 5), 1),
                    Height = Math.Round((decimal)(random.NextDouble() * 50 + 5), 1),
                    Depth = Math.Round((decimal)(random.NextDouble() * 50 + 5), 1),
                    TaxClassId = taxClassId
                };

                await _productService.CreateProductAsync(input, token);
                ProductsProgress = i + 1;
            }
        }
        catch (OperationCanceledException)
        {
            // User cancelled - not an error
        }
        catch (ApiException ex)
        {
            ProductsErrorMessage = ex.CombinedMessage;
        }
        catch (Exception ex)
        {
            ProductsErrorMessage = ex.Message;
        }
        finally
        {
            IsGeneratingProducts = false;
            _productsCts?.Dispose();
            _productsCts = null;
        }
    }

    /// <summary>
    /// Generate demo customers.
    /// </summary>
    public async Task GenerateCustomersAsync(CancellationToken ct = default)
    {
        if (IsGeneratingCustomers) return;

        IsGeneratingCustomers = true;
        CustomersErrorMessage = null;
        CustomersProgress = 0;
        CustomersTotalCount = (int)CustomersCount;

        _customersCts = CancellationTokenSource.CreateLinkedTokenSource(ct);
        var token = _customersCts.Token;

        try
        {
            // 1. Fetch first available Country
            var countries = await _countryService.GetCountriesAsync(token);
            if (countries == null || countries.Count == 0)
            {
                throw new InvalidOperationException(_localizer["DemoDataGeneratorPage.Error.NoCountry"]);
            }
            var countryId = countries[0].Id;

            // 2. Generate names and addresses
            var customerGenerator = _nameGeneratorFactory.CreateCustomerGenerator();
            var addressGenerator = _nameGeneratorFactory.CreateAddressGenerator();
            var customerNames = customerGenerator.GenerateMany(CustomersTotalCount);

            var random = new Random();

            for (int i = 0; i < customerNames.Count; i++)
            {
                token.ThrowIfCancellationRequested();

                var fullName = customerNames[i];
                var nameParts = fullName.Split(' ', 2);
                var firstName = nameParts[0];
                var lastName = nameParts.Length > 1 ? nameParts[1] : string.Empty;

                // Generate 1-2 addresses per customer
                var addressCount = random.Next(1, 3);
                var addresses = addressGenerator.GenerateMany(addressCount);
                var customerAddresses = addresses.Select((addr, idx) => new CustomerAddressListDto
                {
                    Firstname = addr.Firstname,
                    Lastname = addr.Lastname,
                    Street = addr.Street,
                    HouseNr = addr.HouseNr,
                    Zip = addr.Zip,
                    City = addr.City,
                    CountryId = countryId,
                    DefaultDeliveryAddress = idx == 0,
                    DefaultInvoiceAddress = idx == 0
                }).ToList();

                var input = new CustomerInputDto
                {
                    Firstname = firstName,
                    Lastname = lastName,
                    Email = $"demo.{firstName.ToLower()}.{lastName.ToLower()}.{i}@example.com",
                    Phone = $"+49 {random.Next(100, 999)} {random.Next(1000000, 9999999)}",
                    Website = random.NextDouble() > 0.7 ? $"https://www.{lastName.ToLower()}.example.com" : string.Empty,
                    CustomerStatus = CustomerStatus.Active,
                    DateEnrollment = DateTimeOffset.Now.AddDays(-random.Next(1, 365)),
                    CustomerAddresses = customerAddresses
                };

                await _customerService.CreateCustomerAsync(input, token);
                CustomersProgress = i + 1;
            }
        }
        catch (OperationCanceledException)
        {
            // User cancelled
        }
        catch (ApiException ex)
        {
            CustomersErrorMessage = ex.CombinedMessage;
        }
        catch (Exception ex)
        {
            CustomersErrorMessage = ex.Message;
        }
        finally
        {
            IsGeneratingCustomers = false;
            _customersCts?.Dispose();
            _customersCts = null;
        }
    }

    /// <summary>
    /// Generate demo orders.
    /// </summary>
    public async Task GenerateOrdersAsync(CancellationToken ct = default)
    {
        if (IsGeneratingOrders) return;

        IsGeneratingOrders = true;
        OrdersErrorMessage = null;
        OrdersProgress = 0;
        OrdersTotalCount = (int)OrdersCount;

        _ordersCts = CancellationTokenSource.CreateLinkedTokenSource(ct);
        var token = _ordersCts.Token;

        try
        {
            // 1. Check for existing customers
            var customersResponse = await _customerService.GetCustomersAsync(
                new QueryParameters { PageNumber = 0, PageSize = 100 }, token);

            if (customersResponse.Data == null || customersResponse.Data.Count == 0)
            {
                throw new InvalidOperationException(_localizer["DemoDataGeneratorPage.Error.NoCustomers"]);
            }

            // 2. Check for existing products
            var productsResponse = await _productService.GetProductsAsync(
                new QueryParameters { PageNumber = 0, PageSize = 100 }, token);

            if (productsResponse.Data == null || productsResponse.Data.Count == 0)
            {
                throw new InvalidOperationException(_localizer["DemoDataGeneratorPage.Error.NoProducts"]);
            }

            // 3. Check for sales channels
            var salesChannelsResponse = await _salesChannelService.GetSalesChannelsAsync(
                new QueryParameters { PageNumber = 0, PageSize = 10 }, token);

            if (salesChannelsResponse.Data == null || salesChannelsResponse.Data.Count == 0)
            {
                throw new InvalidOperationException(_localizer["DemoDataGeneratorPage.Error.NoSalesChannel"]);
            }

            var customers = customersResponse.Data;
            var products = productsResponse.Data;
            var salesChannelId = salesChannelsResponse.Data[0].Id;

            var random = new Random();
            var paymentMethods = new[] { "PayPal", "Credit Card", "Invoice", "Direct Debit", "Prepayment" };
            var orderStatuses = new[] { OrderStatus.Pending, OrderStatus.Processing, OrderStatus.Completed, OrderStatus.ReadyForDelivery };
            var paymentStatuses = new[] { PaymentStatus.Invoiced, PaymentStatus.CompletelyPaid, PaymentStatus.PartiallyPaid };

            for (int i = 0; i < OrdersTotalCount; i++)
            {
                token.ThrowIfCancellationRequested();

                // Pick random customer
                var customer = customers[random.Next(customers.Count)];

                // Get customer details for addresses
                var customerDetail = await _customerService.GetCustomerAsync(customer.Id, token);

                // Generate 1-5 order items
                var itemCount = random.Next(1, 6);
                var orderItems = new List<OrderItem>();
                decimal subtotal = 0;
                decimal totalTax = 0;
                const double defaultTaxRate = 19.0;

                for (int j = 0; j < itemCount; j++)
                {
                    var product = products[random.Next(products.Count)];
                    var quantity = random.Next(1, 4);
                    var price = product.Price;

                    orderItems.Add(new OrderItem
                    {
                        ProductId = product.Id,
                        Name = product.Name,
                        Quantity = quantity,
                        Price = price,
                        TaxRate = defaultTaxRate
                    });

                    subtotal += price * quantity;
                    totalTax += price * quantity * (decimal)(defaultTaxRate / 100);
                }

                var shippingCost = Math.Round((decimal)(random.NextDouble() * 10 + 3.99), 2);
                var total = subtotal + totalTax + shippingCost;

                // Get address from customer or use defaults
                var address = customerDetail?.CustomerAddresses?.FirstOrDefault();

                var input = new OrderInputDto
                {
                    SalesChannelId = salesChannelId,
                    RemoteOrderId = $"DEMO-{DateTime.Now:yyyyMMdd}-{i + 1:D4}",
                    CustomerId = customer.CustomerId,
                    Status = orderStatuses[random.Next(orderStatuses.Length)],
                    OrderItems = orderItems,
                    PaymentMethod = paymentMethods[random.Next(paymentMethods.Length)],
                    PaymentStatus = paymentStatuses[random.Next(paymentStatuses.Length)],
                    Subtotal = subtotal,
                    ShippingCost = shippingCost,
                    TotalTax = totalTax,
                    Total = total,
                    DeliveryAddressFirstName = address?.Firstname ?? customer.Firstname,
                    DeliveryAddressLastName = address?.Lastname ?? customer.Lastname,
                    DeliveryAddressStreet = address != null ? $"{address.Street} {address.HouseNr}" : "Demo Street 1",
                    DeliveryAddressCity = address?.City ?? "Demo City",
                    DeliveryAddressZip = address?.Zip ?? "12345",
                    DeliveryAddressCountry = "DE",
                    InvoiceAddressFirstName = address?.Firstname ?? customer.Firstname,
                    InvoiceAddressLastName = address?.Lastname ?? customer.Lastname,
                    InvoiceAddressStreet = address != null ? $"{address.Street} {address.HouseNr}" : "Demo Street 1",
                    InvoiceAddressCity = address?.City ?? "Demo City",
                    InvoiceAddressZip = address?.Zip ?? "12345",
                    InvoiceAddressCountry = "DE",
                    DateOrdered = DateTime.Now.AddDays(-random.Next(1, 90))
                };

                await _orderService.CreateOrderAsync(input, token);
                OrdersProgress = i + 1;
            }
        }
        catch (OperationCanceledException)
        {
            // User cancelled
        }
        catch (ApiException ex)
        {
            OrdersErrorMessage = ex.CombinedMessage;
        }
        catch (Exception ex)
        {
            OrdersErrorMessage = ex.Message;
        }
        finally
        {
            IsGeneratingOrders = false;
            _ordersCts?.Dispose();
            _ordersCts = null;
        }
    }

    /// <summary>
    /// Generate AI-powered demo data.
    /// </summary>
    public async Task GenerateAiDataAsync(CancellationToken ct = default)
    {
        if (IsGeneratingAiData) return;

        IsGeneratingAiData = true;
        AiDataErrorMessage = null;
        AiDataProgress = 0;
        AiDataTotalCount = 4; // 1 AI Model + 3 AI Prompts (fixed count)

        _aiDataCts = CancellationTokenSource.CreateLinkedTokenSource(ct);
        var token = _aiDataCts.Token;

        try
        {
            // 1. Create AI Model with demo Ollama configuration
            var aiModelInput = new AiModelInputDto
            {
                AiModelType = AiModelType.Ollama,
                Name = "Demo AI Model",
                ApiUrl = "http://localhost:11434",
                NCtx = 4096
            };

            var aiModelId = await _aiModelService.CreateAiModelAsync(aiModelInput, token);
            AiDataProgress = 1;

            token.ThrowIfCancellationRequested();

            // 2. Create AI Prompts for the model
            var prompts = new[]
            {
                new AiPromptInputDto
                {
                    AiModelId = aiModelId,
                    Identifier = "rewrite-description",
                    PromptText = "You are a professional copywriter. Rewrite the following product description to be more engaging and persuasive while maintaining accuracy. Keep the same approximate length.\n\nOriginal description:\n{description}"
                },
                new AiPromptInputDto
                {
                    AiModelId = aiModelId,
                    Identifier = "shorten-description",
                    PromptText = "You are a professional copywriter. Create a concise, compelling version of the following product description. Reduce the length by approximately 50% while keeping the most important selling points.\n\nOriginal description:\n{description}"
                },
                new AiPromptInputDto
                {
                    AiModelId = aiModelId,
                    Identifier = "extend-description",
                    PromptText = "You are a professional copywriter. Expand the following product description with additional details, benefits, and use cases. Make it approximately twice as long while maintaining a professional tone.\n\nOriginal description:\n{description}"
                }
            };

            foreach (var prompt in prompts)
            {
                token.ThrowIfCancellationRequested();
                await _aiPromptService.CreateAiPromptAsync(prompt, token);
                AiDataProgress++;
            }
        }
        catch (OperationCanceledException)
        {
            // User cancelled
        }
        catch (ApiException ex)
        {
            AiDataErrorMessage = ex.CombinedMessage;
        }
        catch (Exception ex)
        {
            AiDataErrorMessage = ex.Message;
        }
        finally
        {
            IsGeneratingAiData = false;
            _aiDataCts?.Dispose();
            _aiDataCts = null;
        }
    }

    #endregion

    #region Helper Methods

    /// <summary>
    /// Generate valid EAN-13 with correct check digit.
    /// </summary>
    private static string GenerateEan13(Random random)
    {
        var digits = new int[12];
        for (int i = 0; i < 12; i++)
        {
            digits[i] = random.Next(0, 10);
        }

        // Calculate check digit
        int sum = 0;
        for (int i = 0; i < 12; i++)
        {
            sum += digits[i] * (i % 2 == 0 ? 1 : 3);
        }
        int checkDigit = (10 - (sum % 10)) % 10;

        return string.Concat(digits.Select(d => d.ToString())) + checkDigit;
    }

    /// <summary>
    /// Generate realistic ASIN (Amazon Standard Identification Number).
    /// </summary>
    private static string GenerateAsin(Random random)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        var result = new char[10];
        result[0] = 'B'; // ASINs for products typically start with 'B'
        for (int i = 1; i < 10; i++)
        {
            result[i] = chars[random.Next(chars.Length)];
        }
        return new string(result);
    }

    #endregion
}

/// <summary>
/// Navigation data for the demo data generator page.
/// </summary>
public record DemoDataGeneratorData(Guid TenantId, string TenantName);
