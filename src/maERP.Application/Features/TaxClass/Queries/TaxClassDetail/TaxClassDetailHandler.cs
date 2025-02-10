using AutoMapper;
using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Application.Exceptions;
using maERP.Domain.Dtos.TaxClass;
using MediatR;

namespace maERP.Application.Features.TaxClass.Queries.TaxClassDetail;

public class TaxClassDetailHandler : IRequestHandler<TaxClassDetailQuery, TaxClassDetailDto>
{
    private readonly IMapper _mapper;
    private readonly IAppLogger<TaxClassDetailHandler> _logger;
    private readonly ITaxClassRepository _taxClassRepository;

    public TaxClassDetailHandler(IMapper mapper,
        IAppLogger<TaxClassDetailHandler> logger,
        ITaxClassRepository taxClassRepository)
    {
        _mapper = mapper;
        _logger = logger;
        _taxClassRepository = taxClassRepository;
    }
    public async Task<TaxClassDetailDto> Handle(TaxClassDetailQuery request, CancellationToken cancellationToken)
    {
        var taxClass = await _taxClassRepository.GetByIdAsync(request.Id, true);

        if(taxClass == null)
        {
            throw new NotFoundException("NotFoundException", "TaxClass not found.");
        }

        var data = _mapper.Map<TaxClassDetailDto>(taxClass);

        _logger.LogInformation("TaxClass retrieved successfully.");
        return data;
    }
}