using System.Linq.Dynamic.Core;
using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Application.Extensions;
using maERP.Domain.Dtos.Sales;
using maERP.Domain.Enums;
using maERP.Domain.Wrapper;
using maERP.Application.Mediator;

namespace maERP.Application.Features.Sales.Queries.SalesReadyForDeliveryList;

public class SalesReadyForDeliveryListHandler : IRequestHandler<SalesReadyForDeliveryListQuery, PaginatedResult<SalesListDto>>
{
    private readonly IAppLogger<SalesReadyForDeliveryListHandler> _logger;
    private readonly ISalesRepository _salesRepository;

    public SalesReadyForDeliveryListHandler(
        IAppLogger<SalesReadyForDeliveryListHandler> logger,
        ISalesRepository salesRepository)
    {
        _logger = logger;
        _salesRepository = salesRepository;
    }

    public async Task<PaginatedResult<SalesListDto>> Handle(SalesReadyForDeliveryListQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Handle SalesReadyForDeliveryListQuery: {0}", request);

        if (request.SalesBy.Any() != true)
        {
            return await _salesRepository.Entities
               .Where(o => o.Status == SalesStatus.ReadyForDelivery && o.PaymentStatus == PaymentStatus.CompletelyPaid)
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
            .Where(o => o.Status == SalesStatus.ReadyForDelivery && o.PaymentStatus == PaymentStatus.CompletelyPaid)
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