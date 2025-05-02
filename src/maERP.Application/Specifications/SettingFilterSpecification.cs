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
                Criteria = w => (w.Key.Contains(searchString));
            }
            else
            {
                Criteria = p => true;               
            }
        }

        public SettingFilterSpecification(int id)
        {
            Criteria = o => o.Id == id;
        }
    }
}
