using AutoMapper;
using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Domain.Wrapper;
using MediatR;

namespace maERP.Application.Features.SalesChannel.Commands.SalesChannelUpdate;

public class SalesChannelUpdateQuery : IRequestHandler<SalesChannelInputCommand, Result<int>>
{
    private readonly IMapper _mapper;
    private readonly IAppLogger<SalesChannelUpdateQuery> _logger;
    private readonly ISalesChannelRepository _salesChannelRepository;


    public SalesChannelUpdateQuery(IMapper mapper,
        IAppLogger<SalesChannelUpdateQuery> logger,
        ISalesChannelRepository salesChannelRepository)
    {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _salesChannelRepository = salesChannelRepository ?? throw new ArgumentNullException(nameof(salesChannelRepository));
    }

    public async Task<Result<int>> Handle(SalesChannelInputCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating sales channel with ID: {Id} and name: {Name}", request.Id, request.Name);
        
        var result = new Result<int>();
        
        // Validate incoming data
        var validator = new SalesChannelUpdateValidator(_salesChannelRepository);
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            result.Succeeded = false;
            result.StatusCode = ResultStatusCode.BadRequest;
            result.Messages.AddRange(validationResult.Errors.Select(e => e.ErrorMessage));
            
            _logger.LogWarning("Validation errors in update request for {0}: {1}", 
                nameof(SalesChannelInputCommand), 
                string.Join(", ", result.Messages));
                
            return result;
        }

        try
        {
            // Map to domain entity
            var salesChannelToUpdate = _mapper.Map<Domain.Entities.SalesChannel>(request);
            
            // Update in database
            await _salesChannelRepository.UpdateAsync(salesChannelToUpdate);
            
            result.Succeeded = true;
            result.StatusCode = ResultStatusCode.Ok;
            result.Data = salesChannelToUpdate.Id;
            
            _logger.LogInformation("Successfully updated sales channel with ID: {Id}", salesChannelToUpdate.Id);
        }
        catch (Exception ex)
        {
            result.Succeeded = false;
            result.StatusCode = ResultStatusCode.InternalServerError;
            result.Messages.Add($"An error occurred while updating the sales channel: {ex.Message}");
            
            _logger.LogError("Error updating sales channel: {Message}", ex.Message);
        }

        return result;
    }
}
