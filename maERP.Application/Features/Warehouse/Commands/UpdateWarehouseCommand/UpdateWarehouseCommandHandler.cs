using AutoMapper;
using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using MediatR;

namespace maERP.Application.Features.Warehouse.Commands.UpdateWarehouseCommand;

public class UpdateWarehouseCommandHandler : IRequestHandler<UpdateWarehouseCommand, int>
{
    private readonly IMapper _mapper;
    private readonly IAppLogger<UpdateWarehouseCommandHandler> _logger;
    private readonly IWarehouseRepository _warehouseRepository;


    public UpdateWarehouseCommandHandler(IMapper mapper,
        IAppLogger<UpdateWarehouseCommandHandler> logger,
        IWarehouseRepository warehouseRepository)
    {
        _mapper = mapper;
        _logger = logger;
        _warehouseRepository = warehouseRepository;
    }

    public async Task<int> Handle(UpdateWarehouseCommand request, CancellationToken cancellationToken)
    {
        // Validate incoming data
        var validator = new UpdateWarehouseCommandValidator(_warehouseRepository);
        var validationResult = await validator.ValidateAsync(request);

        if(validationResult.Errors.Any())
        {
            _logger.LogWarning("Validation errors in update request for {0} - {1}", nameof(CreateWarehouseCommand), request.Name);
            throw new Exceptions.ValidationException("Invalid Warehouse", validationResult);
        }

        // convert to domain entity object
        var warehouseToUpdate = _mapper.Map<Domain.Models.Warehouse>(request);

        // add to database
        await _warehouseRepository.UpdateAsync(warehouseToUpdate);

        // return record id
        return warehouseToUpdate.Id;

        // Example Exception:
        // throw new NotFoundException(nameof(Warehouse), request.Id);
    }
}
