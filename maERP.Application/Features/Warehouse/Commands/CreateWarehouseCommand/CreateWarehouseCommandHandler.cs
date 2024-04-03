using AutoMapper;
using maERP.Application.Contracts.Persistence;
using MediatR;

namespace maERP.Application.Features.Warehouse.Commands.CreateWarehouseCommand;

public class CreateWarehouseCommandHandler : IRequestHandler<CreateWarehouseCommand, int>
{
    private readonly IWarehouseRepository _warehouseRepository;
    private readonly IMapper _mapper;

    public CreateWarehouseCommandHandler(IWarehouseRepository warehouseRepository, IMapper mapper)
    {
        _warehouseRepository = warehouseRepository;
        _mapper = mapper;
    }

    public async Task<int> Handle(CreateWarehouseCommand request, CancellationToken cancellationToken)
    {
        // Validate incoming data
        var validator = new CreateWarehouseCommandValidator();
        var validationResult = await validator.ValidateAsync(request);

        if(!validationResult.Errors.Any())
        {
            throw new Exceptions.ValidationException("Invalid Warehouse", validationResult);
        }

        // convert to domain entity object
        var warehouseToCreate = _mapper.Map<Domain.Warehouse>(request);

        // add to database
        await _warehouseRepository.AddAsync(warehouseToCreate);

        // return record id
        return warehouseToCreate.Id;

        // Example Exception:
        // throw new NotFoundException(nameof(Warehouse), request.Id);
    }
}
