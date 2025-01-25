using maERP.Domain.Wrapper;
using maERP.SharedUI.Models.Product;
using maERP.SharedUI.Services.Base;

namespace maERP.SharedUI.Contracts;

public interface IProductService
{
    Task<PaginatedResult<ProductListVm>> GetProducts(int pageNumber = 1, int pageSize = 10, string searchString = "", string orderBy = "");
    Task<ProductVm> GetProductDetails(int id);
    Task<Response<Guid>> CreateProduct(ProductVm product);
    Task<Response<Guid>> UpdateProduct(int id, ProductVm product);
    Task<Response<Guid>> DeleteProduct(int id);
}