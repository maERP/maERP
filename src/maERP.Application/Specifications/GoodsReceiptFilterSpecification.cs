using maERP.Application.Specifications.Base;
using maERP.Domain.Entities;

namespace maERP.Application.Specifications;

public class GoodsReceiptFilterSpecification : FilterSpecification<GoodsReceipt>
{
    public GoodsReceiptFilterSpecification(string searchString)
    {
        Includes.Add(gr => gr.Product!);
        Includes.Add(gr => gr.Warehouse!);

        if (!string.IsNullOrEmpty(searchString))
        {
            Criteria = gr => (gr.Product!.Name.Contains(searchString) ||
                             gr.Product!.Sku.Contains(searchString) ||
                             gr.Warehouse!.Name.Contains(searchString) ||
                             gr.Supplier.Contains(searchString) ||
                             gr.CreatedBy.Contains(searchString));
        }
        else
        {
            Criteria = gr => true;
        }
    }

    public GoodsReceiptFilterSpecification(Guid id)
    {
        Includes.Add(gr => gr.Product!);
        Includes.Add(gr => gr.Warehouse!);
        Criteria = gr => gr.Id == id;
    }
}