using AutoMapper;
using maERP.Application.Features.Product.Commands.ProductCreate;
using maERP.Application.Features.Product.Commands.ProductDelete;
using maERP.Application.Features.Product.Commands.ProductUpdate;
using maERP.Application.Features.Product.Queries.ProductDetail;
using maERP.Application.Features.Product.Queries.ProductList;
using maERP.Domain.Models;

namespace maERP.Application.MappingProfiles;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<Product, ProductCreateResponse>();
        CreateMap<Product, ProductDetailResponse>();
        CreateMap<Product, ProductListResponse>();
        CreateMap<Product, ProductUpdateResponse>();

        CreateMap<ProductCreateCommand, Product>();
        CreateMap<ProductDeleteCommand, Product>();
        CreateMap<ProductUpdateCommand, Product>();
    }
}