using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Application.Contracts.Services;
using maERP.Domain.Wrapper;
using maERP.Application.Mediator;

namespace maERP.Application.Features.Product.Commands.ProductCreate;

/// <summary>
/// Handler for processing product creation commands.
/// Implements IRequestHandler from MediatR to handle ProductCreateCommand requests
/// and return the ID of the newly created product wrapped in a Result.
/// </summary>
public class ProductCreateHandler : IRequestHandler<ProductCreateCommand, Result<int>>
{
    /// <summary>
    /// Logger for recording handler operations
    /// </summary>
    private readonly IAppLogger<ProductCreateHandler> _logger;

    /// <summary>
    /// Repository for product data operations
    /// </summary>
    private readonly IProductRepository _productRepository;

    /// <summary>
    /// Repository for tax class data operations
    /// </summary>
    private readonly ITaxClassRepository _taxClassRepository;

    /// <summary>
    /// Repository for manufacturer data operations
    /// </summary>
    private readonly IManufacturerRepository _manufacturerRepository;

    /// <summary>
    /// Tenant context for handling multi-tenancy
    /// </summary>
    private readonly ITenantContext _tenantContext;

    /// <summary>
    /// Constructor that initializes the handler with required dependencies
    /// </summary>
    /// <param name="logger">Logger for recording operations</param>
    /// <param name="productRepository">Repository for product data access</param>
    /// <param name="taxClassRepository">Repository for tax class data access</param>
    /// <param name="manufacturerRepository">Repository for manufacturer data access</param>
    /// <param name="tenantContext">Tenant context for multi-tenancy</param>
    public ProductCreateHandler(
        IAppLogger<ProductCreateHandler> logger,
        IProductRepository productRepository,
        ITaxClassRepository taxClassRepository,
        IManufacturerRepository manufacturerRepository,
        ITenantContext tenantContext)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
        _taxClassRepository = taxClassRepository ?? throw new ArgumentNullException(nameof(taxClassRepository));
        _manufacturerRepository = manufacturerRepository ?? throw new ArgumentNullException(nameof(manufacturerRepository));
        _tenantContext = tenantContext ?? throw new ArgumentNullException(nameof(tenantContext));
    }

    /// <summary>
    /// Handles the product creation request
    /// </summary>
    /// <param name="request">The product creation command with product details</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Result containing the ID of the newly created product if successful</returns>
    public async Task<Result<int>> Handle(ProductCreateCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Creating new product with SKU: {Sku}, Name: {Name}", request.Sku, request.Name);

        // Validate incoming data
        var validator = new ProductCreateValidator(_productRepository, _taxClassRepository, _manufacturerRepository);
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        // If validation fails, return a Problem Details result with validation error messages
        if (!validationResult.IsValid)
        {
            var validationErrors = string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage));
            
            _logger.LogWarning("Validation errors in create request for {0}: {1}",
                nameof(ProductCreateCommand), validationErrors);

            return Result<int>.Fail(ResultStatusCode.BadRequest, validationErrors);
        }

        try
        {
            // Get current tenant ID for proper data isolation
            var currentTenantId = _tenantContext.GetCurrentTenantId();
            if (!currentTenantId.HasValue)
            {
                _logger.LogError("Attempted to create product without tenant context");
                return Result<int>.Fail(ResultStatusCode.BadRequest, 
                    "Tenant context is not set. Cannot create product without tenant information.");
            }

            // Manual mapping instead of using AutoMapper
            var productToCreate = new Domain.Entities.Product
            {
                Sku = request.Sku,
                Name = request.Name,
                NameOptimized = request.NameOptimized,
                Ean = request.Ean,
                Asin = request.Asin,
                Description = request.Description,
                DescriptionOptimized = request.DescriptionOptimized,
                UseOptimized = request.UseOptimized,
                Price = request.Price,
                Msrp = request.Msrp,
                Weight = request.Weight,
                Width = request.Width,
                Height = request.Height,
                Depth = request.Depth,
                TaxClassId = request.TaxClassId,
                ManufacturerId = request.ManufacturerId,
                TenantId = currentTenantId.Value // Explicitly set TenantId for data isolation
            };

            // Add the new product to the database
            await _productRepository.CreateAsync(productToCreate);

            _logger.LogInformation("Successfully created product with ID: {Id}", productToCreate.Id);
            
            var result = Result<int>.Success(productToCreate.Id);
            result.StatusCode = ResultStatusCode.Created;
            return result;
        }
        catch (Exception ex)
        {
            // Handle any exceptions during product creation
            _logger.LogError("Error creating product: {Message}", ex.Message);
            
            return Result<int>.Fail(ResultStatusCode.InternalServerError, 
                $"An error occurred while creating the product: {ex.Message}");
        }
    }
}