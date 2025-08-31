using maERP.Application.Specifications.Base;
using maERP.Domain.Entities;

namespace maERP.Application.Specifications
{
    /// <summary>
    /// Specification for filtering customers
    /// </summary>
    public class CustomerFilterSpecification : FilterSpecification<Customer>
    {
        public CustomerFilterSpecification(string searchString)
        {
            // Includes.Add(c => c.OrderItems);

            if (!string.IsNullOrEmpty(searchString))
            {
                var lowerSearchString = searchString.ToLower();
                Criteria = c => (c.CompanyName.ToLower().Contains(lowerSearchString) ||
                                c.Firstname.ToLower().Contains(lowerSearchString) ||
                                c.Lastname.ToLower().Contains(lowerSearchString));
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
