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
                Criteria = m => (m.Name.Contains(searchString) || 
                               m.City.Contains(searchString) || 
                               m.Country.Contains(searchString));
            }
            else
            {
                Criteria = m => true;
            }
        }

        public ManufacturerFilterSpecification(int id)
        {
            Criteria = m => m.Id == id;
        }
    }
}