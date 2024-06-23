using maERP.Application.Specifications.Base;
using maERP.Domain.Entities;

namespace maERP.Application.Specifications
{
    /// <summary>
    /// Specification for filtering orders
    /// </summary>
    public class ProductFilterSpecification : FilterSpecification<Product>
    {
        public ProductFilterSpecification(string searchString)
        {
            Includes.Add(p => p.ProductStocks);
            // Includes.Add(p => p.ProductSalesChannels);

            if (!string.IsNullOrEmpty(searchString))
            {
                Criteria = p => (p.Sku.Contains(searchString) || p.Name.Contains(searchString));
            }
            else
            {
                Criteria = p => true;               
            }
        }

        public ProductFilterSpecification(int id)
        {
            Includes.Add(p => p.ProductStocks);
            // Includes.Add(p => p.ProductSalesChannels):
            Criteria = o => o.Id == id;
        }
    }
}