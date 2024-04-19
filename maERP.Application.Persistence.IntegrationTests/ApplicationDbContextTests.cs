using maERP.Domain;
using maERP.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace maERP.Application.Persistence.IntegrationTests;

public class ApplicationDbContextTests
{
    private readonly ApplicationDbContext _applicationDbContext;

    public ApplicationDbContextTests()
    {
        var dbOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

        _applicationDbContext = new ApplicationDbContext(dbOptions);
    }

    [Fact]
    public async void Save_SetDateCreatedValue()
    {
        // Arrange
        var warehouse = new Warehouse
        {
            Id = 1,
            Name = "Test Warehouse 1"
        };        

        // Act
        await _applicationDbContext.Warehouses.AddAsync(warehouse);
        await _applicationDbContext.SaveChangesAsync();

        // Assert
        Assert.NotNull(warehouse.DateCreated);
    }

    [Fact]
    public async void Save_SetDateModifiedValue()
    {
        // Arrange
        var warehouse = new Warehouse
        {
            Id = 1,
            Name = "Test Warehouse 1"
        };

        // Act
        await _applicationDbContext.Warehouses.AddAsync(warehouse);
        await _applicationDbContext.SaveChangesAsync();

        // Assert
        Assert.NotNull(warehouse.DateModified);
    }
}
