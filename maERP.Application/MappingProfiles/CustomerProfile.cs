using AutoMapper;
using maERP.Application.Features.Customer.Commands.CustomerCreate;
using maERP.Application.Features.Customer.Commands.CustomerDelete;
using maERP.Application.Features.Customer.Commands.CustomerUpdate;
using maERP.Application.Features.Customer.Queries.CustomerDetail;
using maERP.Application.Features.Customer.Queries.CustomerList;
using maERP.Domain.Entities;

namespace maERP.Application.MappingProfiles;

public class CustomerProfile : Profile
{
    public CustomerProfile()
    {
        CreateMap<Customer, CustomerCreateResponse>();
        CreateMap<Customer, CustomerDetailResponse>();
        CreateMap<Customer, CustomerListResponse>();
        CreateMap<Customer, CustomerUpdateResponse>();

        CreateMap<CustomerCreateCommand, Customer>();
        CreateMap<CustomerDeleteCommand, Customer>();
        CreateMap<CustomerUpdateCommand, Customer>();
    }
}