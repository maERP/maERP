using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Domain.Dtos.Setting;
using maERP.Domain.Wrapper;
using MediatR;

namespace maERP.Application.Features.Setting.Queries.SettingDetail;

/// <summary>
/// Handler for processing setting detail queries.
/// Implements IRequestHandler from MediatR to handle SettingDetailQuery requests
/// and return detailed setting information wrapped in a Result.
/// </summary>
public class SettingDetailHandler : IRequestHandler<SettingDetailQuery, Result<SettingDetailDto>>
{
    /// <summary>
    /// Logger for recording handler operations
    /// </summary>
    private readonly IAppLogger<SettingDetailHandler> _logger;

    /// <summary>
    /// Repository for setting data operations
    /// </summary>
    private readonly ISettingRepository _settingRepository;

    /// <summary>
    /// Constructor that initializes the handler with required dependencies
    /// </summary>
    /// <param name="logger">Logger for recording operations</param>
    /// <param name="settingRepository">Repository for setting data access</param>
    public SettingDetailHandler(
        IAppLogger<SettingDetailHandler> logger,
        ISettingRepository settingRepository)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _settingRepository = settingRepository ?? throw new ArgumentNullException(nameof(settingRepository));
    }

    /// <summary>
    /// Handles the setting detail query request
    /// </summary>
    /// <param name="request">The query containing the setting ID</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Result containing detailed setting information if successful</returns>
    public async Task<Result<SettingDetailDto>> Handle(SettingDetailQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Retrieving setting details for ID: {Id}", request.Id);

        var result = new Result<SettingDetailDto>();

        try
        {
            // Retrieve setting with all related details from the repository
            var setting = await _settingRepository.GetByIdAsync(request.Id, true);

            // If setting not found, return a not found result
            if (setting == null)
            {
                result.Succeeded = false;
                result.StatusCode = ResultStatusCode.NotFound;
                result.Messages.Add($"Setting with ID {request.Id} not found");

                _logger.LogWarning("Setting with ID {Id} not found", request.Id);
                return result;
            }

            // Map entity to DTO using the mapping method
            var data = MapToDetailDto(setting);

            // Set successful result with the setting details
            result.Succeeded = true;
            result.StatusCode = ResultStatusCode.Ok;
            result.Data = data;

            _logger.LogInformation("Setting with ID {Id} retrieved successfully", request.Id);
        }
        catch (Exception ex)
        {
            // Handle any exceptions during setting retrieval
            result.Succeeded = false;
            result.StatusCode = ResultStatusCode.InternalServerError;
            result.Messages.Add($"An error occurred while retrieving the setting: {ex.Message}");

            _logger.LogError("Error retrieving setting: {Message}", ex.Message);
        }

        return result;
    }

    /// <summary>
    /// Maps a setting entity to a detail DTO
    /// </summary>
    /// <param name="entity">The setting entity to map</param>
    /// <returns>A setting detail DTO with properties from the entity</returns>
    private SettingDetailDto MapToDetailDto(Domain.Entities.Setting entity)
    {
        return new SettingDetailDto()
        {
            Id = entity.Id,
            Key = entity.Key,
            Value = entity.Value
        };
    }
}