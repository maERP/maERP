using maERP.Shared.Models;
using maERP.Shared.Dtos;
using maERP.Shared.Dtos.User;
using maERP.Shared.Dtos.Customer;
using maERP.Shared.Dtos.Order;
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

            CreateMap<Customer, CustomerDto>().ReverseMap();
            CreateMap<Customer, CustomerListDto>().ReverseMap();

            CreateMap<Order, OrderDto>().ReverseMap();
            CreateMap<Order, OrderListDto>().ReverseMap();

            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<Product, ProductListDto>().ReverseMap();

			CreateMap<ProductSalesChannel, ProductSalesChannelDto>().ReverseMap();
			CreateMap<ProductSalesChannel, ProductSalesChannelListDto>().ReverseMap();

			CreateMap<SalesChannel, SalesChannelDto>().ReverseMap();
			CreateMap<SalesChannel, SalesChannelListDto>().ReverseMap();

			CreateMap<TaxClass, TaxClassDto>().ReverseMap();
			CreateMap<TaxClass, TaxClassListDto>().ReverseMap();

			CreateMap<Warehouse, WarehouseDto>().ReverseMap();
			CreateMap<Warehouse, WarehouseListDto>().ReverseMap();
		}
	}
}