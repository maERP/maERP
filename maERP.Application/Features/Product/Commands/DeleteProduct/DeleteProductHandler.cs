using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Application.Exceptions;
using MediatR;

namespace maERP.Application.Features.Product.Commands.DeleteProduct;

public class DeleteProductHandler : IRequestHandler<DeleteProductCommand, int>
{
    private readonly IAppLogger<DeleteProductHandler> _logger;
    private readonly IProductRepository _productRepository;
    
    public DeleteProductHandler(
        IAppLogger<DeleteProductHandler> logger,
        IProductRepository productRepository)
    {
        _logger = logger;
        _productRepository = productRepository;
    }

    public async Task<int> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var validator = new DeleteProductValidator(_productRepository);
        var validationResult = await validator.ValidateAsync(request);

        if(validationResult.Errors.Any())
        {
            _logger.LogWarning("Validation errors in delete request for {0} - {1}", nameof(DeleteProductCommand), request.Id);
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