using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Domain.Wrapper;
using maERP.Application.Mediator;

namespace maERP.Application.Features.Setting.Commands.SettingDelete;

public class SettingDeleteHandler : IRequestHandler<SettingDeleteCommand, Result<Guid>>
{
    private readonly IAppLogger<SettingDeleteHandler> _logger;
    private readonly ISettingRepository _settingRepository;

    public SettingDeleteHandler(
        IAppLogger<SettingDeleteHandler> logger,
        ISettingRepository settingRepository)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _settingRepository = settingRepository ?? throw new ArgumentNullException(nameof(settingRepository));
    }

    public async Task<Result<Guid>> Handle(SettingDeleteCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting setting with ID: {Id}", request.Id);

        var result = new Result<Guid>();

        // Validate incoming data
        var validator = new SettingDeleteValidator(_settingRepository);
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            result.Succeeded = false;

            // Check if the validation error is about setting not found
            var settingNotFoundError = validationResult.Errors
                .FirstOrDefault(e => e.ErrorMessage.Contains("Setting not found"));

            if (settingNotFoundError != null)
            {
                result.StatusCode = ResultStatusCode.NotFound;
                result.Messages.Add("Setting not found.");
            }
            else
            {
                result.StatusCode = ResultStatusCode.BadRequest;
                result.Messages.AddRange(validationResult.Errors.Select(e => e.ErrorMessage));
            }

            _logger.LogWarning("Validation errors in delete request for {0}: {1}",
                nameof(SettingDeleteCommand),
                string.Join(", ", result.Messages));

            return result;
        }

        try
        {
            // Create entity to delete
            var settingToDelete = new Domain.Entities.Setting()
            {
                Id = request.Id
            };

            // Delete from database
            await _settingRepository.DeleteAsync(settingToDelete);

            result.Succeeded = true;
            result.StatusCode = ResultStatusCode.NoContent;
            result.Data = settingToDelete.Id;

            _logger.LogInformation("Successfully deleted setting with ID: {Id}", settingToDelete.Id);
        }
        catch (Exception ex)
        {
            result.Succeeded = false;

            var exceptionMessage = ex.Message ?? string.Empty;

            // Treat missing entity scenarios (including concurrent deletes) as NotFound
            if (ex is maERP.Application.Exceptions.NotFoundException ||
                ex is InvalidOperationException && exceptionMessage.Contains("not found", StringComparison.OrdinalIgnoreCase) ||
                exceptionMessage.Contains("does not exist in the store", StringComparison.OrdinalIgnoreCase) ||
                exceptionMessage.Contains("entity that does not exist", StringComparison.OrdinalIgnoreCase) ||
                exceptionMessage.Contains("not found", StringComparison.OrdinalIgnoreCase))
            {
                result.StatusCode = ResultStatusCode.NotFound;
                result.Messages.Add("Setting not found.");
                _logger.LogWarning(
                    "Setting with ID: {Id} not found for deletion. Reason: {Reason}",
                    request.Id,
                    exceptionMessage);
            }
            else
            {
                result.StatusCode = ResultStatusCode.InternalServerError;
                result.Messages.Add($"An error occurred while deleting the setting: {exceptionMessage}");
                _logger.LogError("Error deleting setting: {Message}", exceptionMessage);
            }
        }

        return result;
    }
}
