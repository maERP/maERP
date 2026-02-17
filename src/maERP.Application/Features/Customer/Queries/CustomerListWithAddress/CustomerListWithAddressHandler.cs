using System.Linq.Dynamic.Core;
using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Application.Extensions;
using maERP.Application.Specifications;
using maERP.Domain.Dtos.Customer;
using maERP.Domain.Wrapper;
using maERP.Application.Mediator;
using Microsoft.EntityFrameworkCore;

namespace maERP.Application.Features.Customer.Queries.CustomerListWithAddress;

public class CustomerListWithAddressHandler : IRequestHandler<CustomerListWithAddressQuery, PaginatedResult<CustomerListWithAddressDto>>
{
    private readonly IAppLogger<CustomerListWithAddressHandler> _logger;
    private readonly ICustomerRepository _customerRepository;

    public CustomerListWithAddressHandler(
        IAppLogger<CustomerListWithAddressHandler> logger,
        ICustomerRepository customerRepository)
    {
        _logger = logger;
        _customerRepository = customerRepository;
    }

    public async Task<PaginatedResult<CustomerListWithAddressDto>> Handle(CustomerListWithAddressQuery request, CancellationToken cancellationToken)
    {
        var customerFilterSpec = new CustomerFilterSpecification(request.SearchString);

        _logger.LogInformation("CustomerListWithAddressHandler.Handle: Retrieving customers with address.");

        var baseQuery = _customerRepository.Entities
            .Specify(customerFilterSpec)
            .Include(c => c.CustomerAddresses);

        if (request.OrderBy.Any() != true)
        {
            return await baseQuery
                .Select(c => new CustomerListWithAddressDto
                {
                    Id = c.Id,
                    CustomerId = c.CustomerId,
                    Firstname = c.Firstname,
                    Lastname = c.Lastname,
                    Email = c.Email,
                    InvoiceAddress = c.CustomerAddresses != null
                        ? c.CustomerAddresses
                            .Where(a => a.DefaultInvoiceAddress)
                            .Select(a => a.Street + " " + a.HouseNr + ", " + a.Zip + " " + a.City)
                            .FirstOrDefault() ?? ""
                        : "",
                    DateEnrollment = c.DateEnrollment
                })
                .ToPaginatedListAsync(request.PageNumber, request.PageSize);
        }

        var ordering = string.Join(",", request.OrderBy);

        return await baseQuery
            .OrderBy(ordering)
            .Select(c => new CustomerListWithAddressDto
            {
                Id = c.Id,
                CustomerId = c.CustomerId,
                Firstname = c.Firstname,
                Lastname = c.Lastname,
                Email = c.Email,
                InvoiceAddress = c.CustomerAddresses != null
                    ? c.CustomerAddresses
                        .Where(a => a.DefaultInvoiceAddress)
                        .Select(a => a.Street + " " + a.HouseNr + ", " + a.Zip + " " + a.City)
                        .FirstOrDefault() ?? ""
                    : "",
                DateEnrollment = c.DateEnrollment
            })
            .ToPaginatedListAsync(request.PageNumber, request.PageSize);
    }
}
