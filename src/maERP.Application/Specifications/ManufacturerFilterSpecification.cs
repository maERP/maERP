using maERP.Application.Specifications.Base;
using maERP.Domain.Entities;

namespace maERP.Application.Specifications
{
    /// <summary>
    /// Specification for filtering manufacturers
    /// </summary>
    public class ManufacturerFilterSpecification : FilterSpecification<Manufacturer>
    {
        public ManufacturerFilterSpecification(string searchString)
        {
            if (!string.IsNullOrEmpty(searchString))
            {
                var lowerSearchString = searchString.ToLower();
                Criteria = m => (m.Name.ToLower().Contains(lowerSearchString) ||
                               m.City.ToLower().Contains(lowerSearchString) ||
                               m.Country.ToLower().Contains(lowerSearchString));
            }
            else
            {
                Criteria = m => true;
            }
        }

        public ManufacturerFilterSpecification(Guid id)
        {
            Criteria = m => m.Id == id;
        }
    }
}