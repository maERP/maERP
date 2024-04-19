using maERP.Application.Contracts.Persistence;
using maERP.Domain.Models;
using Moq;

namespace maERP.Application.UnitTests.Mocks;

public class MockWarehouseRepository
{
    public static Mock<IWarehouseRepository> GetMockWarehousesRepository()
    {
        var warehouses = new List<Warehouse>
        {
            new Warehouse
            {
                Id = 1,
                Name = "Test Warehouse 1"
            },
            new Warehouse
            {
                Id = 2,
                Name = "Test Warehouse 2"
            },
            new Warehouse
            {
                Id = 3,
                Name = "Test Warehouse 3"
            }
        };

        var mockRepo = new Mock<IWarehouseRepository>();

        mockRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(warehouses);

        mockRepo.Setup(r => r.CreateAsync(It.IsAny<Warehouse>()))
            .Returns((Warehouse warehouse) =>
            {
                warehouses.Add(warehouse);
                return Task.FromResult(warehouse);
            });

        return mockRepo;
    }
}