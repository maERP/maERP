using AutoMapper;
using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using MediatR;

namespace maERP.Application.Features.TaxClass.Queries.TaxClassList;

public class TaxClassListHandler : IRequestHandler<TaxClassListQuery, List<TaxClassListResponse>>
{
    private readonly IMapper _mapper;
    private readonly IAppLogger<TaxClassListHandler> _logger;
    private readonly ITaxClassRepository _taxClassRepository;

    public TaxClassListHandler(IMapper mapper,
        IAppLogger<TaxClassListHandler> logger, 
        ITaxClassRepository taxClassRepository)
    {
        _mapper = mapper;
        _logger = logger;
        _taxClassRepository = taxClassRepository; 
    }
    public async Task<List<TaxClassListResponse>> Handle(TaxClassListQuery request, CancellationToken cancellationToken)
    {
        // Query the database
        var taxClasses = await _taxClassRepository.GetAllAsync();

        // Convert data objects to DTO objects
        var data = _mapper.Map<List<TaxClassListResponse>>(taxClasses);

        // Return list of DTO objects
        _logger.LogInformation("All TaxClasses are retrieved successfully.");
        return data;
    }
}