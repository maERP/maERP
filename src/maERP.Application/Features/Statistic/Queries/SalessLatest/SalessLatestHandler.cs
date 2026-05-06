using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Domain.Dtos.Statistic;
using maERP.Domain.Wrapper;
using maERP.Application.Mediator;
using Microsoft.EntityFrameworkCore;

namespace maERP.Application.Features.Statistic.Queries.SalessLatest;

public class SalessLatestHandler : IRequestHandler<SalessLatestQuery, Result<SalessLatestDto>>
{
    private readonly IAppLogger<SalessLatestHandler> _logger;
    private readonly ISalesRepository _salesRepository;

    public SalessLatestHandler(
        IAppLogger<SalessLatestHandler> logger,
        ISalesRepository salesRepository)
    {
        _logger = logger;
        _salesRepository = salesRepository;
    }

    public async Task<Result<SalessLatestDto>> Handle(SalessLatestQuery request, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Handle SalessLatestQuery - fetching {Count} latest saless", request.Count);

            var baseQuery = _salesRepository.Entities.AsQueryable();
            if (request.SalesChannelId.HasValue)
                baseQuery = baseQuery.Where(o => o.SalesChannelId == request.SalesChannelId.Value);

            var saless = await baseQuery
                .Include(o => o.Customer)
                .OrderByDescending(o => o.DateSalesed)
                .Take(request.Count)
                .Select(o => new SalessLatestItemDto
                {
                    Id = o.Id,
                    SalesNumber = $"ORD-{o.DateSalesed.Year}-{o.SalesId:D6}",
                    CustomerName = o.Customer != null
                        ? $"{o.Customer.Firstname} {o.Customer.Lastname}".Trim()
                        : $"{o.DeliveryAddressFirstName} {o.DeliveryAddressLastName}".Trim(),
                    Amount = o.Total,
                    Status = o.Status,
                    SalesDate = o.DateSalesed
                })
                .ToListAsync(cancellationToken);

            var dto = new SalessLatestDto
            {
                Saless = saless
            };

            _logger.LogInformation("Successfully fetched {Count} latest saless", saless.Count);
            return Result<SalessLatestDto>.Success(dto);
        }
        catch (Exception ex)
        {
            _logger.LogError("Error while fetching latest saless: {0}", ex.Message);
            return Result<SalessLatestDto>.Fail(ResultStatusCode.InternalServerError, "Error while fetching latest saless");
        }
    }
}
