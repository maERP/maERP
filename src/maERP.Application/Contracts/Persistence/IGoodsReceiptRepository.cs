using maERP.Domain.Entities;

namespace maERP.Application.Contracts.Persistence;

public interface IGoodsReceiptRepository : IGenericRepository<GoodsReceipt>
{
    Task<GoodsReceipt?> GetByIdWithDetailsAsync(int id);
    Task<ProductStock?> GetProductStockAsync(int productId, int warehouseId);
    Task UpdateProductStockAsync(ProductStock productStock);
    Task CreateProductStockAsync(ProductStock productStock);
}