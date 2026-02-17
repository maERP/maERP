using maERP.Application.Specifications.Base;
using maERP.Domain.Entities;

namespace maERP.Application.Specifications
{
    /// <summary>
    /// Specification for filtering products
    /// </summary>
    public class ProductFilterSpecification : FilterSpecification<Product>
    {
        public ProductFilterSpecification(string searchString)
        {
            Includes.Add(p => p.ProductStocks);
            // Includes.Add(p => p.ProductSalesChannels);

            if (!string.IsNullOrEmpty(searchString))
            {
                var lowerSearchString = searchString.ToLower();
                Criteria = p => (p.Sku.ToLower().Contains(lowerSearchString) || p.Name.ToLower().Contains(lowerSearchString));
            }
            else
            {
                Criteria = p => true;
            }
        }

        public ProductFilterSpecification(Guid id)
        {
            Includes.Add(p => p.ProductStocks);
            // Includes.Add(p => p.ProductSalesChannels):
            Criteria = o => o.Id == id;
        }
    }
}