using maERP.Application.Specifications.Base;
using maERP.Domain.Entities;

namespace maERP.Application.Specifications;

/// <summary>
/// Specification for filtering saless by customer
/// </summary>
public class SalesCustomerFilterSpecification : FilterSpecification<Sales>
{
    public SalesCustomerFilterSpecification(int customerId, string searchString)
    {
        Includes.Add(c => c.SalesItems);

        if (!string.IsNullOrEmpty(searchString))
        {
            Criteria = o => o.CustomerId == customerId && (o.InvoiceAddressCompanyName.Contains(searchString) ||
                                                        o.InvoiceAddressFirstName.Contains(searchString) ||
                                                        o.InvoiceAddressLastName.Contains(searchString) ||
                                                        o.Status.ToString().Contains(searchString) ||
                                                        o.PaymentStatus.ToString().Contains(searchString) ||
                                                        o.SalesId.ToString().Contains(searchString));
        }
        else
        {
            Criteria = o => o.CustomerId == customerId;
        }
    }
}