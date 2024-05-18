using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Application.Exceptions;
using MediatR;

namespace maERP.Application.Features.Product.Commands.DeleteProductCommand;

public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, int>
{
    private readonly IAppLogger<DeleteProductCommandHandler> _logger;
    private readonly IProductRepository _productRepository;
    
    public DeleteProductCommandHandler(
        IAppLogger<DeleteProductCommandHandler> logger,
        IProductRepository productRepository)
    {
        _logger = logger;
        _productRepository = productRepository;
    }

    public async Task<int> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var validator = new DeleteProductCommandValidator(_productRepository);
        var validationResult = await validator.ValidateAsync(request);

        if(validationResult.Errors.Any())
        {
            _logger.LogWarning("Validation errors in delete request for {0} - {1}", nameof(CreateProductCommand), request.Id);
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