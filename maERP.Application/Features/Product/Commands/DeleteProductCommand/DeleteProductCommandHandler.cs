using AutoMapper;
using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using MediatR;

namespace maERP.Application.Features.Product.Commands.DeleteProductCommand;

public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, int>
{
    private readonly IMapper _mapper;
    private readonly IAppLogger<DeleteProductCommandHandler> _logger;
    private readonly IProductRepository _productRepository;


    public DeleteProductCommandHandler(IMapper mapper,
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
            _logger.LogWarning("Validation errors in delete request for {0} - {1}", nameof(CreateProductCommand), request.Id);
            throw new Exceptions.ValidationException("Invalid Product", validationResult);
        }

        // convert to domain entity object
        var productToDelete = _mapper.Map<Domain.Models.Product>(request);

        // add to database
        await _productRepository.CreateAsync(productToDelete);

        // return record id
        return productToDelete.Id;
    }
}
