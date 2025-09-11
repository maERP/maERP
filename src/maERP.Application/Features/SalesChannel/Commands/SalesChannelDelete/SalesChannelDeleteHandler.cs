using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Domain.Wrapper;
using maERP.Application.Mediator;

namespace maERP.Application.Features.SalesChannel.Commands.SalesChannelDelete;

public class SalesChannelDeleteHandler : IRequestHandler<SalesChannelDeleteCommand, Result<Guid>>
{
    private readonly IAppLogger<SalesChannelDeleteHandler> _logger;
    private readonly ISalesChannelRepository _salesChannelRepository;


    public SalesChannelDeleteHandler(
        IAppLogger<SalesChannelDeleteHandler> logger,
        ISalesChannelRepository salesChannelRepository)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _salesChannelRepository = salesChannelRepository ?? throw new ArgumentNullException(nameof(salesChannelRepository));
    }

    public async Task<Result<Guid>> Handle(SalesChannelDeleteCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting sales channel with ID: {Id}", request.Id);

        var result = new Result<Guid>();

        // Validate incoming data
        var validator = new SalesChannelDeleteValidator(_salesChannelRepository);
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            result.Succeeded = false;
            result.StatusCode = ResultStatusCode.BadRequest;
            result.Messages.AddRange(validationResult.Errors.Select(e => e.ErrorMessage));

            _logger.LogWarning("Validation errors in delete request for {0}: {1}",
                nameof(SalesChannelDeleteCommand),
                string.Join(", ", result.Messages));

            return result;
        }

        try
        {
            // Create entity to delete
            var salesChannelToDelete = new Domain.Entities.SalesChannel
            {
                Id = request.Id
            };

            // Delete from database
            await _salesChannelRepository.DeleteAsync(salesChannelToDelete);

            result.Succeeded = true;
            result.StatusCode = ResultStatusCode.Ok;
            result.Data = salesChannelToDelete.Id;

            _logger.LogInformation("Successfully deleted sales channel with ID: {Id}", salesChannelToDelete.Id);
        }
        catch (Exception ex)
        {
            result.Succeeded = false;
            result.StatusCode = ResultStatusCode.InternalServerError;
            result.Messages.Add($"An error occurred while deleting the sales channel: {ex.Message}");

            _logger.LogError("Error deleting sales channel: {Message}", ex.Message);
        }

        return result;
    }
}
