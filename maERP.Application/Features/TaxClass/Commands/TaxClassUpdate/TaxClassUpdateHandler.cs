using AutoMapper;
using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Application.Exceptions;
using MediatR;

namespace maERP.Application.Features.TaxClass.Commands.TaxClassUpdate;

public class TaxClassUpdateHandler : IRequestHandler<TaxClassUpdateCommand, int>
{
    private readonly IMapper _mapper;
    private readonly IAppLogger<TaxClassUpdateHandler> _logger;
    private readonly ITaxClassRepository _taxClassRepository;


    public TaxClassUpdateHandler(IMapper mapper,
        IAppLogger<TaxClassUpdateHandler> logger,
        ITaxClassRepository taxClassRepository)
    {
        _mapper = mapper;
        _logger = logger;
        _taxClassRepository = taxClassRepository;
    }

    public async Task<int> Handle(TaxClassUpdateCommand request, CancellationToken cancellationToken)
    {
        // Validate incoming data
        var validator = new TaxClassUpdateValidator(_taxClassRepository);
        var validationResult = await validator.ValidateAsync(request);

        if(validationResult.Errors.Any())
        {
            _logger.LogWarning("Validation errors in update request for {0} - {1}", nameof(TaxClassUpdateCommand), request.Id);
            throw new ValidationException("Invalid TaxClass", validationResult);
        }

        var taxClassToUpdate = _mapper.Map<Domain.Entities.TaxClass>(request);

        await _taxClassRepository.UpdateAsync(taxClassToUpdate);

        return taxClassToUpdate.Id;
    }
}
