using maERP.Application.Specifications.Base;
using maERP.Domain.Entities;

namespace maERP.Application.Specifications
{
    /// <summary>
    /// Specification for filtering sales channels
    /// </summary>
    public class SalesChannelFilterSpecification : FilterSpecification<SalesChannel>
    {
        public SalesChannelFilterSpecification(string searchString)
        {
            // Includes.Add(c => c.OrderItems);

            if (!string.IsNullOrEmpty(searchString))
            {
                Criteria = s => (s.Name.Contains(searchString));
            }
            else
            {
                Criteria = p => true;
            }
        }

        public SalesChannelFilterSpecification(int id)
        {
            // Includes.Add(o => o.OrderItems);
            Criteria = o => o.Id == id;
        }
    }
}