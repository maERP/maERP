using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Application.Exceptions;
using MediatR;

namespace maERP.Application.Features.Warehouse.Commands.WarehouseDelete;

public class WarehouseDeleteHandler : IRequestHandler<WarehouseDeleteCommand, int>
{
    private readonly IAppLogger<WarehouseDeleteHandler> _logger;
    private readonly IWarehouseRepository _warehouseRepository;
    
    public WarehouseDeleteHandler(
        IAppLogger<WarehouseDeleteHandler> logger,
        IWarehouseRepository warehouseRepository)
    {
        _logger = logger;
        _warehouseRepository = warehouseRepository;
    }

    public async Task<int> Handle(WarehouseDeleteCommand request, CancellationToken cancellationToken)
    {
        // Validate incoming data
        var validator = new WarehouseDeleteValidator(_warehouseRepository);
        var validationResult = await validator.ValidateAsync(request);

        if(validationResult.Errors.Any())
        {
            _logger.LogWarning("Validation errors in delete request for {0} - {1}", nameof(WarehouseDeleteCommand), request.Id);
            throw new ValidationException("Invalid Warehouse", validationResult);
        }

        // convert to domain entity object
        // var warehouseToDelete = _mapper.Map<Domain.Models.Warehouse>(request);
        var warehouseToDelete = new Domain.Models.Warehouse()
        {
            Id = request.Id
        };
        
        await _warehouseRepository.DeleteAsync(warehouseToDelete);
        
        return warehouseToDelete.Id;
    }
}
