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
            // Get the entity with its relationships for proper cleanup
            var salesChannel = await _salesChannelRepository.GetDetails(request.Id);

            // Clear many-to-many relationships before deletion
            if (salesChannel.Warehouses != null && salesChannel.Warehouses.Any())
            {
                salesChannel.Warehouses.Clear();
                await _salesChannelRepository.UpdateAsync(salesChannel);
            }

            // Delete the existing entity
            await _salesChannelRepository.DeleteAsync(salesChannel);

            result.Succeeded = true;
            result.StatusCode = ResultStatusCode.NoContent;
            result.Data = salesChannel.Id;

            _logger.LogInformation("Successfully deleted sales channel with ID: {Id}", salesChannel.Id);
        }
        catch (maERP.Application.Exceptions.NotFoundException)
        {
            // Sales channel not found
            result.Succeeded = false;
            result.StatusCode = ResultStatusCode.NotFound;
            result.Messages.Add($"SalesChannel with ID {request.Id} not found");
            _logger.LogWarning("Sales channel {Id} not found", request.Id);
        }
        catch (Exception ex) when (ex.Message.Contains("does not exist") || ex.Message.Contains("not found"))
        {
            // Handle race condition: Entity was deleted between check and delete
            result.Succeeded = false;
            result.StatusCode = ResultStatusCode.NotFound;
            result.Messages.Add($"SalesChannel with ID {request.Id} not found");
            _logger.LogWarning("Sales channel {Id} was deleted by concurrent operation: {ExceptionType} - {Message}", request.Id, ex.GetType().Name, ex.Message);
        }
        catch (Exception ex)
        {
            result.Succeeded = false;
            result.StatusCode = ResultStatusCode.InternalServerError;
            result.Messages.Add($"An error occurred while deleting the sales channel: {ex.Message}");

            _logger.LogError("Error deleting sales channel: {ExceptionType} - {Message}", ex.GetType().Name, ex.Message);
        }

        return result;
    }
}
