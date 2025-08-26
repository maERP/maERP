using maERP.Domain.Entities;
using maERP.Persistence.DatabaseContext;
using maERP.Application.Contracts.Services;
using Microsoft.EntityFrameworkCore;

namespace maERP.Persistence.Tests;

public class ApplicationDbContextTests
{
    private readonly ApplicationDbContext _applicationDbContext;

    public ApplicationDbContextTests()
    {
        var dbOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

        var mockTenantContext = new TestTenantContext();

        _applicationDbContext = new ApplicationDbContext(dbOptions, mockTenantContext);
    }

    private class TestTenantContext : ITenantContext
    {
        private int? _tenantId = null;
        private HashSet<int> _assignedTenantIds = new HashSet<int>();

        public int? GetCurrentTenantId() => _tenantId;
        public void SetCurrentTenantId(int? tenantId) => _tenantId = tenantId;
        public bool HasTenant() => _tenantId.HasValue;
        public IReadOnlyCollection<int> GetAssignedTenantIds() => _assignedTenantIds;
        public void SetAssignedTenantIds(IEnumerable<int> tenantIds) => _assignedTenantIds = new HashSet<int>(tenantIds ?? new List<int>());
        public bool IsAssignedToTenant(int tenantId) => _assignedTenantIds.Contains(tenantId);
    }

    [Fact]
    public async Task Save_SetDateCreatedValue()
    {
        // Arrange
        var warehouse = new Warehouse
        {
            Id = 1,
            Name = "Test Warehouse 1"
        };

        // Act
        await _applicationDbContext.Warehouse.AddAsync(warehouse);
        await _applicationDbContext.SaveChangesAsync();

        // Assert
        Assert.True(warehouse.DateCreated > DateTime.MinValue);
    }

    [Fact]
    public async Task Save_SetDateModifiedValue()
    {
        // Arrange
        var warehouse = new Warehouse
        {
            Id = 1,
            Name = "Test Warehouse 1"
        };

        // Act
        await _applicationDbContext.Warehouse.AddAsync(warehouse);
        await _applicationDbContext.SaveChangesAsync();

        // Assert
        Assert.True(warehouse.DateModified > DateTime.MinValue);
    }
}