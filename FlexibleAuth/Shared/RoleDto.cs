using FlexibleAuth.Shared.Authorization;
using InfiniteEnumFlags;

namespace FlexibleAuth.Shared;

public class RoleDto
{
    public RoleDto()
    {
        Id = string.Empty;
        Name = string.Empty;
        Permissions = Permission.None.ToBase64Key();
    }

    public RoleDto(string id, string name, string permissions)
    {
        Id = id;
        Name = name;
        Permissions = permissions;
    }

    public string Id { get; set; }

    public string Name { get; set; }

    public string Permissions { get; set; }

    public bool Has(string permissionKey)
    {
        return (Flag<Permission>.FromBase64(Permissions).HasFlag(Flag<Permission>.FromBase64(permissionKey)));
    }

    public void Set(string permissionKey, bool granted)
    {
        if (granted)
        {
            Grant((Flag<Permission>.FromBase64(permissionKey)));
        }
        else
        {
            Revoke((Flag<Permission>.FromBase64(permissionKey)));
        }
    }

    public void Grant(Flag<Permission> permission)
    {
        Permissions = (Flag<Permission>.FromBase64(Permissions) | permission).ToBase64Key();
    }

    public void Revoke(Flag<Permission> permission)
    {
        Permissions = (Flag<Permission>.FromBase64(Permissions) ^ permission).ToBase64Key();
    }
}