using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Application.Exceptions;
using MediatR;

namespace maERP.Application.Features.TaxClass.Commands.TaxClassDelete;

public class TaxClassDeleteHandler : IRequestHandler<TaxClassDeleteCommand, int>
{
    private readonly IAppLogger<TaxClassDeleteHandler> _logger;
    private readonly ITaxClassRepository _taxClassRepository;


    public TaxClassDeleteHandler(
        IAppLogger<TaxClassDeleteHandler> logger,
        ITaxClassRepository taxClassRepository)
    {
        _logger = logger;
        _taxClassRepository = taxClassRepository;
    }

    public async Task<int> Handle(TaxClassDeleteCommand request, CancellationToken cancellationToken)
    {
        var validator = new TaxClassDeleteValidator(_taxClassRepository);
        var validationResult = await validator.ValidateAsync(request);

        if(validationResult.Errors.Any())
        {
            _logger.LogWarning("Validation errors in delete request for {0} - {1}", nameof(TaxClassDeleteCommand), request.Id);
            throw new ValidationException("Invalid TaxClass", validationResult);
        }

        var taxClassToDelete = new Domain.Entities.TaxClass
        {
            Id = request.Id
        };

        await _taxClassRepository.DeleteAsync(taxClassToDelete);

        return taxClassToDelete.Id;
    }
}
