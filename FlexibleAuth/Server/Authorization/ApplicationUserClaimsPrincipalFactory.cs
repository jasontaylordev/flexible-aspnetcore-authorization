using FlexibleAuth.Server.Models;
using FlexibleAuth.Shared.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Security.Claims;

namespace FlexibleAuth.Server.Authorization;

public class ApplicationUserClaimsPrincipalFactory : UserClaimsPrincipalFactory<User, Role>
{
    public ApplicationUserClaimsPrincipalFactory(
        UserManager<User> userManager,
        RoleManager<Role> roleManager,
        IOptions<IdentityOptions> optionsAccessor)
        : base(userManager, roleManager, optionsAccessor)
    {
    }

    protected override async Task<ClaimsIdentity> GenerateClaimsAsync(User user)
    {
        var identity = await base.GenerateClaimsAsync(user);

        var userRoleNames = await UserManager.GetRolesAsync(user) ?? Array.Empty<string>();

        var userRoles = await RoleManager.Roles.Where(r =>
            userRoleNames.Contains(r.Name)).ToListAsync();

        var userPermissions = Permission.None;

        foreach (var role in userRoles)
            userPermissions |= role.Permission;

        var permissionsValue = userPermissions.ToBase64Key();

        identity.AddClaim(new Claim(CustomClaimTypes.Permissions, permissionsValue));

        return identity;
    }
}