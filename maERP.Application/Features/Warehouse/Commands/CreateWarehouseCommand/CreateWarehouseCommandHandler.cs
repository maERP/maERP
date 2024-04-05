using AutoMapper;
using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using MediatR;

namespace maERP.Application.Features.Warehouse.Commands.CreateWarehouseCommand;

public class CreateTaxClassCommandHandler : IRequestHandler<CreateTaxClassCommand, int>
{
    private readonly IMapper _mapper;
    private readonly IAppLogger<CreateTaxClassCommandHandler> _logger;
    private readonly IWarehouseRepository _warehouseRepository;

    public CreateTaxClassCommandHandler(IMapper mapper,
        IAppLogger<CreateTaxClassCommandHandler> logger,
        IWarehouseRepository warehouseRepository)
    {
        _mapper = mapper;
        _logger = logger;
        _warehouseRepository = warehouseRepository;
    }

    public async Task<int> Handle(CreateTaxClassCommand request, CancellationToken cancellationToken)
    {
        // Validate incoming data
        var validator = new CreateWarehouseCommandValidator(_warehouseRepository);
        var validationResult = await validator.ValidateAsync(request);

        if(validationResult.Errors.Any())
        {
            _logger.LogWarning("Validation errors in create request for {0} - {1}", nameof(CreateTaxClassCommand), request.Name);
            throw new Exceptions.ValidationException("Invalid Warehouse", validationResult);
        }

        // convert to domain entity object
        var warehouseToCreate = _mapper.Map<Domain.Warehouse>(request);

        // add to database
        await _warehouseRepository.CreateAsync(warehouseToCreate);

        // return record id
        return warehouseToCreate.Id;
    }
}
