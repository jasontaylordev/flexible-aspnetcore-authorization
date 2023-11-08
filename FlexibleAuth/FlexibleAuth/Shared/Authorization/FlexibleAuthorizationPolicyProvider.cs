using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace FlexibleAuth.Shared.Authorization;

public class FlexibleAuthorizationPolicyProvider : DefaultAuthorizationPolicyProvider
{
    private readonly ConcurrentDictionary<string, AuthorizationPolicy> _policies = new ConcurrentDictionary<string, AuthorizationPolicy>();

    public FlexibleAuthorizationPolicyProvider(IOptions<AuthorizationOptions> options)
        : base(options)
    {
    }

    public override async Task<AuthorizationPolicy?> GetPolicyAsync(string policyName)
    {
        if (!PolicyNameHelper.IsValidPolicyName(policyName))
        {
            return await base.GetPolicyAsync(policyName);
        }

        var policy = _policies.GetOrAdd(policyName, AuthorizationPolicyFactory);

        return policy;
    }

    private static AuthorizationPolicy AuthorizationPolicyFactory(string policyName)
    {
        var permissions = PolicyNameHelper.GetPermissionsFrom(policyName);

        return new AuthorizationPolicyBuilder()
            .AddRequirements(new PermissionAuthorizationRequirement(permissions))
            .Build();
    }
}
