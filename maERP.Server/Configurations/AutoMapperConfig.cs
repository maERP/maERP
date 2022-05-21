using maERP.Server.Data;
using maERP.Server.Models;
using maERP.Server.Models.Users;
using AutoMapper;

namespace maERP.Server.Configurations
{
	public class AutoMapperConfig : Profile
	{
		public AutoMapperConfig()
		{
			CreateMap<ApiUserDto, ApiUser>().ReverseMap();

			CreateMap<Product, GetProductDto>().ReverseMap();
			CreateMap<Product, CreateProductDto>().ReverseMap();
			CreateMap<Product, GetProductDto>().ReverseMap();			
			CreateMap<Product, UpdateProductDto>().ReverseMap();

			CreateMap<SalesChannel, GetSalesChannelDto>().ReverseMap();
			CreateMap<SalesChannel, CreateSalesChannelDto>().ReverseMap();
			CreateMap<SalesChannel, GetSalesChannelDto>().ReverseMap();
			CreateMap<SalesChannel, UpdateSalesChannelDto>().ReverseMap();
		}
	}
}