using AutoMapper;
using Blazored.LocalStorage;
using maERP.Domain.Wrapper;
using maERP.SharedUI.Contracts;
using maERP.SharedUI.Models.Product;
using maERP.SharedUI.Services.Base;

namespace maERP.SharedUI.Services;

public class ProductService : BaseHttpService, IProductService
{
    private readonly IMapper _mapper;

    public ProductService(IClient client, IMapper mapper, ILocalStorageService localStorageService) : base(client, localStorageService)
    {
        _mapper = mapper;
    }

    public async Task<PaginatedResult<ProductListVm>> GetProducts(int pageNumber = 1, int pageSize = 10, string searchString = "", string orderBy = "")
    {
        await AddBearerToken();
        var products = await Client.ProductsGETAsync(pageNumber, pageSize, searchString, orderBy);
        return _mapper.Map<PaginatedResult<ProductListVm>>(products);
    }

    public async Task<ProductVm> GetProductDetails(int id)
    {
        await AddBearerToken();
        var product = await Client.ProductsGET2Async(id);
        return _mapper.Map<ProductVm>(product);
    }

    public async Task<Response<Guid>> CreateProduct(ProductVm product)
    {
        try
        {
            await AddBearerToken();
            var productCreateCommand = _mapper.Map<ProductCreateCommand>(product);
            await Client.ProductsPOSTAsync(productCreateCommand);
            return new Response<Guid>
            {
                Success = true
            };
        }
        catch (ApiException ex)
        {
            return ConvertApiExceptions<Guid>(ex);
        }
    }

    public async Task<Response<Guid>> UpdateProduct(int id, ProductVm product)
    {
        try
        {
            await AddBearerToken();
            var productUpdateCommand = _mapper.Map<ProductUpdateCommand>(product);
            await Client.ProductsPUTAsync(id, productUpdateCommand);
            return new Response<Guid>
            {
                Success = true
            };
        }
        catch (ApiException ex)
        {
            return ConvertApiExceptions<Guid>(ex);
        }
    }
    public async Task<Response<Guid>> DeleteProduct(int id)
    {
        try
        {
            await AddBearerToken();
            await Client.ProductsDELETEAsync(id);
            return new Response<Guid>
            {
                Success = true
            };
        }
        catch (ApiException ex)
        {
            return ConvertApiExceptions<Guid>(ex);
        }
    }
}