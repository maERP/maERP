using maERP.Domain.Dtos.Customer;
using maERP.Domain.Wrapper;
using maERP.SharedUI.Contracts;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace maERP.SharedUI.Pages.Customers;

public partial class Customers
{
    [Inject]
    public required NavigationManager NavigationManager { get; set; }

    [Inject]
    public required IHttpService HttpService { get; set; }

    private string _searchString = string.Empty;

    private readonly MudDataGrid<CustomerDetailDto> _dataGrid = new();

    private async Task<GridData<CustomerDetailDto>> LoadGridData(GridState<CustomerDetailDto> state)
    {
        // TODO: change to CustomerDetailDto
        var apiResponse = await HttpService.GetAsync<PaginatedResult<CustomerDetailDto>>("/api/v1/Customers")
                          ?? new PaginatedResult<CustomerDetailDto>(new List<CustomerDetailDto>());
        
        GridData<CustomerDetailDto> data = new()
        {
            Items = apiResponse.Data,
            TotalItems = apiResponse.TotalCount
        };

        return data;
    }

    private async Task Search()
    {
        await _dataGrid.ReloadServerData();
    }
}