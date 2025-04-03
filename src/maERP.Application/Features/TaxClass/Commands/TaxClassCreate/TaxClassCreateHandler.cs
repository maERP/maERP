using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Domain.Wrapper;
using MediatR;

namespace maERP.Application.Features.TaxClass.Commands.TaxClassCreate;

public class TaxClassCreateHandler : IRequestHandler<TaxClassCreateCommand, Result<int>>
{
    private readonly IAppLogger<TaxClassCreateHandler> _logger;
    private readonly ITaxClassRepository _taxClassRepository;

    public TaxClassCreateHandler(
        IAppLogger<TaxClassCreateHandler> logger,
        ITaxClassRepository taxClassRepository)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _taxClassRepository = taxClassRepository ?? throw new ArgumentNullException(nameof(taxClassRepository));
    }

    public async Task<Result<int>> Handle(TaxClassCreateCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Creating new tax class with tax rate: {TaxRate}", request.TaxRate);
        
        var result = new Result<int>();
        
        // Validate incoming data
        var validator = new TaxClassCreateValidator(_taxClassRepository);
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            result.Succeeded = false;
            result.StatusCode = ResultStatusCode.BadRequest;
            result.Messages.AddRange(validationResult.Errors.Select(e => e.ErrorMessage));
            
            _logger.LogWarning("Validation errors in create request for {0}: {1}", 
                nameof(TaxClassCreateCommand), 
                string.Join(", ", result.Messages));
                
            return result;
        }

        try
        {
            // Manually map to entity
            var taxClassToCreate = new Domain.Entities.TaxClass
            {
                TaxRate = request.TaxRate
            };
            
            // add to database
            await _taxClassRepository.CreateAsync(taxClassToCreate);

            result.Succeeded = true;
            result.StatusCode = ResultStatusCode.Ok;
            result.Data = taxClassToCreate.Id;
            
            _logger.LogInformation("Successfully created tax class with ID: {Id}", taxClassToCreate.Id);
        }
        catch (Exception ex)
        {
            result.Succeeded = false;
            result.StatusCode = ResultStatusCode.InternalServerError;
            result.Messages.Add($"An error occurred while creating the tax class: {ex.Message}");
            
            _logger.LogError("Error creating tax class: {Message}", ex.Message);
        }

        return result;
    }
}