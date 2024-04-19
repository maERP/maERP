using AutoMapper;
using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Application.Dtos.TaxClass;
using MediatR;

namespace maERP.Application.Features.TaxClass.Queries.GetTaxClassesQuery;

public class GetTaxClassesQueryHandler : IRequestHandler<GetTaxClassesQuery, List<TaxClassListDto>>
{
    private readonly IMapper _mapper;
    private readonly IAppLogger<GetTaxClassesQueryHandler> _logger;
    private readonly ITaxClassRepository _taxClassRepository;

    public GetTaxClassesQueryHandler(IMapper mapper,
        IAppLogger<GetTaxClassesQueryHandler> logger, 
        ITaxClassRepository taxClassRepository)
    {
        _mapper = mapper;
        _logger = logger;
        _taxClassRepository = taxClassRepository; 
    }
    public async Task<List<TaxClassListDto>> Handle(GetTaxClassesQuery request, CancellationToken cancellationToken)
    {
        // Query the database
        var taxClasses = await _taxClassRepository.GetAllAsync();

        // Convert data objects to DTO objects
        var data = _mapper.Map<List<TaxClassListDto>>(taxClasses);

        // Return list of DTO objects
        _logger.LogInformation("All TaxClasses are retrieved successfully.");
        return data;
    }
}