using maERP.Application.Specifications.Base;
using maERP.Domain.Entities;

namespace maERP.Application.Specifications;

/// <summary>
/// Specification for filtering orders by customer
/// </summary>
public class OrderCustomerFilterSpecification : FilterSpecification<Order>
{
    public OrderCustomerFilterSpecification(Guid customerId, string searchString)
    {
        Includes.Add(c => c.OrderItems);

        if (!string.IsNullOrEmpty(searchString))
        {
            Criteria = o => o.CustomerId == customerId && (o.InvoiceAddressCompanyName.Contains(searchString) ||
                                                        o.InvoiceAddressFirstName.Contains(searchString) ||
                                                        o.InvoiceAddressLastName.Contains(searchString) ||
                                                        o.Status.ToString().Contains(searchString) ||
                                                        o.PaymentStatus.ToString().Contains(searchString));
        }
        else
        {
            Criteria = o => o.CustomerId == customerId;
        }
    }
}