using BasicAuth.Client.Services;
using BasicAuth.Shared;
using Microsoft.AspNetCore.Components;

namespace BasicAuth.Client.Pages.Admin.Users;

public partial class Edit
{
    [Parameter]
    public string UserId { get; set; }

    [Inject]
    public IUsersClient UsersClient { get; set; }

    [Inject]
    public IRolesClient RolesClient { get; set; }

    [Inject]
    public NavigationManager Navigation { get; set; }

    public UserDto User { get; set; }

    public ICollection<RoleDto> Roles { get; set; }

    protected override async Task OnParametersSetAsync()
    {
        Roles = await RolesClient.GetRolesAsync();

        User = await UsersClient.GetUserAsync(UserId);
    }

    public void ToggleSelectedRole(string roleName)
    {
        if (User.Roles.Contains(roleName))
        {
            User.Roles.Remove(roleName);
        }
        else
        {
            User.Roles.Add(roleName);
        }

        StateHasChanged();
    }

    public async Task UpdateUser()
    {
        await UsersClient.PutUserAsync(User.Id, User);

        Navigation.NavigateTo("/admin/users");
    }
}
