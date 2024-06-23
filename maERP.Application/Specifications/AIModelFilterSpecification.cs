using maERP.Application.Specifications.Base;
using maERP.Domain.Entities;

namespace maERP.Application.Specifications
{
    /// <summary>
    /// Specification for filtering orders
    /// </summary>
    public class AIModelFilterSpecification : FilterSpecification<AIModel>
    {
        public AIModelFilterSpecification(string searchString)
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

        public AIModelFilterSpecification(int id)
        {
            Criteria = o => o.Id == id;
        }
    }
}
