using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Domain.Wrapper;
using MediatR;

namespace maERP.Application.Features.TaxClass.Commands.TaxClassDelete;

public class TaxClassDeleteHandler : IRequestHandler<TaxClassDeleteCommand, Result<int>>
{
    private readonly IAppLogger<TaxClassDeleteHandler> _logger;
    private readonly ITaxClassRepository _taxClassRepository;


    public TaxClassDeleteHandler(
        IAppLogger<TaxClassDeleteHandler> logger,
        ITaxClassRepository taxClassRepository)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _taxClassRepository = taxClassRepository ?? throw new ArgumentNullException(nameof(taxClassRepository));
    }

    public async Task<Result<int>> Handle(TaxClassDeleteCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting tax class with ID: {Id}", request.Id);

        var result = new Result<int>();

        // Validate incoming data
        var validator = new TaxClassDeleteValidator(_taxClassRepository);
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            result.Succeeded = false;
            result.StatusCode = ResultStatusCode.BadRequest;
            result.Messages.AddRange(validationResult.Errors.Select(e => e.ErrorMessage));

            _logger.LogWarning("Validation errors in delete request for {0}: {1}",
                nameof(TaxClassDeleteCommand),
                string.Join(", ", result.Messages));

            return result;
        }

        try
        {
            // Create entity to delete
            var taxClassToDelete = new Domain.Entities.TaxClass
            {
                Id = request.Id
            };

            // Delete from database
            await _taxClassRepository.DeleteAsync(taxClassToDelete);

            result.Succeeded = true;
            result.StatusCode = ResultStatusCode.Ok;
            result.Data = taxClassToDelete.Id;

            _logger.LogInformation("Successfully deleted tax class with ID: {Id}", taxClassToDelete.Id);
        }
        catch (Exception ex)
        {
            result.Succeeded = false;
            result.StatusCode = ResultStatusCode.InternalServerError;
            result.Messages.Add($"An error occurred while deleting the tax class: {ex.Message}");

            _logger.LogError("Error deleting tax class: {Message}", ex.Message);
        }

        return result;
    }
}
