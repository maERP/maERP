using AutoMapper;
using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using MediatR;

namespace maERP.Application.Features.Warehouse.Commands.CreateWarehouseCommand;

public class CreateWarehouseCommandHandler : IRequestHandler<CreateWarehouseCommand, int>
{
    private readonly IMapper _mapper;
    private readonly IAppLogger<CreateWarehouseCommandHandler> _logger;
    private readonly IWarehouseRepository _warehouseRepository;


    public CreateWarehouseCommandHandler(IMapper mapper,
        IAppLogger<CreateWarehouseCommandHandler> logger,
        IWarehouseRepository warehouseRepository)
    {
        _mapper = mapper;
        _logger = logger;
        _warehouseRepository = warehouseRepository;
    }

    public async Task<int> Handle(CreateWarehouseCommand request, CancellationToken cancellationToken)
    {
        // Validate incoming data
        var validator = new CreateWarehouseCommandValidator();
        var validationResult = await validator.ValidateAsync(request);

        if(!validationResult.Errors.Any())
        {
            _logger.LogWarning("Validation errors in create request for {0} - {1}", nameof(CreateWarehouseCommand), request.Name);
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
