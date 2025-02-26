using AutoMapper;
using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Domain.Wrapper;
using MediatR;

namespace maERP.Application.Features.Product.Commands.ProductCreate;

public class ProductCreateHandler : IRequestHandler<ProductCreateCommand, Result<int>>
{
    private readonly IMapper _mapper;
    private readonly IAppLogger<ProductCreateHandler> _logger;
    private readonly IProductRepository _productRepository;

    public ProductCreateHandler(IMapper mapper,
        IAppLogger<ProductCreateHandler> logger,
        IProductRepository productRepository)
    {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
    }

    public async Task<Result<int>> Handle(ProductCreateCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Creating new product with SKU: {Sku}, Name: {Name}", request.Sku, request.Name);
        
        var result = new Result<int>();
        
        // Validate incoming data
        var validator = new ProductCreateValidator(_productRepository);
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            result.Succeeded = false;
            result.StatusCode = ResultStatusCode.BadRequest;
            result.Messages.AddRange(validationResult.Errors.Select(e => e.ErrorMessage));
            
            _logger.LogWarning("Validation errors in create request for {0}: {1}", 
                nameof(ProductCreateCommand), 
                string.Join(", ", result.Messages));
                
            return result;
        }

        try
        {
            // Map and create entity
            var productToCreate = _mapper.Map<Domain.Entities.Product>(request);
            
            // add to database
            await _productRepository.CreateAsync(productToCreate);

            result.Succeeded = true;
            result.StatusCode = ResultStatusCode.Created;
            result.Data = productToCreate.Id;
            
            _logger.LogInformation("Successfully created product with ID: {Id}", productToCreate.Id);
        }
        catch (Exception ex)
        {
            result.Succeeded = false;
            result.StatusCode = ResultStatusCode.InternalServerError;
            result.Messages.Add($"An error occurred while creating the product: {ex.Message}");
            
            _logger.LogError("Error creating product: {Message}", ex.Message);
        }

        return result;
    }
}