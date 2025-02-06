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
    public string userId { get; set; } = "";

    protected string Title = "Benutzerdetail";

    protected UserDetailDto User = new();

    protected override async Task OnParametersSetAsync()
    {
        User = await HttpService.GetAsync<UserDetailDto>($"/api/v1/users/{userId}") ?? new UserDetailDto();
    }
}