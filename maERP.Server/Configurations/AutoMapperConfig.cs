using AutoMapper;
using maERP.Shared.Dtos;
using maERP.Shared.Dtos.User;
using maERP.Shared.Dtos.Customer;
using maERP.Shared.Dtos.Order;
using maERP.Shared.Dtos.Product;
using maERP.Shared.Dtos.ProductSalesChannel;
using maERP.Shared.Dtos.SalesChannel;
using maERP.Shared.Dtos.TaxClass;
using maERP.Shared.Dtos.Warehouse;
using maERP.Shared.Models;
using maERP.Shared.Dtos.ShippingProvider;
using maERP.Server.Models;

namespace maERP.Server.Configurations;

public class AutoMapperConfig : Profile
{
	public AutoMapperConfig()
	{
        CreateMap<ApplicationUser, UserCreateDto>().ReverseMap();
        CreateMap<ApplicationUser, UserDetailDto>().ReverseMap();
        CreateMap<ApplicationUser, UserListDto>().ReverseMap();
        CreateMap<ApplicationUser, UserUpdateDto>().ReverseMap();

        CreateMap<AReferenceDto, Customer>();
        CreateMap<Customer, CustomerCreateDto>().ReverseMap();
        CreateMap<Customer, CustomerListDto>().ReverseMap();
        CreateMap<Customer, CustomerUpdateDto>().ReverseMap();

        CreateMap<AReferenceDto, Order>();
        CreateMap<Order, OrderCreateDto>().ReverseMap();
        CreateMap<Order, OrderDetailDto>().ReverseMap();
        CreateMap<Order, OrderListDto>().ReverseMap();
        CreateMap<Order, OrderUpdateDto>().ReverseMap();

        CreateMap<AReferenceDto, Product>();
        CreateMap<Product, ProductCreateDto>().ReverseMap();
        CreateMap<Product, ProductDetailDto>().ReverseMap();
        CreateMap<Product, ProductListDto>().ReverseMap();
        CreateMap<Product, ProductUpdateDto>().ReverseMap();

		CreateMap<AReferenceDto, ProductSalesChannel>();
		CreateMap<ProductSalesChannel, ProductSalesChannelCreateDto>().ReverseMap();
		CreateMap<ProductSalesChannel, ProductSalesChannelDetailDto>().ReverseMap();
		CreateMap<ProductSalesChannel, ProductSalesChannelListDto>().ReverseMap();
		CreateMap<ProductSalesChannel, ProductSalesChannelUpdateDto>().ReverseMap();

        CreateMap<AReferenceDto, SalesChannel>();
        CreateMap<SalesChannel, SalesChannelCreateDto>().ReverseMap();
		CreateMap<SalesChannel, SalesChannelDetailDto>().ReverseMap();
		CreateMap<SalesChannel, SalesChannelListDto>().ReverseMap();
		CreateMap<SalesChannel, SalesChannelUpdateDto>().ReverseMap();

        CreateMap<AReferenceDto, ShippingProvider>();
        CreateMap<ShippingProvider, ShippingProviderCreateDto>().ReverseMap();
        CreateMap<ShippingProvider, ShippingProviderDetailDto>().ReverseMap();
        CreateMap<ShippingProvider, ShippingProviderListDto>().ReverseMap();
        CreateMap<ShippingProvider, ShippingProviderUpdateDto>().ReverseMap();

        CreateMap<AReferenceDto, TaxClass>();
        CreateMap<TaxClass, TaxClassCreateDto>().ReverseMap();
        CreateMap<TaxClass, TaxClassDetailDto>().ReverseMap();
		CreateMap<TaxClass, TaxClassListDto>().ReverseMap();
		CreateMap<TaxClass, TaxClassUpdateDto>().ReverseMap();

        CreateMap<AReferenceDto, Warehouse>();
        CreateMap<Warehouse, WarehouseCreateDto>().ReverseMap();
        CreateMap<Warehouse, WarehouseDetailDto>().ReverseMap();
		CreateMap<Warehouse, WarehouseListDto>().ReverseMap();
		CreateMap<Warehouse, WarehouseUpdateDto>().ReverseMap();
	}
}