using AutoMapper;
using maERP.SharedUI.Services.Base;
using maERP.SharedUI.Models.Warehouse;
using maERP.SharedUI.Models.Customer;
using maERP.SharedUI.Models.Order;
using maERP.SharedUI.Models.Product;
using maERP.SharedUI.Models.SalesChannel;
using maERP.SharedUI.Models.TaxClass;
using maERP.SharedUI.Models.User;

namespace maERP.SharedUI.MappingProfiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<CustomerVM, CustomerListDto>().ReverseMap();
        CreateMap<CustomerVM, CreateCustomerCommand>().ReverseMap();
        CreateMap<CustomerVM, UpdateCustomerCommand>().ReverseMap();

        CreateMap<OrderVM, OrderListDto>().ReverseMap();
        CreateMap<OrderVM, CreateOrderCommand>().ReverseMap();
        CreateMap<OrderVM, UpdateOrderCommand>().ReverseMap();

        CreateMap<ProductVM, ProductListDto>().ReverseMap();
        CreateMap<ProductVM, CreateProductCommand>().ReverseMap();
        CreateMap<ProductVM, UpdateProductCommand>().ReverseMap();

        CreateMap<SalesChannelVM, SalesChannelListDto>().ReverseMap();
        CreateMap<SalesChannelVM, CreateSalesChannelCommand>().ReverseMap();
        CreateMap<SalesChannelVM, UpdateSalesChannelCommand>().ReverseMap();

        CreateMap<TaxClassVM, TaxClassListDto>().ReverseMap();
        CreateMap<TaxClassVM, CreateTaxClassCommand>().ReverseMap();
        CreateMap<TaxClassVM, UpdateTaxClassCommand>().ReverseMap();

        CreateMap<UserVM, UserListDto>().ReverseMap();
        CreateMap<UserVM, CreateUserCommand>().ReverseMap();
        CreateMap<UserVM, UpdateUserCommand>().ReverseMap();

        CreateMap<WarehouseVM, WarehouseListDto>().ReverseMap();
        CreateMap<WarehouseVM, CreateWarehouseCommand>().ReverseMap();
        CreateMap<WarehouseVM, UpdateWarehouseCommand>().ReverseMap();
    }
}