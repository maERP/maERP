using maERP.Application.Specifications.Base;
using maERP.Domain.Entities;

namespace maERP.Application.Specifications
{
    /// <summary>
    /// Specification for filtering orders
    /// </summary>
    public class OrderFilterSpecification : FilterSpecification<Order>
    {
        public OrderFilterSpecification(string searchString)
        {
            Includes.Add(c => c.OrderItems);

            if (!string.IsNullOrEmpty(searchString))
            {
                Criteria = o => (o.InvoiceAddressCompanyName.Contains(searchString) || o.InvoiceAddressFirstName.Contains(searchString) || o.InvoiceAddressLastName.Contains(searchString));
            }
            else
            {
                Criteria = p => true;
            }
        }

        public OrderFilterSpecification(Guid id)
        {
            Includes.Add(o => o.OrderItems);
            Criteria = o => o.Id == id;
        }
    }
}
