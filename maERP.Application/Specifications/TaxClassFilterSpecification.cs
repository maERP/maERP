using System.Globalization;
using maERP.Application.Specifications.Base;
using maERP.Domain.Entities;

namespace maERP.Application.Specifications
{
    /// <summary>
    /// Specification for filtering orders
    /// </summary>
    public class TaxClassFilterSpecification : FilterSpecification<TaxClass>
    {
        public TaxClassFilterSpecification(string searchString)
        {
            if (!string.IsNullOrEmpty(searchString))
            {
                Criteria = t => (t.TaxRate.ToString(CultureInfo.InvariantCulture).Contains(searchString));
            }
            else
            {
                Criteria = p => true;               
            }
        }

        public TaxClassFilterSpecification(int id)
        {
            Criteria = o => o.Id == id;
        }
    }
}
