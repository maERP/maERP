using AutoMapper;
using maERP.Server.Contracts;
using maERP.Shared.Models.Database;
using maERP.Server.Services;

namespace maERP.Persistence.Repositories;

public class CustomerRepository : GenericRepository<Customer>, ICustomerRepository
{
    public CustomerRepository(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
    {
    }
}