using AutoMapper;
using Blazored.LocalStorage;
using maERP.Domain.Wrapper;
using maERP.SharedUI.Contracts;
using maERP.SharedUI.Models.Warehouse;
using maERP.SharedUI.Services.Base;

namespace maERP.SharedUI.Services;

public class WarehouseService : BaseHttpService, IWarehouseService
{
    private readonly IMapper _mapper;

    public WarehouseService(IClient client, IMapper mapper, ILocalStorageService localStorageService) : base(client, localStorageService)
    {
        _mapper = mapper;
    }

    public async Task<PaginatedResult<WarehouseVM>> GetWarehouses(int pageNumber = 1, int pageSize = 10, string searchString = "", string orderBy = "")
    {
        await AddBearerToken();
        var warehouses = await _client.WarehousesGETAsync(pageNumber, pageSize, searchString, orderBy);
        return _mapper.Map<PaginatedResult<WarehouseVM>>(warehouses);
    }

    public async Task<WarehouseVM> GetWarehouseDetails(int id)
    {
        await AddBearerToken();
        var warehouse = await _client.WarehousesGET2Async(id);
        return _mapper.Map<WarehouseVM>(warehouse);
    }

    public async Task<Response<Guid>> CreateWarehouse(WarehouseVM warehouse)
    {
        try
        {
            await AddBearerToken();
            var warehouseCreateCommand = _mapper.Map<WarehouseCreateCommand>(warehouse);
            await _client.WarehousesPOSTAsync(warehouseCreateCommand);
            return new Response<Guid>
            {
                Success = true
            };
        }
        catch (ApiException ex)
        {
            return ConvertApiExceptions<Guid>(ex);
        }
    }

    public async Task<Response<Guid>> UpdateWarehouse(int id, WarehouseVM warehouse)
    {
        try
        {
            await AddBearerToken();
            var warehouseUpdateCommand = _mapper.Map<WarehouseUpdateCommand>(warehouse);
            await _client.WarehousesPUTAsync(id, warehouseUpdateCommand);
            return new Response<Guid>
            {
                Success = true
            };
        }
        catch (ApiException ex)
        {
            return ConvertApiExceptions<Guid>(ex);
        }
    }
    public async Task<Response<Guid>> DeleteWarehouse(int id)
    {
        try
        {
            await AddBearerToken();
            await _client.WarehousesDELETEAsync(id);
            return new Response<Guid>
            {
                Success = true
            };
        }
        catch (ApiException ex)
        {
            return ConvertApiExceptions<Guid>(ex);
        }
    }
}