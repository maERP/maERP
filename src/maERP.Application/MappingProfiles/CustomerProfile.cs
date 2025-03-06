using AutoMapper;
using maERP.Application.Features.Customer.Commands.CustomerCreate;
using maERP.Application.Features.Customer.Commands.CustomerDelete;
using maERP.Application.Features.Customer.Commands.CustomerUpdate;
using maERP.Domain.Dtos.Customer;
using maERP.Domain.Entities;

namespace maERP.Application.MappingProfiles;

public class CustomerProfile : Profile
{
    public CustomerProfile()
    {
        CreateMap<Customer, CustomerDetailDto>();
        CreateMap<Customer, CustomerListDto>();

        CreateMap<CustomerCreateCommand, Customer>();
        CreateMap<CustomerDeleteCommand, Customer>();
        CreateMap<CustomerInputCommand, Customer>();
    }
}