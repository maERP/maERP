using maERP.Domain.Wrapper;
using maERP.SharedUI.Models.Product;
using maERP.SharedUI.Services.Base;

namespace maERP.SharedUI.Contracts;

public interface IProductService
{
    Task<PaginatedResult<ProductListVM>> GetProducts(int pageNumber = 1, int pageSize = 10, string searchString = "", string orderBy = "");
    Task<ProductVM> GetProductDetails(int id);
    Task<Response<Guid>> CreateProduct(ProductVM product);
    Task<Response<Guid>> UpdateProduct(int id, ProductVM product);
    Task<Response<Guid>> DeleteProduct(int id);
}