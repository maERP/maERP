using AutoMapper;
using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Application.Features.Product.Commands.CreateProductCommand;
using MediatR;

namespace maERP.Application.Features.User.Commands.CreateUserCommand;

public class CreateUserCommandHandler : IRequestHandler<CreateProductCommand, int>
{
    private readonly IMapper _mapper;
    private readonly IAppLogger<CreateProductCommandHandler> _logger;
    private readonly IProductRepository _productRepository;

    public CreateUserCommandHandler(IMapper mapper,
        IAppLogger<CreateProductCommandHandler> logger,
        IProductRepository productRepository)
    {
        _mapper = mapper;
        _logger = logger;
        _productRepository = productRepository;
    }

    public async Task<int> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        // Validate incoming data
        var validator = new CreateProductCommandValidator(_productRepository);
        var validationResult = await validator.ValidateAsync(request);

        if(validationResult.Errors.Any())
        {
            _logger.LogWarning("Validation errors in create request for {0} - {1}", nameof(CreateProductCommand), request.TaxRate);
            throw new Exceptions.ValidationException("Invalid Product", validationResult);
        }

        // convert to domain entity object
        var productToCreate = _mapper.Map<Domain.Product>(request);

        // add to database
        await _productRepository.CreateAsync(productToCreate);

        // return record id
        return productToCreate.Id;
    }
}