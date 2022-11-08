using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using InfiniteEnumFlags;

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

        var userPermissions = Flag<Permission>.FromUniqueId(permissionClaim.Value);

        if (userPermissions.HasFlag(requirement.Permission))
        {
            context.Succeed(requirement);
            return Task.CompletedTask;
        }

        return Task.CompletedTask;
    }
}
