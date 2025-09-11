using maERP.Application.Specifications.Base;
using maERP.Domain.Entities;

namespace maERP.Application.Specifications
{
    /// <summary>
    /// Specification for filtering tax classes
    /// </summary>
    public class TaxClassFilterSpecification : FilterSpecification<TaxClass>
    {
        public TaxClassFilterSpecification(string searchString)
        {
            if (!string.IsNullOrEmpty(searchString))
            {
                // ReSharper disable once SpecifyACultureInStringConversionExplicitly
                Criteria = t => (t.TaxRate.ToString().Contains(searchString));
            }
            else
            {
                Criteria = p => true;
            }
        }

        public TaxClassFilterSpecification(Guid id)
        {
            Criteria = o => o.Id == id;
        }
    }
}
