using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace FlexibleAuth.Shared.Authorization;

public class PermissionAuthorizationHandler : AuthorizationHandler<PermissionAuthorizationRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionAuthorizationRequirement requirement)
    {
        var permissionClaim = context.User.FindFirst(
            c => c.Type == CustomClaimTypes.Permissions);

        if (permissionClaim == null)
        {
            return Task.CompletedTask;
        }

        if (!int.TryParse(permissionClaim.Value, out int permissionClaimValue))
        {
            return Task.CompletedTask;
        }

        var requiredPermissions = new List<Permissions>();
        foreach (var permission in PermissionsProvider.GetAll())
        {
            if (permission == Permissions.None) continue;

            if (requirement.Permissions.HasFlag(permission))
            {
                requiredPermissions.Add(permission);
            }
        }

        var userPermissions = (Permissions)permissionClaimValue;
        foreach (var permission in requiredPermissions)
        {
            if (userPermissions.HasFlag(permission))
            {
                context.Succeed(requirement);
                return Task.CompletedTask;
            }
        }

        return Task.CompletedTask;
    }
}
