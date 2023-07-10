#nullable disable

using Microsoft.EntityFrameworkCore;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using maERP.Server.Exceptions;
using maERP.Server.Models;
using maERP.Shared.Dtos.Customer;
using maERP.Shared.Models;

namespace maERP.Server.Repository;

public interface ICustomerRepository : IGenericRepository<Customer>
{
    
}

public class CustomerRepository : GenericRepository<Customer>, ICustomerRepository
{
    private readonly ApplicationDbContext _context;

    public CustomerRepository(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
    {
        this._context = context;
    }
}