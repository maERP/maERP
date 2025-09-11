using maERP.Application.Specifications.Base;
using maERP.Domain.Entities;

namespace maERP.Application.Specifications
{
    /// <summary>
    /// Specification for filtering ai prompts
    /// </summary>
    public class AiPromptFilterSpecification : FilterSpecification<AiPrompt>
    {
        public AiPromptFilterSpecification(string searchString)
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

        public AiPromptFilterSpecification(Guid id)
        {
            Criteria = o => o.Id == id;
        }
    }
}
