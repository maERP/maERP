using AutoMapper;
using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Application.Exceptions;
using MediatR;

namespace maERP.Application.Features.Warehouse.Commands.WarehouseUpdate;

public class WarehouseUpdateHandler : IRequestHandler<WarehouseUpdateCommand, int>
{
    private readonly IMapper _mapper;
    private readonly IAppLogger<WarehouseUpdateHandler> _logger;
    private readonly IWarehouseRepository _warehouseRepository;


    public WarehouseUpdateHandler(IMapper mapper,
        IAppLogger<WarehouseUpdateHandler> logger,
        IWarehouseRepository warehouseRepository)
    {
        _mapper = mapper;
        _logger = logger;
        _warehouseRepository = warehouseRepository;
    }

    public async Task<int> Handle(WarehouseUpdateCommand request, CancellationToken cancellationToken)
    {
        // Validate incoming data
        var validator = new WarehouseUpdateValidator(_warehouseRepository);
        var validationResult = await validator.ValidateAsync(request);

        if(validationResult.Errors.Any())
        {
            _logger.LogWarning("Validation errors in update request for {0} - {1}", nameof(WarehouseUpdateCommand), request.Name);
            throw new ValidationException("Invalid Warehouse", validationResult);
        }
        
        var warehouseToUpdate = _mapper.Map<Domain.Models.Warehouse>(request);
        
        await _warehouseRepository.UpdateAsync(warehouseToUpdate);

        return warehouseToUpdate.Id;
    }
}
