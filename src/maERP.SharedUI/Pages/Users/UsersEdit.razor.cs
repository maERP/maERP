using maERP.SharedUI.Contracts;
using maERP.SharedUI.Models.User;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace maERP.SharedUI.Pages.Users;

public partial class UsersEdit
{
    [Inject]
    public required NavigationManager NavigationManager { get; set; }

    [Inject]
    public required IUserService UserService { get; set; }

    [Parameter]
    public string userId { get; set; } = "";

    // ReSharper disable once NotAccessedField.Local
    private MudForm? _form;

    // ReSharper disable once NotAccessedField.Local
    protected string Title = "hinzufügen";

    protected UserVm User = new();

    protected override async Task OnParametersSetAsync()
    {
        // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
        if (userId != null)
        {
            Title = "bearbeiten";
            User = await UserService.GetUserDetails(userId);
        }
    }

    protected async Task Save()
    {
        await UserService.UpdateUser(userId, User);
        ReturnToList();
    }

    public void ReturnToList()
    {
        NavigationManager.NavigateTo("/users");
    }
}