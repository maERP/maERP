using maERP.Domain.Dtos.User;
using maERP.SharedUI.Contracts;
using Microsoft.AspNetCore.Components;

namespace maERP.SharedUI.Pages.Users;

public partial class UsersDetail
{
    [Inject]
    public required NavigationManager NavigationManager { get; set; }

    [Inject]
    public required IHttpService HttpService { get; set; }

    [Parameter]
    public string UserId { get; set; } = "";

    private string _title = "Benutzerdetail";

    private UserDetailDto _userDetail = new();

    protected override async Task OnParametersSetAsync()
    {
        _userDetail = await HttpService.GetAsync<UserDetailDto>($"/api/v1/Users/{UserId}") ?? new UserDetailDto();
        
        if(!string.IsNullOrEmpty(_userDetail.Email))
        {
            _title = $"Benutzer {_userDetail.Email}";
        }
    }
}