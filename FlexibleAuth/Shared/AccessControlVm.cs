using FlexibleAuth.Shared.Authorization;
namespace FlexibleAuth.Shared;

public class AccessControlVm
{
    public AccessControlVm(List<RoleDto> roles)
    {
        Roles = roles;
        AvailablePermissions = new();

        foreach(var permission in PermissionsProvider.GetAll())
        {
            if (permission.Value == Permission.None) continue;
            AvailablePermissions.Add(permission.Key, permission.Value.ToUniqueId());
        }
    }

    public List<RoleDto> Roles { get; set; } = new();

    public Dictionary<string,string> AvailablePermissions { get; set; }
}
