using AutoMapper;
using maERP.Shared.Wrapper;
using maERP.SharedUI.Models.Customer;
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
        CreateMap<PaginatedResult<CustomerVM>, CustomerListResponsePaginatedResult>().ReverseMap();
        CreateMap<CustomerVM, CustomerListResponse>().ReverseMap();
        CreateMap<CustomerVM, CustomerDetailResponse>().ReverseMap();
        CreateMap<CustomerVM, CustomerCreateCommand>().ReverseMap();
        CreateMap<CustomerVM, CustomerUpdateCommand>().ReverseMap();

        CreateMap<PaginatedResult<OrderListVM>, OrderListResponsePaginatedResult>().ReverseMap();
        CreateMap<OrderListVM, OrderListResponse>().ReverseMap();
        CreateMap<OrderVM, OrderDetailResponse>().ReverseMap();
        CreateMap<OrderVM, OrderCreateCommand>().ReverseMap();
        CreateMap<OrderVM, OrderUpdateCommand>().ReverseMap();

        CreateMap<PaginatedResult<ProductListVM>, ProductListResponsePaginatedResult>().ReverseMap();
        CreateMap<ProductListVM, ProductListResponse>().ReverseMap();
        CreateMap<ProductVM, ProductDetailResponse>().ReverseMap();
        CreateMap<ProductVM, ProductCreateCommand>().ReverseMap();
        CreateMap<ProductVM, ProductCreateCommand>().ReverseMap();

        CreateMap<PaginatedResult<SalesChannelVM>, SalesChannelListResponsePaginatedResult>().ReverseMap();
        CreateMap<SalesChannelVM, SalesChannelListResponse>().ReverseMap();
        CreateMap<SalesChannelVM, SalesChannelDetailResponse>().ReverseMap();
        CreateMap<SalesChannelVM, SalesChannelCreateCommand>().ReverseMap();
        CreateMap<SalesChannelVM, SalesChannelUpdateCommand>().ReverseMap();

        CreateMap<PaginatedResult<TaxClassVM>, TaxClassListResponsePaginatedResult>().ReverseMap();
        CreateMap<TaxClassVM, TaxClassListResponse>().ReverseMap();
        CreateMap<TaxClassVM, TaxClassDetailResponse>().ReverseMap();
        CreateMap<TaxClassVM, TaxClassCreateCommand>().ReverseMap();
        CreateMap<TaxClassVM, TaxClassUpdateCommand>().ReverseMap();

        CreateMap<UserVM, UserListResponse>().ReverseMap();
        CreateMap<UserVM, UserDetailResponse>().ReverseMap();
        CreateMap<UserVM, UserCreateCommand>().ReverseMap();
        CreateMap<UserVM, UserUpdateCommand>().ReverseMap();

        CreateMap<PaginatedResult<WarehouseVM>, WarehouseListResponsePaginatedResult>().ReverseMap();
        CreateMap<WarehouseVM, WarehouseListResponse>().ReverseMap();
        CreateMap<WarehouseVM, WarehouseDetailResponse>().ReverseMap();
        CreateMap<WarehouseVM, WarehouseCreateCommand>().ReverseMap();
        CreateMap<WarehouseVM, WarehouseUpdateCommand>().ReverseMap();
    }
}