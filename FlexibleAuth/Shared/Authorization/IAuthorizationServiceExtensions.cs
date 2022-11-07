using FlexibleAuth.Shared.Authorization;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using InfiniteEnumFlags;

namespace FlexibleAuth.Shared.Authorization;

public static class IAuthorizationServiceExtensions
{
    public static Task<AuthorizationResult> AuthorizeAsync(this IAuthorizationService service, ClaimsPrincipal user, Flag<Permission> permission)
    {
        return service.AuthorizeAsync(user, PolicyNameHelper.GeneratePolicyNameFor(permission));
    }
}
