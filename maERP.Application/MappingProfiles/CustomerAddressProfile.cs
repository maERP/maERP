using AutoMapper;
using maERP.Application.Dtos.CustomerAddress;
using maERP.Application.Features.Customer.Commands.CreateCustomer;
using maERP.Application.Features.Customer.Commands.DeleteCustomer;
using maERP.Application.Features.Customer.Commands.UpdateCustomer;
using maERP.Domain.Models;

namespace maERP.Application.MappingProfiles;

public class CustomerAddressProfile : Profile
{
    public CustomerAddressProfile()
    {
        CreateMap<CustomerAddress, CustomerAddressCreateDto>();
        CreateMap<CustomerAddress, CustomerAddressDetailDto>();
        CreateMap<CustomerAddress, CustomerAddressListDto>();
        CreateMap<CustomerAddress, CustomerAddressUpdateDto>();

        CreateMap<CreateCustomerCommand, CustomerAddress>();
        CreateMap<DeleteCustomerCommand, CustomerAddress>();
        CreateMap<UpdateCustomerCommand, CustomerAddress>();
    }
}