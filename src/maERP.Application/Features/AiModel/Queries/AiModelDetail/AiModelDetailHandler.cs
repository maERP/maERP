using AutoMapper;
using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Domain.Dtos.AiModel;
using maERP.Domain.Wrapper;
using MediatR;

namespace maERP.Application.Features.AiModel.Queries.AiModelDetail;

public class AiModelDetailHandler : IRequestHandler<AiModelDetailQuery, Result<AiModelDetailDto>>
{
    private readonly IMapper _mapper;
    private readonly IAppLogger<AiModelDetailHandler> _logger;
    private readonly IAiModelRepository _aiModelRepository;

    public AiModelDetailHandler(IMapper mapper,
        IAppLogger<AiModelDetailHandler> logger,
        IAiModelRepository aiModelRepository)
    {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _aiModelRepository = aiModelRepository ?? throw new ArgumentNullException(nameof(aiModelRepository));
    }
    
    public async Task<Result<AiModelDetailDto>> Handle(AiModelDetailQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Retrieving AI model details for ID: {Id}", request.Id);
        
        var result = new Result<AiModelDetailDto>();
        
        try
        {
            var aiModel = await _aiModelRepository.GetByIdAsync(request.Id, true);

            if (aiModel == null)
            {
                result.Succeeded = false;
                result.StatusCode = ResultStatusCode.NotFound;
                result.Messages.Add($"AI model with ID {request.Id} not found");
                
                _logger.LogWarning("AI model with ID {Id} not found", request.Id);
                return result;
            }

            var data = _mapper.Map<AiModelDetailDto>(aiModel);

            result.Succeeded = true;
            result.StatusCode = ResultStatusCode.Ok;
            result.Data = data;
            
            _logger.LogInformation("AI model with ID {Id} retrieved successfully", request.Id);
        }
        catch (Exception ex)
        {
            result.Succeeded = false;
            result.StatusCode = ResultStatusCode.InternalServerError;
            result.Messages.Add($"An error occurred while retrieving the AI model: {ex.Message}");
            
            _logger.LogError("Error retrieving AI model: {Message}", ex.Message);
        }
        
        return result;
    }
}