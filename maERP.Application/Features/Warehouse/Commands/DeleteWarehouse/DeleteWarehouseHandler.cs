using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Application.Exceptions;
using MediatR;

namespace maERP.Application.Features.Warehouse.Commands.DeleteWarehouse;

public class DeleteWarehouseHandler : IRequestHandler<DeleteWarehouseCommand, int>
{
    private readonly IAppLogger<DeleteWarehouseHandler> _logger;
    private readonly IWarehouseRepository _warehouseRepository;
    
    public DeleteWarehouseHandler(
        IAppLogger<DeleteWarehouseHandler> logger,
        IWarehouseRepository warehouseRepository)
    {
        _logger = logger;
        _warehouseRepository = warehouseRepository;
    }

    public async Task<int> Handle(DeleteWarehouseCommand request, CancellationToken cancellationToken)
    {
        // Validate incoming data
        var validator = new DeleteWarehouseValidator(_warehouseRepository);
        var validationResult = await validator.ValidateAsync(request);

        if(validationResult.Errors.Any())
        {
            _logger.LogWarning("Validation errors in delete request for {0} - {1}", nameof(DeleteWarehouseCommand), request.Id);
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
