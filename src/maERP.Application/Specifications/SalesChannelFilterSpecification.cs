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
            Includes.Add(s => s.Warehouses);

            if (!string.IsNullOrEmpty(searchString))
            {
                var lowerSearchString = searchString.ToLower();
                Criteria = s => (s.Name.ToLower().Contains(lowerSearchString));
            }
            else
            {
                Criteria = p => true;
            }
        }

        public SalesChannelFilterSpecification(Guid id)
        {
            Includes.Add(s => s.Warehouses);
            Criteria = o => o.Id == id;
        }
    }
}