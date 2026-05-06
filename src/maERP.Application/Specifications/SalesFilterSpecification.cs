using maERP.Application.Specifications.Base;
using maERP.Domain.Entities;

namespace maERP.Application.Specifications
{
    /// <summary>
    /// Specification for filtering saless
    /// </summary>
    public class SalesFilterSpecification : FilterSpecification<Sales>
    {
        public SalesFilterSpecification(string searchString)
        {
            Includes.Add(c => c.SalesItems);

            if (!string.IsNullOrEmpty(searchString))
            {
                Criteria = o => (o.InvoiceAddressCompanyName.Contains(searchString) ||
                                o.InvoiceAddressFirstName.Contains(searchString) ||
                                o.InvoiceAddressLastName.Contains(searchString) ||
                                o.PaymentStatus.ToString().Contains(searchString) ||
                                o.Status.ToString().Contains(searchString) ||
                                o.SalesId.ToString().Contains(searchString));
            }
            else
            {
                Criteria = p => true;
            }
        }

        public SalesFilterSpecification(Guid id)
        {
            Includes.Add(o => o.SalesItems);
            Criteria = o => o.Id == id;
        }
    }
}
