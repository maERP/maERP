using maERP.Application.Contracts.Services;
using maERP.Domain.Entities;
using maERP.Domain.Enums;
using maERP.Persistence.DatabaseContext;
using maERP.Persistence.Services;
using Microsoft.EntityFrameworkCore;

namespace maERP.Persistence.Tests;

public class CustomerDedupeServiceTests
{
    private static readonly Guid TenantA = Guid.Parse("11111111-1111-1111-1111-111111111111");
    private static readonly Guid TenantB = Guid.Parse("22222222-2222-2222-2222-222222222222");

    private static (ApplicationDbContext db, ICustomerDedupeService svc) CreateService()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
        var db = new ApplicationDbContext(options, new NullTenantContext());
        var svc = new CustomerDedupeService(db);
        return (db, svc);
    }

    private sealed class NullTenantContext : ITenantContext
    {
        public Guid? GetCurrentTenantId() => null;
        public void SetCurrentTenantId(Guid? tenantId) { }
        public bool HasTenant() => false;
        public IReadOnlyCollection<Guid> GetAssignedTenantIds() => Array.Empty<Guid>();
        public void SetAssignedTenantIds(IEnumerable<Guid> tenantIds) { }
        public bool IsAssignedToTenant(Guid tenantId) => false;
    }

    private static int _customerIdSeed = 1000;

    private static Customer NewCustomer(Guid tenantId, string email, DateTime created, string firstName = "")
    {
        return new Customer
        {
            Id = Guid.NewGuid(),
            CustomerId = Interlocked.Increment(ref _customerIdSeed),
            TenantId = tenantId,
            Email = email,
            Firstname = firstName,
            CustomerStatus = CustomerStatus.Active,
            DateCreated = created,
            DateModified = created,
        };
    }

    [Fact]
    public async Task DryRun_FindsDuplicates_ReportsGroupsWithoutWriting()
    {
        var (db, svc) = CreateService();

        var older = NewCustomer(TenantA, "foo@bar.com", new DateTime(2025, 1, 1), "Old");
        var newer = NewCustomer(TenantA, "FOO@bar.com", new DateTime(2025, 6, 1), "New");
        await db.Customer.AddRangeAsync(older, newer);
        await db.SaveChangesAsync();
        older.DateCreated = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        newer.DateCreated = new DateTime(2025, 6, 1, 0, 0, 0, DateTimeKind.Utc);
        await db.SaveChangesAsync();

        var result = await svc.RunAsync(dryRun: true);

        Assert.True(result.DryRun);
        Assert.Equal(1, result.GroupsFound);
        Assert.Equal(1, result.TenantsAffected);
        Assert.Equal(1, result.CustomersToMerge);

        var group = result.Groups.Single();
        Assert.Equal(TenantA, group.TenantId);
        Assert.Equal("foo@bar.com", group.EmailKey);
        Assert.Equal(older.Id, group.SurvivorCustomerId);
        Assert.Single(group.SiblingCustomerIds);
        Assert.Contains(newer.Id, group.SiblingCustomerIds);

        // DryRun must not flip status to Inactive — only the Run path does that.
        // Email is canonicalized to lowercase by the DbContext save hook on insert.
        var newerStillActive = await db.Customer.IgnoreQueryFilters().FirstAsync(c => c.Id == newer.Id);
        Assert.Equal(CustomerStatus.Active, newerStillActive.CustomerStatus);
        Assert.Equal("foo@bar.com", newerStillActive.Email);
    }

    [Fact]
    public async Task Run_MergesSiblingsIntoOldestSurvivor()
    {
        var (db, svc) = CreateService();

        var survivor = NewCustomer(TenantA, "merge@me.com", new DateTime(2025, 1, 1), "Survivor");
        var sibling = NewCustomer(TenantA, "merge@me.com", new DateTime(2025, 6, 1), "Sibling");
        await db.Customer.AddRangeAsync(survivor, sibling);
        await db.SaveChangesAsync();
        // DbContext overwrites DateCreated to UtcNow on Add — restore the test fixture's intent.
        survivor.DateCreated = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        sibling.DateCreated = new DateTime(2025, 6, 1, 0, 0, 0, DateTimeKind.Utc);
        await db.SaveChangesAsync();

        var result = await svc.RunAsync(dryRun: false);

        Assert.False(result.DryRun);
        Assert.Equal(1, result.CustomersToMerge);

        var siblingAfter = await db.Customer.IgnoreQueryFilters().FirstAsync(c => c.Id == sibling.Id);
        Assert.Equal(CustomerStatus.Inactive, siblingAfter.CustomerStatus);
        Assert.Equal(string.Empty, siblingAfter.Email);
        Assert.Contains($"Merged into {survivor.Id:D}", siblingAfter.Note);

        var survivorAfter = await db.Customer.IgnoreQueryFilters().FirstAsync(c => c.Id == survivor.Id);
        Assert.Equal(CustomerStatus.Active, survivorAfter.CustomerStatus);
        Assert.Equal("merge@me.com", survivorAfter.Email);
    }

    [Fact]
    public async Task DifferentTenants_SameEmail_AreNotConsideredDuplicates()
    {
        var (db, svc) = CreateService();

        await db.Customer.AddRangeAsync(
            NewCustomer(TenantA, "shared@x.de", new DateTime(2025, 1, 1)),
            NewCustomer(TenantB, "shared@x.de", new DateTime(2025, 1, 1)));
        await db.SaveChangesAsync();

        var result = await svc.RunAsync(dryRun: true);

        Assert.Equal(0, result.GroupsFound);
        Assert.Equal(0, result.CustomersToMerge);
    }

    [Fact]
    public async Task EmptyOrNullEmails_AreIgnored()
    {
        var (db, svc) = CreateService();

        await db.Customer.AddRangeAsync(
            NewCustomer(TenantA, "", new DateTime(2025, 1, 1)),
            NewCustomer(TenantA, "", new DateTime(2025, 6, 1)));
        await db.SaveChangesAsync();

        var result = await svc.RunAsync(dryRun: true);

        Assert.Equal(0, result.GroupsFound);
    }

    [Fact]
    public async Task Run_ReassignsCustomerSalesChannelLinksToSurvivor()
    {
        var (db, svc) = CreateService();

        var survivor = NewCustomer(TenantA, "scc@x.de", new DateTime(2025, 1, 1));
        var sibling = NewCustomer(TenantA, "scc@x.de", new DateTime(2025, 6, 1));
        await db.Customer.AddRangeAsync(survivor, sibling);

        var siblingLink = new CustomerSalesChannel
        {
            Id = Guid.NewGuid(),
            TenantId = TenantA,
            CustomerId = sibling.Id,
            SalesChannelId = Guid.NewGuid(),
            RemoteCustomerId = "ext-123",
        };
        await db.CustomerSalesChannel.AddAsync(siblingLink);
        await db.SaveChangesAsync();
        survivor.DateCreated = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        sibling.DateCreated = new DateTime(2025, 6, 1, 0, 0, 0, DateTimeKind.Utc);
        await db.SaveChangesAsync();

        await svc.RunAsync(dryRun: false);

        var linkAfter = await db.CustomerSalesChannel.IgnoreQueryFilters().FirstAsync(s => s.Id == siblingLink.Id);
        Assert.Equal(survivor.Id, linkAfter.CustomerId);
    }

    [Fact]
    public async Task Run_ReassignsCustomerAddressesToSurvivor()
    {
        var (db, svc) = CreateService();

        var survivor = NewCustomer(TenantA, "addr@x.de", new DateTime(2025, 1, 1));
        var sibling = NewCustomer(TenantA, "addr@x.de", new DateTime(2025, 6, 1));
        await db.Customer.AddRangeAsync(survivor, sibling);

        var address = new CustomerAddress
        {
            Id = Guid.NewGuid(),
            TenantId = TenantA,
            CustomerId = sibling.Id,
            Firstname = "Sibling",
            Lastname = "Customer",
            Street = "Hauptstr.",
            HouseNr = "1",
            Zip = "12345",
            City = "Berlin",
        };
        await db.CustomerAddress.AddAsync(address);
        await db.SaveChangesAsync();
        survivor.DateCreated = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        sibling.DateCreated = new DateTime(2025, 6, 1, 0, 0, 0, DateTimeKind.Utc);
        await db.SaveChangesAsync();

        await svc.RunAsync(dryRun: false);

        var addressAfter = await db.CustomerAddress.IgnoreQueryFilters().FirstAsync(a => a.Id == address.Id);
        Assert.Equal(survivor.Id, addressAfter.CustomerId);
    }

    [Fact]
    public async Task NoDuplicates_ReturnsEmptyResult()
    {
        var (db, svc) = CreateService();

        await db.Customer.AddRangeAsync(
            NewCustomer(TenantA, "alice@x.de", new DateTime(2025, 1, 1)),
            NewCustomer(TenantA, "bob@x.de", new DateTime(2025, 1, 1)));
        await db.SaveChangesAsync();

        var result = await svc.RunAsync(dryRun: true);

        Assert.Equal(0, result.GroupsFound);
        Assert.Equal(0, result.TenantsAffected);
        Assert.Empty(result.Groups);
    }
}
