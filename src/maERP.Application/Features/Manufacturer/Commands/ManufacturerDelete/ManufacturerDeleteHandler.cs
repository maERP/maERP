using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Domain.Wrapper;
using maERP.Application.Mediator;

namespace maERP.Application.Features.Manufacturer.Commands.ManufacturerDelete;

public class ManufacturerDeleteHandler : IRequestHandler<ManufacturerDeleteCommand, Result<Guid>>
{
    private readonly IAppLogger<ManufacturerDeleteHandler> _logger;
    private readonly IManufacturerRepository _manufacturerRepository;
    private readonly IProductRepository _productRepository;

    public ManufacturerDeleteHandler(
        IAppLogger<ManufacturerDeleteHandler> logger,
        IManufacturerRepository manufacturerRepository,
        IProductRepository productRepository)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _manufacturerRepository = manufacturerRepository ?? throw new ArgumentNullException(nameof(manufacturerRepository));
        _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
    }

    public async Task<Result<Guid>> Handle(ManufacturerDeleteCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting manufacturer with ID: {Id}", request.Id);

        var result = new Result<Guid>();

        // Validate incoming data
        var validator = new ManufacturerDeleteValidator(_manufacturerRepository, _productRepository);
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            result.Succeeded = false;
            result.StatusCode = ResultStatusCode.BadRequest;
            result.Messages.AddRange(validationResult.Errors.Select(e => e.ErrorMessage));

            _logger.LogWarning("Validation errors in delete request for {0}: {1}",
                nameof(ManufacturerDeleteCommand),
                string.Join(", ", result.Messages));

            return result;
        }

        try
        {
            // Create entity to delete
            var manufacturerToDelete = new Domain.Entities.Manufacturer
            {
                Id = request.Id
            };

            // Delete from database
            await _manufacturerRepository.DeleteAsync(manufacturerToDelete);

            result.Succeeded = true;
            result.StatusCode = ResultStatusCode.Ok;
            result.Data = manufacturerToDelete.Id;

            _logger.LogInformation("Successfully deleted manufacturer with ID: {Id}", manufacturerToDelete.Id);
        }
        catch (Exception ex)
        {
            result.Succeeded = false;
            result.StatusCode = ResultStatusCode.InternalServerError;
            result.Messages.Add($"An error occurred while deleting the manufacturer: {ex.Message}");

            _logger.LogError("Error deleting manufacturer: {Message}", ex.Message);
        }

        return result;
    }
}