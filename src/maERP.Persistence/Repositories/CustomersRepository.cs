using maERP.Application.Contracts.Persistence;
using maERP.Application.Contracts.Services;
using maERP.Domain.Entities;
using maERP.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace maERP.Persistence.Repositories;

public class CustomerRepository : GenericRepository<Customer>, ICustomerRepository
{
    public CustomerRepository(ApplicationDbContext context, ITenantContext tenantContext) : base(context, tenantContext)
    {

    }

    public async Task<Customer?> GetCustomerWithDetails(Guid id)
    {
        var query = Context.Customer
            .Where(x => x.Id == id);

        // Apply manual tenant filtering
        var currentTenantId = TenantContext.GetCurrentTenantId();
        if (currentTenantId.HasValue)
        {
            query = query.Where(x => x.TenantId == null || x.TenantId == currentTenantId.Value);
        }

        return await query
            .Include(x => x.CustomerAddresses)
            .Include(x => x.Orders)
            .AsSplitQuery()
            .FirstOrDefaultAsync() ?? null;
    }

    public async Task<Customer?> GetCustomerByEmailAsync(string email)
    {
        var query = Context.Customer
            .Where(x => x.Email == email);

        // Apply manual tenant filtering
        var currentTenantId = TenantContext.GetCurrentTenantId();
        if (currentTenantId.HasValue)
        {
            query = query.Where(x => x.TenantId == null || x.TenantId == currentTenantId.Value);
        }

        return await query.FirstOrDefaultAsync() ?? null;
    }

    public async Task<Customer?> GetCustomerByRemoteCustomerIdAsync(Guid salesChannelId, string remoteCustomerId)
    {
        var query = Context.Customer
            .Where(x => x.CustomerSalesChannels!.Any(y => y.SalesChannelId == salesChannelId && y.RemoteCustomerId == remoteCustomerId));

        // Apply manual tenant filtering
        var currentTenantId = TenantContext.GetCurrentTenantId();
        if (currentTenantId.HasValue)
        {
            query = query.Where(x => x.TenantId == null || x.TenantId == currentTenantId.Value);
        }

        return await query.FirstOrDefaultAsync() ?? null;
    }

    public async Task AddCustomerToSalesChannelAsync(Guid customerId, Guid salesChannelId, string remoteCustomerId)
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

    public async Task<ICollection<CustomerAddress>> GetCustomerAddressByCustomerIdAsync(Guid customerId)
    {
        var query = Context.CustomerAddress
            .Where(x => x.CustomerId == customerId);

        // Apply manual tenant filtering
        var currentTenantId = TenantContext.GetCurrentTenantId();
        if (currentTenantId.HasValue)
        {
            query = query.Where(x => x.TenantId == null || x.TenantId == currentTenantId.Value);
        }

        return await query.ToListAsync();
    }

    public async Task<CustomerAddress> AddCustomerAddressAsync(CustomerAddress customerAddress)
    {
        await Context.CustomerAddress.AddAsync(customerAddress);
        await Context.SaveChangesAsync();
        return customerAddress;
    }

    public override async Task<bool> IsUniqueAsync(Customer entity, Guid? id = null)
    {
        var currentTenantId = TenantContext.GetCurrentTenantId();

        var query = Context.Customer.AsQueryable();

        // Add tenant isolation
        if (currentTenantId.HasValue)
        {
            query = query.Where(c => c.TenantId == currentTenantId.Value);
        }

        // Check for duplicate Firstname and Lastname combination
        query = query.Where(c => c.Firstname == entity.Firstname && c.Lastname == entity.Lastname);

        // Exclude entity with provided id (for updates)
        if (id.HasValue)
        {
            query = query.Where(c => c.Id != id.Value);
        }

        var exists = await query.AnyAsync();
        return !exists;
    }
}