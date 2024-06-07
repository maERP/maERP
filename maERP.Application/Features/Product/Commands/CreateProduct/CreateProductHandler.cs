using AutoMapper;
using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Application.Exceptions;
using MediatR;

namespace maERP.Application.Features.Product.Commands.CreateProduct;

public class CreateProductHandler : IRequestHandler<CreateProductCommand, int>
{
    private readonly IMapper _mapper;
    private readonly IAppLogger<CreateProductHandler> _logger;
    private readonly IProductRepository _productRepository;

    public CreateProductHandler(IMapper mapper,
        IAppLogger<CreateProductHandler> logger,
        IProductRepository productRepository)
    {
        _mapper = mapper;
        _logger = logger;
        _productRepository = productRepository;
    }

    public async Task<int> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var validator = new CreateProductValidator(_productRepository);
        var validationResult = await validator.ValidateAsync(request);

        if(validationResult.Errors.Any())
        {
            _logger.LogWarning("Validation errors in create request for {0} - {1}", nameof(CreateProductCommand), request.Name);
            throw new ValidationException("Invalid Product", validationResult);
        }

        var productToCreate = _mapper.Map<Domain.Models.Product>(request);

        await _productRepository.CreateAsync(productToCreate);

        return productToCreate.Id;
    }
}