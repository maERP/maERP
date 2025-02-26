using AutoMapper;
using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Domain.Dtos.SalesChannel;
using maERP.Domain.Wrapper;
using MediatR;

namespace maERP.Application.Features.SalesChannel.Queries.SalesChannelDetail;

public class SalesChannelDetailHandler : IRequestHandler<SalesChannelDetailQuery, Result<SalesChannelDetailDto>>
{
    private readonly IMapper _mapper;
    private readonly IAppLogger<SalesChannelDetailHandler> _logger;
    private readonly ISalesChannelRepository _salesChannelRepository;

    public SalesChannelDetailHandler(IMapper mapper,
        IAppLogger<SalesChannelDetailHandler> logger,
        ISalesChannelRepository salesChannelRepository)
    {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _salesChannelRepository = salesChannelRepository ?? throw new ArgumentNullException(nameof(salesChannelRepository));
    }
    
    public async Task<Result<SalesChannelDetailDto>> Handle(SalesChannelDetailQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Retrieving sales channel details for ID: {Id}", request.Id);
        
        var result = new Result<SalesChannelDetailDto>();
        
        try
        {
            var salesChannel = await _salesChannelRepository.GetByIdAsync(request.Id, true);

            if (salesChannel == null)
            {
                result.Succeeded = false;
                result.StatusCode = ResultStatusCode.NotFound;
                result.Messages.Add($"Sales channel with ID {request.Id} not found");
                
                _logger.LogWarning("Sales channel with ID {Id} not found", request.Id);
                return result;
            }

            var data = _mapper.Map<SalesChannelDetailDto>(salesChannel);

            result.Succeeded = true;
            result.StatusCode = ResultStatusCode.Ok;
            result.Data = data;
            
            _logger.LogInformation("Sales channel with ID {Id} retrieved successfully", request.Id);
        }
        catch (Exception ex)
        {
            result.Succeeded = false;
            result.StatusCode = ResultStatusCode.InternalServerError;
            result.Messages.Add($"An error occurred while retrieving the sales channel: {ex.Message}");
            
            _logger.LogError("Error retrieving sales channel: {Message}", ex.Message);
        }
        
        return result;
    }
}