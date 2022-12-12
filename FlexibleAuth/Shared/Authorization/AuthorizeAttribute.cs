using InfiniteEnumFlags;

namespace FlexibleAuth.Shared.Authorization;

public class AuthorizeAttribute : Microsoft.AspNetCore.Authorization.AuthorizeAttribute
{
    public AuthorizeAttribute() { }

    //public AuthorizeAttribute(string policy) : base(policy) { }

    public AuthorizeAttribute(params string[] permissions)
    {
        var result = new Flag<Permission>();
        Permissions = permissions.Select(Permission.FromName)
            .Where(perm => perm is not null)
            .Aggregate(result, (current, perm) => current | (perm ?? Permission.None));
    }

    public Flag<Permission> Permissions
    {
        get
        {
            return !string.IsNullOrEmpty(Policy) 
                ? PolicyNameHelper.GetPermissionsFrom(Policy) 
                : Permission.None;
        }
        set
        {
            Policy = value != Permission.None 
                ? PolicyNameHelper.GeneratePolicyNameFor(value)
                : string.Empty;
        }
    }
}
