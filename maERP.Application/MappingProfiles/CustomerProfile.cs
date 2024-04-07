using AutoMapper;
using maERP.Domain;
using maERP.Application.Dtos.Customer;
using maERP.Application.Features.Customer.Commands.CreateCustomerCommand;
using maERP.Application.Features.Customer.Commands.DeleteCustomerCommand;
using maERP.Application.Features.Customer.Commands.UpdateCustomerCommand;

namespace maERP.Application.MappingProfiles;

public class CustomerProfile : Profile
{
    public CustomerProfile()
    {
        CreateMap<Customer, CustomerCreateDto>();
        CreateMap<Customer, CustomerDetailDto>();
        CreateMap<Customer, CustomerListDto>();
        CreateMap<Customer, CustomerUpdateDto>();

        CreateMap<CreateCustomerCommand, Customer>();
        CreateMap<DeleteCustomerCommand, Customer>();
        CreateMap<UpdateCustomerCommand, Customer>();
    }
}