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
            // Load existing manufacturer from database
            var existingManufacturer = await _manufacturerRepository.GetByIdAsync(request.Id);

            if (existingManufacturer == null)
            {
                result.Succeeded = false;
                result.StatusCode = ResultStatusCode.NotFound;
                result.Messages.Add($"Manufacturer with ID {request.Id} not found");
                return result;
            }

            // Update only the provided fields, preserving system fields like TenantId, DateCreated, etc.
            existingManufacturer.Name = request.Name;
            existingManufacturer.Street = request.Street;
            existingManufacturer.City = request.City;
            existingManufacturer.State = request.State;
            existingManufacturer.Country = request.Country;
            existingManufacturer.ZipCode = request.ZipCode;
            existingManufacturer.Phone = request.Phone;
            existingManufacturer.Email = request.Email;
            existingManufacturer.Website = request.Website;
            existingManufacturer.Logo = request.Logo;

            // Update in database
            await _manufacturerRepository.UpdateAsync(existingManufacturer);

            result.Succeeded = true;
            result.StatusCode = ResultStatusCode.Ok;
            result.Data = existingManufacturer.Id;

            _logger.LogInformation("Successfully updated manufacturer with ID: {Id}", existingManufacturer.Id);
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