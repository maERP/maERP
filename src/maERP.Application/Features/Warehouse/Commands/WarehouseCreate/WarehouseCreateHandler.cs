using AutoMapper;
using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Domain.Wrapper;
using MediatR;

namespace maERP.Application.Features.Warehouse.Commands.WarehouseCreate;

public class WarehouseCreateHandler : IRequestHandler<WarehouseCreateCommand, Result<int>>
{
    private readonly IMapper _mapper;
    private readonly IAppLogger<WarehouseCreateHandler> _logger;
    private readonly IWarehouseRepository _warehouseRepository;

    public WarehouseCreateHandler(IMapper mapper,
        IAppLogger<WarehouseCreateHandler> logger,
        IWarehouseRepository warehouseRepository)
    {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _warehouseRepository = warehouseRepository ?? throw new ArgumentNullException(nameof(warehouseRepository));
    }

    public async Task<Result<int>> Handle(WarehouseCreateCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Creating new warehouse with name: {Name}", request.Name);
        
        var result = new Result<int>();
        
        // Validate incoming data
        var validator = new WarehouseCreateValidator(_warehouseRepository);
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            result.Succeeded = false;
            result.StatusCode = ResultStatusCode.BadRequest;
            result.Messages.AddRange(validationResult.Errors.Select(e => e.ErrorMessage));
            
            _logger.LogWarning("Validation errors in create request for {0}: {1}", 
                nameof(WarehouseCreateCommand), 
                string.Join(", ", result.Messages));
                
            return result;
        }

        try
        {
            // convert to domain entity object
            var warehouseToCreate = _mapper.Map<Domain.Entities.Warehouse>(request);
            
            // add to database
            await _warehouseRepository.CreateAsync(warehouseToCreate);

            // return record id
            result.Succeeded = true;
            result.StatusCode = ResultStatusCode.Created;
            result.Data = warehouseToCreate.Id;
            
            _logger.LogInformation("Successfully created warehouse with ID: {Id}", warehouseToCreate.Id);
        }
        catch (Exception ex)
        {
            result.Succeeded = false;
            result.StatusCode = ResultStatusCode.InternalServerError;
            result.Messages.Add($"An error occurred while creating the warehouse: {ex.Message}");
            
            _logger.LogError("Error creating warehouse: {Message}", ex.Message);
        }

        return result;
    }
}
