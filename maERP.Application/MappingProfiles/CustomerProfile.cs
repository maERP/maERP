using AutoMapper;
using maERP.Application.Features.Customer.Commands.CreateCustomer;
using maERP.Application.Features.Customer.Commands.DeleteCustomer;
using maERP.Application.Features.Customer.Commands.UpdateCustomer;
using maERP.Application.Features.Customer.Queries.GetCustomerDetail;
using maERP.Application.Features.Customer.Queries.GetCustomers;
using maERP.Domain.Models;

namespace maERP.Application.MappingProfiles;

public class CustomerProfile : Profile
{
    public CustomerProfile()
    {
        CreateMap<Customer, CreateCustomerResponse>();
        CreateMap<Customer, GetCustomerDetailResponse>();
        CreateMap<Customer, GetCustomersResponse>();
        CreateMap<Customer, UpdateCustomerResponse>();

        CreateMap<CreateCustomerCommand, Customer>();
        CreateMap<DeleteCustomerCommand, Customer>();
        CreateMap<UpdateCustomerCommand, Customer>();
    }
}