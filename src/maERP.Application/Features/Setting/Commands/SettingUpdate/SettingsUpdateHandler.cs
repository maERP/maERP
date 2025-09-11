using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Domain.Wrapper;
using maERP.Application.Mediator;

namespace maERP.Application.Features.Setting.Commands.SettingUpdate;

public class SettingUpdateQuery : IRequestHandler<SettingUpdateCommand, Result<Guid>>
{
    private readonly IAppLogger<SettingUpdateQuery> _logger;
    private readonly ISettingRepository _settingRepository;


    public SettingUpdateQuery(
        IAppLogger<SettingUpdateQuery> logger,
        ISettingRepository settingRepository)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _settingRepository = settingRepository ?? throw new ArgumentNullException(nameof(settingRepository));
    }

    public async Task<Result<Guid>> Handle(SettingUpdateCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating setting with ID: {Id} and name: {Name}", request.Id, request.Key);

        var result = new Result<Guid>();

        // Validate incoming data
        var validator = new SettingUpdateValidator(_settingRepository);
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            result.Succeeded = false;
            result.StatusCode = ResultStatusCode.BadRequest;
            result.Messages.AddRange(validationResult.Errors.Select(e => e.ErrorMessage));

            _logger.LogWarning("Validation errors in update request for {0}: {1}",
                nameof(SettingUpdateCommand),
                string.Join(", ", result.Messages));

            return result;
        }

        try
        {
            // Map to domain entity
            var settingToUpdate = MapToEntity(request);

            // Update in database
            await _settingRepository.UpdateAsync(settingToUpdate);

            result.Succeeded = true;
            result.StatusCode = ResultStatusCode.Ok;
            result.Data = settingToUpdate.Id;

            _logger.LogInformation("Successfully updated setting with ID: {Id}", settingToUpdate.Id);
        }
        catch (Exception ex)
        {
            result.Succeeded = false;
            result.StatusCode = ResultStatusCode.InternalServerError;
            result.Messages.Add($"An error occurred while updating the setting: {ex.Message}");

            _logger.LogError("Error updating setting: {Message}", ex.Message);
        }

        return result;
    }

    private Domain.Entities.Setting MapToEntity(SettingUpdateCommand command)
    {
        return new Domain.Entities.Setting
        {
            Id = command.Id,
            Key = command.Key,
            Value = command.Value
        };
    }
}
