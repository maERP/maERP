using AutoMapper;
using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Application.Exceptions;
using MediatR;

namespace maERP.Application.Features.Warehouse.Commands.WarehouseCreate;

public class WarehouseCreateHandler : IRequestHandler<WarehouseCreateCommand, int>
{
    private readonly IMapper _mapper;
    private readonly IAppLogger<WarehouseCreateHandler> _logger;
    private readonly IWarehouseRepository _warehouseRepository;

    public WarehouseCreateHandler(IMapper mapper,
        IAppLogger<WarehouseCreateHandler> logger,
        IWarehouseRepository warehouseRepository)
    {
        _mapper = mapper;
        _logger = logger;
        _warehouseRepository = warehouseRepository;
    }

    public async Task<int> Handle(WarehouseCreateCommand request, CancellationToken cancellationToken)
    {
        // Validate incoming data
        var validator = new WarehouseCreateValidator(_warehouseRepository);
        var validationResult = await validator.ValidateAsync(request);

        if(validationResult.Errors.Any())
        {
            _logger.LogWarning("Validation errors in create request for {0} - {1}", nameof(WarehouseCreateCommand), request.Name);
            throw new ValidationException("Invalid Warehouse", validationResult);
        }

        // convert to domain entity object
        var warehouseToCreate = _mapper.Map<Domain.Entities.Warehouse>(request);

        // add to database
        await _warehouseRepository.CreateAsync(warehouseToCreate);

        // return record id
        return warehouseToCreate.Id;
    }
}
