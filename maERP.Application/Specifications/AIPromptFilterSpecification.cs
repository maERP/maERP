using maERP.Application.Specifications.Base;
using maERP.Domain.Entities;

namespace maERP.Application.Specifications
{
    /// <summary>
    /// Specification for filtering orders
    /// </summary>
    public class AIPromptFilterSpecification : FilterSpecification<AIPrompt>
    {
        public AIPromptFilterSpecification(string searchString)
        {
            if (!string.IsNullOrEmpty(searchString))
            {
                Criteria = w => (w.Identifier.Contains(searchString));
            }
            else
            {
                Criteria = p => true;               
            }
        }

        public AIPromptFilterSpecification(int id)
        {
            Criteria = o => o.Id == id;
        }
    }
}
