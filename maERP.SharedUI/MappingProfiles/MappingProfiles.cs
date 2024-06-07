using AutoMapper;
using maERP.Shared.Wrapper;
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
        CreateMap<CustomerVM, GetCustomersResponse>().ReverseMap();
        // CreateMap<CustomerVM, GetCustomerDetailResponse>().ReverseMap();
        CreateMap<CustomerVM, CreateCustomerCommand>().ReverseMap();
        CreateMap<CustomerVM, UpdateCustomerCommand>().ReverseMap();

        // CreateMap<CustomerAddressListVM, CustomerAddressListDto>().ReverseMap();
        // CreateMap<CustomerAddressVM, CustomerAddressDetailDto>().ReverseMap();

        CreateMap<OrderListVM, GetOrdersResponse>().ReverseMap();
        CreateMap<OrderListVM, GetOrdersResponse>().ReverseMap();

        CreateMap<PaginatedResult<OrderListVM>, GetOrdersResponsePaginatedResult>().ReverseMap();

        CreateMap<OrderVM, GetOrderDetailResponse>().ReverseMap();
        CreateMap<OrderVM, CreateOrderCommand>().ReverseMap();
        CreateMap<OrderVM, UpdateOrderCommand>().ReverseMap();

        CreateMap<ProductVM, GetProductsResponse>().ReverseMap();
        CreateMap<ProductVM, GetProductDetailResponse>().ReverseMap();
        CreateMap<ProductVM, CreateProductCommand>().ReverseMap();
        CreateMap<ProductVM, UpdateProductCommand>().ReverseMap();

        CreateMap<SalesChannelVM, GetSalesChannelsResponse>().ReverseMap();
        CreateMap<SalesChannelVM, GetSalesChannelDetailResponse>().ReverseMap();
        CreateMap<SalesChannelVM, CreateSalesChannelCommand>().ReverseMap();
        CreateMap<SalesChannelVM, UpdateSalesChannelCommand>().ReverseMap();

        CreateMap<TaxClassVM, GetTaxClassesResponse>().ReverseMap();
        CreateMap<TaxClassVM, GetTaxClassDetailResponse>().ReverseMap();
        CreateMap<TaxClassVM, CreateTaxClassCommand>().ReverseMap();
        CreateMap<TaxClassVM, UpdateTaxClassCommand>().ReverseMap();

        // CreateMap<UserVM, GetUsersResponse>().ReverseMap();
        // CreateMap<UserVM, GetUserDetailResponse>().ReverseMap();
        CreateMap<UserVM, CreateUserCommand>().ReverseMap();
        CreateMap<UserVM, UpdateUserCommand>().ReverseMap();

        CreateMap<WarehouseVM, GetWarehousesResponse>().ReverseMap();
        CreateMap<WarehouseVM, GetWarehouseDetailResponse>().ReverseMap();
        CreateMap<WarehouseVM, CreateWarehouseCommand>().ReverseMap();
        CreateMap<WarehouseVM, UpdateWarehouseCommand>().ReverseMap();
    }
}