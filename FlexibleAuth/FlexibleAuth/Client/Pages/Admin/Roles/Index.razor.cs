using FlexibleAuth.Client.Services;
using FlexibleAuth.Shared;
using FlexibleAuth.Shared.Authorization;
using Microsoft.AspNetCore.Components;

namespace FlexibleAuth.Client.Pages.Admin.Roles;

public partial class Index
{
    [Inject]
    public IRolesClient RolesClient { get; set; } = null!;

    public ICollection<RoleDto> Roles { get; set; } = new List<RoleDto>();

    private string newRoleName = string.Empty;

    private RoleDto? roleToEdit;

    protected override async Task OnInitializedAsync()
    {
        Roles = await RolesClient.GetRolesAsync();
    }

    private async Task AddRole()
    {
        if (!string.IsNullOrWhiteSpace(newRoleName))
        {
            var role = await RolesClient.PostRoleAsync(
                new RoleDto("", newRoleName, Permissions.None));

            Roles.Add(role);
        }

        newRoleName = string.Empty;
    }

    private void EditRole(RoleDto role)
    {
        roleToEdit = role;
    }

    private void CancelEditRole()
    {
        roleToEdit = null;
    }

    private async Task UpdateRole()
    {
        await RolesClient.PutRoleAsync(roleToEdit!.Id, roleToEdit);

        roleToEdit = null;
    }

    private async Task DeleteRole(RoleDto role)
    {
        await RolesClient.DeleteRoleAsync(role.Id);
        Roles.Remove(role);
    }
}
