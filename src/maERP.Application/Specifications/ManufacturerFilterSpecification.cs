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
                Criteria = m => (m.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase) ||
                               m.City.Contains(searchString, StringComparison.OrdinalIgnoreCase) ||
                               m.Country.Contains(searchString, StringComparison.OrdinalIgnoreCase));
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