﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace FlexibleAuth.Shared.Authorization;

public class FlexibleAuthorizationPolicyProvider : DefaultAuthorizationPolicyProvider
{
    private readonly AuthorizationOptions _options;

    public FlexibleAuthorizationPolicyProvider(IOptions<AuthorizationOptions> options)
        : base(options)
    {
        _options = options.Value;
    }

    public override async Task<AuthorizationPolicy?> GetPolicyAsync(string policyName)
    {
        var policy = await base.GetPolicyAsync(policyName);

        if (policy == null && PolicyNameHelper.IsValidPolicyName(policyName))
        {
            var permissions = PolicyNameHelper.GetPermissionsFrom(policyName);

            policy = new AuthorizationPolicyBuilder()
                .AddRequirements(new PermissionAuthorizationRequirement(permissions))
                .Build();

            _options.AddPolicy(policyName!, policy);
        }

        return policy;
    }
}
