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
            .Include(x => x.CustomerAddress)
            .Include(x => x.Orders)
            .FirstOrDefaultAsync() ?? throw new NotFoundException("User not found", "User not found");
    }
    
    public async Task<Customer?> GetCustomerByEmailAsync(string email)
    {
        return await _context.Customer
            .Where(x => x.Email == email)
            .FirstOrDefaultAsync() ?? null;
    }
    
    public async Task<Customer?> GetCustomerByRemoteCustomerIdAsync(int salesChannelId, string remoteCustomerId)
    {
        return await _context.Customer
            .Where(x => x.CustomerSalesChannel!.Any(y => y.SalesChannelId == salesChannelId && y.RemoteCustomerId == remoteCustomerId))
            .FirstOrDefaultAsync() ?? null;
    }
    
    public async Task AddCustomerToSalesChannelAsync(int customerId, int salesChannelId, string remoteCustomerId)
    {
        var CustomerSalesChannel = new CustomerSalesChannel
        {
            CustomerId = customerId,
            SalesChannelId = salesChannelId,
            RemoteCustomerId = remoteCustomerId
        };
        
        await _context.CustomerSalesChannel.AddAsync(CustomerSalesChannel);
    }
}