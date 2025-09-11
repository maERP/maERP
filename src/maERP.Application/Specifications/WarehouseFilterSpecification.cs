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
            if (!string.IsNullOrEmpty(searchString))
            {
                Criteria = w => (w.Name.Contains(searchString));
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
