using AutoMapper;
using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Application.Dtos;
using maERP.Application.Dtos.TaxClass;
using MediatR;

namespace maERP.Application.Features.TaxClass.Queries.GetAllTaxClassesQuery;

public class GetAllTaxClassesQueryHandler : IRequestHandler<GetAllTaxClassesQuery, List<TaxClassListDto>>
{
    private readonly IMapper _mapper;
    private readonly IAppLogger<GetAllTaxClassesQueryHandler> _logger;
    private readonly ITaxClassRepository _taxClassRepository;

    public GetAllTaxClassesQueryHandler(IMapper mapper,
        IAppLogger<GetAllTaxClassesQueryHandler> logger, 
        ITaxClassRepository taxClassRepository)
    {
        _mapper = mapper;
        _logger = logger;
        _taxClassRepository = taxClassRepository; 
    }
    public async Task<List<TaxClassListDto>> Handle(GetAllTaxClassesQuery request, CancellationToken cancellationToken)
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