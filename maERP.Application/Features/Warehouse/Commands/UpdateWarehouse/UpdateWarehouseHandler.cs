using AutoMapper;
using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Application.Exceptions;
using MediatR;

namespace maERP.Application.Features.Warehouse.Commands.UpdateWarehouse;

public class UpdateWarehouseHandler : IRequestHandler<UpdateWarehouseCommand, int>
{
    private readonly IMapper _mapper;
    private readonly IAppLogger<UpdateWarehouseHandler> _logger;
    private readonly IWarehouseRepository _warehouseRepository;


    public UpdateWarehouseHandler(IMapper mapper,
        IAppLogger<UpdateWarehouseHandler> logger,
        IWarehouseRepository warehouseRepository)
    {
        _mapper = mapper;
        _logger = logger;
        _warehouseRepository = warehouseRepository;
    }

    public async Task<int> Handle(UpdateWarehouseCommand request, CancellationToken cancellationToken)
    {
        // Validate incoming data
        var validator = new UpdateWarehouseValidator(_warehouseRepository);
        var validationResult = await validator.ValidateAsync(request);

        if(validationResult.Errors.Any())
        {
            _logger.LogWarning("Validation errors in update request for {0} - {1}", nameof(UpdateWarehouseCommand), request.Name);
            throw new ValidationException("Invalid Warehouse", validationResult);
        }
        
        var warehouseToUpdate = _mapper.Map<Domain.Models.Warehouse>(request);
        
        await _warehouseRepository.UpdateAsync(warehouseToUpdate);

        return warehouseToUpdate.Id;
    }
}
