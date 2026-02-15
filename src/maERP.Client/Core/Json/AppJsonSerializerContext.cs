using System.Text.Json.Serialization;
using maERP.Client.Core.Models;
using maERP.Domain.Dtos.AiModel;
using maERP.Domain.Dtos.AiPrompt;
using maERP.Domain.Dtos.Auth;
using maERP.Domain.Dtos.Country;
using maERP.Domain.Dtos.Customer;
using maERP.Domain.Dtos.Invoice;
using maERP.Domain.Dtos.Manufacturer;
using maERP.Domain.Dtos.Order;
using maERP.Domain.Dtos.Product;
using maERP.Domain.Dtos.SalesChannel;
using maERP.Domain.Dtos.Statistic;
using maERP.Domain.Dtos.Superadmin;
using maERP.Domain.Dtos.TaxClass;
using maERP.Domain.Dtos.Tenant;
using maERP.Domain.Dtos.User;
using maERP.Domain.Dtos.Warehouse;

namespace maERP.Client.Core.Json;

[JsonSourceGenerationOptions(PropertyNameCaseInsensitive = true)]

// PaginatedResponse<T> types
[JsonSerializable(typeof(PaginatedResponse<AiModelListDto>))]
[JsonSerializable(typeof(PaginatedResponse<AiPromptListDto>))]
[JsonSerializable(typeof(PaginatedResponse<TenantListDto>))]
[JsonSerializable(typeof(PaginatedResponse<ProductListDto>))]
[JsonSerializable(typeof(PaginatedResponse<WarehouseListDto>))]
[JsonSerializable(typeof(PaginatedResponse<CustomerListDto>))]
[JsonSerializable(typeof(PaginatedResponse<TaxClassListDto>))]
[JsonSerializable(typeof(PaginatedResponse<InvoiceListDto>))]
[JsonSerializable(typeof(PaginatedResponse<OrderListDto>))]
[JsonSerializable(typeof(PaginatedResponse<ManufacturerListDto>))]
[JsonSerializable(typeof(PaginatedResponse<SalesChannelListDto>))]
[JsonSerializable(typeof(PaginatedResponse<CountryListDto>))]
[JsonSerializable(typeof(PaginatedResponse<UserListDto>))]

// ApiResponse<T> types
[JsonSerializable(typeof(ApiResponse<TenantDetailDto>))]
[JsonSerializable(typeof(ApiResponse<Guid>))]
[JsonSerializable(typeof(ApiResponse<UserListDto>))]
[JsonSerializable(typeof(ApiResponse<ProductDetailDto>))]
[JsonSerializable(typeof(ApiResponse<WarehouseDetailDto>))]
[JsonSerializable(typeof(ApiResponse<CustomerDetailDto>))]
[JsonSerializable(typeof(ApiResponse<TaxClassDetailDto>))]
[JsonSerializable(typeof(ApiResponse<SuperadminTenantDetailDto>))]
[JsonSerializable(typeof(ApiResponse<InvoiceDetailDto>))]
[JsonSerializable(typeof(ApiResponse<SalesTodayDto>))]
[JsonSerializable(typeof(ApiResponse<OrdersTodayDto>))]
[JsonSerializable(typeof(ApiResponse<CustomersTodayDto>))]
[JsonSerializable(typeof(ApiResponse<ProductsTodayDto>))]
[JsonSerializable(typeof(ApiResponse<OrdersLatestDto>))]
[JsonSerializable(typeof(ApiResponse<ProductsBestSellingDto>))]
[JsonSerializable(typeof(ApiResponse<OrderDetailDto>))]
[JsonSerializable(typeof(ApiResponse<SalesChannelDetailDto>))]
[JsonSerializable(typeof(ApiResponse<LoginResponseDto>))]

// Direct response types
[JsonSerializable(typeof(AiModelDetailDto))]
[JsonSerializable(typeof(AiPromptDetailDto))]
[JsonSerializable(typeof(ManufacturerDetailDto))]
[JsonSerializable(typeof(Guid))]

// Input/payload types
[JsonSerializable(typeof(AiModelInputDto))]
[JsonSerializable(typeof(AiPromptInputDto))]
[JsonSerializable(typeof(TenantInputDto))]
[JsonSerializable(typeof(WarehouseInputDto))]
[JsonSerializable(typeof(TaxClassInputDto))]
[JsonSerializable(typeof(SalesChannelInputDto))]
[JsonSerializable(typeof(ProductInputDto))]
[JsonSerializable(typeof(OrderInputDto))]
[JsonSerializable(typeof(ManufacturerInputDto))]
[JsonSerializable(typeof(CustomerInputDto))]
[JsonSerializable(typeof(LoginRequestDto))]
[JsonSerializable(typeof(AddUserToTenantPayload))]
[JsonSerializable(typeof(AssignUserToTenantPayload))]

// Error parsing
[JsonSerializable(typeof(ApiErrorResponse))]
internal partial class AppJsonSerializerContext : JsonSerializerContext;
