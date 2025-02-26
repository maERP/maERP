using maERP.Application.Contracts.Persistence;
using maERP.Domain.Entities;
using maERP.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace maERP.Persistence.Repositories;

public class CustomerRepository : GenericRepository<Customer>, ICustomerRepository
{
    public CustomerRepository(ApplicationDbContext context) : base(context)
    {

    }
    
    public async Task<Customer?> GetCustomerWithDetails(int id)
    {
        return await Context.Customer
            .Where(x => x.Id == id)
            .Include(x => x.CustomerAddresses)
            .Include(x => x.Orders)
            .FirstOrDefaultAsync() ?? null;
    }
    
    public async Task<Customer?> GetCustomerByEmailAsync(string email)
    {
        return await Context.Customer
            .Where(x => x.Email == email)
            .FirstOrDefaultAsync() ?? null;
    }
    
    public async Task<Customer?> GetCustomerByRemoteCustomerIdAsync(int salesChannelId, string remoteCustomerId)
    {
        return await Context.Customer
            .Where(x => x.CustomerSalesChannels!.Any(y => y.SalesChannelId == salesChannelId && y.RemoteCustomerId == remoteCustomerId))
            .FirstOrDefaultAsync() ?? null;
    }
    
    public async Task AddCustomerToSalesChannelAsync(int customerId, int salesChannelId, string remoteCustomerId)
    {
        var customerSalesChannel = new CustomerSalesChannel
        {
            CustomerId = customerId,
            SalesChannelId = salesChannelId,
            RemoteCustomerId = remoteCustomerId
        };
        
        await Context.CustomerSalesChannel.AddAsync(customerSalesChannel);
        await Context.SaveChangesAsync();
        Console.WriteLine("saved");
    }
    
    public async Task<ICollection<CustomerAddress>> GetCustomerAddressByCustomerIdAsync(int customerId)
    {
        return await Context.CustomerAddress
            .Where(x => x.CustomerId == customerId)
            .ToListAsync();
    }

    public async Task<CustomerAddress> AddCustomerAddressAsync(CustomerAddress customerAddress)
    {
        await Context.CustomerAddress.AddAsync(customerAddress);
        await Context.SaveChangesAsync();
        return customerAddress;
    }
}