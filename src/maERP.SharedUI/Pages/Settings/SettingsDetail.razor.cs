using maERP.Domain.Dtos.Setting;
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
    public int SettingId { get; set; }

    private SettingDetailDto _settingDetail = new();
    private string _title = "Einstellung";

    protected override async Task OnParametersSetAsync()
    {
        if (SettingId != 0)
        {
            var result = await HttpService.GetAsync<Result<SettingDetailDto>>($"/api/v1/Settings/{SettingId}");
            
            if (result != null && result.Succeeded)
            {
                _title = $"Einstellung {_settingDetail.Key}";
                _settingDetail = result.Data;
            }
        }
    }
}
