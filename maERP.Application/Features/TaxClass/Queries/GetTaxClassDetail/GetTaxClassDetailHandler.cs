using AutoMapper;
using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Application.Exceptions;
using MediatR;

namespace maERP.Application.Features.TaxClass.Queries.GetTaxClassDetail;

public class GetTaxClassDetailHandler : IRequestHandler<GetTaxClassDetailQuery, GetTaxClassDetailResponse>
{
    private readonly IMapper _mapper;
    private readonly IAppLogger<GetTaxClassDetailHandler> _logger;
    private readonly ITaxClassRepository _taxClassRepository;

    public GetTaxClassDetailHandler(IMapper mapper,
        IAppLogger<GetTaxClassDetailHandler> logger,
        ITaxClassRepository taxClassRepository)
    {
        _mapper = mapper;
        _logger = logger;
        _taxClassRepository = taxClassRepository;
    }
    public async Task<GetTaxClassDetailResponse> Handle(GetTaxClassDetailQuery request, CancellationToken cancellationToken)
    {
        var taxClass = await _taxClassRepository.GetByIdAsync(request.Id, true);

        if(taxClass == null)
        {
            throw new NotFoundException("NotFoundException", "TaxClass not found.");
        }

        var data = _mapper.Map<GetTaxClassDetailResponse>(taxClass);

        _logger.LogInformation("All TaxClasses are retrieved successfully.");
        return data;
    }
}