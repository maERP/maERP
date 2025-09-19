using maERP.Domain.Entities;

namespace maERP.Application.Contracts.Persistence;

public interface IGoodsReceiptRepository : IGenericRepository<GoodsReceipt>
{
    Task<GoodsReceipt?> GetByIdWithDetailsAsync(Guid id);
    Task<ProductStock?> GetProductStockAsync(Guid productId, Guid warehouseId);
    Task UpdateProductStockAsync(ProductStock productStock);
    Task CreateProductStockAsync(ProductStock productStock);
}