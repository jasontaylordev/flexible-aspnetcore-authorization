using FlexibleAuth.Shared.Authorization;
using InfiniteEnumFlags;
using Microsoft.AspNetCore.Components;

namespace FlexibleAuth.Client.Shared;

public class FlexibleAuthorizeView : Microsoft.AspNetCore.Components.Authorization.AuthorizeView
{
    [Parameter]
    public Flag<Permission> Permissions
    {
        get
        {
            return string.IsNullOrEmpty(Policy) ? Permission.None : PolicyNameHelper.GetPermissionsFrom(Policy);
        }
        set
        {
            Policy = PolicyNameHelper.GeneratePolicyNameFor(value);
        }
    }
}
