using maERP.SharedUI.Contracts;
using maERP.SharedUI.Models.User;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace maERP.SharedUI.Pages.Users;

public partial class UsersEdit
{
    [Inject]
    public required NavigationManager _navigationManager { get; set; }

    [Inject]
    public required IUserService _userService { get; set; }

    [Parameter]
    public string userId { get; set; } = "";

    private MudForm? _form;

    protected string Title = "hinzuf�gen";

    protected UserVM user = new();

    protected override async Task OnParametersSetAsync()
    {
        if (userId != null)
        {
            Title = "bearbeiten";
            user = await _userService.GetUserDetails(userId);
        }
    }

    protected async Task Save()
    {
        await _userService.UpdateUser(userId, user);
        ReturnToList();
    }

    public void ReturnToList()
    {
        _navigationManager.NavigateTo("/users");
    }
}