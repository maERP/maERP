using maERP.Application.Specifications.Base;
using maERP.Domain.Entities;

namespace maERP.Application.Specifications
{
    /// <summary>
    /// Specification for filtering warehouses
    /// </summary>
    public class WarehouseFilterSpecification : FilterSpecification<Warehouse>
    {
        public WarehouseFilterSpecification(string searchString)
        {
            if (!string.IsNullOrWhiteSpace(searchString))
            {
                var normalizedSearch = searchString.Trim().ToUpperInvariant();

                Criteria = w => w.Name != null && w.Name.ToUpper().Contains(normalizedSearch);
            }
            else
            {
                Criteria = p => true;
            }
        }

        public WarehouseFilterSpecification(Guid id)
        {
            Criteria = o => o.Id == id;
        }
    }
}
