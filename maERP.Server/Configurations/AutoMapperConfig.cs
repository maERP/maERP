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
using maERP.Shared.Dtos.ShippingProvider;

namespace maERP.Server.Configurations;

public class AutoMapperConfig : Profile
{
	public AutoMapperConfig()
	{
		CreateMap<ApiUserDto, ApiUser>().ReverseMap();

        CreateMap<ReferenceDto, Customer>();
        CreateMap<Customer, CustomerCreateDto>().ReverseMap();
        CreateMap<Customer, CustomerListDto>().ReverseMap();
        CreateMap<Customer, CustomerUpdateDto>().ReverseMap();

        CreateMap<ReferenceDto, Order>();
        CreateMap<Order, OrderCreateDto>().ReverseMap();
        CreateMap<Order, OrderDetailDto>().ReverseMap();
        CreateMap<Order, OrderListDto>().ReverseMap();
        CreateMap<Order, OrderUpdateDto>().ReverseMap();

        CreateMap<ReferenceDto, Product>();
        CreateMap<Product, ProductCreateDto>().ReverseMap();
        CreateMap<Product, ProductDetailDto>().ReverseMap();
        CreateMap<Product, ProductListDto>().ReverseMap();
        CreateMap<Product, ProductUpdateDto>().ReverseMap();

		CreateMap<ReferenceDto, ProductSalesChannel>();
		CreateMap<ProductSalesChannel, ProductSalesChannelCreateDto>().ReverseMap();
		CreateMap<ProductSalesChannel, ProductSalesChannelDetailDto>().ReverseMap();
		CreateMap<ProductSalesChannel, ProductSalesChannelListDto>().ReverseMap();
		CreateMap<ProductSalesChannel, ProductSalesChannelUpdateDto>().ReverseMap();

        CreateMap<ReferenceDto, SalesChannel>();
        CreateMap<SalesChannel, SalesChannelCreateDto>().ReverseMap();
		CreateMap<SalesChannel, SalesChannelDetailDto>().ReverseMap();
		CreateMap<SalesChannel, SalesChannelListDto>().ReverseMap();
		CreateMap<SalesChannel, SalesChannelUpdateDto>().ReverseMap();

        CreateMap<ReferenceDto, ShippingProvider>();
        CreateMap<ShippingProvider, ShippingProviderCreateDto>().ReverseMap();
        CreateMap<ShippingProvider, ShippingProviderDetailDto>().ReverseMap();
        CreateMap<ShippingProvider, ShippingProviderListDto>().ReverseMap();
        CreateMap<ShippingProvider, ShippingProviderUpdateDto>().ReverseMap();

        CreateMap<ReferenceDto, TaxClass>();
        CreateMap<TaxClass, TaxClassCreateDto>().ReverseMap();
        CreateMap<TaxClass, TaxClassDetailDto>().ReverseMap();
		CreateMap<TaxClass, TaxClassListDto>().ReverseMap();
		CreateMap<TaxClass, TaxClassUpdateDto>().ReverseMap();

        CreateMap<ReferenceDto, Warehouse>();
        CreateMap<Warehouse, WarehouseCreateDto>().ReverseMap();
        CreateMap<Warehouse, WarehouseDetailDto>().ReverseMap();
		CreateMap<Warehouse, WarehouseListDto>().ReverseMap();
		CreateMap<Warehouse, WarehouseUpdateDto>().ReverseMap();
	}
}