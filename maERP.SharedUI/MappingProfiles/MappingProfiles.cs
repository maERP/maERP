using AutoMapper;
using maERP.SharedUI.Models.Customer;
using maERP.SharedUI.Models.CustomerAddress;
using maERP.SharedUI.Models.Order;
using maERP.SharedUI.Models.Product;
using maERP.SharedUI.Models.SalesChannel;
using maERP.SharedUI.Models.TaxClass;
using maERP.SharedUI.Models.User;
using maERP.SharedUI.Models.Warehouse;
using maERP.SharedUI.Services.Base;

namespace maERP.SharedUI.MappingProfiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<CustomerVM, CustomerListDto>().ReverseMap();
        CreateMap<CustomerVM, CustomerDetailDto>().ReverseMap();
        CreateMap<CustomerVM, CreateCustomerCommand>().ReverseMap();
        CreateMap<CustomerVM, UpdateCustomerCommand>().ReverseMap();

        CreateMap<CustomerAddressListVM, CustomerAddressListDto>().ReverseMap();
        // CreateMap<CustomerAddressVM, CustomerAddressDetailDto>().ReverseMap();

        CreateMap<CustomerVM, CustomerListDto>().ReverseMap();
        CreateMap<CustomerVM, CustomerDetailDto>().ReverseMap();
        CreateMap<CustomerVM, CreateCustomerCommand>().ReverseMap();
        CreateMap<CustomerVM, UpdateCustomerCommand>().ReverseMap();

        CreateMap<OrderListVM, OrderListDto>().ReverseMap();
        CreateMap<OrderVM, OrderDetailDto>().ReverseMap();
        CreateMap<OrderVM, CreateOrderCommand>().ReverseMap();
        CreateMap<OrderVM, UpdateOrderCommand>().ReverseMap();

        CreateMap<ProductVM, ProductListDto>().ReverseMap();
        CreateMap<ProductVM, ProductDetailDto>().ReverseMap();
        CreateMap<ProductVM, CreateProductCommand>().ReverseMap();
        CreateMap<ProductVM, UpdateProductCommand>().ReverseMap();

        CreateMap<SalesChannelVM, SalesChannelListDto>().ReverseMap();
        CreateMap<SalesChannelVM, SalesChannelDetailDto>().ReverseMap();
        CreateMap<SalesChannelVM, CreateSalesChannelCommand>().ReverseMap();
        CreateMap<SalesChannelVM, UpdateSalesChannelCommand>().ReverseMap();

        CreateMap<TaxClassVM, TaxClassListDto>().ReverseMap();
        CreateMap<TaxClassVM, TaxClassDetailDto>().ReverseMap();
        CreateMap<TaxClassVM, CreateTaxClassCommand>().ReverseMap();
        CreateMap<TaxClassVM, UpdateTaxClassCommand>().ReverseMap();

        CreateMap<UserVM, UserListDto>().ReverseMap();
        CreateMap<UserVM, UserDetailDto>().ReverseMap();
        CreateMap<UserVM, CreateUserCommand>().ReverseMap();
        CreateMap<UserVM, UpdateUserCommand>().ReverseMap();

        CreateMap<WarehouseVM, WarehouseListDto>().ReverseMap();
        CreateMap<WarehouseVM, WarehouseDetailDto>().ReverseMap();
        CreateMap<WarehouseVM, CreateWarehouseCommand>().ReverseMap();
        CreateMap<WarehouseVM, UpdateWarehouseCommand>().ReverseMap();
    }
}