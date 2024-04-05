using AutoMapper;
using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using MediatR;

namespace maERP.Application.Features.Warehouse.Commands.DeleteWarehouseCommand;

public class DeleteTaxClassCommandHandler : IRequestHandler<DeleteTaxClassCommand, int>
{
    private readonly IMapper _mapper;
    private readonly IAppLogger<DeleteTaxClassCommandHandler> _logger;
    private readonly IWarehouseRepository _warehouseRepository;


    public DeleteTaxClassCommandHandler(IMapper mapper,
        IAppLogger<DeleteTaxClassCommandHandler> logger,
        IWarehouseRepository warehouseRepository)
    {
        _mapper = mapper;
        _logger = logger;
        _warehouseRepository = warehouseRepository;
    }

    public async Task<int> Handle(DeleteTaxClassCommand request, CancellationToken cancellationToken)
    {
        // Validate incoming data
        var validator = new DeleteWarehouseCommandValidator(_warehouseRepository);
        var validationResult = await validator.ValidateAsync(request);

        if(!validationResult.Errors.Any())
        {
            _logger.LogWarning("Validation errors in delete request for {0} - {1}", nameof(CreateWarehouseCommand), request.Id);
            throw new Exceptions.ValidationException("Invalid Warehouse", validationResult);
        }

        // convert to domain entity object
        var warehouseToCreate = _mapper.Map<Domain.Warehouse>(request);

        // add to database
        await _warehouseRepository.CreateAsync(warehouseToCreate);

        // return record id
        return warehouseToCreate.Id;

        // Example Exception:
        // throw new NotFoundException(nameof(Warehouse), request.Id);
    }
}
