using AutoMapper;
using maERP.Shared.Dtos;
using maERP.Shared.Dtos.User;
using maERP.Shared.Dtos.Customer;
using maERP.Shared.Dtos.Order;
using maERP.Shared.Dtos.Product;
using maERP.Shared.Dtos.ProductSalesChannel;
using maERP.Shared.Dtos.TaxClass;
using maERP.Shared.Dtos.Warehouse;
using maERP.Shared.Models;

namespace maERP.Server.Configurations;

public class AutoMapperConfig : Profile
{
	public AutoMapperConfig()
	{
		CreateMap<ApiUserDto, ApiUser>().ReverseMap();

        CreateMap<Customer, CustomerDetailDto>().ReverseMap();
        CreateMap<Customer, CustomerListDto>().ReverseMap();

        CreateMap<Order, OrderDetailDto>().ReverseMap();
        CreateMap<Order, OrderListDto>().ReverseMap();

        CreateMap<Product, ProductDetailDto>().ReverseMap();
        CreateMap<Product, ProductListDto>().ReverseMap();

		CreateMap<ProductSalesChannel, ProductSalesChannelDetailDto>().ReverseMap();
		CreateMap<ProductSalesChannel, ProductSalesChannelListDto>().ReverseMap();

		CreateMap<SalesChannel, SalesChannelDto>().ReverseMap();
		CreateMap<SalesChannel, SalesChannelListDto>().ReverseMap();

		CreateMap<TaxClass, TaxClassDetailDto>().ReverseMap();
		CreateMap<TaxClass, TaxClassListDto>().ReverseMap();

		CreateMap<Warehouse, WarehouseDetailDto>().ReverseMap();
		CreateMap<Warehouse, WarehouseListDto>().ReverseMap();
	}
}