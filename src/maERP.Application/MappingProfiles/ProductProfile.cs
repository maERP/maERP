using AutoMapper;
using maERP.Application.Features.Product.Commands.ProductCreate;
using maERP.Application.Features.Product.Commands.ProductDelete;
using maERP.Application.Features.Product.Commands.ProductUpdate;
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
        CreateMap<ProductInputCommand, Product>();
    }
}