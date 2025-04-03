using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Domain.Wrapper;
using MediatR;

namespace maERP.Application.Features.SalesChannel.Commands.SalesChannelCreate;

public class SalesChannelCreateHandler : IRequestHandler<SalesChannelCreateCommand, Result<int>>
{
    private readonly IAppLogger<SalesChannelCreateHandler> _logger;
    private readonly ISalesChannelRepository _salesChannelRepository;

    public SalesChannelCreateHandler(
        IAppLogger<SalesChannelCreateHandler> logger,
        ISalesChannelRepository salesChannelRepository)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _salesChannelRepository = salesChannelRepository ?? throw new ArgumentNullException(nameof(salesChannelRepository));
    }

    public async Task<Result<int>> Handle(SalesChannelCreateCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Creating new sales channel with name: {Name}", request.Name);
        
        var result = new Result<int>();
        
        // Validate incoming data
        var validator = new SalesChanneLCreateValidator(_salesChannelRepository);
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            result.Succeeded = false;
            result.StatusCode = ResultStatusCode.BadRequest;
            result.Messages.AddRange(validationResult.Errors.Select(e => e.ErrorMessage));
            
            _logger.LogWarning("Validation errors in create request for {0}: {1}", 
                nameof(SalesChannelCreateCommand), 
                string.Join(", ", result.Messages));
                
            return result;
        }

        try
        {
            // Map und create entity
            var salesChannelToCreate = MapToEntity(request);
            
            // add to database
            await _salesChannelRepository.CreateAsync(salesChannelToCreate);

            result.Succeeded = true;
            result.StatusCode = ResultStatusCode.Created;
            result.Data = salesChannelToCreate.Id;
            
            _logger.LogInformation("Successfully created sales channel with ID: {Id}", salesChannelToCreate.Id);
        }
        catch (Exception ex)
        {
            result.Succeeded = false;
            result.StatusCode = ResultStatusCode.InternalServerError;
            result.Messages.Add($"An error occurred while creating the sales channel: {ex.Message}");
            
            _logger.LogError("Error creating sales channel: {Message}", ex.Message);
        }

        return result;
    }
    
    private Domain.Entities.SalesChannel MapToEntity(SalesChannelCreateCommand command)
    {
        return new Domain.Entities.SalesChannel
        {
            Type = command.SalesChannelType,
            Name = command.Name,
            Url = command.Url,
            Username = command.Username,
            Password = command.Password,
            ImportProducts = command.ImportProducts,
            ImportCustomers = command.ImportCustomers,
            ImportOrders = command.ImportOrders,
            ExportProducts = command.ExportProducts,
            ExportCustomers = command.ExportCustomers,
            ExportOrders = command.ExportOrders,
            WarehouseId = command.WarehouseId
        };
    }
}