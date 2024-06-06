using maERP.Application.Specifications.Base;
using maERP.Domain.Models;

namespace maERP.Application.Specifications
{
    /// <summary>
    /// Specification for filtering orders
    /// </summary>
    public class OrderFilterSpecification : FilterSpecification<Order>
    {
        public OrderFilterSpecification(string searchString)
        {
            // Includes.Add(p => p.Likes);

            /*
            if (!string.IsNullOrEmpty(searchString))
            {
                Criteria = p => (p.Title.Contains(searchString) || p.Slug.Contains(searchString) || p.Summary.Contains(searchString));
            }
            else
            {
                if (categoryId.HasValue)
                {
                    Criteria = p => p.Categories.Select(cat => cat.Id).Contains(categoryId.Value);
                }
                else
                {
                    Criteria = p => statuses.Contains(p.Status);
                }
            }
            */
        }

        public OrderFilterSpecification(int id)
        {
            //Includes.Add(p => p.Categories);
            // Includes.Add(p => p.PostTags);
            Criteria = p => p.Id == id;
        }
    }
}
