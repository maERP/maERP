using AutoMapper;
using maERP.Server.Contracts;
using maERP.Server.Models;

namespace maERP.Server.Repository;

public class CustomerRepository : GenericRepository<Customer>, ICustomerRepository
{
    public CustomerRepository(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
    {
    }
}