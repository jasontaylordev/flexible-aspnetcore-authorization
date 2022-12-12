using InfiniteEnumFlags;
using Microsoft.AspNetCore.Authorization;

namespace FlexibleAuth.Shared.Authorization;

public class PermissionAuthorizationRequirement : IAuthorizationRequirement
{
    public PermissionAuthorizationRequirement(Flag<Permission> permission)
    {
        Permission = permission;
    }

    public Flag<Permission> Permission { get; }
}
