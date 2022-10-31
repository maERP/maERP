using maERP.Shared.Models;
using maERP.Shared.Dtos;
using maERP.Shared.Dtos.User;
using maERP.Shared.Dtos.Product;
using maERP.Shared.Dtos.ProductSalesChannel;
using maERP.Shared.Dtos.TaxClass;
using maERP.Shared.Dtos.SalesChannel;
using maERP.Shared.Dtos.Warehouse;
using AutoMapper;

namespace maERP.Server.Configurations
{
	public class AutoMapperConfig : Profile
	{
		public AutoMapperConfig()
		{
			CreateMap<ApiUserDto, ApiUser>().ReverseMap();
			// CreateMap<List<ApiUserDto>, List<ApiUser>>().ReverseMap();

			CreateMap<Product, ProductDto>().ReverseMap();
			CreateMap<Product, GetProductDto>().ReverseMap();
			CreateMap<Product, CreateProductDto>().ReverseMap();
			CreateMap<Product, GetProductDto>().ReverseMap();			
			CreateMap<Product, UpdateProductDto>().ReverseMap();

			CreateMap<ProductSalesChannel, ProductSalesChannelDto>().ReverseMap();
			CreateMap<ProductSalesChannel, GetProductSalesChannelDto>().ReverseMap();
			CreateMap<ProductSalesChannel, CreateProductSalesChannelDto>().ReverseMap();
			CreateMap<ProductSalesChannel, GetProductSalesChannelDto>().ReverseMap();
			CreateMap<ProductSalesChannel, UpdateProductSalesChannelDto>().ReverseMap();

			// SalesChannel does not have a basic SalesChannelDto
			CreateMap<SalesChannel, GetSalesChannelDto>().ReverseMap();
			CreateMap<SalesChannel, CreateSalesChannelDto>().ReverseMap();
			CreateMap<SalesChannel, GetSalesChannelDto>().ReverseMap();
			CreateMap<SalesChannel, UpdateSalesChannelDto>().ReverseMap();

			CreateMap<TaxClass, TaxClassDto>().ReverseMap();
			CreateMap<TaxClass, GetTaxClassDto>().ReverseMap();
			CreateMap<TaxClass, CreateTaxClassDto>().ReverseMap();
			CreateMap<TaxClass, GetTaxClassDto>().ReverseMap();			
			CreateMap<TaxClass, UpdateTaxClassDto>().ReverseMap();

			CreateMap<Warehouse, WarehouseDto>().ReverseMap();
			CreateMap<Warehouse, GetWarehouseDto>().ReverseMap();
			CreateMap<Warehouse, CreateWarehouseDto>().ReverseMap();
			CreateMap<Warehouse, GetWarehouseDto>().ReverseMap();
			CreateMap<TaxClass, UpdateWarehouseDto>().ReverseMap();
		}
	}
}