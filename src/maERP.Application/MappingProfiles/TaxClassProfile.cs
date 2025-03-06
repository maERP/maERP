using AutoMapper;
using maERP.Application.Features.TaxClass.Commands.TaxClassCreate;
using maERP.Application.Features.TaxClass.Commands.TaxClassDelete;
using maERP.Application.Features.TaxClass.Commands.TaxClassUpdate;
using maERP.Domain.Dtos.TaxClass;
using maERP.Domain.Entities;

namespace maERP.Application.MappingProfiles;

public class TaxClassProfile : Profile
{
    public TaxClassProfile()
    {
        CreateMap<TaxClass, TaxClassDetailDto>();
        CreateMap<TaxClass, TaxClassListDto>();
        
        CreateMap<TaxClassCreateCommand, TaxClass>();
        CreateMap<TaxClassDeleteCommand, TaxClass>();
        CreateMap<TaxClassInputCommand, TaxClass>();
    }
}