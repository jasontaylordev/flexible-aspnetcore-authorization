using FlexibleAuth.Shared.Authorization;
using InfiniteEnumFlags;

namespace FlexibleAuth.Shared;

public class RoleDto
{
    public RoleDto()
    {
        Id = string.Empty;
        Name = string.Empty;
        Permissions = Permission.None.ToUniqueId();
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
        return (Flag<Permission>.FromUniqueId(Permissions).HasFlag(Flag<Permission>.FromUniqueId(permissionKey)));
    }

    public void Set(string permissionKey, bool granted)
    {
        if (granted)
        {
            Grant((Flag<Permission>.FromUniqueId(permissionKey)));
        }
        else
        {
            Revoke((Flag<Permission>.FromUniqueId(permissionKey)));
        }
    }

    public void Grant(Flag<Permission> permission)
    {
        Permissions = (Flag<Permission>.FromUniqueId(Permissions) | permission).ToUniqueId();
    }

    public void Revoke(Flag<Permission> permission)
    {
        Permissions = (Flag<Permission>.FromUniqueId(Permissions) ^ permission).ToUniqueId();
    }
}