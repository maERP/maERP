using AutoMapper;
using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using MediatR;

namespace maERP.Application.Features.Warehouse.Commands.UpdateWarehouseCommand;

public class UpdateTaxClassCommandHandler : IRequestHandler<UpdateTaxClassCommand, int>
{
    private readonly IMapper _mapper;
    private readonly IAppLogger<UpdateTaxClassCommandHandler> _logger;
    private readonly IWarehouseRepository _warehouseRepository;


    public UpdateTaxClassCommandHandler(IMapper mapper,
        IAppLogger<UpdateTaxClassCommandHandler> logger,
        IWarehouseRepository warehouseRepository)
    {
        _mapper = mapper;
        _logger = logger;
        _warehouseRepository = warehouseRepository;
    }

    public async Task<int> Handle(UpdateTaxClassCommand request, CancellationToken cancellationToken)
    {
        // Validate incoming data
        var validator = new UpdateWarehouseCommandValidator(_warehouseRepository);
        var validationResult = await validator.ValidateAsync(request);

        if(!validationResult.Errors.Any())
        {
            _logger.LogWarning("Validation errors in update request for {0} - {1}", nameof(CreateWarehouseCommand), request.Name);
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
