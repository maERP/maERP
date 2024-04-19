using maERP.SharedUI.Models.Product;
using maERP.SharedUI.Services.Base;

namespace maERP.SharedUI.Contracts;

public interface IProductService
{
    Task<List<ProductVM>> GetProducts();
    Task<ProductVM> GetProductDetails(int id);
    Task<Response<Guid>> CreateProduct(ProductVM product);
    Task<Response<Guid>> UpdateProduct(int id, ProductVM product);
    Task<Response<Guid>> DeleteProduct(int id);
}
