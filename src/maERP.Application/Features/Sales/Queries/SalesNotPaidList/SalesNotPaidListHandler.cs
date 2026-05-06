using System.Linq.Dynamic.Core;
using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Application.Extensions;
using maERP.Domain.Dtos.Sales;
using maERP.Domain.Enums;
using maERP.Domain.Wrapper;
using maERP.Application.Mediator;

namespace maERP.Application.Features.Sales.Queries.SalesNotPaidList;

public class SalesNotPaidListHandler : IRequestHandler<SalesNotPaidListQuery, PaginatedResult<SalesListDto>>
{
    private readonly IAppLogger<SalesNotPaidListHandler> _logger;
    private readonly ISalesRepository _salesRepository;

    // Statische Listen für Entity Framework Expression Trees
    private static readonly List<PaymentStatus> NotPaidStatuses = new()
    {
        PaymentStatus.Unknown,
        PaymentStatus.Invoiced,
        PaymentStatus.PartiallyPaid,
        PaymentStatus.FirstReminder,
        PaymentStatus.SecondReminder,
        PaymentStatus.ThirdReminder,
        PaymentStatus.Encashment,
        PaymentStatus.Reserved,
        PaymentStatus.Delayed,
        PaymentStatus.ReviewNecessary,
        PaymentStatus.NoCreditApproved,
        PaymentStatus.CreditPreliminarilyAccepted
    };

    private static readonly List<SalesStatus> ShippableStatuses = new()
    {
        SalesStatus.Pending,
        SalesStatus.Processing,
        SalesStatus.ReadyForDelivery,
        SalesStatus.OnHold
    };

    public SalesNotPaidListHandler(
        IAppLogger<SalesNotPaidListHandler> logger,
        ISalesRepository salesRepository)
    {
        _logger = logger;
        _salesRepository = salesRepository;
    }

    public async Task<PaginatedResult<SalesListDto>> Handle(SalesNotPaidListQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Handle SalesNotPaidListQuery: {0}", request);

        if (request.SalesBy.Any() != true)
        {
            return await _salesRepository.Entities
               .Where(o => NotPaidStatuses.Contains(o.PaymentStatus) && ShippableStatuses.Contains(o.Status))
               .Select(o => new SalesListDto
               {
                   Id = o.Id,
                   SalesId = o.SalesId,
                   CustomerId = o.CustomerId,
                   InvoiceAddressFirstName = o.InvoiceAddressFirstName,
                   InvoiceAddressLastName = o.InvoiceAddressLastName,
                   Total = o.Total,
                   Status = o.Status,
                   PaymentStatus = o.PaymentStatus,
                   DateSalesed = o.DateSalesed
               })
               .ToPaginatedListAsync(request.PageNumber, request.PageSize);
        }

        var salesing = string.Join(",", request.SalesBy);

        return await _salesRepository.Entities
            .Where(o => NotPaidStatuses.Contains(o.PaymentStatus) && ShippableStatuses.Contains(o.Status))
            .OrderBy(salesing)
            .Select(o => new SalesListDto
            {
                Id = o.Id,
                SalesId = o.SalesId,
                CustomerId = o.CustomerId,
                InvoiceAddressFirstName = o.InvoiceAddressFirstName,
                InvoiceAddressLastName = o.InvoiceAddressLastName,
                Total = o.Total,
                Status = o.Status,
                PaymentStatus = o.PaymentStatus,
                DateSalesed = o.DateSalesed
            })
            .ToPaginatedListAsync(request.PageNumber, request.PageSize);
    }
}
