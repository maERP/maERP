using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Domain.Wrapper;
using maERP.Application.Mediator;

namespace maERP.Application.Features.Manufacturer.Commands.ManufacturerUpdate;

public class ManufacturerUpdateHandler : IRequestHandler<ManufacturerUpdateCommand, Result<Guid>>
{
    private readonly IAppLogger<ManufacturerUpdateHandler> _logger;
    private readonly IManufacturerRepository _manufacturerRepository;

    public ManufacturerUpdateHandler(
        IAppLogger<ManufacturerUpdateHandler> logger,
        IManufacturerRepository manufacturerRepository)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _manufacturerRepository = manufacturerRepository ?? throw new ArgumentNullException(nameof(manufacturerRepository));
    }

    public async Task<Result<Guid>> Handle(ManufacturerUpdateCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating manufacturer with ID: {Id}, Name: {Name}", request.Id, request.Name);

        var result = new Result<Guid>();

        // Validate incoming data
        var validator = new ManufacturerUpdateValidator(_manufacturerRepository);
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            result.Succeeded = false;
            result.StatusCode = ResultStatusCode.BadRequest;
            result.Messages.AddRange(validationResult.Errors.Select(e => e.ErrorMessage));

            _logger.LogWarning("Validation errors in update request for {0}: {1}",
                nameof(ManufacturerUpdateCommand),
                string.Join(", ", result.Messages));

            return result;
        }

        try
        {
            // Manual mapping to domain entity
            var manufacturerToUpdate = new Domain.Entities.Manufacturer
            {
                Id = request.Id,
                Name = request.Name,
                Street = request.Street,
                City = request.City,
                State = request.State,
                Country = request.Country,
                ZipCode = request.ZipCode,
                Phone = request.Phone,
                Email = request.Email,
                Website = request.Website,
                Logo = request.Logo
            };

            // Update in database
            await _manufacturerRepository.UpdateAsync(manufacturerToUpdate);

            result.Succeeded = true;
            result.StatusCode = ResultStatusCode.Ok;
            result.Data = manufacturerToUpdate.Id;

            _logger.LogInformation("Successfully updated manufacturer with ID: {Id}", manufacturerToUpdate.Id);
        }
        catch (Exception ex)
        {
            result.Succeeded = false;
            result.StatusCode = ResultStatusCode.InternalServerError;
            result.Messages.Add($"An error occurred while updating the manufacturer: {ex.Message}");

            _logger.LogError("Error updating manufacturer: {Message}", ex.Message);
        }

        return result;
    }
}