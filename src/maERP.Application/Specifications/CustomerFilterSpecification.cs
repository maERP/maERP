using maERP.Application.Specifications.Base;
using maERP.Domain.Entities;

namespace maERP.Application.Specifications
{
    /// <summary>
    /// Specification for filtering orders
    /// </summary>
    public class CustomerFilterSpecification : FilterSpecification<Customer>
    {
        public CustomerFilterSpecification(string searchString)
        {
            // Includes.Add(c => c.OrderItems);

            if (!string.IsNullOrEmpty(searchString))
            {
                Criteria = c => (c.CompanyName.Contains(searchString) || c.Firstname.Contains(searchString) || c.Lastname.Contains(searchString));
            }
            else
            {
                Criteria = p => true;               
            }
        }

        public CustomerFilterSpecification(int id)
        {
            // Includes.Add(o => o.OrderItems);
            Criteria = o => o.Id == id;
        }
    }
}
