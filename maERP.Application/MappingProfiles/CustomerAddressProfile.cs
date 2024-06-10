using AutoMapper;
using maERP.Application.Dtos.CustomerAddress;
using maERP.Application.Features.Customer.Commands.CustomerCreate;
using maERP.Application.Features.Customer.Commands.CustomerDelete;
using maERP.Application.Features.Customer.Commands.CustomerUpdate;
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

        CreateMap<CustomerCreateCommand, CustomerAddress>();
        CreateMap<CustomerDeleteCommand, CustomerAddress>();
        CreateMap<CustomerUpdateCommand, CustomerAddress>();
    }
}