using maERP.Domain.Dtos.User;
using maERP.Domain.Wrapper;
using maERP.SharedUI.Contracts;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace maERP.SharedUI.Pages.Users;

public partial class Users
{
    [Inject]
    public required NavigationManager NavigationManager { get; set; }

    [Inject]
    public required IHttpService HttpService { get; set; }

    private string _searchString = string.Empty;
    
    private MudDataGrid<UserListDto> _dataGrid = new();

    private async Task<GridData<UserListDto>> LoadGridData(GridState<UserListDto> state)
    {
        var apiResponse = await HttpService.GetAsync<PaginatedResult<UserListDto>>("/api/v1/Users")
                          ?? new PaginatedResult<UserListDto>(new List<UserListDto>());
            
        GridData<UserListDto> data = new()
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