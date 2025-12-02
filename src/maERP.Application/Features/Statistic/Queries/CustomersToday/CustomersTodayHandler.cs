using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Domain.Dtos.Statistic;
using maERP.Domain.Wrapper;
using maERP.Application.Mediator;
using Microsoft.EntityFrameworkCore;

namespace maERP.Application.Features.Statistic.Queries.CustomersToday;

public class CustomersTodayHandler : IRequestHandler<CustomersTodayQuery, Result<CustomersTodayDto>>
{
    private readonly IAppLogger<CustomersTodayHandler> _logger;
    private readonly ICustomerRepository _customerRepository;

    public CustomersTodayHandler(
        IAppLogger<CustomersTodayHandler> logger,
        ICustomerRepository customerRepository)
    {
        _logger = logger;
        _customerRepository = customerRepository;
    }

    public async Task<Result<CustomersTodayDto>> Handle(CustomersTodayQuery request, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Handle CustomersTodayQuery");

            var dto = new CustomersTodayDto();

            var now = DateTime.UtcNow;
            var monthStart = new DateTime(now.Year, now.Month, 1, 0, 0, 0, DateTimeKind.Utc);
            var lastMonthStart = monthStart.AddMonths(-1);
            var lastMonthEnd = monthStart;

            // Customer calculations
            dto.CustomersTotal = await _customerRepository.Entities.CountAsync(cancellationToken);

            dto.CustomersNewThisMonth = await _customerRepository.Entities
                .Where(c => c.DateCreated >= monthStart)
                .CountAsync(cancellationToken);

            // Customers change compared to last month
            var customersLastMonth = await _customerRepository.Entities
                .Where(c => c.DateCreated >= lastMonthStart && c.DateCreated < lastMonthEnd)
                .CountAsync(cancellationToken);

            dto.CustomersChangePercent = customersLastMonth > 0
                ? ((decimal)(dto.CustomersNewThisMonth - customersLastMonth) / customersLastMonth) * 100
                : dto.CustomersNewThisMonth > 0 ? 100 : 0;

            return Result<CustomersTodayDto>.Success(dto);
        }
        catch (Exception ex)
        {
            _logger.LogError("Error while calculating customer statistics: {0}", ex.Message);
            return Result<CustomersTodayDto>.Fail(ResultStatusCode.InternalServerError, "Error while calculating customer statistics");
        }
    }
}
