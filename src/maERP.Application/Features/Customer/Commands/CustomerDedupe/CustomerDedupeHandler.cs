using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Services;
using maERP.Application.Mediator;
using maERP.Domain.Dtos.Customer;
using maERP.Domain.Wrapper;

namespace maERP.Application.Features.Customer.Commands.CustomerDedupe;

public class CustomerDedupeHandler : IRequestHandler<CustomerDedupeCommand, Result<CustomerDedupeResultDto>>
{
    private readonly IAppLogger<CustomerDedupeHandler> _logger;
    private readonly ICustomerDedupeService _dedupeService;

    public CustomerDedupeHandler(
        IAppLogger<CustomerDedupeHandler> logger,
        ICustomerDedupeService dedupeService)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _dedupeService = dedupeService ?? throw new ArgumentNullException(nameof(dedupeService));
    }

    public async Task<Result<CustomerDedupeResultDto>> Handle(CustomerDedupeCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Customer dedupe started (dryRun={DryRun})", request.DryRun);

        try
        {
            var dto = await _dedupeService.RunAsync(request.DryRun, cancellationToken);

            _logger.LogInformation(
                "Customer dedupe finished (dryRun={DryRun}, tenants={Tenants}, groups={Groups}, customersToMerge={CustomersToMerge})",
                request.DryRun, dto.TenantsAffected, dto.GroupsFound, dto.CustomersToMerge);

            return Result<CustomerDedupeResultDto>.Success(dto);
        }
        catch (Exception ex)
        {
            _logger.LogError("Customer dedupe failed: {Message}", ex.Message);
            return Result<CustomerDedupeResultDto>.Fail(ResultStatusCode.InternalServerError,
                $"Customer dedupe failed: {ex.Message}");
        }
    }
}
