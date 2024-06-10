using AutoMapper;
using Blazored.LocalStorage;
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

    public async Task<List<ProductVM>> GetProducts()
    {
        await AddBearerToken();
        var products = await _client.ProductsAllAsync();
        return _mapper.Map<List<ProductVM>>(products);
    }

    public async Task<ProductVM> GetProductDetails(int id)
    {
        await AddBearerToken();
        var product = await _client.ProductsGETAsync(id);
        return _mapper.Map<ProductVM>(product);
    }

    public async Task<Response<Guid>> CreateProduct(ProductVM product)
    {
        try
        {
            await AddBearerToken();
            var productCreateCommand = _mapper.Map<ProductCreateCommand>(product);
            await _client.ProductsPOSTAsync(productCreateCommand);
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

    public async Task<Response<Guid>> UpdateProduct(int id, ProductVM product)
    {
        try
        {
            await AddBearerToken();
            var productUpdateCommand = _mapper.Map<ProductUpdateCommand>(product);
            await _client.ProductsPUTAsync(id, productUpdateCommand);
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
            await _client.ProductsDELETEAsync(id);
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