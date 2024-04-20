using AutoMapper;
using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Application.Exceptions;
using MediatR;

namespace maERP.Application.Features.TaxClass.Commands.DeleteTaxClassCommand;

public class DeleteTaxClassCommandHandler : IRequestHandler<DeleteTaxClassCommand, int>
{
    private readonly IMapper _mapper;
    private readonly IAppLogger<DeleteTaxClassCommandHandler> _logger;
    private readonly ITaxClassRepository _taxClassRepository;


    public DeleteTaxClassCommandHandler(IMapper mapper,
        IAppLogger<DeleteTaxClassCommandHandler> logger,
        ITaxClassRepository taxClassRepository)
    {
        _mapper = mapper;
        _logger = logger;
        _taxClassRepository = taxClassRepository;
    }

    public async Task<int> Handle(DeleteTaxClassCommand request, CancellationToken cancellationToken)
    {
        // Validate incoming data
        var validator = new DeleteTaxClassCommandValidator(_taxClassRepository);
        var validationResult = await validator.ValidateAsync(request);

        if(validationResult.Errors.Any())
        {
            _logger.LogWarning("Validation errors in delete request for {0} - {1}", nameof(CreateTaxClassCommand), request.Id);
            throw new ValidationException("Invalid TaxClass", validationResult);
        }

        // convert to domain entity object
        var taxClassToDelete = _mapper.Map<Domain.Models.TaxClass>(request);

        // add to database
        await _taxClassRepository.CreateAsync(taxClassToDelete);

        // return record id
        return taxClassToDelete.Id;
    }
}
