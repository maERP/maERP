using maERP.Domain.Dtos.Settings;
using maERP.Domain.Wrapper;
using maERP.SharedUI.Contracts;
using Microsoft.AspNetCore.Components;

namespace maERP.SharedUI.Pages.Settings;

public partial class SettingsDetail
{
    [Inject]
    public required NavigationManager NavigationManager { get; set; }

    [Inject]
    public required IHttpService HttpService { get; set; }

    [Parameter]
    public int Id { get; set; }

    private SettingDetailDto? _setting;

    protected override async Task OnParametersSetAsync()
    {
        if (Id != 0)
        {
            var result = await HttpService.GetAsync<Result<SettingDetailDto>>($"/api/v1/Settings/{Id}");
            
            if (result != null && result.Succeeded)
            {
                _setting = result.Data;
            }
        }
    }
}
