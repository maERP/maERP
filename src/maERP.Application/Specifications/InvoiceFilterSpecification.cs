using maERP.Application.Specifications.Base;
using maERP.Domain.Entities;

namespace maERP.Application.Specifications
{
    /// <summary>
    /// Specification for filtering invoices
    /// </summary>
    public class InvoiceFilterSpecification : FilterSpecification<Invoice>
    {
        /// <summary>
        /// Constructor for filtering invoices by search string
        /// </summary>
        /// <param name="searchString">Search string to find matching invoices</param>
        public InvoiceFilterSpecification(string searchString)
        {
            Includes.Add(i => i.InvoiceItems);

            if (!string.IsNullOrEmpty(searchString))
            {
                Criteria = i => (i.InvoiceNumber.Contains(searchString) ||
                                i.InvoiceAddressCompanyName.Contains(searchString) ||
                                i.InvoiceAddressFirstName.Contains(searchString) ||
                                i.InvoiceAddressLastName.Contains(searchString));
            }
            else
            {
                Criteria = i => true;
            }
        }

        /// <summary>
        /// Constructor for finding a specific invoice by ID
        /// </summary>
        /// <param name="id">Invoice ID</param>
        public InvoiceFilterSpecification(int id)
        {
            Includes.Add(i => i.InvoiceItems);
            Criteria = i => i.Id == id;
        }
    }
}
