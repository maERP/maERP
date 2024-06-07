using AutoMapper;
using maERP.Application.Features.Product.Commands.CreateProduct;
using maERP.Application.Features.Product.Commands.DeleteProduct;
using maERP.Application.Features.Product.Commands.UpdateProduct;
using maERP.Application.Features.Product.Queries.GetProductDetail;
using maERP.Application.Features.Product.Queries.GetProducts;
using maERP.Domain.Models;

namespace maERP.Application.MappingProfiles;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<Product, CreateProductResponse>();
        CreateMap<Product, GetProductDetailResponse>();
        CreateMap<Product, GetProductsResponse>();
        CreateMap<Product, UpdateProductResponse>();

        CreateMap<CreateProductCommand, Product>();
        CreateMap<DeleteProductCommand, Product>();
        CreateMap<UpdateProductCommand, Product>();
    }
}