using AutoMapper;
using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Application.Dtos.TaxClass;
using MediatR;

namespace maERP.Application.Features.TaxClass.Queries.GetTaxClassQuery;

public class GetTaxClassQueryHandler : IRequestHandler<GetTaxClassQuery, TaxClassDetailDto>
{
    private readonly IMapper _mapper;
    private readonly IAppLogger<GetTaxClassQueryHandler> _logger;
    private readonly ITaxClassRepository _taxClassRepository;

    public GetTaxClassQueryHandler(IMapper mapper,
        IAppLogger<GetTaxClassQueryHandler> logger,
        ITaxClassRepository taxClassRepository)
    {
        _mapper = mapper;
        _logger = logger;
        _taxClassRepository = taxClassRepository;
    }
    public async Task<TaxClassDetailDto> Handle(GetTaxClassQuery request, CancellationToken cancellationToken)
    {
        // Query the database
        var taxClass = await _taxClassRepository.GetByIdAsync(request.Id);

        // Convert data objects to DTO objects
        var data = _mapper.Map<TaxClassDetailDto>(taxClass);

        // Return list of DTO objects
        _logger.LogInformation("All TaxClasses are retrieved successfully.");
        return data;
    }
}