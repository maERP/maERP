using maERP.Application.Specifications.Base;
using maERP.Domain.Entities;

namespace maERP.Application.Specifications
{
    /// <summary>
    /// Specification for filtering ai models
    /// </summary>
    public class AiModelFilterSpecification : FilterSpecification<AiModel>
    {
        public AiModelFilterSpecification(string searchString)
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

        public AiModelFilterSpecification(int id)
        {
            Criteria = o => o.Id == id;
        }
    }
}
