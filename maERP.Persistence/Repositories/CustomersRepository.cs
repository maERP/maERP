using maERP.Application.Contracts.Persistence;
using maERP.Application.Exceptions;
using maERP.Domain.Models;
using maERP.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace maERP.Persistence.Repositories;

public class CustomerRepository : GenericRepository<Customer>, ICustomerRepository
{
    public CustomerRepository(ApplicationDbContext context) : base(context)
    {

    }
    
    public async Task<Customer> GetCustomerWithDetails(int id)
    {
        return await _context.Customer
            .Where(x => x.Id == id)
            .Include(x => x.CustomerAddresses)
            .Include(x => x.Orders)
            .FirstOrDefaultAsync() ?? throw new NotFoundException("User not found", "User not found");
    }
}