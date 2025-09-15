using maERP.Application.Specifications.Base;
using maERP.Domain.Entities;

namespace maERP.Application.Specifications
{
    /// <summary>
    /// Specification for filtering settings
    /// </summary>
    public class SettingFilterSpecification : FilterSpecification<Setting>
    {
        public SettingFilterSpecification(string searchString)
        {
            if (!string.IsNullOrEmpty(searchString))
            {
                Criteria = w => (w.Key.ToLower().Contains(searchString.ToLower()) ||
                                w.Value.ToLower().Contains(searchString.ToLower()));
            }
            else
            {
                Criteria = p => true;
            }
        }

        public SettingFilterSpecification(Guid id)
        {
            Criteria = o => o.Id == id;
        }
    }
}
