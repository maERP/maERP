using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Domain.Wrapper;
using maERP.Application.Mediator;
using System.Diagnostics;

namespace maERP.Application.Features.DemoData.Commands.ClearAllData;

public class ClearAllDataHandler : IRequestHandler<ClearAllDataCommand, Result<string>>
{
    private readonly IAppLogger<ClearAllDataHandler> _logger;
    private readonly IOrderRepository _orderRepository;
    private readonly IInvoiceRepository _invoiceRepository;
    private readonly IProductSalesChannelRepository _productSalesChannelRepository;
    private readonly IProductRepository _productRepository;
    private readonly ICustomerRepository _customerRepository;
    private readonly IWarehouseRepository _warehouseRepository;
    private readonly ITaxClassRepository _taxClassRepository;
    private readonly ISalesChannelRepository _salesChannelRepository;
    private readonly ISettingRepository _settingRepository;
    private readonly IAiPromptRepository _aiPromptRepository;
    private readonly IAiModelRepository _aiModelRepository;
    private readonly ICountryRepository _countryRepository;

    public ClearAllDataHandler(
        IAppLogger<ClearAllDataHandler> logger,
        IOrderRepository orderRepository,
        IInvoiceRepository invoiceRepository,
        IProductSalesChannelRepository productSalesChannelRepository,
        IProductRepository productRepository,
        ICustomerRepository customerRepository,
        IWarehouseRepository warehouseRepository,
        ITaxClassRepository taxClassRepository,
        ISalesChannelRepository salesChannelRepository,
        ISettingRepository settingRepository,
        IAiPromptRepository aiPromptRepository,
        IAiModelRepository aiModelRepository,
        ICountryRepository countryRepository)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
        _invoiceRepository = invoiceRepository ?? throw new ArgumentNullException(nameof(invoiceRepository));
        _productSalesChannelRepository = productSalesChannelRepository ?? throw new ArgumentNullException(nameof(productSalesChannelRepository));
        _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
        _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
        _warehouseRepository = warehouseRepository ?? throw new ArgumentNullException(nameof(warehouseRepository));
        _taxClassRepository = taxClassRepository ?? throw new ArgumentNullException(nameof(taxClassRepository));
        _salesChannelRepository = salesChannelRepository ?? throw new ArgumentNullException(nameof(salesChannelRepository));
        _settingRepository = settingRepository ?? throw new ArgumentNullException(nameof(settingRepository));
        _aiPromptRepository = aiPromptRepository ?? throw new ArgumentNullException(nameof(aiPromptRepository));
        _aiModelRepository = aiModelRepository ?? throw new ArgumentNullException(nameof(aiModelRepository));
        _countryRepository = countryRepository ?? throw new ArgumentNullException(nameof(countryRepository));
    }

    public async Task<Result<string>> Handle(ClearAllDataCommand request, CancellationToken cancellationToken)
    {
        if (!Debugger.IsAttached)
        {
            return Result<string>.Fail("Demo data clearing is only available when debugger is attached.");
        }

        _logger.LogInformation("Starting to clear all data from database");

        var result = new Result<string>();
        var deletedCounts = new List<string>();

        try
        {
            // Delete in correct order to respect foreign key constraints
            // Start with dependent entities first, then referenced entities

            // 1. Delete Orders (depends on Customer, SalesChannel)
            var orders = await _orderRepository.GetAllAsync();
            foreach (var order in orders)
            {
                await _orderRepository.DeleteAsync(order);
            }
            if (orders.Count > 0)
                deletedCounts.Add($"{orders.Count} orders");

            // 2. Delete Invoices (depends on Customer)
            var invoices = await _invoiceRepository.GetAllAsync();
            foreach (var invoice in invoices)
            {
                await _invoiceRepository.DeleteAsync(invoice);
            }
            if (invoices.Count > 0)
                deletedCounts.Add($"{invoices.Count} invoices");

            // 3. Delete ProductSalesChannel relationships (depends on Product, SalesChannel)
            var productSalesChannels = await _productSalesChannelRepository.GetAllAsync();
            foreach (var productSalesChannel in productSalesChannels)
            {
                await _productSalesChannelRepository.DeleteAsync(productSalesChannel);
            }
            if (productSalesChannels.Count > 0)
                deletedCounts.Add($"{productSalesChannels.Count} product-sales channel relationships");

            // 4. Delete Products (depends on TaxClass)
            var products = await _productRepository.GetAllAsync();
            foreach (var product in products)
            {
                await _productRepository.DeleteAsync(product);
            }
            if (products.Count > 0)
                deletedCounts.Add($"{products.Count} products");

            // 5. Delete Customers
            var customers = await _customerRepository.GetAllAsync();
            foreach (var customer in customers)
            {
                await _customerRepository.DeleteAsync(customer);
            }
            if (customers.Count > 0)
                deletedCounts.Add($"{customers.Count} customers");

            // 6. Delete SalesChannels
            var salesChannels = await _salesChannelRepository.GetAllAsync();
            foreach (var salesChannel in salesChannels)
            {
                await _salesChannelRepository.DeleteAsync(salesChannel);
            }
            if (salesChannels.Count > 0)
                deletedCounts.Add($"{salesChannels.Count} sales channels");

            // 7. Delete Warehouses
            var warehouses = await _warehouseRepository.GetAllAsync();
            foreach (var warehouse in warehouses)
            {
                await _warehouseRepository.DeleteAsync(warehouse);
            }
            if (warehouses.Count > 0)
                deletedCounts.Add($"{warehouses.Count} warehouses");

            // 8. Delete TaxClasses
            var taxClasses = await _taxClassRepository.GetAllAsync();
            foreach (var taxClass in taxClasses)
            {
                await _taxClassRepository.DeleteAsync(taxClass);
            }
            if (taxClasses.Count > 0)
                deletedCounts.Add($"{taxClasses.Count} tax classes");

            // 9. Delete AI Prompts
            var aiPrompts = await _aiPromptRepository.GetAllAsync();
            foreach (var aiPrompt in aiPrompts)
            {
                await _aiPromptRepository.DeleteAsync(aiPrompt);
            }
            if (aiPrompts.Count > 0)
                deletedCounts.Add($"{aiPrompts.Count} AI prompts");

            // 10. Delete AI Models
            var aiModels = await _aiModelRepository.GetAllAsync();
            foreach (var aiModel in aiModels)
            {
                await _aiModelRepository.DeleteAsync(aiModel);
            }
            if (aiModels.Count > 0)
                deletedCounts.Add($"{aiModels.Count} AI models");

            // 11. Delete Settings
            var settings = await _settingRepository.GetAllAsync();
            foreach (var setting in settings)
            {
                await _settingRepository.DeleteAsync(setting);
            }
            if (settings.Count > 0)
                deletedCounts.Add($"{settings.Count} settings");

            // 12. Delete Countries
            var countries = await _countryRepository.GetAllAsync();
            foreach (var country in countries)
            {
                await _countryRepository.DeleteAsync(country);
            }
            if (countries.Count > 0)
                deletedCounts.Add($"{countries.Count} countries");

            result.Succeeded = true;
            result.StatusCode = ResultStatusCode.Ok;

            if (deletedCounts.Count > 0)
            {
                result.Data = $"Successfully deleted: {string.Join(", ", deletedCounts)}";
                _logger.LogInformation("Successfully cleared all data: {Items}", string.Join(", ", deletedCounts));
            }
            else
            {
                result.Data = "Database was already empty - no data to delete";
                _logger.LogInformation("Database was already empty - no data to delete");
            }
        }
        catch (Exception ex)
        {
            result.Succeeded = false;
            result.StatusCode = ResultStatusCode.InternalServerError;
            result.Messages.Add($"An error occurred while clearing database: {ex.Message}");

            _logger.LogError("Error clearing database: {Message}", ex.Message);
        }

        return result;
    }
}