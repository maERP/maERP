using AutoMapper;
using maERP.Domain.Wrapper;
using maERP.SharedUI.Models.AiModel;
using maERP.SharedUI.Models.AiPrompt;
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
        CreateMap<PaginatedResult<AiModelListVm>, AiModelListResponsePaginatedResult>().ReverseMap();
        CreateMap<AiModelListVm, AiModelListResponse>().ReverseMap();
        CreateMap<AiModelVm, AiModelDetailResponse>().ReverseMap();
        CreateMap<AiModelVm, AiModelCreateCommand>().ReverseMap();
        CreateMap<AiModelVm, AiModelUpdateCommand>().ReverseMap();
        
        CreateMap<PaginatedResult<AiPromptListVm>, AiPromptListResponsePaginatedResult>().ReverseMap();
        CreateMap<AiPromptListVm, AiPromptListResponse>().ReverseMap();
        CreateMap<AiPromptVm, AiPromptDetailResponse>().ReverseMap();
        CreateMap<AiPromptVm, AiPromptCreateCommand>().ReverseMap();
        CreateMap<AiPromptVm, AiPromptUpdateCommand>().ReverseMap();
        
        CreateMap<PaginatedResult<CustomerVm>, CustomerListResponsePaginatedResult>().ReverseMap();
        CreateMap<CustomerVm, CustomerListResponse>().ReverseMap();
        CreateMap<CustomerVm, CustomerDetailResponse>().ReverseMap();
        CreateMap<CustomerVm, CustomerCreateCommand>().ReverseMap();
        CreateMap<CustomerVm, CustomerUpdateCommand>().ReverseMap();

        CreateMap<PaginatedResult<OrderListVm>, OrderListResponsePaginatedResult>().ReverseMap();
        CreateMap<OrderListVm, OrderListResponse>().ReverseMap();
        CreateMap<OrderVm, OrderDetailResponse>().ReverseMap();
        CreateMap<OrderVm, OrderCreateCommand>().ReverseMap();
        CreateMap<OrderVm, OrderUpdateCommand>().ReverseMap();

        CreateMap<PaginatedResult<ProductListVm>, ProductListResponsePaginatedResult>().ReverseMap();
        CreateMap<ProductListVm, ProductListResponse>().ReverseMap();
        CreateMap<ProductVm, ProductDetailResponse>().ReverseMap();
        CreateMap<ProductVm, ProductCreateCommand>().ReverseMap();
        CreateMap<ProductVm, ProductCreateCommand>().ReverseMap();

        CreateMap<PaginatedResult<SalesChannelVm>, SalesChannelListResponsePaginatedResult>().ReverseMap();
        CreateMap<SalesChannelVm, SalesChannelListResponse>().ReverseMap();
        CreateMap<SalesChannelVm, SalesChannelDetailResponse>().ReverseMap();
        CreateMap<SalesChannelVm, SalesChannelCreateCommand>().ReverseMap();
        CreateMap<SalesChannelVm, SalesChannelUpdateCommand>().ReverseMap();

        CreateMap<PaginatedResult<TaxClassVm>, TaxClassListResponsePaginatedResult>().ReverseMap();
        CreateMap<TaxClassVm, TaxClassListResponse>().ReverseMap();
        CreateMap<TaxClassVm, TaxClassDetailResponse>().ReverseMap();
        CreateMap<TaxClassVm, TaxClassCreateCommand>().ReverseMap();
        CreateMap<TaxClassVm, TaxClassUpdateCommand>().ReverseMap();

        CreateMap<UserVm, UserListResponse>().ReverseMap();
        CreateMap<UserVm, UserDetailResponse>().ReverseMap();
        CreateMap<UserVm, UserCreateCommand>().ReverseMap();
        CreateMap<UserVm, UserUpdateCommand>().ReverseMap();

        CreateMap<PaginatedResult<WarehouseVm>, WarehouseListResponsePaginatedResult>().ReverseMap();
        CreateMap<WarehouseVm, WarehouseListResponse>().ReverseMap();
        CreateMap<WarehouseVm, WarehouseDetailResponse>().ReverseMap();
        CreateMap<WarehouseVm, WarehouseCreateCommand>().ReverseMap();
        CreateMap<WarehouseVm, WarehouseUpdateCommand>().ReverseMap();
    }
}