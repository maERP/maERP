using AutoMapper;
using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Application.Features.Product.Commands.DeleteProductCommand;
using MediatR;

namespace maERP.Application.Features.User.Commands.DeleteUserCommand;

public class DeleteUserCommandHandler : IRequestHandler<DeleteProductCommand, int>
{
    private readonly IMapper _mapper;
    private readonly IAppLogger<DeleteProductCommandHandler> _logger;
    private readonly IProductRepository _productRepository;


    public DeleteUserCommandHandler(IMapper mapper,
        IAppLogger<DeleteProductCommandHandler> logger,
        IProductRepository productRepository)
    {
        _mapper = mapper;
        _logger = logger;
        _productRepository = productRepository;
    }

    public async Task<int> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        // Validate incoming data
        var validator = new DeleteProductCommandValidator(_productRepository);
        var validationResult = await validator.ValidateAsync(request);

        if(validationResult.Errors.Any())
        {
            _logger.LogWarning("Validation errors in delete request for {0} - {1}", nameof(Product.Commands.CreateProductCommand), request.Id);
            throw new Exceptions.ValidationException("Invalid Product", validationResult);
        }

        // convert to domain entity object
        var productToDelete = _mapper.Map<Domain.Product>(request);

        // add to database
        await _productRepository.CreateAsync(productToDelete);

        // return record id
        return productToDelete.Id;
    }
}
