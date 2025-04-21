using maERP.Domain.Dtos.Settings;
using maERP.Domain.Wrapper;
using maERP.SharedUI.Contracts;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace maERP.SharedUI.Pages.Settings;

public partial class Settings
{
    [Inject]
    public required NavigationManager NavigationManager { get; set; }

    [Inject]
    public required IHttpService HttpService { get; set; }

    private string _searchString = string.Empty;
    public MudDataGrid<SettingListDto> DataGrid = new();

    protected override void OnInitialized()
    {
    }

    private async Task<GridData<SettingListDto>> LoadGridData(GridState<SettingListDto> state)
    {
        var pageSize = state.PageSize;
        var pageNumber = state.Page;
        var orderBy = state.SortDefinitions.Count > 0 
            ? string.Join(",", state.SortDefinitions.Select(s => $"{s.SortBy} {(s.Descending ? "Descending" : "Ascending")}"))
            : "DateCreated Descending";

        var apiResponse = await HttpService.GetAsync<PaginatedResult<SettingListDto>>(
            $"/api/v1/Settings?pageNumber={pageNumber}&pageSize={pageSize}&searchString={_searchString}&orderBy={orderBy}")
            ?? new PaginatedResult<SettingListDto>(new List<SettingListDto>());
        
        GridData<SettingListDto> data = new()
        {
            Items = apiResponse.Data,
            TotalItems = apiResponse.TotalCount
        };

        return data;
    }

    private async Task Search(string searchString)
    {
        _searchString = searchString;
        await DataGrid.ReloadServerData();
    }

                if (result != null && result.IsSuccessStatusCode)
                {
                    Snackbar.Add("Einstellungen erfolgreich gespeichert", Severity.Success);
                    NavigationManager.NavigateTo("/Settings", true);
                }
                else
                {
                    Snackbar.Add("Fehler beim Speichern der Einstellungen", Severity.Error);
                }
            }
        }
    }
}
