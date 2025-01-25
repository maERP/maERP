using maERP.SharedUI.Contracts;
using maERP.SharedUI.Models.User;
using Microsoft.AspNetCore.Components;

namespace maERP.SharedUI.Pages.Users;

public partial class UsersDetail
{
    [Inject]
    public required NavigationManager NavigationManager { get; set; }

    [Inject]
    public required IUserService UserService { get; set; }

    [Parameter]
    public string userId { get; set; } = "";

    protected string Title = "Benutzerdetail";

    protected UserVm User = new();

    protected override async Task OnParametersSetAsync()
    {
        User = await UserService.GetUserDetails(userId);
    }
}