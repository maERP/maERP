using AutoMapper;
using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Application.Exceptions;
using MediatR;

namespace maERP.Application.Features.Product.Commands.ProductUpdate;

public class ProductUpdateHandler : IRequestHandler<ProductUpdateCommand, int>
{
    private readonly IMapper _mapper;
    private readonly IAppLogger<ProductUpdateHandler> _logger;
    private readonly IProductRepository _productRepository;


    public ProductUpdateHandler(IMapper mapper,
        IAppLogger<ProductUpdateHandler> logger,
        IProductRepository productRepository)
    {
        _mapper = mapper;
        _logger = logger;
        _productRepository = productRepository;
    }

    public async Task<int> Handle(ProductUpdateCommand request, CancellationToken cancellationToken)
    {
        // Validate incoming data
        var validator = new ProductUpdateValidator(_productRepository);
        var validationResult = await validator.ValidateAsync(request);

        if(validationResult.Errors.Any())
        {
            _logger.LogWarning("Validation errors in update request for {0} - {1}", nameof(ProductUpdateCommand), request.Id);
            throw new ValidationException("Invalid Product", validationResult);
        }

        // convert to domain entity object
        var productToUpdate = _mapper.Map<Domain.Entities.Product>(request);

        // add to database
        await _productRepository.UpdateAsync(productToUpdate);

        // return record id
        return productToUpdate.Id;
    }
}
