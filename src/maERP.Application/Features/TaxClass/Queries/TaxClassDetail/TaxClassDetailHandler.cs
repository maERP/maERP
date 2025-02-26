using AutoMapper;
using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Domain.Dtos.TaxClass;
using maERP.Domain.Wrapper;
using MediatR;

namespace maERP.Application.Features.TaxClass.Queries.TaxClassDetail;

public class TaxClassDetailHandler : IRequestHandler<TaxClassDetailQuery, Result<TaxClassDetailDto>>
{
    private readonly IMapper _mapper;
    private readonly IAppLogger<TaxClassDetailHandler> _logger;
    private readonly ITaxClassRepository _taxClassRepository;

    public TaxClassDetailHandler(IMapper mapper,
        IAppLogger<TaxClassDetailHandler> logger,
        ITaxClassRepository taxClassRepository)
    {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _taxClassRepository = taxClassRepository ?? throw new ArgumentNullException(nameof(taxClassRepository));
    }
    
    public async Task<Result<TaxClassDetailDto>> Handle(TaxClassDetailQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Retrieving tax class details for ID: {Id}", request.Id);
        
        var result = new Result<TaxClassDetailDto>();
        
        try
        {
            var taxClass = await _taxClassRepository.GetByIdAsync(request.Id, true);

            if (taxClass == null)
            {
                result.Succeeded = false;
                result.StatusCode = ResultStatusCode.NotFound;
                result.Messages.Add($"Tax class with ID {request.Id} not found");
                
                _logger.LogWarning("Tax class with ID {Id} not found", request.Id);
                return result;
            }

            var data = _mapper.Map<TaxClassDetailDto>(taxClass);

            result.Succeeded = true;
            result.StatusCode = ResultStatusCode.Ok;
            result.Data = data;
            
            _logger.LogInformation("Tax class with ID {Id} retrieved successfully", request.Id);
        }
        catch (Exception ex)
        {
            result.Succeeded = false;
            result.StatusCode = ResultStatusCode.InternalServerError;
            result.Messages.Add($"An error occurred while retrieving the tax class: {ex.Message}");
            
            _logger.LogError("Error retrieving tax class: {Message}", ex.Message);
        }
        
        return result;
    }
}