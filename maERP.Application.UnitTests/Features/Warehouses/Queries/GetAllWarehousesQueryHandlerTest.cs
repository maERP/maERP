using AutoMapper;
using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Application.Features.Warehouse.Queries.GetWarehouseQuery;
using maERP.Application.MappingProfiles;
using maERP.Application.UnitTests.Mocks;
using Moq;
using Shouldly;

namespace maERP.Application.UnitTests.Features.Warehouses.Queries;

public class GetAllWarehousesQueryHandlerTest
{
    private readonly Mock<IWarehouseRepository> _mocKRepo;
    private IMapper _mapper;
    private Mock<IAppLogger<GetAllWarehousesQueryHandler>> _mockAppLogger;

    public GetAllWarehousesQueryHandlerTest()
    {
        _mocKRepo = MockWarehouseRepository.GetMockWarehousesRepository();

        var mapperConfig = new MapperConfiguration(c =>
        {
            c.AddProfile<WarehouseProfile>();
        });

        _mapper = mapperConfig.CreateMapper();

        _mockAppLogger = new Mock<IAppLogger<GetAllWarehousesQueryHandler>>();
    }

    [Fact]
    public async Task GetAllWarehousesTest()
    {
        var handler = new GetAllWarehousesQueryHandler(_mapper, _mockAppLogger.Object, _mocKRepo.Object);

        var result = await handler.Handle(new GetAllWarehousesQuery(), CancellationToken.None);

        Assert.Equal(3, result.Count);
        // result.ShouldBeOfType<List<WarehouseListDto>>();
    }
}