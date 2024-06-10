using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Application.Exceptions;
using MediatR;

namespace maERP.Application.Features.Product.Commands.ProductDelete;

public class ProductDeleteHandler : IRequestHandler<ProductDeleteCommand, int>
{
    private readonly IAppLogger<ProductDeleteHandler> _logger;
    private readonly IProductRepository _productRepository;
    
    public ProductDeleteHandler(
        IAppLogger<ProductDeleteHandler> logger,
        IProductRepository productRepository)
    {
        _logger = logger;
        _productRepository = productRepository;
    }

    public async Task<int> Handle(ProductDeleteCommand request, CancellationToken cancellationToken)
    {
        var validator = new ProductDeleteValidator(_productRepository);
        var validationResult = await validator.ValidateAsync(request);

        if(validationResult.Errors.Any())
        {
            _logger.LogWarning("Validation errors in delete request for {0} - {1}", nameof(ProductDeleteCommand), request.Id);
            throw new ValidationException("Invalid Product", validationResult);
        }

        var productToDelete = new Domain.Models.Product
        {
            Id = request.Id
        };

        await _productRepository.DeleteAsync(productToDelete);

        return productToDelete.Id;
    }
}