using AutoMapper;
using Blazored.LocalStorage;
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

    public async Task<List<WarehouseVM>> GetWarehouses()
    {
        await AddBearerToken();
        var warehouses = await _client.WarehousesAllAsync();
        return _mapper.Map<List<WarehouseVM>>(warehouses);
    }

    public async Task<WarehouseVM> GetWarehouseDetails(int id)
    {
        await AddBearerToken();
        var warehouse = await _client.WarehousesGETAsync(id);
        return _mapper.Map<WarehouseVM>(warehouse);
    }

    public async Task<Response<Guid>> CreateWarehouse(WarehouseVM warehouse)
    {
        try
        {
            await AddBearerToken();
            var createWarehouseCommand = _mapper.Map<CreateWarehouseCommand>(warehouse);
            await _client.WarehousesPOSTAsync(createWarehouseCommand);
            return new Response<Guid>()
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
            var updateWarehouseCommand = _mapper.Map<UpdateWarehouseCommand>(warehouse);
            await _client.WarehousesPUTAsync(id.ToString(), updateWarehouseCommand);
            return new Response<Guid>()
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
            return new Response<Guid>()
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