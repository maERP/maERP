using maERP.Application.Specifications.Base;
using maERP.Domain.Entities;

namespace maERP.Application.Specifications;

/// <summary>
/// Specification for filtering orders by customer
/// </summary>
public class OrderCustomerFilterSpecification : FilterSpecification<Order>
{
    public OrderCustomerFilterSpecification(int customerId, string searchString)
    {
        Includes.Add(c => c.OrderItems);

        if (!string.IsNullOrEmpty(searchString))
        {
            Criteria = o => o.CustomerId == customerId && (o.InvoiceAddressCompanyName.Contains(searchString) || 
                                                        o.InvoiceAddressFirstName.Contains(searchString) || 
                                                        o.InvoiceAddressLastName.Contains(searchString));
        }
        else
        {
            Criteria = o => o.CustomerId == customerId;               
        }
    }
} 