using AutoMapper;
using maERP.Shared.Dtos;
using maERP.Shared.Dtos.User;
using maERP.Shared.Dtos.Customer;
using maERP.Shared.Dtos.Order;
using maERP.Shared.Dtos.Product;
using maERP.Shared.Dtos.ProductSalesChannel;
using maERP.Shared.Dtos.SalesChannel;
using maERP.Shared.Dtos.TaxClass;
using maERP.Shared.Dtos.Warehouse;
using maERP.Shared.Models;
using maERP.Shared.Dtos.ShippingProvider;
using maERP.Shared.Models.Database;

namespace maERP.Server.Configurations;

public class AutoMapperConfig : Profile
{
	public AutoMapperConfig()
	{

	}
}