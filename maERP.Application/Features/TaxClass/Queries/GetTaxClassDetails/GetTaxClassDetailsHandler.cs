using AutoMapper;
using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Application.Dtos.TaxClass;
using maERP.Application.Exceptions;
using MediatR;

namespace maERP.Application.Features.TaxClass.Queries.GetTaxClassDetailQuery;

public class GetTaxClassDetailsHandler : IRequestHandler<GetTaxClassDetailsQuery, TaxClassDetailDto>
{
    private readonly IMapper _mapper;
    private readonly IAppLogger<GetTaxClassDetailsHandler> _logger;
    private readonly ITaxClassRepository _taxClassRepository;

    public GetTaxClassDetailsHandler(IMapper mapper,
        IAppLogger<GetTaxClassDetailsHandler> logger,
        ITaxClassRepository taxClassRepository)
    {
        _mapper = mapper;
        _logger = logger;
        _taxClassRepository = taxClassRepository;
    }
    public async Task<TaxClassDetailDto> Handle(GetTaxClassDetailsQuery request, CancellationToken cancellationToken)
    {
        var taxClass = await _taxClassRepository.GetByIdAsync(request.Id, true);

        if(taxClass == null)
        {
            throw new NotFoundException("NotFoundException", "TaxClass not found.");
        }

        var data = _mapper.Map<TaxClassDetailDto>(taxClass);

        _logger.LogInformation("All TaxClasses are retrieved successfully.");
        return data;
    }
}