using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Domain.Dtos.SalesChannel;
using maERP.Domain.Dtos.Warehouse;
using maERP.Domain.Wrapper;
using MediatR;

namespace maERP.Application.Features.SalesChannel.Queries.SalesChannelDetail;

/// <summary>
/// Handler for processing sales channel detail queries.
/// Implements IRequestHandler from MediatR to handle SalesChannelDetailQuery requests
/// and return detailed sales channel information wrapped in a Result.
/// </summary>
public class SalesChannelDetailHandler : IRequestHandler<SalesChannelDetailQuery, Result<SalesChannelDetailDto>>
{
    /// <summary>
    /// Logger for recording handler operations
    /// </summary>
    private readonly IAppLogger<SalesChannelDetailHandler> _logger;

    /// <summary>
    /// Repository for sales channel data operations
    /// </summary>
    private readonly ISalesChannelRepository _salesChannelRepository;

    /// <summary>
    /// Constructor that initializes the handler with required dependencies
    /// </summary>
    /// <param name="logger">Logger for recording operations</param>
    /// <param name="salesChannelRepository">Repository for sales channel data access</param>
    public SalesChannelDetailHandler(
        IAppLogger<SalesChannelDetailHandler> logger,
        ISalesChannelRepository salesChannelRepository)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _salesChannelRepository = salesChannelRepository ?? throw new ArgumentNullException(nameof(salesChannelRepository));
    }

    /// <summary>
    /// Handles the sales channel detail query request
    /// </summary>
    /// <param name="request">The query containing the sales channel ID</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Result containing detailed sales channel information if successful</returns>
    public async Task<Result<SalesChannelDetailDto>> Handle(SalesChannelDetailQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Retrieving sales channel details for ID: {Id}", request.Id);

        var result = new Result<SalesChannelDetailDto>();

        try
        {
            // Retrieve sales channel with all related details from the repository
            var salesChannel = await _salesChannelRepository.GetByIdAsync(request.Id, true);

            // If sales channel not found, return a not found result
            if (salesChannel == null)
            {
                result.Succeeded = false;
                result.StatusCode = ResultStatusCode.NotFound;
                result.Messages.Add($"Sales channel with ID {request.Id} not found");

                _logger.LogWarning("Sales channel with ID {Id} not found", request.Id);
                return result;
            }

            // Map entity to DTO using the mapping method
            var data = MapToDetailDto(salesChannel);

            // Set successful result with the sales channel details
            result.Succeeded = true;
            result.StatusCode = ResultStatusCode.Ok;
            result.Data = data;

            _logger.LogInformation("Sales channel with ID {Id} retrieved successfully", request.Id);
        }
        catch (Exception ex)
        {
            // Handle any exceptions during sales channel retrieval
            result.Succeeded = false;
            result.StatusCode = ResultStatusCode.InternalServerError;
            result.Messages.Add($"An error occurred while retrieving the sales channel: {ex.Message}");

            _logger.LogError("Error retrieving sales channel: {Message}", ex.Message);
        }

        return result;
    }

    /// <summary>
    /// Maps a sales channel entity to a detail DTO
    /// </summary>
    /// <param name="entity">The sales channel entity to map</param>
    /// <returns>A sales channel detail DTO with properties from the entity</returns>
    private SalesChannelDetailDto MapToDetailDto(Domain.Entities.SalesChannel entity)
    {
        return new SalesChannelDetailDto
        {
            Id = entity.Id,
            SalesChannelType = entity.Type,
            Name = entity.Name,
            Url = entity.Url,
            Username = entity.Username,
            Password = entity.Password,
            ImportProducts = entity.ImportProducts,
            ImportCustomers = entity.ImportCustomers,
            ImportOrders = entity.ImportOrders,
            ExportProducts = entity.ExportProducts,
            ExportCustomers = entity.ExportCustomers,
            ExportOrders = entity.ExportOrders,
            Warehouses = entity.Warehouses?.Select(w => new WarehouseDetailDto
            {
                Id = w.Id,
                Name = w.Name
            }).ToList() ?? new List<WarehouseDetailDto>()
        };
    }
}