using InfiniteEnumFlags;

namespace FlexibleAuth.Shared.Authorization;

public static class PolicyNameHelper
{
    public const string Prefix = "Permissions";

    public static bool IsValidPolicyName(string? policyName)
    {
        return policyName != null && policyName.StartsWith(Prefix, StringComparison.OrdinalIgnoreCase);
    }

    public static string GeneratePolicyNameFor(Flag<Permission> permission)
    {
        return $"{Prefix}{permission.ToBase64Key()}";
    }

    public static Flag<Permission> GetPermissionsFrom(string policyName)
    {
        var key = policyName.Replace(Prefix, "");
        return Flag<Permission>.FromBase64(key);
    }
}
