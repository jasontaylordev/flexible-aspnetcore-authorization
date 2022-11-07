using FlexibleAuth.Shared.Authorization;

namespace FlexibleAuth.Shared;

public class AccessControlVm
{
    internal AccessControlVm() { }

    public AccessControlVm(List<RoleDto> roles)
    {
        Roles = roles;

        foreach(var permission in PermissionsProvider.GetAll())
        {
            if (permission == Permissions.None) continue;

            AvailablePermissions.Add(permission);
        }
    }

    public List<RoleDto> Roles { get; set; } = new();

    public List<Permissions> AvailablePermissions { get; set; } = new();
}
