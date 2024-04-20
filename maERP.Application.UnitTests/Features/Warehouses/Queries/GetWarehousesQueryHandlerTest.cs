using AutoMapper;
using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Application.MappingProfiles;
using maERP.Application.UnitTests.Mocks;
using maERP.Application.Features.Warehouse.Queries.GetWarehousesQuery;
using Moq;

namespace maERP.Application.UnitTests.Features.Warehouses.Queries;

public class GetWarehousesQueryHandlerTest
{
    private readonly Mock<IWarehouseRepository> _mocKRepo;
    private IMapper _mapper;
    private Mock<IAppLogger<GetWarehousesQueryHandler>> _mockAppLogger;

    public GetWarehousesQueryHandlerTest()
    {
        _mocKRepo = MockWarehouseRepository.GetMockWarehousesRepository();

        var mapperConfig = new MapperConfiguration(c =>
        {
            c.AddProfile<WarehouseProfile>();
        });

        _mapper = mapperConfig.CreateMapper();

        _mockAppLogger = new Mock<IAppLogger<GetWarehousesQueryHandler>>();
    }

    [Fact]
    public async Task GetAllWarehousesTest()
    {
        var handler = new GetWarehousesQueryHandler(_mapper, _mockAppLogger.Object, _mocKRepo.Object);

        var result = await handler.Handle(new GetWarehousesQuery(), CancellationToken.None);

        Assert.Equal(3, result.Count);
        // result.ShouldBeOfType<List<WarehouseListDto>>();
    }
}