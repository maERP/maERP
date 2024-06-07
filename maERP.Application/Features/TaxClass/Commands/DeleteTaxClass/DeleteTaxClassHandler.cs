using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Application.Exceptions;
using MediatR;

namespace maERP.Application.Features.TaxClass.Commands.DeleteTaxClass;

public class DeleteTaxClassHandler : IRequestHandler<DeleteTaxClassCommand, int>
{
    private readonly IAppLogger<DeleteTaxClassHandler> _logger;
    private readonly ITaxClassRepository _taxClassRepository;


    public DeleteTaxClassHandler(
        IAppLogger<DeleteTaxClassHandler> logger,
        ITaxClassRepository taxClassRepository)
    {
        _logger = logger;
        _taxClassRepository = taxClassRepository;
    }

    public async Task<int> Handle(DeleteTaxClassCommand request, CancellationToken cancellationToken)
    {
        var validator = new DeleteTaxClassCommandValidator(_taxClassRepository);
        var validationResult = await validator.ValidateAsync(request);

        if(validationResult.Errors.Any())
        {
            _logger.LogWarning("Validation errors in delete request for {0} - {1}", nameof(DeleteTaxClassCommand), request.Id);
            throw new ValidationException("Invalid TaxClass", validationResult);
        }

        var taxClassToDelete = new Domain.Models.TaxClass
        {
            Id = request.Id
        };

        await _taxClassRepository.DeleteAsync(taxClassToDelete);

        return taxClassToDelete.Id;
    }
}
