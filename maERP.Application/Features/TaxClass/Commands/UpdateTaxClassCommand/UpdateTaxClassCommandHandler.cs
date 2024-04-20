using AutoMapper;
using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Application.Exceptions;
using MediatR;

namespace maERP.Application.Features.TaxClass.Commands.UpdateTaxClassCommand;

public class UpdateTaxClassCommandHandler : IRequestHandler<UpdateTaxClassCommand, int>
{
    private readonly IMapper _mapper;
    private readonly IAppLogger<UpdateTaxClassCommandHandler> _logger;
    private readonly ITaxClassRepository _taxClassRepository;


    public UpdateTaxClassCommandHandler(IMapper mapper,
        IAppLogger<UpdateTaxClassCommandHandler> logger,
        ITaxClassRepository taxClassRepository)
    {
        _mapper = mapper;
        _logger = logger;
        _taxClassRepository = taxClassRepository;
    }

    public async Task<int> Handle(UpdateTaxClassCommand request, CancellationToken cancellationToken)
    {
        // Validate incoming data
        var validator = new UpdateTaxClassCommandValidator(_taxClassRepository);
        var validationResult = await validator.ValidateAsync(request);

        if(validationResult.Errors.Any())
        {
            _logger.LogWarning("Validation errors in update request for {0} - {1}", nameof(CreateTaxClassCommand), request.Id);
            throw new ValidationException("Invalid TaxClass", validationResult);
        }

        // convert to domain entity object
        var taxClassToUpdate = _mapper.Map<Domain.Models.TaxClass>(request);

        // add to database
        await _taxClassRepository.UpdateAsync(taxClassToUpdate);

        // return record id
        return taxClassToUpdate.Id;
    }
}
