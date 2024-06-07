using AutoMapper;
using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using MediatR;

namespace maERP.Application.Features.TaxClass.Queries.GetTaxClasses;

public class GetTaxClassesHandler : IRequestHandler<GetTaxClassesQuery, List<GetTaxClassesResponse>>
{
    private readonly IMapper _mapper;
    private readonly IAppLogger<GetTaxClassesHandler> _logger;
    private readonly ITaxClassRepository _taxClassRepository;

    public GetTaxClassesHandler(IMapper mapper,
        IAppLogger<GetTaxClassesHandler> logger, 
        ITaxClassRepository taxClassRepository)
    {
        _mapper = mapper;
        _logger = logger;
        _taxClassRepository = taxClassRepository; 
    }
    public async Task<List<GetTaxClassesResponse>> Handle(GetTaxClassesQuery request, CancellationToken cancellationToken)
    {
        // Query the database
        var taxClasses = await _taxClassRepository.GetAllAsync();

        // Convert data objects to DTO objects
        var data = _mapper.Map<List<GetTaxClassesResponse>>(taxClasses);

        // Return list of DTO objects
        _logger.LogInformation("All TaxClasses are retrieved successfully.");
        return data;
    }
}