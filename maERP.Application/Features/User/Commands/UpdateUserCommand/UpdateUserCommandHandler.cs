using AutoMapper;
using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Application.Exceptions;
using maERP.Application.Features.Product.Commands.UpdateProductCommand;
using MediatR;

namespace maERP.Application.Features.User.Commands.UpdateUserCommand;

public class UpdateUserCommandHandler : IRequestHandler<UpdateProductCommand, int>
{
    private readonly IMapper _mapper;
    private readonly IAppLogger<UpdateProductCommandHandler> _logger;
    private readonly IProductRepository _productRepository;


    public UpdateUserCommandHandler(IMapper mapper,
        IAppLogger<UpdateProductCommandHandler> logger,
        IProductRepository productRepository)
    {
        _mapper = mapper;
        _logger = logger;
        _productRepository = productRepository;
    }

    public async Task<int> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        // Validate incoming data
        var validator = new UpdateProductCommandValidator(_productRepository);
        var validationResult = await validator.ValidateAsync(request);

        if(validationResult.Errors.Any())
        {
            _logger.LogWarning("Validation errors in update request for {0} - {1}", nameof(Product.Commands.CreateProductCommand), request.Id);
            throw new ValidationException("Invalid Product", validationResult);
        }

        // convert to domain entity object
        var productToUpdate = _mapper.Map<Domain.Models.Product>(request);

        // add to database
        await _productRepository.UpdateAsync(productToUpdate);

        // return record id
        return productToUpdate.Id;
    }
}
