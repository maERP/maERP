using AutoMapper;
using maERP.Application.Features.Product.Commands.ProductCreate;
using maERP.Application.Features.Product.Commands.ProductDelete;
using maERP.Application.Features.Product.Commands.ProductUpdate;
using maERP.Application.Features.Product.Queries.ProductDetail;
using maERP.Application.Features.Product.Queries.ProductList;
using maERP.Domain.Dtos.Product;
using maERP.Domain.Entities;

namespace maERP.Application.MappingProfiles;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<Product, ProductDetailDto>();
        CreateMap<Product, ProductListDto>();

        CreateMap<ProductCreateCommand, Product>();
        CreateMap<ProductDeleteCommand, Product>();
        CreateMap<ProductUpdateCommand, Product>();
    }
}