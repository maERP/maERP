using maERP.Server.Data;
using maERP.Server.Models;
using maERP.Server.Models.User;
using maERP.Server.Models.Product;
using maERP.Server.Models.TaxClass;
using maERP.Server.Models.SalesChannel;
using maERP.Server.Models.Warehouse;
using AutoMapper;

namespace maERP.Server.Configurations
{
	public class AutoMapperConfig : Profile
	{
		public AutoMapperConfig()
		{
			CreateMap<ApiUserDto, ApiUser>().ReverseMap();

			CreateMap<Product, ProductDto>().ReverseMap();
			CreateMap<Product, GetProductDto>().ReverseMap();
			CreateMap<Product, CreateProductDto>().ReverseMap();
			CreateMap<Product, GetProductDto>().ReverseMap();			
			CreateMap<Product, UpdateProductDto>().ReverseMap();

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