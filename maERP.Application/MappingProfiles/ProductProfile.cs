using AutoMapper;
using maERP.Application.Features.Product.Commands.CreateProductCommand;
using maERP.Application.Features.Product.Commands.DeleteProductCommand;
using maERP.Application.Features.Product.Commands.UpdateProductCommand;
using maERP.Domain.Models;
using maERP.Application.Dtos.Product;

namespace maERP.Application.MappingProfiles;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<Product, ProductCreateDto>();
        CreateMap<Product, ProductDetailDto>();
        CreateMap<Product, ProductListDto>();
        CreateMap<Product, ProductUpdateDto>();

        CreateMap<CreateProductCommand, Product>();
        CreateMap<DeleteProductCommand, Product>();
        CreateMap<UpdateProductCommand, Product>();
    }
}